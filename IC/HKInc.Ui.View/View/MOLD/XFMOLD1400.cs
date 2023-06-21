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
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Service.Handler;
using System.IO;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;


namespace HKInc.Ui.View.View.MOLD
{
    /// <summary>    
    /// 금형일상점검등록
    /// </summary>
    public partial class XFMOLD1400 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<TN_MOLD1100> ModelService = (IService<TN_MOLD1100>)ProductionFactory.GetDomainService("TN_MOLD1100");
        #endregion

        public XFMOLD1400()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

        }

       

        protected override void InitCombo()
        {
            lup_MoldCode.SetDefault(true, "MoldMCode", DataConvert.GetCultureDataFieldName("MoldName"), ModelService.GetChildList<TN_MOLD1100>(p => p.UseYN == "Y").ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            MasterGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldName"), LabelConvert.GetLabelText("MoldName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemCode"), LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMakerCust"), LabelConvert.GetLabelText("MoldMakerCust"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("TransferDate"), LabelConvert.GetLabelText("TransferDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MainMachineCode"), LabelConvert.GetLabelText("MainMachineCode"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Cavity"), LabelConvert.GetLabelText("Cavity"), HorzAlignment.Far, FormatType.Numeric, "n0");

            //MasterGridExControl.MainGrid.AddColumn("MoldWhCode", LabelConvert.GetLabelText("MoldWhCode"));
            //MasterGridExControl.MainGrid.AddColumn("MoldWhPosition", LabelConvert.GetLabelText("MoldWhPosition"));
            ////GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion1"), LabelConvert.GetLabelText("StPostion1"));
            ////GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion2"), LabelConvert.GetLabelText("StPostion2"));
            ////GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StPostion3"), LabelConvert.GetLabelText("StPostion3"));
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("StdShotcnt"), LabelConvert.GetLabelText("StdShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"));
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("NextCheckDate"), LabelConvert.GetLabelText("NextMoldCheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldClass"), LabelConvert.GetLabelText("MoldClass"));
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("RealShotcnt"), LabelConvert.GetLabelText("RealShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("BaseShotcnt"), LabelConvert.GetLabelText("BaseShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckShotcnt"), LabelConvert.GetLabelText("CheckShotcnt"), HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("FileName"), LabelConvert.GetLabelText("FileName"));
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("FileUrl"), LabelConvert.GetLabelText("FileUrl"), false);
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UploadFilePath"), LabelConvert.GetLabelText("UploadFilePath"), false);
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DeleteFilePath"), LabelConvert.GetLabelText("DeleteFilePath"), false);
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DisuseDate"), LabelConvert.GetLabelText("DisuseDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            //MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("UseYN"), LabelConvert.GetLabelText("UseFlag"), HorzAlignment.Center, true);

            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldMCode"), LabelConvert.GetLabelText("MoldMcode"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("MoldCode"), LabelConvert.GetLabelText("MoldCode"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Seq"), LabelConvert.GetLabelText("Seq"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ReqType"), LabelConvert.GetLabelText("ReqType"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("FileName"), LabelConvert.GetLabelText("FileName"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("FileUrl"), LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckPosition"), LabelConvert.GetLabelText("CheckPosition"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckList"), LabelConvert.GetLabelText("CheckList"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckWay"), LabelConvert.GetLabelText("CheckWay"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckCycle"), LabelConvert.GetLabelText("CheckCycle"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("CheckStandardDate"), LabelConvert.GetLabelText("CheckStandardDate"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ManagementStandard"), LabelConvert.GetLabelText("ManagementStandard"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("DisplayOrder"), LabelConvert.GetLabelText("DisplayOrder"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Memo"), LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "ReqType", "FileName", "CheckPosition", "CheckList", "CheckWay", "CheckCycle", "CheckStandardDate", "ManagementStandard", "DisplayOrder", "Memo");

            var barButtonItemCopy = new DevExpress.XtraBars.BarButtonItem();
            barButtonItemCopy.Id = 4;
            barButtonItemCopy.ImageOptions.Image = IconImageList.GetIconImage("miscellaneous/wizard");
            barButtonItemCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.I));
            barButtonItemCopy.Name = "barButtonItemCopy";
            barButtonItemCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonItemCopy.ShortcutKeyDisplayString = "Alt+I";
            barButtonItemCopy.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonItemCopy.Caption = LabelConvert.GetLabelText("MoldInspectionCopy") + "[Alt+I]";
            barButtonItemCopy.ItemClick += BarButtonItemCopy_ItemClick;
            DetailGridExControl.BarTools.AddItem(barButtonItemCopy);
        }

        protected override void InitBindingSource()
        {
            base.InitBindingSource();

        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MoldMakerCust", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldMakercust), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));            
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MainMachineCode", ModelService.GetChildList<TN_MEA1000>(p => p.UseFlag == "Y").ToList(), "MachineMCode", DataConvert.GetCultureDataFieldName("MachineName"), true);
            MasterGridExControl.MainGrid.SetRepositoryItemCodeLookUpEdit("ItemCode", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());    // 2021-07-15 김진우 주임 SetRepositoryItemSearchLookUpEdit 에서 SetRepositoryItemCodeLookUpEdit 로 변경

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ReqType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldReqType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_MoldCheckImage, "FileName", "FileUrl");            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPosition", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckPosition), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckList), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckCycle", DbRequestHandler.GetCommTopCode(MasterCodeSTR.MoldCheckCycle), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("CheckStandardDate");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", true);
            

        }
        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            DetailGridRowLocator.GetCurrentRow("MoldMCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            var moldCode = lup_MoldCode.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(moldCode) ? true : p.MoldMCode == moldCode) &&
                                                                            (p.UseYN.Equals("Y"))).OrderBy(o => o.MoldMCode).ToList(); 

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();

            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }
        protected override void MasterFocusedRowChanged()
        {
            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (MasterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            if (MasterObj.TN_MOLD1400List != null)
                if (MasterObj.TN_MOLD1400List.Count > 0)
                {
                    List<TN_MOLD1400> tempList = MasterObj.TN_MOLD1400List.OrderBy(o => o.DisplayOrder).ToList();
                    DetailGridBindingSource.DataSource = tempList;
                }
                else
                {
                    DetailGridBindingSource.DataSource = null;
                }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            DetailGridRowLocator.SetCurrentRow();

        }
        protected override void DetailAddRowClicked()
        {
            if (!UserRight.HasEdit) return;
            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (MasterObj != null)
            {
                TN_MOLD1400 NewObj = new TN_MOLD1400()
                {
                    MoldMCode = MasterObj.MoldMCode,
                    MoldCode = MasterObj.MoldCode,
                    Seq = MasterObj.TN_MOLD1400List.Count == 0 ? 1 : MasterObj.TN_MOLD1400List.Max(p => p.Seq) + 1,
                    DisplayOrder = MasterObj.TN_MOLD1400List.Count == 0 ? 1 : MasterObj.TN_MOLD1400List.Max(p => p.DisplayOrder) + 1
                };

                MasterObj.TN_MOLD1400List.Add(NewObj);
                DetailGridBindingSource.Add(NewObj);
            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }
        protected override void DeleteDetailRow()
        {

            TN_MOLD1100 MasterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (MasterObj == null)
                return;

            TN_MOLD1400 delobj = DetailGridBindingSource.Current as TN_MOLD1400;
            if (delobj == null)
                return;           

            MasterObj.TN_MOLD1400List.Remove(delobj);
            DetailGridBindingSource.RemoveCurrent();
        }

        protected override void DataSave()
        {
            DetailGridBindingSource.EndEdit();
            DetailGridExControl.MainGrid.PostEditor();

            #region 파일 CHECK
           

            //if (DeleteFileList.Count > 0)
            //{
            //    foreach (var v in DeleteFileList)
            //    {
            //        try
            //        {
            //            FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, v.FileUrl);
            //        }
            //        catch { }
            //    }
            //}


            if (DetailGridBindingSource != null && DetailGridBindingSource.DataSource != null)
            {
                var detailList = DetailGridBindingSource.List as List<TN_MOLD1400>;
                var editList = detailList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = v.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.MoldMCode + "_" + v.Seq + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_MoldCheckImage + "/" + realFileName;

                            FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_MoldCheckImage + "/");

                            v.FileName = realFileName;
                            v.FileUrl = ftpFileUrl;
                        }
                    }
                    //foreach (var d in masterList.Where(p => !p.DeleteFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.PathDeleteFTP(GlobalVariable.FTP_SERVER, d.DeleteFilePath);
                    //    }
                    //    catch { }
                    //}

                    //foreach (var d in masterList.Where(p => !p.UploadFilePath.IsNullOrEmpty()))
                    //{
                    //    try
                    //    {
                    //        FileHandler.UploadFTP(d.UploadFilePath, string.Format("{0}{1}/{2}/", GlobalVariable.FTP_SERVER, MasterCodeSTR.FtpFolder_Inspection_IN_File, d.InspNo));
                    //        d.FileUrl = string.Format("{0}/{1}/{2}", MasterCodeSTR.FtpFolder_Inspection_IN_File, d.InspNo, d.FileName);
                    //    }
                    //    catch { }
                    //}
                }
            }
            #endregion

            ModelService.Save();
            DataLoad();
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            TN_MOLD1400 delobj = DetailGridBindingSource.Current as TN_MOLD1400;
            if (delobj == null)
                return;
            delobj.EditRowFlag = "Y";
        }

        private void BarButtonItemCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null)
                return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Value_1, masterObj.ItemCode);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.XPFMOLD1400_COPY, param, ItemCopyPopupCallback);

            form.ShowPopup(true);
        }

        private void ItemCopyPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<TN_MOLD1400>)e.Map.GetValue(PopupParameter.ReturnObject);

            var masterObj = MasterGridBindingSource.Current as TN_MOLD1100;
            if (masterObj == null)
                return;

            //var qcList = qcRev.TN_QCT1001List.Where(p => p.UseFlag == "Y" && p.CheckDivision == MasterCodeSTR.InspectionDivision_IN).OrderBy(p => p.DisplayOrder).ToList();

            //if (masterObj.TN_MOLD1400List.Count == 0) 기존데이터 있을때도 복사
            //{

                foreach (var v in returnList)
                {

                    var newObj = new TN_MOLD1400()
                    {
                        MoldMCode = v.MoldMCode,
                        MoldCode = v.MoldCode,
                        Seq = masterObj.TN_MOLD1400List.Count == 0 ? 1 : masterObj.TN_MOLD1400List.Max(p => p.Seq) + 1,
                        ReqType = v.ReqType,
                        FileName = v.FileName,
                        FileUrl = v.FileUrl,
                        CheckPosition = v.CheckPosition,
                        CheckList = v.CheckList,
                        CheckWay = v.CheckWay,
                        EyeCheckFlag = v.EyeCheckFlag,
                        CheckCycle = v.CheckCycle,
                        CheckStandardDate = v.CheckStandardDate,
                        ManagementStandard = v.ManagementStandard,
                        DisplayOrder = masterObj.TN_MOLD1400List.Count == 0 ? 1 : masterObj.TN_MOLD1400List.Max(p => p.DisplayOrder) + 1

                    };
                    DetailGridBindingSource.Add(newObj);
                    masterObj.TN_MOLD1400List.Add(newObj);
                }
                DetailGridExControl.BestFitColumns();             
            //}
        }
    }
}