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
using HKInc.Utils.Common;
using System.IO;

namespace HKInc.Ui.View.QC
{
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
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            editEnable(true);           // 2022-02-15 김진우 추가

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
                //editEnable(true);     // 2022-02-15 김진우 수정
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
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("ResultDate", "발생일");
            GridExControl.MainGrid.AddColumn("PType", false);
            GridExControl.MainGrid.AddColumn("WorkNo","작업지시번호");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목", false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명");
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품목");       // 2022-02-15 김진우 추가
            GridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", "차종");
            GridExControl.MainGrid.AddColumn("Seq",false);
            GridExControl.MainGrid.AddColumn("ProcessCode","공정");
            GridExControl.MainGrid.AddColumn("LotNo","LOTNO");
            GridExControl.MainGrid.AddColumn("FailQty","불량수량", HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("FaleType","불량유형");
            GridExControl.MainGrid.AddColumn("PNo","처리번호");

            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.CARTYPE), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("ResultDate");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("ProcessCode", DbRequestHandler.GetCommCode(MasterCodeSTR.Process), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("FaleType", DbRequestHandler.GetCommCode(MasterCodeSTR.QCFAIL), "Mcode", "Codename");
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
            dtlobj = null;          // 2022-02-15 김진우 추가

            string work = tx_workno.EditValue.GetNullToEmpty();
            string lotno = tx_lotno.EditValue.GetNullToEmpty();
            GridBindingSource.DataSource = ModelService.GetList(p =>(p.ResultDate>= dp_dt.DateFrEdit.DateTime && p.ResultDate<= dp_dt.DateToEdit.DateTime)        
                                                                 && (string.IsNullOrEmpty(work) ? true : p.WorkNo.Contains(work))       // 2022-02-03 김진우 대리 추가
                                                                 && (string.IsNullOrEmpty(lotno) ? true : p.WorkNo.Contains(lotno))     // 2022-02-03 김진우 대리 추가
                                                                    ).ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            if (dtlobj != null)
            {
                dtlobj.PNo = dtlobj.PNo.GetNullToEmpty() == "" ? DbRequestHandler.GetRequestNumber("QCR") : dtlobj.PNo;
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
                QCModelService.Save();
            }
            DataLoad();
        }
        
        protected override void DeleteRow()
        {
            mobj = GridBindingSource.Current as VI_QCT1500_LIST;
            DialogResult dlg = MessageBox.Show("처리번호: " + mobj.PNo.ToString() + "를 삭제하시겠습니까?", "주의", MessageBoxButtons.OKCancel);
            if (dlg == DialogResult.OK)
            {
                TN_QCT1500 tn = QCModelService.GetList(p => p.PNo == mobj.PNo).OrderBy(o => o.PNo).FirstOrDefault();
                QCModelService.Delete(tn);
                QCModelService.Save();
                DataLoad();
            }
        }
        protected override void AddRowClicked()
        {
            mobj = GridBindingSource.Current as VI_QCT1500_LIST;
            if (mobj == null) return;

            if (mobj.PNo == null)
            {
                dtlobj = new TN_QCT1500()
                {
                    PNo = DbRequestHandler.GetRequestNumber("QCR"),     // 2022-02-15 김진우 추가
                    ResultDate = mobj.ResultDate,
                    UseFlag = "N",
                    Seq = Convert.ToInt32(mobj.Seq),
                    WorkNo = mobj.WorkNo,
                    ProcessCode = mobj.ProcessCode,
                    FaleType = mobj.FaleType
                };
                QCModelService.Insert(dtlobj);
            }
            editEnable(true);
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

        private void btn1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            ImageLoad(sender, e, fp1);
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

        /// <summary>
        /// 2022-02-03 김진우 대리 추가
        /// 더블클릭시 팝업으로 사진 보여주도록 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picture_DoubleClick(object sender, EventArgs e)
        {
            PictureEdit Pictire = sender as PictureEdit;
            object obj = Pictire.EditValue;

            SELECT_Popup.XPF_PIC PopUp = new SELECT_Popup.XPF_PIC(obj);
            PopUp.ShowDialog();
        }
    }
}