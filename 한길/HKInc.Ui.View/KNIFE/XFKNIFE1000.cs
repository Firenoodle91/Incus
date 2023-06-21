using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using HKInc.Service.Handler;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.KNIFE
{
    public partial class XFKNIFE1000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_KNIFE001> ModelService = (IService<TN_KNIFE001>)ProductionFactory.GetDomainService("TN_KNIFE001");
        public XFKNIFE1000()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitControls()
        {
            base.InitControls();
            GridExControl.MainGrid.MainView.RowCellClick += MainView_RowCellClick;
            checkEdit1.EditValue = "Y";
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.Init();
            IsGridButtonFileChooseEnabled = true;
            GridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, "바코드출력[F10]", IconImageList.GetIconImage("print/printer"));
            GridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "KnifeCode", true);
            GridExControl.MainGrid.AddColumn("_Check", "출력선택");
            GridExControl.MainGrid.AddColumn("KnifeMcode", "관리번호");
            GridExControl.MainGrid.AddColumn("KnifeCode", "칼 코드");
            GridExControl.MainGrid.AddColumn("KnifeName", "칼 명");
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드", false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm1", "품번", false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.ItemNm", "품명", false);
            GridExControl.MainGrid.AddColumn("MakeCust", "거래처");
            GridExControl.MainGrid.AddColumn("InputDt", "이관일");
            GridExControl.MainGrid.AddColumn("MastMc", "메인설비");
            GridExControl.MainGrid.AddColumn("XCase", "Cavity", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("StPostion1", "창고");
            GridExControl.MainGrid.AddColumn("StPostion2", "위치");
            GridExControl.MainGrid.AddColumn("StPostion3", "위치3", false);
            GridExControl.MainGrid.AddColumn("StdShotcnt", "기준샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Memo", "비고");
            GridExControl.MainGrid.AddColumn("CheckCycle", "점검주기");
            GridExControl.MainGrid.AddColumn("NextCheckDate", "다음점검일");
            GridExControl.MainGrid.AddColumn("Class", "등급");
            GridExControl.MainGrid.AddColumn("RealShotcnt", "샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("BaseShotcnt", "기본샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("SumShotcnt", "누적샷수", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("CheckPoint", "점검기준", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Imgurl", "이미지");
            GridExControl.MainGrid.AddColumn("UseYN", "사용여부");
            GridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check");
            GridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseYN", "N");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("InputDt");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("NextCheckDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MastMc", ModelService.GetChildList<TN_MEA1000>(p => p.UseYn == "Y").ToList(), "MachineCode", "MachineName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MakeCust", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle",  DbRequesHandler.GetCommCode(MasterCodeSTR.CHECKCYCLE).ToList(), "Mcode", "Codename");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, UserRight.HasEdit, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            GridExControl.MainGrid.MainView.Columns["Imgurl"].ColumnEdit = new HKInc.Service.Controls.ftpFileGridButtonEdit(GridExControl, "Imgurl");


            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion1", ModelService.GetChildList<TN_WMS1000>(p => p.UseYn == "Y").ToList(), "WhCode", "WhName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("StPostion2", ModelService.GetChildList<TN_WMS2000>(p => p.UseYn == "Y").ToList(), "PosionCode", "PosionName");
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("KnifeMcode");
            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();
            string useyn = checkEdit1.EditValue.GetNullToEmpty();
            string KnifeCodeName = textKnifeCodeName.Text.GetNullToEmpty();
            if (useyn == "Y")
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.KnifeMcode.Contains(KnifeCodeName) || p.KnifeName.Contains(KnifeCodeName) || p.KnifeCode.Contains(KnifeCodeName)))
                                                            .OrderBy(p => p.KnifeMcode)
                                                            .ToList();
            }
            else
            {
                GridBindingSource.DataSource = ModelService.GetList(p => (p.KnifeMcode.Contains(KnifeCodeName) || p.KnifeName.Contains(KnifeCodeName) || p.KnifeCode.Contains(KnifeCodeName)) &&
                                                                         (p.UseYN == "Y"))
                                                            .OrderBy(p => p.KnifeMcode)
                                                            .ToList();
            }
            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }
        
        protected override void DeleteRow()
        {
            TN_KNIFE001 obj = GridBindingSource.Current as TN_KNIFE001;

            //if (obj != null)
            //{                
            //    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYN", "N");
            //    obj.UseYN = "N";
            //}
            if (obj != null)
            {
                DialogResult result = Service.Handler.MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage(29), "칼정보"), HelperFactory.GetLabelConvert().GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("UseYN", "N");
                    //obj.UseYn = "N";
                    //ModelService.Update(obj);
                    return;
                }

                ModelService.Delete(obj);
                GridBindingSource.RemoveCurrent();
            }
        }

        protected override void FileChooseClicked()
        {
            if (GridBindingSource == null) return;
            var allList = GridBindingSource.List as List<TN_KNIFE001>;
            var checkList = allList.Where(p => p._Check == "Y").OrderBy(p => p.KnifeCode).ToList();
            var obj = GridBindingSource.Current as TN_KNIFE001;
            if (obj == null) return;

            if (checkList.Count > 0)
            {
                try
                {
                    WaitHandler.ShowWait();

                    var FirstReport = new REPORT.RKNIFELABEL();
                    foreach (var v in checkList)
                    {
                        var report = new REPORT.RKNIFELABEL(v);
                        report.CreateDocument();
                        FirstReport.Pages.AddRange(report.Pages);
                    }
                    FirstReport.PrintingSystem.ShowMarginsWarning = false;
                    FirstReport.ShowPrintStatusDialog = false;
                    FirstReport.ShowPreview();
                }
                catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                finally { WaitHandler.CloseWait(); }
            }
            else
            {
                var FirstReport = new REPORT.RKNIFELABEL(obj);
                FirstReport.CreateDocument();
                FirstReport.PrintingSystem.ShowMarginsWarning = false;
                FirstReport.ShowPrintStatusDialog = false;
                FirstReport.ShowPreview();
                //FirstReport.Print();
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFKNIFE1000, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }

        private void MainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            try
            {
                if (e.Clicks == 1)
                {
                    if (e.Column.Name.ToString() == "Imgurl")
                    {
                        WaitHandler.ShowWait();
                        String filename = gv.GetRowCellValue(e.RowHandle, gv.Columns["Imgurl"]).GetNullToEmpty();
                        if (filename.IsNullOrEmpty()) return;
                        byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + filename);
                        string[] lfileName = filename.Split('/');
                        if (lfileName.Length > 1)
                        {
                            File.WriteAllBytes(lfileName[lfileName.Length - 1], img);
                            HKInc.Service.Handler.FileHandler.StartProcess(lfileName[lfileName.Length - 1]);
                        }
                        else
                        {
                            File.WriteAllBytes(filename, img);
                            HKInc.Service.Handler.FileHandler.StartProcess(filename);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxHandler.ErrorShow(ex);
            }
            finally { WaitHandler.CloseWait(); }
        }
    }
}