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
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Ui.Model.Domain.VIEW;
using DevExpress.XtraEditors.Controls;
using HKInc.Ui.Model.Domain;
using DevExpress.Utils;
using HKInc.Service.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Mask;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 최종검사 POP 팝업 창
    /// </summary>
    public partial class XPFINSPECTION_FINAL : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1001> ModelService = (IService<TN_QCT1001>)ProductionFactory.GetDomainService("TN_QCT1001");
        IService<TN_QCT1100> QcModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        BindingSource gridEx3BindingSource = new BindingSource();

        TN_MPS1201 MasterObj;
        TEMP_XFPOP_INSP_FINAL TEMP_XFPOP_INSP_FINAL;
        string ProductLotNo;
        string ItemMoveNo;

        RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit;
        RepositoryItemSpinEdit repositoryItemSpinEdit;
        RepositoryItemTextEdit repositoryItemTextEdit;

        public XPFINSPECTION_FINAL(TEMP_XFPOP_INSP_FINAL obj, string productLotNo, string itemMoveNo)
        {
            InitializeComponent();

            this.Text = "최종검사";
            
            GridExControl = gridEx1;

            TEMP_XFPOP_INSP_FINAL = obj;
            ProductLotNo = productLotNo;
            ItemMoveNo = itemMoveNo;

            GridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            GridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
            GridExControl.MainGrid.MainView.CustomRowCellEditForEditing += MainView_CustomRowCellEditForEditing;
            GridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            GridExControl.MainGrid.MainView.KeyDown += MainView_KeyDown;

            gridEx3.MainGrid.MainView.RowCellStyle += SubMainView_RowCellStyle;
            //tx_Reading.KeyDown += Tx_Reading_KeyDown;
            //tx_Reading.GotFocus += Tx_Reading_GotFocus;

            //lup_ReadingNumber.EditValueChanged += Lup_ReadingNumber_EditValueChanged;

            //btn_Apply.Click += Btn_Apply_Click;

            this.WindowState = FormWindowState.Maximized;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Save, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            layoutControlGroup2.Expanded = false;

            //lup_Eye.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), TextEditStyles.DisableTextEditor);
            //lup_ReadingNumber.SetDefault(false, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), TextEditStyles.DisableTextEditor);
            //lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p=>p.Active=="Y").ToList(), TextEditStyles.DisableTextEditor);
            var procTeamCode = TEMP_XFPOP_INSP_FINAL.ProcTeamCode.GetNullToNull();
            lup_CheckId.SetDefault(false, "LoginId", "UserName", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            lup_CheckId.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_ReadingNumber.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            //lup_Eye.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            //btn_Apply.Text = LabelConvert.GetLabelText("Apply") + "(&Y)";

            lup_CheckId.EditValue = GlobalVariable.LoginId;
            lup_CheckId.ReadOnly = true;

            repositoryItemGridLookUpEdit = new RepositoryItemGridLookUpEdit()
            {
                ValueMember = "CodeVal"
                ,
                DisplayMember = DataConvert.GetCultureDataFieldName("CodeName")
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
            pic_DesignFileName.MouseWheel += Pic_DesignFileName_MouseWheel;
            pic_DesignFileName.Properties.ShowScrollBars = true;
            pic_DesignFileName.Properties.AllowScrollViaMouseDrag = true;
            pic_DesignFileName.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pic_DesignFileName.Dock = System.Windows.Forms.DockStyle.Fill;

            pdf_Design.Dock = System.Windows.Forms.DockStyle.Fill;
            pdf_Design.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;
            pdf_Design.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;

            pic_WorkStandardDocument.KeyDown += Pic_DesignFileName_KeyDown;
            pic_WorkStandardDocument.KeyUp += Pic_DesignFileName_KeyUp;
            pic_WorkStandardDocument.MouseWheel += Pic_DesignFileName_MouseWheel;
            pic_WorkStandardDocument.Properties.ShowScrollBars = true;
            pic_WorkStandardDocument.Properties.AllowScrollViaMouseDrag = true;
            pic_WorkStandardDocument.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;

            pic_ItemLimitImage.KeyDown += Pic_DesignFileName_KeyDown;
            pic_ItemLimitImage.KeyUp += Pic_DesignFileName_KeyUp;
            pic_ItemLimitImage.MouseWheel += Pic_DesignFileName_MouseWheel;
            pic_ItemLimitImage.Properties.ShowScrollBars = true;
            pic_ItemLimitImage.Properties.AllowScrollViaMouseDrag = true;
            pic_ItemLimitImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;

            //품목 도면
            if (!TEMP_XFPOP_INSP_FINAL.DesignFileUrl.IsNullOrEmpty())
            {
                var fileName = TEMP_XFPOP_INSP_FINAL.DesignFileName;
                int fileExtPos = fileName.LastIndexOf(".");
                string extName = string.Empty;
                if (fileExtPos >= 0)
                    extName = fileName.Substring(fileExtPos + 1, fileName.Length - fileExtPos - 1);

                if (extName.ToLower() == "pdf")
                {
                    pic_DesignFileName.Visible = false;
                    pic_DesignFileName.EditValue = null;
                    pdf_Design.Visible = true;

                    byte[] documentContent = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TEMP_XFPOP_INSP_FINAL.DesignFileUrl);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(documentContent);
                    pdf_Design.LoadDocument(ms);
                }
                else
                {
                    pic_DesignFileName.Visible = true;
                    pdf_Design.Visible = false;

                    pic_DesignFileName.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TEMP_XFPOP_INSP_FINAL.DesignFileUrl);
                    pdf_Design.CloseDocument();
                }
            }
            else
            {
                pic_DesignFileName.Visible = true;
                pdf_Design.Visible = false;
                pic_DesignFileName.EditValue = null;
                pdf_Design.CloseDocument();
            }

            //작업표준서
            if (!TEMP_XFPOP_INSP_FINAL.WorkStandardDocumentUrl.IsNullOrEmpty())
            {
                var fileName = TEMP_XFPOP_INSP_FINAL.WorkStandardDocument;
                pic_WorkStandardDocument.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TEMP_XFPOP_INSP_FINAL.WorkStandardDocumentUrl);
            }

            if (TEMP_XFPOP_INSP_FINAL.ProcessSeq > 1)
            {
                //현 이동표번호 가져오기
                var currentItemMoveNo = TEMP_XFPOP_INSP_FINAL.ItemMoveNo;
                var previousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.ItemMoveNo == currentItemMoveNo
                                                                                    && p.WorkNo == TEMP_XFPOP_INSP_FINAL.WorkNo
                                                                                    && p.ProcessSeq == 1).FirstOrDefault();
                if (previousItemMoveObj != null)
                {
                    spin_ResultQty.EditValue = previousItemMoveObj.OkQty.GetDecimalNullToZero();
                }
            }

            spin_ResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_ResultQty.Properties.Mask.EditMask = "n0";
            spin_ResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_ResultQty.Properties.Buttons[0].Visible = false;

            spin_BadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_BadQty.Properties.Mask.EditMask = "n0";
            spin_BadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_BadQty.Properties.Buttons[0].Visible = false;

            spin_SumResultQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumResultQty.Properties.Mask.EditMask = "n0";
            spin_SumResultQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumResultQty.Properties.Buttons[0].Visible = false;

            spin_SumOkQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumOkQty.Properties.Mask.EditMask = "n0";
            spin_SumOkQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumOkQty.Properties.Buttons[0].Visible = false;

            spin_SumBadQty.Properties.Mask.MaskType = MaskType.Numeric;
            spin_SumBadQty.Properties.Mask.EditMask = "n0";
            spin_SumBadQty.Properties.Mask.UseMaskAsDisplayFormat = true;
            spin_SumBadQty.Properties.Buttons[0].Visible = false;

            MasterObj = QcModelService.GetChildList<TN_MPS1201>(p => p.WorkNo == TEMP_XFPOP_INSP_FINAL.WorkNo
                                                    && p.ProcessCode == TEMP_XFPOP_INSP_FINAL.ProcessCode
                                                    && p.ProcessSeq == TEMP_XFPOP_INSP_FINAL.ProcessSeq
                                                    && p.ProductLotNo == TEMP_XFPOP_INSP_FINAL.ProductLotNo
                                                )
                                                .FirstOrDefault();

            spin_SumResultQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.ResultSumQty).GetDecimalNullToZero();
            spin_SumOkQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.OkSumQty).GetDecimalNullToZero();
            spin_SumBadQty.EditValue = MasterObj.TN_MPS1200.TN_MPS1201List.Sum(p => p.BadSumQty).GetDecimalNullToZero();

            tx_ItemCode.EditValue = TEMP_XFPOP_INSP_FINAL.ItemCode;
            tx_ItemName.EditValue = TEMP_XFPOP_INSP_FINAL.ItemName;
            tx_ItemMoveNo.EditValue = TEMP_XFPOP_INSP_FINAL.ItemMoveNo;
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
            var picture = sender as PictureEdit;
            if (picZoomFlag)
            {
                var v = e.Delta;
                if (v < 0)
                    picture.Properties.ZoomPercent -= zoomPer;
                else
                    picture.Properties.ZoomPercent += zoomPer;
            }
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            //GridExControl.SetToolbarButtonVisible(false);
            //GridExControl.SetToolbarButtonVisible(GridToolbarButton.AddRow, true);
            //GridExControl.SetToolbarButtonCaption(GridToolbarButton.AddRow, LabelConvert.GetLabelText("Apply") + "[F3]", IconImageList.GetIconImage("actions/apply"));

            var checkRandomGroupId = (int)UserGroupEnum.CheckRandomGroup;
            var checkObj = ModelService.GetChildList<UserGroup>(p => p.UserGroupId == checkRandomGroupId && p.Active == "Y").FirstOrDefault();
            if (checkObj != null)
            {
                if (checkObj.UserUserGroupList.Where(p => p.User.LoginId == GlobalVariable.LoginId).Count() == 1)
                {
                    IsGridButtonFileChooseEnabled = true;
                    GridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ReadingRandomAdd"), IconImageList.GetIconImage("business%20objects/botask"));
                }
            }

            GridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;

            GridExControl.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            GridExControl.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"), false);
            GridExControl.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);           
            GridExControl.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), HorzAlignment.Center, false);
            GridExControl.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"), HorzAlignment.Center, true);
            GridExControl.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            GridExControl.MainGrid.SetEditable(true, "Reading1", "Reading2", "Reading3", "Reading4", "Reading5", "Reading6", "Reading7", "Reading8", "Reading9", "Memo");

            GridExControl.MainGrid.Columns["Reading1"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading2"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading3"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading4"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading5"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading6"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading7"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading8"].MinWidth = 60;
            GridExControl.MainGrid.Columns["Reading9"].MinWidth = 60;

            gridEx3.SetToolbarVisible(false);
            gridEx3.MainGrid.AddColumn("CheckDate", LabelConvert.GetLabelText("CheckDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx3.MainGrid.AddColumn("CheckId", LabelConvert.GetLabelText("CheckId"));
            gridEx3.MainGrid.AddColumn("InspNo", LabelConvert.GetLabelText("InspNo"));
            gridEx3.MainGrid.AddColumn("InspSeq", LabelConvert.GetLabelText("InspSeq"), HorzAlignment.Far, FormatType.Numeric, "n0", false);
            gridEx3.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            //SubDetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            gridEx3.MainGrid.AddColumn("CheckWay", LabelConvert.GetLabelText("InspectionWay"));
            gridEx3.MainGrid.AddColumn("CheckList", LabelConvert.GetLabelText("InspectionItem"));
            gridEx3.MainGrid.AddColumn("InstrumentCode", LabelConvert.GetLabelText("InstrumentCode"));
            gridEx3.MainGrid.AddColumn("CheckDataType", LabelConvert.GetLabelText("CheckDataType"));
            gridEx3.MainGrid.AddColumn("InspectionReportMemo", LabelConvert.GetLabelText("InspectionReportMemo"), HorzAlignment.Center, true);
            gridEx3.MainGrid.AddColumn("CheckMin", LabelConvert.GetLabelText("CheckMin"), HorzAlignment.Center, true);
            gridEx3.MainGrid.AddColumn("CheckMax", LabelConvert.GetLabelText("CheckMax"), HorzAlignment.Center, true);
            gridEx3.MainGrid.AddColumn("CheckSpec", LabelConvert.GetLabelText("CheckSpec"), false);
            gridEx3.MainGrid.AddColumn("CheckUpQuad", LabelConvert.GetLabelText("CheckUpQuad"), false);
            gridEx3.MainGrid.AddColumn("CheckDownQuad", LabelConvert.GetLabelText("CheckDownQuad"), false);
            gridEx3.MainGrid.AddColumn("MaxReading", LabelConvert.GetLabelText("MaxReading"));
            gridEx3.MainGrid.AddColumn("Reading1", LabelConvert.GetLabelText("Reading1"));
            gridEx3.MainGrid.AddColumn("Reading2", LabelConvert.GetLabelText("Reading2"));
            gridEx3.MainGrid.AddColumn("Reading3", LabelConvert.GetLabelText("Reading3"));
            gridEx3.MainGrid.AddColumn("Reading4", LabelConvert.GetLabelText("Reading4"));
            gridEx3.MainGrid.AddColumn("Reading5", LabelConvert.GetLabelText("Reading5"));
            gridEx3.MainGrid.AddColumn("Reading6", LabelConvert.GetLabelText("Reading6"));
            gridEx3.MainGrid.AddColumn("Reading7", LabelConvert.GetLabelText("Reading7"));
            gridEx3.MainGrid.AddColumn("Reading8", LabelConvert.GetLabelText("Reading8"));
            gridEx3.MainGrid.AddColumn("Reading9", LabelConvert.GetLabelText("Reading9"));
            gridEx3.MainGrid.AddColumn("Judge", LabelConvert.GetLabelText("Judge"));
            gridEx3.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckList", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionItem), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(GridExControl, "Memo", true);

            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckWay", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionWay), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("InstrumentCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InstrumentCode), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("CheckDataType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionDataType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("Judge", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Judge_OKNG), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            gridEx3.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(gridEx3, "Memo");
            gridEx3.MainGrid.SetRepositoryItemSearchLookUpEdit("MaxReading", DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReadingNumber), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            GridExControl.BestFitColumns();
            gridEx3.BestFitColumns();
        }

        protected override void GridRowDoubleClicked() { }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            IsFirstLoaded = true;
            //var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_INSP_FINAL.ItemCode).First();

            //if (TN_STD1100.ShipmentInspFlag != "Y")
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_67), LabelConvert.GetLabelText("ShipmentInspFlag")));
            //    ActClose();
            //    return;
            //}

            GridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            pic_ItemLimitImage.EditValue = null;

            lup_CheckId.EditValue = GlobalVariable.LoginId;
            //lup_ReadingNumber.EditValue = 1;
            //lup_Eye.EditValue = null;
            //tx_Reading.EditValue = null;

            var qcRev = ModelService.GetChildList<TN_QCT1000>(p => p.ItemCode == TEMP_XFPOP_INSP_FINAL.ItemCode && p.UseFlag == "Y").OrderBy(p => p.RowId).LastOrDefault();
            if (qcRev == null)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ActClose();
                return;
            }

            var qualityList = ModelService.GetList(p => p.RevNo == qcRev.RevNo
                                                        && p.ItemCode == TEMP_XFPOP_INSP_FINAL.ItemCode
                                                        && p.CheckDivision == MasterCodeSTR.InspectionDivision_Final
                                                        && (p.ProcessCode == TEMP_XFPOP_INSP_FINAL.ProcessCode || p.ProcessCode == null)
                                                        && p.UseFlag == "Y"
                                                    )
                                                    .OrderBy(p => p.DisplayOrder)
                                                    .ToList();
            if (qualityList.Count == 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspectionItem")));
                ActClose();
                return;
            }

            var TN_QCT1100_OldObj = QcModelService.GetChildList<TN_QCT1100>(p => p.WorkNo == TEMP_XFPOP_INSP_FINAL.WorkNo
                                                                                    && p.WorkSeq == TEMP_XFPOP_INSP_FINAL.ProcessSeq
                                                                                    && (p.ProcessCode == TEMP_XFPOP_INSP_FINAL.ProcessCode || p.ProcessCode == null)
                                                                                    && p.CheckDivision == MasterCodeSTR.InspectionDivision_Final
                                                                                    //&& p.Temp1 == ItemMoveNo
                                                                                    ).OrderBy(p => p.RowId).LastOrDefault();
            if (TN_QCT1100_OldObj != null)
            {
                var oldList = TN_QCT1100_OldObj.TN_QCT1101List.ToList();
                if (oldList.Count > 0)
                {
                    foreach (var v in qualityList)
                    {
                        var Obj = oldList.Where(p => p.RevNo == v.RevNo && p.ItemCode == v.ItemCode && p.Seq == v.Seq).FirstOrDefault();
                        if (Obj != null)
                        {
                            v.Reading1 = Obj.Reading1;
                            v.Reading2 = Obj.Reading2;
                            v.Reading3 = Obj.Reading3;
                            v.Reading4 = Obj.Reading4;
                            v.Reading5 = Obj.Reading5;
                            v.Reading6 = Obj.Reading6;
                            v.Reading7 = Obj.Reading7;
                            v.Reading8 = Obj.Reading8;
                            v.Reading9 = Obj.Reading9;
                            v.Judge = Obj.Judge;
                            v.Memo = Obj.Memo;
                        }
                    }
                }
            }

            GridBindingSource.DataSource = qualityList;
            gridEx1.DataSource = GridBindingSource;
            gridEx1.MainGrid.BestFitColumns();
        }

        /// <summary>
        /// 적용 클릭이벤트
        /// </summary>
        //private void Btn_Apply_Click(object sender, EventArgs e)
        //{
        //    var view = GridExControl.MainGrid.MainView as GridView;
        //    var list = GridBindingSource.List as List<TN_QCT1001>;

        //    var readingNumber = "Reading" + lup_ReadingNumber.EditValue.GetNullToEmpty();
        //    int readingNumberCount = ((List<TN_STD1000>)lup_ReadingNumber.DataSource).Count;

        //    if (list == null) return;

        //    //if (view.GetRowCellValue(rowid, view.Columns["CheckWay"]).ToString() == MasterCodeSTR.InspectionWay_Eye)
        //    //{
        //    //    string val = lup_Eye.EditValue.GetNullToEmpty();
        //    //    if (val.IsNullOrEmpty())
        //    //    {
        //    //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
        //    //        return;
        //    //    }

        //    //    view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //    //    rowid++;
        //    //    if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //    //    {
        //    //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //    //        rowid--;
        //    //    }
        //    //    else if (rowid >= view.RowCount)
        //    //    {
        //    //        rowid = 0;
        //    //        lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //    //    }
        //    //    view.FocusedRowHandle = rowid;
        //    //}
        //    //else
        //    //{
        //    //    string val = tx_Reading.EditValue.GetNullToEmpty();
        //    //    if (val.IsNullOrEmpty())
        //    //    {
        //    //        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
        //    //        return;
        //    //    }

        //    //    view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //    //    tx_Reading.Text = "";
        //    //    rowid++;
        //    //    if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //    //    {
        //    //        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //    //        rowid--;
        //    //    }
        //    //    else if (rowid >= view.RowCount)
        //    //    {
        //    //        rowid = 0;
        //    //        lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //    //    }
        //    //    view.FocusedRowHandle = rowid;
        //    //}

        //    var maxReading = view.GetRowCellValue(rowid, view.Columns["MaxReading"]).GetNullToEmpty();
        //    if (!maxReading.IsNullOrEmpty() && maxReading.GetDecimalNullToZero() < lup_ReadingNumber.EditValue.GetDecimalNullToZero())
        //    {
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //        return;
        //    }

        //    if (view.GetRowCellValue(rowid, view.Columns["CheckDataType"]).ToString() == MasterCodeSTR.CheckDataType_C)
        //    {
        //        string val = lup_Eye.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //    else
        //    {
        //        string val = tx_Reading.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        tx_Reading.Text = "";
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //}

        //protected override void AddRowClicked()
        //{
        //    var view = GridExControl.MainGrid.MainView as GridView;
        //    var list = GridBindingSource.List as List<TN_QCT1001>;

        //    var readingNumber = "Reading" + lup_ReadingNumber.EditValue.GetNullToEmpty();
        //    int readingNumberCount = ((List<TN_STD1000>)lup_ReadingNumber.DataSource).Count;

        //    if (list == null) return;

        //    if (view.GetRowCellValue(rowid, view.Columns["CheckWay"]).ToString() == MasterCodeSTR.InspectionWay_Eye)
        //    {
        //        string val = lup_Eye.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("InspEye")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //    else
        //    {
        //        string val = tx_Reading.EditValue.GetNullToEmpty();
        //        if (val.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("Reading")));
        //            return;
        //        }

        //        view.SetRowCellValue(rowid, view.Columns[readingNumber], val);
        //        tx_Reading.Text = "";
        //        rowid++;
        //        if (rowid >= view.RowCount && lup_ReadingNumber.EditValue.GetIntNullToZero() == readingNumberCount)
        //        {
        //            MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_63));
        //            rowid--;
        //        }
        //        else if (rowid >= view.RowCount)
        //        {
        //            rowid = 0;
        //            lup_ReadingNumber.EditValue = lup_ReadingNumber.EditValue.GetIntNullToZero() + 1;
        //        }
        //        view.FocusedRowHandle = rowid;
        //    }
        //}

        protected override void DataSave()
        {
            gridEx1.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            SetSaveMessageCheck = false;
            var view = GridExControl.MainGrid.MainView as GridView;
            var list = GridBindingSource.List as List<TN_QCT1001>;
            if (list == null || list.Count == 0) return;

            if (list.Count == list.Where(p => p.Judge.IsNullOrEmpty()).Count())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_32), LabelConvert.GetLabelText("CheckingInspection")));
                return;
            }

            var checkId = lup_CheckId.EditValue.GetNullToEmpty();
            var resultQty = spin_ResultQty.EditValue.GetDecimalNullToZero();
            var badQty = spin_BadQty.EditValue.GetDecimalNullToZero();
            var badType = lup_BadType.EditValue.GetNullToNull();

            if (checkId.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_40), LabelConvert.GetLabelText("CheckId2")));
                return;
            }

            if (badQty > 0 && badType.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("BadType")));
                return;
            }

            if (badQty == 0 && !badType.IsNullOrEmpty())
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_68), LabelConvert.GetLabelText("BadType"), LabelConvert.GetLabelText("BadQty")));
                return;
            }

            if (resultQty - badQty < 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_69), LabelConvert.GetLabelText("BadQty"), LabelConvert.GetLabelText("CheckQty")));
                return;
            }

            if (MasterObj.ProcessSeq > 1)
            {
                var previousItemMoveObj = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                                                                                && p.ProcessSeq == 1
                                                                                && p.ProductLotNo == MasterObj.ProductLotNo
                                                                                && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
                if (previousItemMoveObj != null)
                {
                    var checkItemMoveObj2 = ModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                                                                                    && p.ProcessCode == MasterObj.ProcessCode
                                                                                    && p.ProcessSeq == MasterObj.ProcessSeq
                                                                                    && p.ProductLotNo == MasterObj.ProductLotNo
                                                                                    && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
                    var checkQty = checkItemMoveObj2.ResultQty + resultQty;
                    if (checkQty > previousItemMoveObj.ResultQty)
                    {
                        var possibleQty = previousItemMoveObj.ResultQty - checkItemMoveObj2.ResultQty;
                        MessageBoxHandler.Show(string.Format("이전 공정의 생산 수량을 벗어났습니다. 가능한 생산 수량 : {0}", possibleQty.GetDecimalNullToZero().ToString("N0")));
                        return;
                    }
                }
            }

            #region 실적

            MasterObj.ResultSumQty += resultQty;
            MasterObj.OkSumQty += (resultQty - badQty);
            MasterObj.BadSumQty += badQty;

            var detailNewObj = new TN_MPS1202();
            detailNewObj.WorkNo = MasterObj.WorkNo;
            detailNewObj.ProcessCode = MasterObj.ProcessCode;
            detailNewObj.ProcessSeq = MasterObj.ProcessSeq;
            detailNewObj.ProductLotNo = MasterObj.ProductLotNo;
            detailNewObj.ResultSeq = MasterObj.TN_MPS1202List.Count == 0 ? 1 : MasterObj.TN_MPS1202List.Max(p => p.ResultSeq) + 1;
            detailNewObj.ItemCode = MasterObj.ItemCode;
            detailNewObj.CustomerCode = MasterObj.CustomerCode;
            detailNewObj.ResultInsDate = DateTime.Today;
            detailNewObj.ResultQty = resultQty;
            detailNewObj.OkQty = (resultQty - badQty);
            detailNewObj.BadQty = badQty;
            detailNewObj.BadType = badType;
            detailNewObj.WorkId = checkId;
            detailNewObj.ItemMoveNo = ItemMoveNo;

            var checkItemMoveObj = QcModelService.GetChildList<TN_ITEM_MOVE>(p => p.WorkNo == MasterObj.WorkNo
                                                                            && p.ProcessCode == MasterObj.ProcessCode
                                                                            && p.ProcessSeq == MasterObj.ProcessSeq
                                                                            && p.ProductLotNo == MasterObj.ProductLotNo
                                                                            && p.ItemMoveNo == MasterObj.ItemMoveNo).FirstOrDefault();
            if (checkItemMoveObj != null)
            {
                var sumResultQty = spin_SumResultQty.EditValue.GetDecimalNullToZero();
                var sumOkQty = spin_SumOkQty.EditValue.GetDecimalNullToZero();
                var sumBadQty = spin_SumBadQty.EditValue.GetDecimalNullToZero();
                checkItemMoveObj.ResultSumQty = sumResultQty + resultQty;
                checkItemMoveObj.OkSumQty = sumOkQty + (resultQty - badQty);
                checkItemMoveObj.BadSumQty = sumBadQty + badQty;
                checkItemMoveObj.ResultQty += resultQty;
                checkItemMoveObj.OkQty += (resultQty - badQty);
                checkItemMoveObj.BadQty += (sumBadQty + badQty);
                checkItemMoveObj.UpdateTime = DateTime.Now;
                QcModelService.UpdateChild(checkItemMoveObj);
            }
            MasterObj.UpdateTime = DateTime.Now;
            MasterObj.UpdateId = checkId;
            QcModelService.InsertChild(detailNewObj);
            QcModelService.UpdateChild(MasterObj);
            #endregion

            var masterCheckResult = list.Any(p => p.Judge == "NG") ? "NG" : "OK";

            var TN_QCT1100_NewObj = new TN_QCT1100()
            {
                InspNo = DbRequestHandler.GetSeqMonth(MasterCodeSTR.InspectionDivision_Final),
                CheckDivision = MasterCodeSTR.InspectionDivision_Final,
                CheckPoint = MasterCodeSTR.CheckPoint_General,
                WorkNo = TEMP_XFPOP_INSP_FINAL.WorkNo,
                WorkSeq = TEMP_XFPOP_INSP_FINAL.ProcessSeq,
                WorkDate = TEMP_XFPOP_INSP_FINAL.WorkDate,
                ItemCode = TEMP_XFPOP_INSP_FINAL.ItemCode,
                CustomerCode = TEMP_XFPOP_INSP_FINAL.CustomerCode,
                ProcessCode = TEMP_XFPOP_INSP_FINAL.ProcessCode,
                ProductLotNo = ProductLotNo,
                Temp1 = ItemMoveNo,
                CheckDate = DateTime.Today,
                CheckId = checkId,
                CheckResult = masterCheckResult,
            };

            foreach (var v in list)
            {
                var TN_QCT1101_NewObj = new TN_QCT1101()
                {
                    InspNo = TN_QCT1100_NewObj.InspNo,
                    InspSeq = TN_QCT1100_NewObj.TN_QCT1101List.Count == 0 ? 1 : TN_QCT1100_NewObj.TN_QCT1101List.Max(o => o.InspSeq) + 1,
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
                    Reading1 = v.Reading1,
                    Reading2 = v.Reading2,
                    Reading3 = v.Reading3,
                    Reading4 = v.Reading4,
                    Reading5 = v.Reading5,
                    Reading6 = v.Reading6,
                    Reading7 = v.Reading7,
                    Reading8 = v.Reading8,
                    Reading9 = v.Reading9,
                    Judge = v.Judge,
                    Memo = v.Memo
                };
                TN_QCT1100_NewObj.TN_QCT1101List.Add(TN_QCT1101_NewObj);
            }

            QcModelService.Insert(TN_QCT1100_NewObj);
            QcModelService.Save();
            SetIsFormControlChanged(false);
            DialogResult = DialogResult.OK;
            ActClose();
        }

        protected override void ActClose()
        {
            SetIsFormControlChanged(false);
            base.ActClose();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridEx3.MainGrid.Clear();
            pic_ItemLimitImage.EditValue = null;

            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null)
            {
                return;
            }

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                var _CheckList = new SqlParameter("@CheckList", obj.CheckList);
                var result = context.Database.SqlQuery<TEMP_XFQCT1100_SUB>("USP_GET_INSP_FINAL_CHECK_LIST_RECORD @ItemCode, @CheckList", _ItemCode, _CheckList).ToList();
                gridEx3BindingSource.DataSource = result.ToList();
            }
            
            gridEx3.DataSource = gridEx3BindingSource;
            gridEx3.BestFitColumns();

            //품목한도사진
            var TN_STD1104 = obj.TN_QCT1000.TN_STD1100.TN_STD1104List.Where(p => p.CheckList == obj.CheckList).OrderBy(p => p.Seq).LastOrDefault();
            if (TN_STD1104 == null || TN_STD1104.FileUrl.IsNullOrEmpty())
            {
                pic_ItemLimitImage.EditValue = null;
            }
            else
            {
                pic_ItemLimitImage.EditValue = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + TN_STD1104.FileUrl);
            }
        }

        private void MainView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var view = sender as GridView;

            var obj = GridBindingSource.Current as TN_QCT1001;
            if (obj == null) return;

            if (e.Column.FieldName.Contains("Reading"))
            {
                //var inspectionWay = obj.CheckWay.GetNullToEmpty();
                //if (inspectionWay == MasterCodeSTR.InspectionWay_Input)
                //{
                //    CheckInput(obj);
                //}
                //else if (inspectionWay == MasterCodeSTR.InspectionWay_Eye)
                //{
                //    CheckEye(obj);
                //}
                //else
                //{
                //    CheckInput(obj);
                //}

                var checkDataType = obj.CheckDataType.GetNullToEmpty();
                if (checkDataType == MasterCodeSTR.CheckDataType_C)
                {
                    CheckEye(obj);
                }
                else
                {
                    CheckInput(obj);
                }
            }
        }

        private void MainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        /// <summary>
        /// 난수입력
        /// </summary>
        protected override void FileChooseClicked()
        {
            var detailObj = GridBindingSource.Current as TN_QCT1001;
            if (detailObj == null) return;

            GridExControl.MainGrid.PostEditor();

            if (detailObj.CheckDataType == MasterCodeSTR.CheckDataType_C)
            {
                for (int i = 3; i < 10; i++)
                {
                    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("Reading" + i.ToString(), "OK");
                    if (!detailObj.MaxReading.IsNullOrEmpty() && detailObj.MaxReading.GetIntNullToZero() == i)
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
                    GridExControl.MainGrid.MainView.SetFocusedRowCellValue("Reading" + i.ToString(), Math.Round(randomValue, detailObj.CheckDataType.GetIntNullToZero()));
                    if (!detailObj.MaxReading.IsNullOrEmpty() && detailObj.MaxReading.GetIntNullToZero() == i)
                        break;
                }
            }
        }

        /// <summary>
        /// 측정치 키 이벤트
        /// </summary> 
        //private void Tx_Reading_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (!tx_Reading.EditValue.IsNullOrEmpty())
        //            AddRowClicked();
        //    }
        //}

        /// <summary>
        /// 시료수 변경 시 이벤트
        /// </summary>
        private void Lup_ReadingNumber_EditValueChanged(object sender, EventArgs e)
        {
            var view = GridExControl.MainGrid.MainView as GridView;
            view.FocusedRowHandle = 0;
        }

        /// <summary>
        /// 치수검사 체크
        /// </summary>
        private void CheckInput(TN_QCT1001 obj)
        {
            var checkSpec = obj.CheckSpec.GetDecimalNullToNull();
            if (checkSpec == null) return;
            var checkUpQuad = obj.CheckUpQuad.GetDecimalNullToZero();
            var checkDownQuad = obj.CheckDownQuad.GetDecimalNullToZero();
            var checkUp = checkSpec + checkUpQuad;
            var checkDown = checkSpec - checkDownQuad;

            int NgQty = 0;
            int OkQty = 0;

            var reading1 = obj.Reading1.GetDecimalNullToNull();
            var reading2 = obj.Reading2.GetDecimalNullToNull();
            var reading3 = obj.Reading3.GetDecimalNullToNull();
            var reading4 = obj.Reading4.GetDecimalNullToNull();
            var reading5 = obj.Reading5.GetDecimalNullToNull();
            var reading6 = obj.Reading6.GetDecimalNullToNull();
            var reading7 = obj.Reading7.GetDecimalNullToNull();
            var reading8 = obj.Reading8.GetDecimalNullToNull();
            var reading9 = obj.Reading9.GetDecimalNullToNull();

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
                obj.Judge = null;
            }
            else if (NgQty > 0)
            {
                obj.Judge = "NG";
            }
            else
            {
                obj.Judge = "OK";
            }

            GridExControl.BestFitColumns();
        }

        /// <summary>
        /// 육안검사 체크
        /// </summary>
        /// <param name="detailObj"></param>
        private void CheckEye(TN_QCT1001 obj)
        {
            int NgQty = 0;
            int OkQty = 0;

            var reading1 = obj.Reading1.GetNullToNull();
            var reading2 = obj.Reading2.GetNullToNull();
            var reading3 = obj.Reading3.GetNullToNull();
            var reading4 = obj.Reading4.GetNullToNull();
            var reading5 = obj.Reading5.GetNullToNull();
            var reading6 = obj.Reading6.GetNullToNull();
            var reading7 = obj.Reading7.GetNullToNull();
            var reading8 = obj.Reading8.GetNullToNull();
            var reading9 = obj.Reading9.GetNullToNull();

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
                obj.Judge = null;
            }
            else if (NgQty > 0)
            {
                obj.Judge = "NG";
            }
            else
            {
                obj.Judge = "OK";
            }

            GridExControl.BestFitColumns();
        }

        //private void Tx_Reading_GotFocus(object sender, EventArgs e)
        //{
        //    tx_Reading.SelectAll();
        //}

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

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            var detailObj = GridBindingSource.Current as TN_QCT1001;
            if (detailObj == null) return;

            var maxReading = detailObj.MaxReading.GetNullToEmpty();
            if (gv.FocusedColumn.FieldName.Contains("Reading") && !gv.FocusedColumn.FieldName.Contains("MaxReading"))
            {
                if (gv.FocusedColumn.FieldName.Right(1).GetDecimalNullToNull() > maxReading.GetDecimalNullToNull())
                    e.Cancel = true;
            }
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            var view = sender as GridView;
            if (e.KeyCode == Keys.Enter)
            {
                if (view.FocusedColumn.FieldName.Contains("Reading") && !view.FocusedColumn.FieldName.Contains("MaxReading"))
                {
                    var maxReading = view.GetFocusedRowCellValue("MaxReading").GetNullToEmpty();
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

        private void SubMainView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (e.Column.FieldName.Contains("Reading") && !e.Column.FieldName.Contains("MaxReading"))
                {
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
    }
}
