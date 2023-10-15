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
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.QCT
{
    /// <summary>
    /// 출하검사(성적서)관리
    /// </summary>
    public partial class XFQCT1105 : Service.Base.ListMasterDetailSubDetailFormTemplate
    {
        IService<TN_QCT1500> ModelService = (IService<TN_QCT1500>)ProductionFactory.GetDomainService("TN_QCT1500");

        string reportType;

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        public XFQCT1105()
        {
            InitializeComponent();

            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            SubDetailGridExControl = gridEx3;
            
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MasterMainView_RowCellStyle;
            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.RowCellStyle += DetailMainView_RowCellStyle;
            DetailGridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.KeyDown += MainView_KeyDown;

            SubDetailGridExControl.MainGrid.MainView.RowCellStyle += SubMainView_RowCellStyle;

            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
                , DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
            };
            repositoryItemGridLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AutoPopulateColumns = false;
            repositoryItemGridLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            repositoryItemGridLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            repositoryItemGridLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
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
            dt_CheckDate.SetTodayIsMonth();
        }
        
        bool picZoomFlag = false;
        float zoomPer = 10;
        private void Pic_DesignFileName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.ControlKey)
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
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());

            lcDesign.Expanded = false;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"));
            //MasterGridExControl.MainGrid.AddColumn("FinalInspNo", LabelConvert.GetLabelText("FinalInspNo"));
            MasterGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.AQL", LabelConvert.GetLabelText("AQL"));
            //MasterGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"));
            MasterGridExControl.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"));
            MasterGridExControl.MainGrid.AddColumn("CheckResult", LabelConvert.GetLabelText("CheckResult"));
            MasterGridExControl.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId2"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn("PrintDate", LabelConvert.GetLabelText("PrintDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "CheckDate", "CheckId", "CheckResult", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1500>(MasterGridExControl);

            RepositoryItemSearchLookUpEdit searchLookUpEdit = new RepositoryItemSearchLookUpEdit()
            {
                ValueMember = "CodeVal",
                DisplayMember = "CodeName", //DataConvert.GetCultureDataFieldName("CodeName"),
                DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReportType).Select(p => new { p.CodeVal, p.CodeName }).Where(x => x.CodeVal != null).ToList()
            };
            searchLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            searchLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            searchLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            searchLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            searchLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            //var barSearchLookUpEditReprotType = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, searchLookUpEdit);
            //barSearchLookUpEditReprotType.Id = 5;
            //barSearchLookUpEditReprotType.Name = "barSearchLookUpEditReportType";
            //barSearchLookUpEditReprotType.Edit.NullText = LabelConvert.GetLabelText("InspectionReportType");
            //barSearchLookUpEditReprotType.EditWidth = 130;
            //barSearchLookUpEditReprotType.Alignment = BarItemLinkAlignment.Right;
            //barSearchLookUpEditReprotType.EditValueChanged += BarSearchLookUpEditReprotType_EditValueChanged;
            //MasterGridExControl.BarTools.AddItem(barSearchLookUpEditReprotType);


            //var barButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            //barButtonPrint.Id = 6;
            //barButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            //barButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.P));
            //barButtonPrint.Name = "barButtonPrint";
            //barButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //barButtonPrint.ShortcutKeyDisplayString = "Alt+P";
            //barButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            //barButtonPrint.Caption = LabelConvert.GetLabelText("InspectionReportPrint") + "[Alt+P]";
            //barButtonPrint.ItemClick += BarButtonPrint_ItemClick;
            //barButtonPrint.Alignment = BarItemLinkAlignment.Right;
            //MasterGridExControl.BarTools.AddItem(barButtonPrint);

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

            DetailGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"), false);
            DetailGridExControl.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            DetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            DetailGridExControl.MainGrid.AddColumn("TN_QCT1001.InstrumentCode", LabelConvert.GetLabelText("InstrumentCode"));
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
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_QCT1501>(DetailGridExControl);

            SubDetailGridExControl.SetToolbarVisible(false);
            SubDetailGridExControl.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"));
            SubDetailGridExControl.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            //SubDetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            SubDetailGridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            SubDetailGridExControl.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InstrumentCode"));
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
        }

        protected override void InitRepository()
        {
            //MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty");
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDivision", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDivision), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckPoint", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CheckPoint), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("TN_STD1100.AQL", DbRequestHandler.GetCommTopCode(MasterCodeSTR.AQL), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("CheckDate");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemGridLookUpEdit("CheckResult", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo", UserRight.HasEdit);

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_QCT1001.InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InstrumentCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_QCT1001.MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InstrumentCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            SubDetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo");
            SubDetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SubDetailGridExControl.BestFitColumns();
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

            InitRepository();
            InitCombo();

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            var printDate = dt_printDt.DateTime.GetNullToDateTime();

            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.CheckDate >= dt_CheckDate.DateFrEdit.DateTime && p.CheckDate <= dt_CheckDate.DateToEdit.DateTime)
                                                                            && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                            && (printDate == null ? true : p.PrintDate == printDate)
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
            pic_DesignFileName.EditValue = null;
            pdf_Design.CloseDocument();

            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null)
            {
                return;
            }
            
            /*
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
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(documentContent);
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
            */

            DetailGridBindingSource.DataSource = masterObj.TN_QCT1501List.OrderBy(o => o.InspSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
            
        }

        protected override void DetailFocusedRowChanged()
        {
            var detailObj = DetailGridBindingSource.Current as TN_QCT1501;
            if (detailObj == null)
            {
                SubDetailGridExControl.MainGrid.Clear();
                return;
            }

            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null)
                return;

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _ItemCode = new SqlParameter("@ItemCode", detailObj.ItemCode);
                var _CheckList = new SqlParameter("@CheckList", detailObj.CheckList);
                var result = context.Database.SqlQuery<TEMP_XFQCT1100_SUB>("USP_GET_INSP_SHIPMENT_CHECK_LIST_RECORD @ItemCode, @CheckList", _ItemCode, _CheckList).ToList();
                SubDetailGridBindingSource.DataSource = result.ToList();
            }

            SubDetailGridExControl.DataSource = SubDetailGridBindingSource;
            SubDetailGridExControl.BestFitColumns();
        }

        protected override void AddRowClicked()
        {
            var constraintList = MasterGridBindingSource.List as List<TN_QCT1500>;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.IsMultiSelect, true);
            param.SetValue(PopupParameter.Constraint, constraintList);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_QCT_SHIPMENT, param, MasterAddCallBack);
            form.ShowPopup(true);
        }

        private void MasterAddCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<VI_INSP_SHIPMENT_OBJECT>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList.ToList())
            {
                var newObj = new TN_QCT1500()
                {
                    InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_Shipment),
                    ItemCode = v.ItemCode,
                    ProductLotNo = v.ProductLotNo,
                    CustomerCode = v.CustomerCode,
                    Temp1 = v.ItemMoveNo,
                    CheckDate = DateTime.Today,
                    CheckId = GlobalVariable.LoginId,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).First()
                };

                MasterGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);
                IsFormControlChanged = true;
            }

            MasterGridExControl.BestFitColumns();
        }

        private void MasterAddCallBack_old(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var returnList = (List<VI_INSP_SHIPMENT_OBJECT>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var v in returnList.ToList())
            {
                var newObj = new TN_QCT1500()
                {
                    InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_Shipment),
                    //FinalInspNo = v.FinalInspNo,
                    ItemCode = v.ItemCode,
                    ProductLotNo = v.ProductLotNo,
                    CustomerCode = v.CustomerCode,
                    Temp1 = v.ItemMoveNo,
                    CheckDate = DateTime.Today,
                    CheckId = GlobalVariable.LoginId,
                    TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).First()
                };

                MasterGridBindingSource.Add(newObj);
                ModelService.Insert(newObj);

                
                ////var finalDetailInspectionList = ModelService.GetChildList<TN_QCT1101>(p => p.InspNo == v.FinalInspNo).ToList();
                //foreach (var c in finalDetailInspectionList)
                //{
                //    var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == v.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
                //    if (qcRev == null) break;

                //    var qcList = qcRev.TN_QCT1001List.Where(p => p.UseFlag == "Y" && p.CheckDivision == MasterCodeSTR.InspectionDivision_Shipment).OrderBy(p => p.DisplayOrder).ToList();
                //    foreach (var x in qcList)
                //    {
                //        if (x.CheckList == c.CheckList)
                //        {
                //            var newObj2 = new TN_QCT1501()
                //            {
                //                InspNo = newObj.InspNo,
                //                InspSeq = newObj.TN_QCT1501List.Count == 0 ? 1 : newObj.TN_QCT1501List.Max(o => o.InspSeq) + 1,
                //                RevNo = x.RevNo,
                //                ItemCode = newObj.ItemCode,
                //                Seq = x.Seq,
                //                CheckWay = x.CheckWay,
                //                CheckList = x.CheckList,
                //                CheckMax = x.CheckMax,
                //                CheckMin = x.CheckMin,
                //                CheckSpec = x.CheckSpec,
                //                CheckUpQuad = x.CheckUpQuad,
                //                CheckDownQuad = x.CheckDownQuad,
                //                CheckDataType = x.CheckDataType,
                //                Reading1 = c.Reading1,
                //                Reading2 = c.Reading2,
                //                Reading3 = c.Reading3,
                //                Reading4 = c.Reading4,
                //                Reading5 = c.Reading5,
                //                Reading6 = c.Reading6,
                //                Reading7 = c.Reading7,
                //                Reading8 = c.Reading8,
                //                Reading9 = c.Reading9,
                //                Judge = c.Judge,
                //                TN_QCT1001 = x,
                //            };

                //            if (newObj2.CheckWay == MasterCodeSTR.InspectionWay_Input)
                //            {
                //                DetailCheckInput(newObj2);
                //            }
                //            else if (newObj2.CheckWay == MasterCodeSTR.InspectionWay_Eye)
                //            {
                //                DetailCheckEye(newObj2);
                //            }
                //            else
                //            {
                //                DetailCheckInput(newObj2);
                //            }

                //            DetailGridBindingSource.Add(newObj2);
                //            newObj.TN_QCT1501List.Add(newObj2);
                //        }
                //    }
                //    DetailGridExControl.BestFitColumns();
                //}
                //MasterCheckResult(newObj);

                IsFormControlChanged = true;
            }

            MasterGridExControl.BestFitColumns();
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null) return;

            if (masterObj.TN_QCT1501List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_51), LabelConvert.GetLabelText("InspectionDetailInfo")));
                return;
            }

            MasterGridBindingSource.Remove(masterObj);
            ModelService.Delete(masterObj);
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null) return;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == masterObj.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_172));
                return;
            }

            var qcList = qcRev.TN_QCT1001List.Where(p=>p.UseFlag == "Y" && p.CheckDivision == MasterCodeSTR.InspectionDivision_Shipment).OrderBy(p => p.DisplayOrder).ToList();

            foreach (var v in qcList)
            {
                //if (masterObj.TN_QCT1501List.Any(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq))
                //    continue;

                var newObj = new TN_QCT1501()
                {
                    InspNo = masterObj.InspNo,
                    InspSeq = masterObj.TN_QCT1501List.Count == 0 ? 1 : masterObj.TN_QCT1501List.Max(o => o.InspSeq) + 1,
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
                };
                DetailGridBindingSource.Add(newObj);
                masterObj.TN_QCT1501List.Add(newObj);
            }
            DetailGridExControl.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_QCT1501;
            if (detailObj == null) return;

            masterObj.TN_QCT1501List.Remove(detailObj);
            DetailGridBindingSource.Remove(detailObj);
            DetailGridExControl.BestFitColumns();

            MasterCheckResult(masterObj);
        }

        /// <summary>
        /// 난수입력
        /// </summary>
        protected override void DetailFileChooseClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_QCT1501;
            if (detailObj == null) return;

            DetailGridExControl.MainGrid.PostEditor();

            if (detailObj.CheckDataType == MasterCodeSTR.CheckDataType_C)
            {
                for (int i = 3; i < 10; i++)
                {
                    DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("Reading" + i.ToString(), "OK");
                    if (!detailObj.TN_QCT1001.MaxReading.IsNullOrEmpty() && detailObj.TN_QCT1001.MaxReading.GetIntNullToZero() == i)
                        break;
                }
            }
            else
            {
                if (detailObj.Reading1.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_96));
                    return;
                }

                if (detailObj.Reading2.IsNullOrEmpty())
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_97));
                    return;
                }

                //if (detailObj.Reading1.GetDecimalNullToZero() < detailObj.Reading2.GetDecimalNullToZero())
                //{
                //    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_98));
                //    return;
                //}

                decimal minValue = 0;
                decimal maxValue = 0;

                var reading1 = detailObj.Reading1.GetDecimalNullToZero();
                var reading2 = detailObj.Reading2.GetDecimalNullToZero();

                if (reading1 < reading2)
                {
                    minValue = reading1;
                    maxValue = reading2;
                }
                else if (reading1 > reading2)
                {
                    minValue = reading2;
                    maxValue = reading1;
                }
                else
                {
                    minValue = reading1;
                    maxValue = reading2;
                }

                Random random = new Random();
                //var minValue = detailObj.Reading2.GetDecimalNullToZero();
                //var maxValue = detailObj.Reading1.GetDecimalNullToZero();
                for (int i = 3; i < 10; i++)
                {
                    var randomValue = random.NextDouble().GetDecimalNullToZero() * (maxValue - minValue) + minValue;
                    DetailGridExControl.MainGrid.MainView.SetFocusedRowCellValue("Reading" + i.ToString(), Math.Round(randomValue, detailObj.CheckDataType.GetIntNullToZero()));
                    if (!detailObj.TN_QCT1001.MaxReading.IsNullOrEmpty() && detailObj.TN_QCT1001.MaxReading.GetIntNullToZero() == i)
                        break;
                }
            }
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            ModelService.Save();
            DataLoad();
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

            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null) return;
            var detailObj = DetailGridBindingSource.Current as TN_QCT1501;
            if (detailObj == null) return;

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

            var detailObj = DetailGridBindingSource.Current as TN_QCT1501;
            if (detailObj == null) return;

            var maxReading = detailObj.TN_QCT1001.MaxReading.GetNullToEmpty();
            if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            {
                if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading.GetDecimalNullToNull())
                    e.Cancel = true;
            }
        }

        /// <summary> 성적서타입 </summary>
        private void BarSearchLookUpEditReprotType_EditValueChanged(object sender, EventArgs e)
        {
            BarEditItem lookup = sender as BarEditItem;

            if (lookup == null) return;

            if (!lookup.IsNullOrEmpty())
            {
                reportType = lookup.EditValue.GetNullToEmpty();
                Console.WriteLine(reportType);
            }
        }

        /// <summary> 성적서출력 </summary>
        private void BarButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null) return;

            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            //WaitHandler.ShowWait();


            var masterObj = MasterGridBindingSource.Current as TN_QCT1500;
            if (masterObj == null) return;

            var listCount = masterObj.TN_QCT1501List.Count;
            if (listCount == 0) return;

            if (reportType.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_84), LabelConvert.GetLabelText("InspectionReportType")));
                return;
            }

            ReportCreateToPrint(masterObj, reportType);

            //출력일 update
            masterObj.PrintDate = DateTime.Today;
            ModelService.Update(masterObj);
            ModelService.Save();

            ActRefresh();
        }

        private void ReportCreateToPrint(TN_QCT1500 masterObj, string reportType)
        {
            try
            {
                WaitHandler.ShowWait();

                if (reportType == "01")
                {
                    var report = new REPORT.XRQCT1000(masterObj);
                    report.CreateDocument();

                    report.PrintingSystem.ShowMarginsWarning = false;
                    report.ShowPrintStatusDialog = false;
                    report.ShowPreview();
                }
                else if (reportType == "02")
                {
                    var report = new REPORT.XRQCT1001(masterObj);
                    report.CreateDocument();

                    report.PrintingSystem.ShowMarginsWarning = false;
                    report.ShowPrintStatusDialog = false;
                    report.ShowPreview();
                }
                else if (reportType == "03")
                {
                    //var report = new REPORT.XRQCT1002(masterObj);
                    //report.CreateDocument();

                    //report.PrintingSystem.ShowMarginsWarning = false;
                    //report.ShowPrintStatusDialog = false;
                    //report.ShowPreview();
                    MessageBox.Show("주석처리");
                }

            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            finally
            {

                foreach (var v in masterObj.TN_QCT1501List.Where(p => p.InspSeq == -1).ToList())
                    masterObj.TN_QCT1501List.Remove(v);
                WaitHandler.CloseWait();
            }
        }

        private void DetailCheckInput(TN_QCT1501 detailObj)
        {
            var checkSpec = detailObj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null) return;
            var checkUpQuad = detailObj.CheckUpQuad.GetDecimalNullToZero();
            var checkDownQuad = detailObj.CheckDownQuad.GetDecimalNullToZero();
            var checkUp = checkSpec + checkUpQuad;
            var checkDown = checkSpec - checkDownQuad;

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

        private void DetailCheckEye(TN_QCT1501 detailObj)
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

        private void MasterCheckResult(TN_QCT1500 masterObj)
        {
            if (masterObj.TN_QCT1501List.Any(p => p.Judge == "NG"))
                masterObj.CheckResult = "NG";
            else if (masterObj.TN_QCT1501List.Any(p => p.Judge == "OK"))
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

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                var menu = ModelService.GetChildList<Model.Domain.Menu>(p => p.Screen.ClassName == "XFQCT1300").FirstOrDefault();
                if (menu == null) return;

                //WaitHandler waitHandler = new WaitHandler();
                try
                {
                    if (FormHelper.IsLoadedForm(string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId)))
                    {
                        FormHelper.GetParentForm(string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId)).Focus();
                        return;
                    }

                    WaitHandler.ShowWait();

                    XtraForm form = FormHelper.CreateForm(menu.Screen.Module.Assembly, menu.Screen.NameSpace, menu.Screen.ClassName);
                    if (form == null)
                    {
                        MessageBoxHandler.ErrorShow(new Exception(string.Format("Form creation error, null returned from FormHelper.CreateForm{0}{1}Assembly : {2}{3}NameSpace: {4}{5}Class : {6}",
                                                                                Environment.NewLine, Environment.NewLine,
                                                                                menu.Screen.Module.Assembly, Environment.NewLine,
                                                                                menu.Screen.NameSpace, Environment.NewLine,
                                                                                menu.Screen.ClassName)));
                        return;
                    }

                    form.Name = string.Format("{0}.{1}.{2}", menu.Screen.NameSpace, menu.Screen.ClassName, menu.MenuId);
                    form.Text = GlobalVariable.Culture.Equals(GlobalVariable.DefaultCulture) ? menu.MenuName : menu.MenuName2;
                    ((Utils.Interface.Forms.IBaseForm)form).UserRight = MenuFactory.GetUserRight(menu.MenuId, GlobalVariable.UserId);
                    ((Utils.Interface.Forms.IBaseForm)form).FormMenu = menu;
                    ((Utils.Interface.Forms.IBaseForm)form).MdiTabImage = IconImageList.GetIconImage(menu.IconIndex.GetIntNullToZero());
                    ((Utils.Interface.Forms.IBaseForm)form).isFormOpen = true;
                    form.MdiParent = this.MdiParent;
                    form.Show();
                    ((Utils.Interface.Forms.IBaseForm)form).isFormOpen = false;
                }
                catch (Exception ex)
                {
                    HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
                }
                finally
                {
                    WaitHandler.CloseWait();
                }
            }
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