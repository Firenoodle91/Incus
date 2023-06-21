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
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Common;
using HKInc.Service.Handler;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using System.IO;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 수입검사관리
    /// </summary>
    public partial class XFQCT1100 : Service.Base.ListMasterDetailSubDetailTreeDetailFormTemplate
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");        

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        public XFQCT1100()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            TreeDetailGridExControl = gridEx4;
            

            MasterGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged1;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MasterMainView_RowCellStyle;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailMainView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.KeyDown += MainView_KeyDown;

            SubDetailGridExControl.MainGrid.MainView.RowCellStyle += SubMainView_RowCellStyle;

            TreeDetailGridExControl.MainGrid.MainView.FocusedRowChanged += TreeMainView_FocusedRowChanged;


            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
                , DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
            };
            repositoryItemGridLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            repositoryItemGridLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            repositoryItemGridLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            repositoryItemGridLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
            repositoryItemGridLookUpEdit.NullText = "";
            repositoryItemGridLookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            repositoryItemGridLookUpEdit.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            repositoryItemGridLookUpEdit.Appearance.BackColor = Color.White;
            repositoryItemGridLookUpEdit.DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG).ToList();
            GridColumn col1 = repositoryItemGridLookUpEdit.View.Columns.AddField(repositoryItemGridLookUpEdit.DisplayMember);
            col1.VisibleIndex = 0;
                       
            repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            repositoryItemSpinEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.Default;
            repositoryItemSpinEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryItemSpinEdit.Mask.UseMaskAsDisplayFormat = true;
            repositoryItemSpinEdit.AllowMouseWheel = true;
            repositoryItemSpinEdit.Appearance.BackColor = Color.White;
            repositoryItemSpinEdit.Buttons[0].Visible = false;

            repositoryItemTextEdit = new RepositoryItemTextEdit();
            pic_DesignFileName.KeyDown += Pic_DesignFileName_KeyDown;
            pic_DesignFileName.KeyUp += Pic_DesignFileName_KeyUp;
            pic_DesignFileName.MouseWheel += Pic_DesignFileName_MouseWheel; ;
            //pic_DesignFileName.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            //pic_DesignFileName.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pic_DesignFileName.Properties.ShowScrollBars = true;
            //pic_DesignFileName.Properties.ZoomingOperationMode = DevExpress.XtraEditors.Repository.ZoomingOperationMode.MouseWheel;
            pic_DesignFileName.Properties.AllowScrollViaMouseDrag = true;
            pic_DesignFileName.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;

            pdf_Design.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;
            pdf_Design.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;

            var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonPrint.Id = 6;
            barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            barButtonPrint.Name = "barButtonPrint";
            barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonPrint.Caption = LabelConvert.GetLabelText("InspectionReportPrint") + "[Alt+P]";
            barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            MasterGridExControl.BarTools.AddItem(barButtonPrint);
            dt_CheckDate.SetTodayIsMonth();
        }

        

        bool picZoomFlag = false;
        float zoomPer = 10;
        private void Pic_DesignFileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                picZoomFlag = true;
            }
        }
        private void Pic_DesignFileName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                picZoomFlag = false;
            }
        }

        private void Pic_DesignFileName_MouseWheel(object sender, MouseEventArgs e)
        {
            if (picZoomFlag)
            {
                var v = e.Delta;
                if (v < 0)
                    pic_DesignFileName.Properties.ZoomPercent -= zoomPer;
                else
                    pic_DesignFileName.Properties.ZoomPercent += zoomPer;
            }
        }

        protected override void InitCombo()
        {         
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());

            lcDesign.Expanded = false;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "InspNo", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            MasterGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.StockInspFlag", LabelConvert.GetLabelText("StockInspFlag"), HorzAlignment.Center, true);
            MasterGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("InNo"));
            MasterGridExControl.MainGrid.AddColumn("WorkDate", LabelConvert.GetLabelText("InDate"));
            MasterGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.BottomCategory", LabelConvert.GetLabelText("BottomCategory"), false);
            MasterGridExControl.MainGrid.AddColumn("ProcessCode", LabelConvert.GetLabelText("ProcessName"));
            MasterGridExControl.MainGrid.AddColumn("CheckDivision", LabelConvert.GetLabelText("InspectionDivision"));
            MasterGridExControl.MainGrid.AddColumn("CheckPoint", LabelConvert.GetLabelText("CheckPoint"));
            MasterGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"));
            MasterGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId2"));
            MasterGridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("InspectOkQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("NgQty", LabelConvert.GetLabelText("InspectNgQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("ReturnQty", LabelConvert.GetLabelText("InspectReturnQty"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            MasterGridExControl.MainGrid.AddColumn("CheckResult", LabelConvert.GetLabelText("CheckResult"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("CheckReport"));
            MasterGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            MasterGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            MasterGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "CheckDate", "CheckResult", "Memo", "OkQty", "NgQty", "ReturnQty");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1100>(MasterGridExControl);

            //if (UserRight.HasEdit)
            //{
            //    var checkRandomGroupId = (int)UserGroupEnum.CheckRandomGroup;
            //    var checkObj = ModelService.GetChildList<UserGroup>(p => p.UserGroupId == checkRandomGroupId).FirstOrDefault();
            //    if (checkObj != null)
            //    {
            //        if (checkObj.UserUserGroupList.Where(p => p.User.LoginId == GlobalVariable.LoginId).Count() == 1)
            //        {
            //            IsDetailGridButtonFileChooseEnabled = true;
            //            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ReadingRandomAdd"), IconImageList.GetIconImage("business%20objects/botask"));
            //        }
            //    }
            //}

            IsDetailGridButtonFileChooseEnabled = true;
            DetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, true);
            DetailGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("Copy") + "[F10]", IconImageList.GetIconImage("spreadsheet/showdetail"));
            DetailGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            DetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            //DetailGridExControl.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InspectionItem"));
            DetailGridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_QCT1001.InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), false);
            DetailGridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_QCT1001.MaxReading", LabelConvert.GetLabelText("MaxReading"));
            DetailGridExControl.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"));
            DetailGridExControl.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"));
            DetailGridExControl.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"));
            DetailGridExControl.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"));
            DetailGridExControl.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"));
            DetailGridExControl.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"));
            DetailGridExControl.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"));
            DetailGridExControl.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"));
            DetailGridExControl.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"));
            DetailGridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            DetailGridExControl.MainGrid.AddColumn("TN_QCT1001.InstrMemo", LabelConvert.GetLabelText("InstrMemo"));
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("AttachFile"));
            DetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            DetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            DetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1101>(DetailGridExControl);

            SubDetailGridExControl.SetToolbarVisible(false);
            SubDetailGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"));
            SubDetailGridExControl.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            SubDetailGridExControl.MainGrid.AddColumn("InLotNo", LabelConvert.GetLabelText("InLotNo"));
            SubDetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            SubDetailGridExControl.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InstrType"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"));
            SubDetailGridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);
            SubDetailGridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), false);
            SubDetailGridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), false);
            SubDetailGridExControl.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"));
            SubDetailGridExControl.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"));
            SubDetailGridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            SubDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, false);
            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.DeleteRow, false);
            TreeDetailGridExControl.SetToolbarButtonVisible(GridToolbarButton.FileChoose, false);
            TreeDetailGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            TreeDetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            TreeDetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            TreeDetailGridExControl.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InspectionItem"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("TN_QCT1001.InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            TreeDetailGridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, false);
            TreeDetailGridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, false);
            TreeDetailGridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), true);
            TreeDetailGridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), true);
            TreeDetailGridExControl.MainGrid.AddColumn("TN_QCT1001.MaxReading", LabelConvert.GetLabelText("MaxReading"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"));
            TreeDetailGridExControl.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"));
            TreeDetailGridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            TreeDetailGridExControl.MainGrid.AddColumn("TN_QCT1001.InstrMemo", LabelConvert.GetLabelText("InstrMemo"));
            TreeDetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            TreeDetailGridExControl.MainGrid.AddColumn("FileName", LabelConvert.GetLabelText("AttachFile"));
            TreeDetailGridExControl.MainGrid.AddColumn("FileUrl", LabelConvert.GetLabelText("FileUrl"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("UploadFilePath", LabelConvert.GetLabelText("UploadFilePath"), false);
            TreeDetailGridExControl.MainGrid.AddColumn("DeleteFilePath", LabelConvert.GetLabelText("DeleteFilePath"), false);
            //TreeDetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1101>(TreeDetailGridExControl);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPoint", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckResult", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, MasterGridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");
            //MasterGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit_BAK(UserRight.HasEdit, MasterGridExControl, "FileName", "FileUrl");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("OkQty", DefaultBoolean.Default, "n0");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("NgQty", DefaultBoolean.Default, "n0");
            MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("ReturnQty", DefaultBoolean.Default, "n0");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.BottomCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 3), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));


            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["TN_QCT1001.InstrMemo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "TN_QCT1001.InstrMemo", false);
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(UserRight.HasEdit, DetailGridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_QCT1001.MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));            
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            TreeDetailGridExControl.MainGrid.MainView.Columns["TN_QCT1001.InstrMemo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeDetailGridExControl, "TN_QCT1001.InstrMemo", false);
            TreeDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(TreeDetailGridExControl, "Memo", false);
            TreeDetailGridExControl.MainGrid.MainView.Columns["FileName"].ColumnEdit = new Service.Controls.FtpFileGridButtonEdit(false, TreeDetailGridExControl, MasterCodeSTR.FtpFolder_Inspection_IN_File, "FileName", "FileUrl");
            TreeDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_QCT1001.MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
            TreeDetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("InspNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();

            pic_DesignFileName.EditValue = null;
            pdf_Design.CloseDocument();

            ModelService.ReLoad();

            //데이터리로드
            InitRepository();
            InitCombo();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.CheckDate >= dt_CheckDate.DateFrEdit.DateTime && p.CheckDate <= dt_CheckDate.DateToEdit.DateTime)
                                                                            && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                            && (p.CheckDivision == MasterCodeSTR.InspectionDivision_IN)
                                                                      )
                                                                      .OrderBy(p => p.CheckDate)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            SubDetailGridExControl.MainGrid.Clear();
            TreeDetailGridExControl.MainGrid.Clear();
            pic_DesignFileName.EditValue = null;
            pdf_Design.CloseDocument();

            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null)
            {
                return;
            }

            var TN_STD1101 = masterObj.TN_STD1100.TN_STD1101List.OrderBy(p => p.Seq).LastOrDefault();
            //품목 도면
            if (TN_STD1101 != null && !TN_STD1101.DesignFileUrl.IsNullOrEmpty())
            {
                var fileName = TN_STD1101.DesignFileName;
                int fileExtPos = fileName.LastIndexOf(".");
                string extName = string.Empty;
                if (fileExtPos >= 0)
                    extName = fileName.Substring(fileExtPos + 1, fileName.Length - fileExtPos - 1);

                if (extName.ToLower() == "pdf")
                {
                    lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    pic_DesignFileName.EditValue = null;

                    byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1101.DesignFileUrl);
                    MemoryStream ms = new MemoryStream(documentContent);
                    pdf_Design.LoadDocument(ms);
                }
                else
                {
                    lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    pic_DesignFileName.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1101.DesignFileUrl);
                    pdf_Design.CloseDocument();
                }
            }
            else
            {
                lcDesign3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcDesign2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                pic_DesignFileName.EditValue = null;
                pdf_Design.CloseDocument();
            }

            //SCM을 먼저 넣은 이유는 DetailFocused 함수를 2개가 타서
            // 먼저 TreeDetail 후 DetailGrid 탈수있게 변경 (SubDetail List)
            SCM_VI_QCT1100_SCM SCMQC = ModelService.GetChildList<SCM_VI_QCT1100_SCM>(P => P.WorkNo == masterObj.WorkNo && P.ItemCode == masterObj.ItemCode).FirstOrDefault();
            if (SCMQC == null) { }
            else
            {
                TreeDetailGridBindingSource.DataSource = SCMQC.TN_QCT1101List_SCM;// ModelService.GetChildList<SCM_VI_QCT1101_SCM>(P => P.InspNo == SCMQC.InspNo).ToList();
                TreeDetailGridExControl.DataSource = TreeDetailGridBindingSource;
                TreeDetailGridExControl.BestFitColumns();
            }

            DetailGridBindingSource.DataSource = masterObj.TN_QCT1101List.OrderBy(o => o.InspSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            
        }

        protected override void DetailFocusedRowChanged()
        {
            var detailObj = DetailGridBindingSource.Current as TN_QCT1101;
            if (detailObj == null)
            {
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _ItemCode = new SqlParameter("@ItemCode", detailObj.ItemCode);
                var _CheckList = new SqlParameter("@CheckList", detailObj.CheckList);
                //var _ScmYn = new SqlParameter("@ScmYn", detailObj.ScmYn);
                var result = context.Database.SqlQuery<TEMP_XFQCT1100_SUB>("USP_GET_INSP_IN_CHECK_LIST_RECORD @ItemCode,@CheckList", _ItemCode, _CheckList).ToList();
                SubDetailGridBindingSource.DataSource = result.ToList();
            }

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        private void TreeMainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {

            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null)
            {
                return;
            }

            var treeObj = TreeDetailGridBindingSource.Current as TN_QCT1101;
            if (treeObj == null)
            {
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _ItemCode = new SqlParameter("@ItemCode", treeObj.ItemCode);
                var _CheckList = new SqlParameter("@CheckList", treeObj.CheckList);
                //var _ScmYn = new SqlParameter("@ScmYn", treeObj.ScmYn);
                //var result = context.Database.SqlQuery<TEMP_XFQCT1100_SUB>("USP_GET_INSP_IN_CHECK_LIST_RECORD @ItemCode,@CheckList,@ScmYn", _ItemCode, _CheckList, _ScmYn).ToList();
                var result = context.Database.SqlQuery<TEMP_XFQCT1100_SUB>("USP_GET_INSP_IN_CHECK_LIST_RECORD @ItemCode,@CheckList", _ItemCode, _CheckList).ToList();
                SubDetailGridBindingSource.DataSource = result.ToList();
            }

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }



        protected override void AddRowClicked()
        {
            var constraintList = MasterGridBindingSource.List as List<TN_QCT1100>;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.IsMultiSelect, true);
            param.SetValue(PopupParameter.Constraint, constraintList);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_QCT_IN, param, MasterAddCallBack);
            form.ShowPopup(true);
        }

        private void MasterAddCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<VI_INSP_IN_OBJECT_V2>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList.ToList())
            {
                if (!v.InCustomerLotNo.IsNullOrEmpty())
                {
                    //var checkObj = ModelService.GetChildList<TN_QCT1100>(p => p.Temp2 == v.InCustomerLotNo);

                }
                var newObj = new TN_QCT1100()
                {
                    InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_IN),
                    CheckDivision = MasterCodeSTR.InspectionDivision_IN,
                    CheckPoint = MasterCodeSTR.CheckPoint_General,
                    WorkNo = v.InNo,
                    WorkSeq = v.InSeq,
                    WorkDate = v.InDate,
                    CustomerCode = v.CustomerCode,
                    ItemCode = v.ItemCode,
                    InLotNo = v.InLotNo,
                    ProductLotNo = v.ProductLotNo.GetNullToNull(),
                    CheckDate = DateTime.Today,
                    CheckId = GlobalVariable.LoginId,
                    Temp2 = v.InCustomerLotNo,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).First(),
                    NewRowFlag = "Y",
                    EditRowFlag = "Y",
                    ScmYn = "N",
                    OkQty = v.InQty,
                    NgQty = 0,
                    
                };
                MasterGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
                IsFormControlChanged = true;
            }

            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null) return;

            if(masterObj.ScmYn == "Y")
            {
                MessageBox.Show("협력업체 검사규격이 존재합니다. 삭제가 불가합니다.");
                return;
            }

            var detailchk = masterObj.TN_QCT1101List.Where(p => p.ScmYn == "N").ToList();

            //if (masterObj.TN_QCT1101List.Count >0 )
            if (detailchk.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("InspectionDetailInfo")));
                return;
            }

            if (ModelService.GetChildList<TN_PUR1301>(p => p.InNo == masterObj.WorkNo && p.InSeq == masterObj.WorkSeq).FirstOrDefault() != null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("OutInfo")));
                return;
            }
            MasterGridBindingSource.Remove(masterObj);
            ModelService.Delete(masterObj);
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null) return;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == masterObj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBox.Show("해당품번의 검사규격이 존재하지 않습니다.");
                return;
            }

            var qcList = qcRev.TN_QCT1001List.Where(p=>p.UseFlag == "Y" && p.CheckDivision == MasterCodeSTR.InspectionDivision_IN).OrderBy(p => p.DisplayOrder).ToList();

            foreach (var v in qcList)
            {
                if (masterObj.TN_QCT1101List.Any(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq && p.ScmYn == "N"))
                    continue;

                var newObj = new TN_QCT1101()
                {
                    InspNo = masterObj.InspNo,
                    InspSeq = masterObj.TN_QCT1101List.Count == 0 ? 1 : masterObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
                    RevNo = v.RevNo,
                    ItemCode = v.ItemCode,
                    Seq = v.Seq,
                    CheckWay = v.CheckWay,
                    CheckList = v.CheckList,
                    CheckMax = v.CheckMax,
                    CheckMin = v.CheckMin,
                    CheckSpec = v.CheckSpec,
                    CheckUpQuad = v.CheckUpQuad,
                    CheckDownQuad = v.CheckDownQuad,
                    CheckDataType = v.CheckDataType,                    
                    TN_QCT1001 = v,
                    EditRowFlag = "Y",                    
                };
                DetailGridBindingSource.Add(newObj);
                masterObj.TN_QCT1101List.Add(newObj);
            }
            DetailGridExControl.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_QCT1101;
            if (detailObj == null) return;

            masterObj.TN_QCT1101List.Remove(detailObj);
            DetailGridBindingSource.Remove(detailObj);
            DetailGridExControl.BestFitColumns();

            MasterCheckResult(masterObj);
        }

        /// <summary>
        /// COPY
        /// </summary>
        protected override void DetailFileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null) return;
            SCM_VI_QCT1100_SCM SCMQC = ModelService.GetChildList<SCM_VI_QCT1100_SCM>(P => P.WorkNo == masterObj.WorkNo && P.ItemCode == masterObj.ItemCode).FirstOrDefault();
            if (SCMQC == null) return;
         

                foreach (var v in SCMQC.TN_QCT1101List_SCM)
                {

                    TN_QCT1101 scmd = new TN_QCT1101()
                    {
                        InspNo = masterObj.InspNo,
                        InspSeq = masterObj.TN_QCT1101List.Count == 0 ? 1 : masterObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
                        RevNo = v.RevNo,
                        ItemCode = v.ItemCode,
                        Seq = v.Seq,
                        CheckWay = v.CheckWay,
                        CheckList = v.CheckList,
                        CheckMax = v.CheckMax,
                        CheckMin = v.CheckMin,
                        CheckSpec = v.CheckSpec,
                        CheckUpQuad = v.CheckUpQuad,
                        CheckDownQuad = v.CheckDownQuad,
                        CheckDataType = v.CheckDataType,
                        Reading1= v.Reading1,
                        Reading2=v.Reading2,
                        Reading3=v.Reading3,
                        Reading4=v.Reading4,
                        Reading5=v.Reading5,
                        Reading6=v.Reading6,
                        Reading7=v.Reading7,
                        Reading8=v.Reading8,
                        Reading9=v.Reading9,
                      TN_QCT1001 = v.TN_QCT1001,
                        EditRowFlag = "Y"
                    };
                var checkDataType = scmd.CheckDataType.GetNullToEmpty();
                if (checkDataType == MasterCodeSTR.CheckDataType_C)
                {
                    //육안검사 
                    DetailCheckEye(scmd);
                }
                else if (checkDataType.IsNullOrEmpty())
                {
                    DetailCheckInput(scmd);
                }
                else
                {
                    DetailCheckInput(scmd);
                }
                DetailGridBindingSource.Add(scmd);

                masterObj.TN_QCT1101List.Add(scmd);
            }
           

            MasterCheckResult(masterObj);
            DetailGridExControl.BestFitColumns();


        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();            

            MasterGridBindingSource.EndEdit();
            DetailGridBindingSource.EndEdit();            

            #region 파일 CHECK
            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_QCT1100>;
                var editList = masterList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = v.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.InspNo + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_Inspection_IN_File + "/" + realFileName;

                            FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_Inspection_IN_File + "/");
                            
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
                var detailList = DetailGridBindingSource.List as List<TN_QCT1101>;
                var editList = detailList.Where(p => p.EditRowFlag == "Y").ToList();
                if (editList.Count > 0)
                {
                    foreach (var v in editList.Where(c => c.FileUrl != null && c.FileUrl.Contains("\\")).ToList())
                    {
                        string[] filename = v.FileUrl.ToString().Split('\\');
                        if (filename.Length != 1)
                        {
                            var realFileName = v.InspNo + "_" + v.Seq + "_" + filename[filename.Length - 1];
                            var ftpFileUrl = MasterCodeSTR.FtpFolder_Inspection_IN_File + "/" + realFileName;

                            FileHandler.UploadFTP(v.FileUrl, realFileName, GlobalVariable.FTP_SERVER + MasterCodeSTR.FtpFolder_Inspection_IN_File + "/");

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

        /// <summary>
        /// 마스터 셀 변경 시 Edit 체크를 위함.
        /// </summary>
        private void MainView_CellValueChanged1(object sender, CellValueChangedEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null) return;

            masterObj.EditRowFlag = "Y";


            if (e.Column.FieldName == "NgQty")
            {
                var tN_PUR1201 = ModelService.GetChildList<TN_PUR1201>(x => x.InLotNo == masterObj.InLotNo).FirstOrDefault();
                var tN_PUR1501 = ModelService.GetChildList<TN_PUR1501>(x => x.InLotNo == masterObj.InLotNo).FirstOrDefault();

                decimal productQty = 0;
                decimal okQty = 0;
                decimal ngQty = masterObj.NgQty.GetNullToZero();

                if (tN_PUR1201 != null)
                {
                    productQty = tN_PUR1201.InQty.GetNullToZero();
                    okQty = productQty - ngQty;
                }
                else if (tN_PUR1501 != null)
                {
                    productQty = tN_PUR1501.InQty.GetNullToZero();
                    okQty = productQty - ngQty;
                }

                masterObj.OkQty = okQty;
            }
        }

        private void MainView_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (!checkDataType.IsNullOrEmpty())
                    //    {
                    //        if (!string.IsNullOrEmpty(checkDataType)) repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", checkDataType);
                    //        e.RepositoryItem = repositoryItemSpinEdit;
                    //    }
                    //    else
                    //    {
                    //        e.RepositoryItem = repositoryItemTextEdit;
                    //    }
                    //}
                    //else if(inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.RepositoryItem = repositoryItemGridLookUpEdit;
                    //}
                    //else
                    //{
                    //    e.RepositoryItem = repositoryItemTextEdit;
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        //육안검사 
                        e.RepositoryItem = repositoryItemGridLookUpEdit;
                    }
                    else if (checkDataType.IsNullOrEmpty())
                    {
                        e.RepositoryItem = repositoryItemTextEdit;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(checkDataType)) repositoryItemSpinEdit.Mask.EditMask = string.Format("N{0}", checkDataType);
                        e.RepositoryItem = repositoryItemSpinEdit;
                    }
                }
            }
        }

        private void MasterMainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName == "CheckResult")
                {
                    var judgeValue = view.GetRowCellValue(e.RowHandle, "CheckResult").GetNullToEmpty();
                    if (judgeValue == "NG")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void DetailMainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (checkDataType.IsNullOrEmpty())
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    }
                    //    else
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                    //    }

                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}
                    //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    //    var readingValue = e.CellValue.GetNullToEmpty();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    //}
                    //else
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        var readingValue = e.CellValue.GetNullToEmpty();
                        e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    }
                    else
                    {
                        //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                        var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                        var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                        var readingValue = e.CellValue.GetDecimalNullToNull();
                        e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    }
                }
                else if (e.Column.FieldName == "Judge")
                {
                    var judgeValue = view.GetRowCellValue(e.RowHandle, "Judge").GetNullToEmpty();
                    if (judgeValue == "NG")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void SubMainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
                    //var inspectionWay = view.GetRowCellValue(e.RowHandle, "CheckWay").GetNullToEmpty();
                    //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                    //{
                    //    //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                    //    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    //    if (checkDataType.IsNullOrEmpty())
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    }
                    //    else
                    //    {
                    //        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                    //    }

                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}
                    //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    //    var readingValue = e.CellValue.GetNullToEmpty();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    //}
                    //else
                    //{
                    //    e.Appearance.TextOptions.HAlignment = HorzAlignment.Default;
                    //    var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                    //    var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                    //    var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                    //    var readingValue = e.CellValue.GetDecimalNullToNull();
                    //    e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    //}

                    var checkDataType = view.GetRowCellValue(e.RowHandle, "CheckDataType").GetNullToEmpty();
                    if (checkDataType == MasterCodeSTR.CheckDataType_C)
                    {
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                        var readingValue = e.CellValue.GetNullToEmpty();
                        e.Appearance.ForeColor = DetailCheckInputColor(readingValue);
                    }
                    else
                    {
                        //검사방법이 치수검사일 때 검사데이터타입이 없을 경우 
                        e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
                        var checkSpec = view.GetRowCellValue(e.RowHandle, "CheckSpec").GetDecimalNullToNull();
                        var checkUpQuad = view.GetRowCellValue(e.RowHandle, "CheckUpQuad").GetDecimalNullToZero();
                        var checkDownQuad = view.GetRowCellValue(e.RowHandle, "CheckDownQuad").GetDecimalNullToZero();
                        var readingValue = e.CellValue.GetDecimalNullToNull();
                        e.Appearance.ForeColor = DetailCheckInputColor(checkSpec, checkUpQuad, checkDownQuad, readingValue);
                    }
                }
                else if (e.Column.FieldName == "Judge")
                {
                    var judgeValue = view.GetRowCellValue(e.RowHandle, "Judge").GetNullToEmpty();
                    if (judgeValue == "NG")
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            var masterObj = MasterGridBindingSource.Current as TN_QCT1100;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_QCT1101;
            if (detailObj == null) return;

            detailObj.EditRowFlag = "Y";

            if (e.Column.FieldName.Contains("Reading"))
            {
                //var inspectionWay = detailObj.CheckWay.GetNullToEmpty();
                //if (inspectionWay == MasterCodeSTR.InspectionWay_Input) 
                //{
                //    DetailCheckInput(detailObj);
                //}
                //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                //{
                //    DetailCheckEye(detailObj);
                //}
                //else
                //{
                //    DetailCheckInput(detailObj);
                //}

                var checkDataType = detailObj.CheckDataType.GetNullToEmpty();
                if (checkDataType == MasterCodeSTR.CheckDataType_C)
                {
                    //육안검사 
                    DetailCheckEye(detailObj);
                }
                else if (checkDataType.IsNullOrEmpty())
                {
                    DetailCheckInput(detailObj);
                }
                else
                {
                    DetailCheckInput(detailObj);
                }

                MasterCheckResult(masterObj);
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            var detailObj = DetailGridBindingSource.Current as TN_QCT1101;
            if (detailObj == null) return;

            var maxReading = detailObj.TN_QCT1001.MaxReading.GetNullToEmpty();
            if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            {
                if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading.GetDecimalNullToNull())
                    e.Cancel = true;
            }
        }

        /// <summary> 성적서출력 </summary>
        private void BarButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (DetailGridBindingSource == null) return;

                MasterGridExControl.MainGrid.PostEditor();
                DetailGridExControl.MainGrid.PostEditor();

                WaitHandler.ShowWait();

                var masterList = MasterGridBindingSource.List as List<TN_QCT1100>;
                var printList = masterList.Where(p => p._Check == "Y").ToList();
                if (printList.Count == 0) return;

                var mainReport = new REPORT.XRQCT1005();
                foreach (var v in printList.OrderByDescending(p => p.InspNo).ToList())
                {
                    if (v.TN_QCT1101List.Count > 0)
                    {
                        var report = new REPORT.XRQCT1005(v);
                        report.CreateDocument();
                        mainReport.Pages.AddRange(report.Pages);
                    }

                    v._Check = "N";
                }

                if (mainReport.Pages.Count > 0)
                {
                    MasterGridExControl.BestFitColumns();
                    mainReport.PrintingSystem.ShowMarginsWarning = false;
                    mainReport.ShowPrintStatusDialog = false;
                    mainReport.ShowPreview();
                }
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {
                var masterList = MasterGridBindingSource.List as List<TN_QCT1100>;

                foreach (var mstObj in masterList)
                {
                    foreach(var v in mstObj.TN_QCT1101List.Where(p => p.InspSeq == -1).ToList())
                    {
                        mstObj.TN_QCT1101List.Remove(v);
                    }
                }

                WaitHandler.CloseWait();
            }

            MasterGridExControl.MainGrid.BestFitColumns();
        }

        private void DetailCheckInput(TN_QCT1101 detailObj)
        {
            var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null) return;
            var checkUpQuad = detailObj.CheckUpQuad.GetDecimalNullToZero();
            var checkDownQuad = detailObj.CheckDownQuad.GetDecimalNullToZero();
            var checkUp = checkSpec + checkUpQuad;
            var checkDown = checkSpec - checkDownQuad;
            //var checkUp = checkUpQuad;
            //var checkDown = checkDownQuad;

            int NgQty = 0;
            int OkQty = 0;

            var reading1 = detailObj.Reading1.GetDecimalNullToNull();
            var reading2 = detailObj.Reading2.GetDecimalNullToNull();
            var reading3 = detailObj.Reading3.GetDecimalNullToNull();
            var reading4 = detailObj.Reading4.GetDecimalNullToNull();
            var reading5 = detailObj.Reading5.GetDecimalNullToNull();
            var reading6 = detailObj.Reading6.GetDecimalNullToNull();
            var reading7 = detailObj.Reading7.GetDecimalNullToNull();
            var reading8 = detailObj.Reading8.GetDecimalNullToNull();
            var reading9 = detailObj.Reading9.GetDecimalNullToNull();

            if (reading1 != null)
            {
                if (reading1 >= checkDown && reading1 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 >= checkDown && reading2 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 >= checkDown && reading3 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 >= checkDown && reading4 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 >= checkDown && reading5 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 >= checkDown && reading6 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 >= checkDown && reading7 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 >= checkDown && reading8 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 >= checkDown && reading9 <= checkUp)
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                detailObj.Judge = null;
            }
            else if (NgQty > 0)
            {
                detailObj.Judge = "NG";
            }
            else
            {
                detailObj.Judge = "OK";
            }

            DetailGridExControl.BestFitColumns();
        }

        private void DetailCheckEye(TN_QCT1101 detailObj)
        {
            int NgQty = 0;
            int OkQty = 0;

            var reading1 = detailObj.Reading1.GetNullToNull();
            var reading2 = detailObj.Reading2.GetNullToNull();
            var reading3 = detailObj.Reading3.GetNullToNull();
            var reading4 = detailObj.Reading4.GetNullToNull();
            var reading5 = detailObj.Reading5.GetNullToNull();
            var reading6 = detailObj.Reading6.GetNullToNull();
            var reading7 = detailObj.Reading7.GetNullToNull();
            var reading8 = detailObj.Reading8.GetNullToNull();
            var reading9 = detailObj.Reading9.GetNullToNull();

            if (reading1 != null)
            {
                if (reading1 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading2 != null)
            {
                if (reading2 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading3 != null)
            {
                if (reading3 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading4 != null)
            {
                if (reading4 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading5 != null)
            {
                if (reading5 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading6 != null)
            {
                if (reading6 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading7 != null)
            {
                if (reading7 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading8 != null)
            {
                if (reading8 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (reading9 != null)
            {
                if (reading9 == "OK")
                    OkQty++;
                else
                    NgQty++;
            }

            if (NgQty == 0 && OkQty == 0)
            {
                detailObj.Judge = null;
            }
            else if (NgQty > 0)
            {
                detailObj.Judge = "NG";
            }
            else
            {
                detailObj.Judge = "OK";
            }

            DetailGridExControl.BestFitColumns();
        }

        private void MasterCheckResult(TN_QCT1100 masterObj)
        {
            if (masterObj.TN_QCT1101List.Any(p => p.Judge == "NG"))
                masterObj.CheckResult = "NG";
            else if (masterObj.TN_QCT1101List.Any(p => p.Judge == "OK"))
                masterObj.CheckResult = "OK";
            else
                masterObj.CheckResult = null;
            MasterGridExControl.BestFitColumns();
        }

        private Color DetailCheckInputColor(decimal? checkSpec, decimal checkUpQuad, decimal checkDownQuad, decimal? readingValue)
        {
            //var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null)
                return Color.Black;
            else
            {
                //var checkUpQuad = detailObj.CheckUpQuad.GetDecimalNullToZero();
                //var checkDownQuad = detailObj.CheckDownQuad.GetDecimalNullToZero();
                var checkUp = checkSpec + checkUpQuad;
                var checkDown = checkSpec - checkDownQuad;
                //var checkUp = checkUpQuad;
                //var checkDown = checkDownQuad;

                if (readingValue != null)
                {
                    if (readingValue >= checkDown && readingValue <= checkUp)
                        return Color.Black;
                    else
                        return Color.Red;
                }
                else
                    return Color.Black;
            }
        }

        private Color DetailCheckInputColor(string readingValue)
        {
            if (!readingValue.IsNullOrEmpty())
            {
                if (readingValue == "OK")
                    return Color.Black;
                else
                    return Color.Red;
            }
            else
                return Color.Black;
        }
        
        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            var view = sender as GridView;
            if (e.KeyCode == Keys.Enter)
            {
                if (view.FocusedColumn.FieldName.Contains("Reading") && !view.FocusedColumn.FieldName.Contains("MaxReading"))
                {
                    var maxReading = view.GetFocusedRowCellValue("TN_QCT1001.MaxReading").GetNullToEmpty();
                    if (view.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() == maxReading.GetDecimalNullToNull())
                    {
                        if (view.RowCount == view.FocusedRowHandle + 1)
                        {
                            view.FocusedRowHandle = 0;
                        }
                        else
                        {
                            view.FocusedRowHandle = view.FocusedRowHandle + 1;
                        }

                        view.FocusedColumn = view.Columns["Reading1"];
                    }
                    else
                    {
                        if (view.FocusedColumn.VisibleIndex + 1 == view.VisibleColumns.Count)
                        {
                            view.FocusedColumn = view.VisibleColumns[0];
                            if (view.RowCount == view.FocusedRowHandle + 1)
                                view.FocusedRowHandle = 0;
                            else
                                view.FocusedRowHandle = view.FocusedRowHandle + 1;
                        }
                        else
                        {
                            view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                        }
                    }
                }
                else
                {
                    if (view.FocusedColumn.VisibleIndex + 1 == view.VisibleColumns.Count)
                    {
                        view.FocusedColumn = view.VisibleColumns[0];
                        if (view.RowCount == view.FocusedRowHandle + 1)
                            view.FocusedRowHandle = 0;
                        else
                            view.FocusedRowHandle = view.FocusedRowHandle + 1;
                    }
                    else
                    {
                        view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex + 1];
                    }
                }
            }
        }

    }
}