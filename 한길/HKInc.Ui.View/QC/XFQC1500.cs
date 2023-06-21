using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Handler.EventHandler;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.QC
{
    /// <summary>
    /// 부적합관리화면
    /// </summary>
    public partial class XFQC1500 : HKInc.Service.Base.ListFormTemplate
    {
        IService<VI_QCT1500_LIST> ModelService = (IService<VI_QCT1500_LIST>)ProductionFactory.GetDomainService("VI_QCT1500_LIST");
        IService<TN_QCT1500> QCModelService = (IService<TN_QCT1500>)ProductionFactory.GetDomainService("TN_QCT1500");
        TN_QCT1500 dtlobj;
        VI_QCT1500_LIST mobj;

        public XFQC1500()
        {
            InitializeComponent();
            GridExControl = gridEx1;
            dp_dt.DateFrEdit.DateTime = DateTime.Today.AddDays(-7);
            dp_dt.DateToEdit.DateTime = DateTime.Today;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            btn1.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            btn2.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            btn3.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));
            btn4.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Delete));    
        }

        protected override void InitCombo()
        {
            lupruser.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            lup_fuser.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<UserView>(p => p.Active == "Y").ToList());
            cboReceiptWay.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.NCR_ReceiptWay), TextEditStyles.DisableTextEditor, HorzAlignment.Center);

            editEnable(false);
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            GridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            GridExControl.MainGrid.AddColumn("ResultDate", "발생일");
            GridExControl.MainGrid.AddColumn("PType", false);
            GridExControl.MainGrid.AddColumn("WorkNo","작업지시번호");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("Seq",false);
            GridExControl.MainGrid.AddColumn("ProcessCode","공정");
            GridExControl.MainGrid.AddColumn("LotNo","생산 LOT NO");
            GridExControl.MainGrid.AddColumn("FailQty","불량수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FaleType","불량유형");
            GridExControl.MainGrid.AddColumn("PNo","처리번호");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ItemCode",ModelService.GetChildList<TN_STD1100>(P=>P.UseYn=="Y").OrderBy(o => o.ItemNm1).ToList(), "ItemCode", "ItemNm1");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequesHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("FaleType", DbRequesHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");         
        }

        protected override void DataLoad()
        {
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("RowId", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            string work = tx_workno.EditValue.GetNullToEmpty();
            string lot = tx_lotno.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ModelService.GetList(p => (p.ResultDate >= dp_dt.DateFrEdit.DateTime 
                                                                   && p.ResultDate <= dp_dt.DateToEdit.DateTime)
                                                                   && (string.IsNullOrEmpty(work) ? true : p.WorkNo == work)
                                                                   && (string.IsNullOrEmpty(lot) ? true : p.LotNo == lot)
                                                                )
                                                                .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            mobj = GridBindingSource.Current as VI_QCT1500_LIST;

            dtlobj = QCModelService.GetList(p => p.PNo == mobj.PNo).OrderBy(o => o.PNo).FirstOrDefault();
            if (dtlobj == null)
            {
                tx_pno.EditValue = null;
                chk_yn.EditValue = null;
                tx_OKqty.EditValue = null;
                fp1.EditValue = null;
                btn1.EditValue = null;
                fp2.EditValue = null;
                btn2.EditValue = null;
                fmemo.EditValue = null;
                dp_fdt.DateTime = DateTime.Today;
                lup_fuser.EditValue = null;
                pr1.EditValue = null;
                btn3.EditValue = null;
                pr2.EditValue = null;
                btn4.EditValue = null;
                rmemo.EditValue = null;
                dp_rdt.DateTime = DateTime.Today;
                lupruser.EditValue = null;
                cboReceiptWay.EditValue = null;
                editEnable(false);
            }
            else
            {
                tx_pno.EditValue = dtlobj.PNo;
                chk_yn.EditValue = dtlobj.UseFlag;
                tx_OKqty.EditValue = dtlobj.UseQty;
                fp1.EditValue = dtlobj.FImg1 as byte[];
                btn1.EditValue = dtlobj.Fimg1File;
                fp2.EditValue = dtlobj.FImg2 as byte[];
                btn2.EditValue = dtlobj.Fimg2File;
                fmemo.EditValue = dtlobj.Fmemo;
                dp_fdt.DateTime = dtlobj.Fdate.GetNullToEmpty() == "" ? DateTime.Today : Convert.ToDateTime(dtlobj.Fdate);
                lup_fuser.EditValue = dtlobj.Fuser;
                pr1.EditValue = dtlobj.RImg1 as byte[];
                btn3.EditValue = dtlobj.Rimg1File;
                pr2.EditValue = dtlobj.RImg2 as byte[];
                btn4.EditValue = dtlobj.Rimg2File;
                rmemo.EditValue = dtlobj.Rmemo;
                dp_rdt.DateTime = dtlobj.Rdate.GetNullToEmpty() == "" ? DateTime.Today : Convert.ToDateTime(dtlobj.Rdate);
                lupruser.EditValue = dtlobj.Ruser;
                cboReceiptWay.EditValue = dtlobj.ReceiptWay;
                editEnable(true);
            }
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            dtlobj.PNo= dtlobj.PNo.GetNullToEmpty()== "" ? DbRequesHandler.GetRequestNumber("QCR") : dtlobj.PNo;
            dtlobj.UseFlag = chk_yn.EditValue.GetNullToEmpty();
            dtlobj.PType = mobj.PType;
            dtlobj.Seq = 1;
            dtlobj.UseQty = tx_OKqty.EditValue.GetIntNullToZero();
            dtlobj.FImg1 = fp1.EditValue as byte[];
            dtlobj.Fimg1File = btn1.EditValue.GetNullToEmpty();
            dtlobj.FImg2 = fp2.EditValue as byte[];
            dtlobj.Fimg2File = btn2.EditValue.GetNullToEmpty();
            dtlobj.Fmemo = fmemo.EditValue.GetNullToEmpty();
            dtlobj.Fdate = dp_fdt.DateTime;
            dtlobj.Fuser = lup_fuser.EditValue.GetNullToEmpty();
            dtlobj.RImg1 = pr1.EditValue as byte[];
            dtlobj.Rimg1File = btn3.EditValue.GetNullToEmpty();
            dtlobj.RImg2 = pr2.EditValue as byte[];
            dtlobj.Rimg2File = btn4.EditValue.GetNullToEmpty();
            dtlobj.Rmemo = rmemo.EditValue.GetNullToEmpty();
            dtlobj.Rdate = dp_rdt.DateTime;
            dtlobj.Ruser = lupruser.EditValue.GetNullToEmpty();
            dtlobj.ReceiptWay = cboReceiptWay.EditValue.GetNullToNull();
            QCModelService.Save();
            DataLoad();
        }
        
        protected override void AddRowClicked()
        {
            mobj = GridBindingSource.Current as VI_QCT1500_LIST;
            if (mobj == null) return;
            if (dtlobj != null) return;
            dtlobj = new TN_QCT1500()
            {
                ResultDate = mobj.ResultDate,
                UseFlag = "N",
                Seq = Convert.ToInt32(mobj.Seq),
                WorkNo = mobj.WorkNo,
                ProcessCode = mobj.ProcessCode,
                FaleType = mobj.FaleType
            };
            QCModelService.Insert(dtlobj);
            editEnable(true);
        }

        protected override void DeleteRow()
        {
            mobj = GridBindingSource.Current as VI_QCT1500_LIST;
            if (mobj != null)
            {
                if (dtlobj != null)
                {
                    DialogResult dlg = MessageBox.Show("처리번호: " + dtlobj.PNo.ToString() + "를 삭제하시겠습니까?", "주의", MessageBoxButtons.OKCancel);
                    if (dlg == DialogResult.OK)
                    {
                        //TN_QCT1500 tn = QCModelService.GetList(p => p.PNo == mobj.PNo).OrderBy(o => o.PNo).FirstOrDefault();
                        QCModelService.Delete(dtlobj);
                        QCModelService.Save();
                        DataLoad();
                    }
                }
            }
        }

        protected override void DataPrint()
        {
            if (mobj != null)
            {
                if (dtlobj != null)
                {
                    try
                    {
                        WaitHandler.ShowWait();

                        var FirstReport = new REPORT.RNCR();
                        var report = new REPORT.RNCR(dtlobj, mobj);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);

                        FirstReport.PrintingSystem.ShowMarginsWarning = false;
                        FirstReport.ShowPrintStatusDialog = false;
                        FirstReport.ShowPreview();
                    }
                    catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                    finally { WaitHandler.CloseWait(); }
                }
            }
            
        }

        private void btn1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ImageLoad(sender, e,fp1);
        }

        private void btn2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ImageLoad(sender, e, fp2);
        }

        private void btn3_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ImageLoad(sender, e, pr1);
        }

        private void btn4_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ImageLoad(sender, e, pr2);
        }

        private void ImageLoad(object sender, ButtonPressedEventArgs e, PictureEdit fp1)
        {
            ButtonEdit btn = sender as ButtonEdit;
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                btn.Tag = null;
                btn.EditValue = null;

                if (fp1 != null)
                    fp1.EditValue = null;
            }
            else
            {
                FileHolder fileHolder = FileHandler.OpenFile("");

                if (fileHolder != null)
                {
                    btn.Tag = fileHolder.FileData;
                    btn.EditValue = fileHolder.FileName;

                    if (fp1 != null)
                        fp1.EditValue = btn.Tag;
                }
            }
        }

        private void editEnable(bool ibool)
        {
            tx_OKqty.Enabled = ibool;
            tx_pno.Enabled = ibool;
            fp1.Enabled = ibool;
            fp2.Enabled = ibool;
            btn1.Enabled = ibool;
            btn2.Enabled = ibool;
            btn3.Enabled = ibool;
            btn4.Enabled = ibool;
            fmemo.Enabled = ibool;
            rmemo.Enabled = ibool;
            pr1.Enabled = ibool;
            pr2.Enabled = ibool;
            dp_fdt.Enabled = ibool;
            dp_rdt.Enabled = ibool;
            lup_fuser.Enabled = ibool;
            lupruser.Enabled = ibool;
            chk_yn.Enabled = ibool;
            cboReceiptWay.Enabled = ibool;
        }
    }
}