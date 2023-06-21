using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Service.Handler;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Service;
using HKInc.Ui.Model.BaseDomain;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Base;
using System.Collections.Generic;
using HKInc.Utils.Common;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.View.View.REPORT;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품출고관리
    /// </summary>
    public partial class XFORD1200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1200> ModelService = (IService<TN_ORD1200>)ProductionFactory.GetDomainService("TN_ORD1200");

        string reportType;
       
        public XFORD1200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;

            reportType = null;

            btn_OrderStatus.Click += Btn_OrdStatus_Click;

            dateOrderDate.SetTodayIsWeek();
        }
        
        protected override void InitCombo()
        {   
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

            btn_OrderStatus.Text = LabelConvert.GetLabelText("OrderStatus") + "(&O)";
        }

        protected override void InitGrid()
        {
        //    IsMasterGridButtonFileChooseEnabled = true;
          //  MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ShipmentReportRef") + "[F10]", IconImageList.GetIconImage("business%20objects/botask"));
            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutNo", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far,FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("OrderQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutPlanDate", LabelConvert.GetLabelText("OutDatePlan"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");            
            MasterGridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            MasterGridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"));
            MasterGridExControl.MainGrid.AddColumn("BillDate", LabelConvert.GetLabelText("BillDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM");
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutDate", "BillDate", "Memo");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1200>(MasterGridExControl);

            var lblButtonPrint = new DevExpress.XtraBars.BarButtonItem();
            lblButtonPrint.Id = 7;
            lblButtonPrint.ImageOptions.Image = IconImageList.GetIconImage("print/printer");
            lblButtonPrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.T));
            lblButtonPrint.Name = "barButtonPrint";
            lblButtonPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            lblButtonPrint.ShortcutKeyDisplayString = "P";
            lblButtonPrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            lblButtonPrint.Caption = LabelConvert.GetLabelText("LabelPrint") + "[Alt+T]";
            lblButtonPrint.ItemClick += LblButtonPrint_ItemClick;
            lblButtonPrint.Visibility = BarItemVisibility.Never;
            lblButtonPrint.Enabled = false;
            MasterGridExControl.BarTools.AddItem(lblButtonPrint);

            var barButtonTradingStatePrint = new DevExpress.XtraBars.BarButtonItem();
            barButtonTradingStatePrint.Id = 4;
            barButtonTradingStatePrint.ImageOptions.Image = IconImageList.GetIconImage("business%20objects/bosale");
            //barButtonDevide.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonAdd.ImageOptions.LargeImage")));
            barButtonTradingStatePrint.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.O));
            barButtonTradingStatePrint.Name = "barButtonTradingStatePrint";
            barButtonTradingStatePrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonTradingStatePrint.ShortcutKeyDisplayString = "Alt+O";
            barButtonTradingStatePrint.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barButtonTradingStatePrint.Caption = LabelConvert.GetLabelText("TradingStatePrint") + "[Alt+O]";
            barButtonTradingStatePrint.Alignment = BarItemLinkAlignment.Right;
            barButtonTradingStatePrint.ItemClick += BarButtonTradingStatePrint_ItemClick;
            MasterGridExControl.BarTools.AddItem(barButtonTradingStatePrint);

            //DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "ProductLotNo", true);
            //DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", LabelConvert.GetLabelText("OutSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhCode"));
            DetailGridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("PositionName"));
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"));            
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            // 20210913 오세완 차장 생산LOT별로 출하검사여부를 설정한 것만 출하검사관리에 출력하게 처리
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("ShipmentInspFlag")); 
            //DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutQty", "Memo", "PositionCode", "WhCode");

            // 20210913 오세완 차장 생산LOT별로 출하검사여부를 설정한 것만 출하검사관리에 출력하게 처리
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutQty", "Memo", "PositionCode", "WhCode", "Temp");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1201>(DetailGridExControl);

            #region 기존 생략 처리
            //RepositoryItemSearchLookUpEdit searchLookUpEdit = new RepositoryItemSearchLookUpEdit()
            //{
            //    ValueMember = "CodeVal",
            //    DisplayMember = "CodeName", //DataConvert.GetCultureDataFieldName("CodeName"),
            //    DataSource = DbRequestHandler.GetCommTopCode(MasterCodeSTR.InspectionReportType).Select(p => new { p.CodeVal, p.CodeName }).ToList()
            //};
            //searchLookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            //searchLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            //searchLookUpEdit.View.OptionsBehavior.AllowIncrementalSearch = true;
            //searchLookUpEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            //searchLookUpEdit.View.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;

            //var barSearchLookUpEditReprotType = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, searchLookUpEdit);
            //barSearchLookUpEditReprotType.Id = 5;
            //barSearchLookUpEditReprotType.Name = "barSearchLookUpEditReportType";
            //barSearchLookUpEditReprotType.Edit.NullText = LabelConvert.GetLabelText("InspectionReportType");
            //barSearchLookUpEditReprotType.EditWidth = 130;
            //barSearchLookUpEditReprotType.Alignment = BarItemLinkAlignment.Right;
            //barSearchLookUpEditReprotType.EditValueChanged += BarSearchLookUpEditReprotType_EditValueChanged;
            //DetailGridExControl.BarTools.AddItem(barSearchLookUpEditReprotType);


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
            //DetailGridExControl.BarTools.AddItem(barButtonPrint);
            #endregion

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 5;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 130;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown_V2;
            //DetailGridExControl.BarTools.AddItem(barTextEditBarCode);

            var barTextEditBarCodeStaticItem = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem.Id = 6;
            barTextEditBarCodeStaticItem.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem.Edit.NullText = LabelConvert.GetLabelText("ProductLotNo") + ":";
            barTextEditBarCodeStaticItem.EditWidth = barTextEditBarCodeStaticItem.Edit.NullText.Length * 9;
            //barTextEditBarCodeStaticItem.EditWidth = 120;
            barTextEditBarCodeStaticItem.Enabled = false;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem.Alignment = BarItemLinkAlignment.Left;

            #region 기존 생략 처리
            //var barTextEditBarCode2 = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            //barTextEditBarCode2.Id = 7;
            //barTextEditBarCode2.Enabled = UserRight.HasEdit;
            //barTextEditBarCode2.Name = "barTextEditBarCode2";
            //barTextEditBarCode2.EditWidth = 130;
            //barTextEditBarCode2.Edit.KeyDown += Edit_KeyDown1;

            //var barTextEditBarCodeStaticItem2 = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            //barTextEditBarCodeStaticItem2.Id = 8;
            //barTextEditBarCodeStaticItem2.Name = "barTextEditBarCodeStaticItem";
            //barTextEditBarCodeStaticItem2.Edit.NullText = LabelConvert.GetLabelText("CustomerLotNo") + ":";
            //barTextEditBarCodeStaticItem2.EditWidth = barTextEditBarCodeStaticItem2.Edit.NullText.Length * 9;
            //barTextEditBarCodeStaticItem2.Enabled = false;
            //barTextEditBarCodeStaticItem2.Edit.AppearanceDisabled.ForeColor = Color.Black;
            //barTextEditBarCodeStaticItem2.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            //barTextEditBarCodeStaticItem2.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            //barTextEditBarCodeStaticItem2.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //barTextEditBarCodeStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            //barTextEditBarCodeStaticItem2.Alignment = BarItemLinkAlignment.Left;
            #endregion

            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
            //DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode2);
            //DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem2);
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl,"Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");            
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.ProcTeamCode", DbRequestHandler.GetCommCode(MasterCodeSTR.ProductTeamCode, 1), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));

            DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dateEditYear = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            dateEditYear.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            dateEditYear.VistaDisplayMode = DefaultBoolean.True;
            dateEditYear.Mask.EditMask = "yyyy-MM";
            dateEditYear.AllowNullInput = DefaultBoolean.True;
            MasterGridExControl.MainGrid.Columns["BillDate"].ColumnEdit = dateEditYear;

            //DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OutQty");
            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_WAN || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionCode", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName");

            var PositionCodeEdit = DetailGridExControl.MainGrid.Columns["PositionCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            PositionCodeEdit.Popup += PositionCodeEdit_Popup;

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N"); // 20210913 오세완 차장 생산LOT별로 출하검사여부를 설정한 것만 출하검사관리에 출력하게 처리
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            string customerCode = lup_CustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dateOrderDate.DateFrEdit.DateTime 
                                                                        &&  p.OutDate <= dateOrderDate.DateToEdit.DateTime) 
                                                                        && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                     )
                                                                     .OrderBy(p => p.OutDate)
                                                                     .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }
            DetailGridBindingSource.DataSource = obj.TN_ORD1201List.OrderBy(o => o.OutSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            ModelService.Save();         

            DataLoad();
        }

        protected override void AddRowClicked()
        {
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, "OutConfirmFlag");
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD1100, param, AddMasterCallBack);
            form.ShowPopup(true);
        }
        
        private void AddMasterCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_ORD1100> partList = (List<TN_ORD1100>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                TN_ORD1200 newObj = new TN_ORD1200();
                newObj.OutNo = DbRequestHandler.GetSeqDay("POUT");
                newObj.OrderNo = returnedPart.OrderNo;
                newObj.OrderSeq = Convert.ToInt32(returnedPart.OrderSeq);
                newObj.DelivNo = returnedPart.DelivNo;
                newObj.ItemCode = returnedPart.ItemCode;
                newObj.CustomerCode = returnedPart.CustomerCode;
                newObj.OutDate = DateTime.Today;
                newObj.BillDate= DateTime.Today;
                newObj.OutId = GlobalVariable.LoginId;
                newObj.Memo = returnedPart.Memo;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.OrderSeq == returnedPart.OrderSeq && p.DelivNo == returnedPart.DelivNo).First();

                MasterGridBindingSource.Add(newObj);
                MasterGridBindingSource.MoveLast();
                ModelService.Insert(newObj);
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, newObj.OutNo);
            }
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }
        
        /// <summary>출고증참조</summary>
        protected override void FileChooseClicked()
        {
            if (!UserRight.HasEdit) return;

            if (!IsFirstLoaded) ActRefresh();

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD1101, param, AddShipmentRefCallBack);
            form.ShowPopup(true);
        }

        private void AddShipmentRefCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            List<TN_ORD1101> partList = (List<TN_ORD1101>)e.Map.GetValue(PopupParameter.ReturnObject);

            foreach (var returnedPart in partList)
            {
                TN_ORD1200 newObj = new TN_ORD1200();
                newObj.OutNo = DbRequestHandler.GetSeqDay("POUT");
                newObj.OrderNo = returnedPart.OrderNo;
                newObj.OrderSeq = Convert.ToInt32(returnedPart.OrderSeq);
                newObj.DelivNo = returnedPart.DelivNo;
                newObj.ItemCode = returnedPart.ItemCode;
                newObj.CustomerCode = returnedPart.CustomerCode;
                newObj.OutDate = returnedPart.OutDate;
                newObj.OutId = GlobalVariable.LoginId;
                newObj.Memo = returnedPart.Memo;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.OrderSeq == returnedPart.OrderSeq && p.DelivNo == returnedPart.DelivNo).First();
                newObj.OutPlanDate = returnedPart.OutDate;

                MasterGridBindingSource.Add(newObj);
                MasterGridBindingSource.MoveLast();
                ModelService.Insert(newObj);
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, newObj.OutNo);
            }
            MasterGridExControl.BestFitColumns();
            IsFormControlChanged = true;
        }

        protected override void DeleteRow()
        {
            if (!UserRight.HasEdit) return;

            TN_ORD1200 tn = MasterGridBindingSource.Current as TN_ORD1200;
            if (tn == null) return;

            if (tn.TN_ORD1201List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OutMasterInfo"), LabelConvert.GetLabelText("OutDetailInfo"), LabelConvert.GetLabelText("OutDetailInfo")));
            }
            else
            {
                MasterGridBindingSource.RemoveCurrent();
                ModelService.Delete(tn);
                MasterGridExControl.BestFitColumns();
            }
        }

        protected override void DetailAddRowClicked()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.Constraint, "XFORD1200");
            param.SetValue(PopupParameter.Value_1, obj.ItemCode);
            param.SetValue(PopupParameter.Value_2, DetailGridBindingSource.List);

            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PROD_STOCK, param, AddDetailCallBack);
            form.ShowPopup(true);
        }

        private void AddDetailCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            if (e == null) return;

            var returnList = (List<VI_PROD_STOCK_PRODUCT_LOT_NO>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var positionObj = ModelService.GetChildList<TN_MPS1300>(p => p.ProductLotNo == v.ProductLotNo).OrderBy(p => p.InQty).LastOrDefault();
                string whCode = null;
                string positionCode = null;
                if (positionObj != null)
                {
                    whCode = positionObj.WhCode;
                    positionCode = positionObj.PositionCode;
                }

                TN_ORD1201 newobj = new TN_ORD1201()
                {
                    OutNo = obj.OutNo,
                    OutSeq = obj.TN_ORD1201List.Count == 0 ? 1 : obj.TN_ORD1201List.Max(o => o.OutSeq) + 1,
                    ItemCode = v.ItemCode,
                    ProductLotNo = v.ProductLotNo,
                    OutQty = v.StockQty.GetDecimalNullToZero(),
                    WhCode = whCode,
                    PositionCode = positionCode,
                    Temp = "N" // 20210913 오세완 차장 생산LOT별로 출하검사여부를 설정한 것만 출하검사관리에 출력하게 처리
                };
                DetailGridBindingSource.Add(newobj);
                obj.TN_ORD1201List.Add(newobj);
            }
            DetailGridExControl.BestFitColumns();
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            var textEdit = sender as TextEdit;
            if (textEdit == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (masterObj == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var productLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                    if (productLotNo.IsNullOrEmpty()) return;

                    var stockObj = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == productLotNo).FirstOrDefault();
                    if (stockObj == null || stockObj.StockQty == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
                    }
                    else
                    {
                        var positionObj = ModelService.GetChildList<TN_MPS1300>(p => p.ProductLotNo == productLotNo).OrderBy(p=>p.InQty).LastOrDefault();
                        string whCode = null;
                        string positionCode = null;
                        if (positionObj != null)
                        {
                            whCode = positionObj.WhCode;
                            positionCode = positionObj.PositionCode;
                        }
                        TN_ORD1201 newobj = new TN_ORD1201()
                        {
                            OutNo = masterObj.OutNo,
                            OutSeq = masterObj.TN_ORD1201List.Count == 0 ? 1 : masterObj.TN_ORD1201List.Max(o => o.OutSeq) + 1,
                            ItemCode = stockObj.ItemCode,
                            ProductLotNo = stockObj.ProductLotNo,
                            OutQty = stockObj.StockQty.GetDecimalNullToZero(),
                            WhCode = whCode,
                            PositionCode = positionCode,
                        };
                        DetailGridBindingSource.Add(newobj);
                        masterObj.TN_ORD1201List.Add(newobj);
                        DetailGridExControl.BestFitColumns();
                    }
                }
                finally
                {
                    textEdit.EditValue = "";
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 20211007 오세완 차장 
        /// Edit_KeyDown은 재고가 없는 제품이 들어온 경우 기존 lot번호가 없어지지 않는 버그가 있다. 그래서 교체함
        /// 또한 마스터의 품목코드를 비교하지 않는 오류를 수정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_KeyDown_V2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextEdit textEdit = sender as TextEdit;
                if (textEdit == null)
                    return;

                TN_ORD1200 masterObj = MasterGridBindingSource.Current as TN_ORD1200;
                if (masterObj == null)
                    return;

                string productLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                if (productLotNo.IsNullOrEmpty())
                    return;

                //var stockObj = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == productLotNo).FirstOrDefault();
                List<VI_PROD_STOCK_PRODUCT_LOT_NO> tempList = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == productLotNo); // 20220110 오세완 차장 잘못된 lot 입력한 경우 오류가 발생해서 에외처리 추가
                if(tempList == null)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_127), LabelConvert.GetLabelText("ItemCode")));
                }
                else if(tempList.Count == 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_127), LabelConvert.GetLabelText("ItemCode")));
                }
                else
                {
                    VI_PROD_STOCK_PRODUCT_LOT_NO stockObj = tempList.FirstOrDefault();

                    if (stockObj.ItemCode != masterObj.ItemCode)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_127), LabelConvert.GetLabelText("ItemCode")));
                    }
                    else if (stockObj == null || stockObj.StockQty == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
                    }
                    else
                    {
                        var positionObj = ModelService.GetChildList<TN_MPS1300>(p => p.ProductLotNo == productLotNo).OrderBy(p => p.InQty).LastOrDefault();
                        string whCode = null;
                        string positionCode = null;
                        if (positionObj != null)
                        {
                            whCode = positionObj.WhCode;
                            positionCode = positionObj.PositionCode;
                        }

                        TN_ORD1201 newobj = new TN_ORD1201()
                        {
                            OutNo = masterObj.OutNo,
                            OutSeq = masterObj.TN_ORD1201List.Count == 0 ? 1 : masterObj.TN_ORD1201List.Max(o => o.OutSeq) + 1,
                            ItemCode = stockObj.ItemCode,
                            ProductLotNo = stockObj.ProductLotNo,
                            OutQty = stockObj.StockQty.GetDecimalNullToZero(),
                            WhCode = whCode,
                            PositionCode = positionCode,
                        };

                        DetailGridBindingSource.Add(newobj);
                        masterObj.TN_ORD1201List.Add(newobj);
                        DetailGridExControl.BestFitColumns();
                    }
                }

                textEdit.EditValue = "";
                //e.Handled = true; // 20220110 오세완 차장 이게 없어야 텍스트 박스 값이 잘 없어지는 것 같다. 
            }
        }

        /// <summary>
        /// 고객사 LOT 스캔
        /// </summary>
        private void Edit_KeyDown1(object sender, KeyEventArgs e)
        {
            var textEdit = sender as TextEdit;
            if (textEdit == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (masterObj == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var customerLotNo = textEdit.EditValue.GetNullToEmpty().ToUpper();
                    if (customerLotNo.IsNullOrEmpty()) return;

                    var stockObj = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.CustomerLotNo == customerLotNo).Where(p => p.StockQty > 0).OrderBy(p => p.ProductLotNo).FirstOrDefault();
                    if (stockObj == null || stockObj.StockQty == 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
                    }
                    else
                    {
                        var positionObj = ModelService.GetChildList<TN_MPS1300>(p => p.ProductLotNo == stockObj.ProductLotNo).OrderBy(p => p.InQty).LastOrDefault();
                        string whCode = null;
                        string positionCode = null;
                        if (positionObj != null)
                        {
                            whCode = positionObj.WhCode;
                            positionCode = positionObj.PositionCode;
                        }
                        TN_ORD1201 newobj = new TN_ORD1201()
                        {
                            OutNo = masterObj.OutNo,
                            OutSeq = masterObj.TN_ORD1201List.Count == 0 ? 1 : masterObj.TN_ORD1201List.Max(o => o.OutSeq) + 1,
                            ItemCode = stockObj.ItemCode,
                            ProductLotNo = stockObj.ProductLotNo,
                            OutQty = stockObj.StockQty.GetDecimalNullToZero(),
                            WhCode = whCode,
                            PositionCode = positionCode,
                        };
                        DetailGridBindingSource.Add(newobj);
                        masterObj.TN_ORD1201List.Add(newobj);
                        DetailGridExControl.BestFitColumns();
                    }
                }
                finally
                {
                    textEdit.EditValue = "";
                    e.Handled = true;
                }
            }
        }

        protected override void DeleteDetailRow()
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            TN_ORD1201 delobj = DetailGridBindingSource.Current as TN_ORD1201;
            if (delobj == null) return;

            obj.TN_ORD1201List.Remove(delobj);
            DetailGridBindingSource.Remove(delobj);
            DetailGridExControl.BestFitColumns();
        }

        /// <summary> 성적서타입 </summary>
        //private void BarSearchLookUpEditReprotType_EditValueChanged(object sender, EventArgs e)
        //{
        //    BarEditItem lookup = sender as BarEditItem;

        //    if (lookup == null) return;

        //    if(!lookup.IsNullOrEmpty())
        //    {
        //        reportType = lookup.EditValue.GetNullToEmpty();
        //    }
        //}

        /// <summary> 수주현황 </summary>
        private void Btn_OrdStatus_Click(object sender, EventArgs e)
        {
            PopupDataParam param = new PopupDataParam();
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD_STATUS, param, null);
            form.ShowPopup(false);
        }

        ///// <summary> 성적서출력 </summary>
        //private void BarButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        if (DetailGridBindingSource == null) return;
        //        if (DetailGridBindingSource.Count == 0) return;

        //        MasterGridExControl.MainGrid.PostEditor();
        //        DetailGridExControl.MainGrid.PostEditor();

        //        WaitHandler.ShowWait();

        //        var detailList = DetailGridBindingSource.List as List<TN_ORD1201>;
        //        var checkList = detailList.Where(p => p._Check == "Y").ToList();

        //        if (checkList.Count == 0) return;

        //        if (reportType.IsNullOrEmpty())
        //        {
        //            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_84), LabelConvert.GetLabelText("InspectionReportType")));
        //            return;
        //        }

        //        if(reportType == "01")
        //        {
        //            var mainReport = new REPORT.XRQCT1000();
        //            foreach (var v in checkList.OrderByDescending(p => p.OutSeq).ToList())
        //            {
        //                var report = new REPORT.XRQCT1000(v);
        //                report.CreateDocument();
        //                mainReport.Pages.AddRange(report.Pages);
        //                v._Check = "N";
        //            }

        //            DetailGridExControl.BestFitColumns();
        //            mainReport.PrintingSystem.ShowMarginsWarning = false;
        //            mainReport.ShowPrintStatusDialog = false;
        //            mainReport.ShowPreview();
        //        }
        //        else if(reportType == "02")
        //        {
        //            var mainReport = new REPORT.XRQCT1001();
        //            foreach (var v in checkList.OrderByDescending(p => p.OutSeq).ToList())
        //            {
        //                var report = new REPORT.XRQCT1001(v);
        //                report.CreateDocument();
        //                mainReport.Pages.AddRange(report.Pages);
        //                v._Check = "N";
        //            }

        //            DetailGridExControl.BestFitColumns();
        //            mainReport.PrintingSystem.ShowMarginsWarning = false;
        //            mainReport.ShowPrintStatusDialog = false;
        //            mainReport.ShowPreview();
        //        }
        //        else if (reportType == "03")
        //        {
        //            var mainReport = new REPORT.XRQCT1002();
        //            foreach (var v in checkList.OrderByDescending(p => p.OutSeq).ToList())
        //            {
        //                var report = new REPORT.XRQCT1002(v);
        //                report.CreateDocument();
        //                mainReport.Pages.AddRange(report.Pages);
        //                v._Check = "N";
        //            }

        //            DetailGridExControl.BestFitColumns();
        //            mainReport.PrintingSystem.ShowMarginsWarning = false;
        //            mainReport.ShowPrintStatusDialog = false;
        //            mainReport.ShowPreview();
        //        }
        //    }
        //    catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
        //    finally
        //    {
        //        WaitHandler.CloseWait();
        //    }
        //}
        
        /// <summary> 라벨출력 </summary>
        private void LblButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DetailGridBindingSource == null) return;

            var mstObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (mstObj == null) return;

            try
            {
                WaitHandler.ShowWait();
                var report = new REPORT.XRORD1200(mstObj);
                report.CreateDocument();

                report.PrintingSystem.ShowMarginsWarning = false;
                report.ShowPrintStatusDialog = false;
                report.ShowPreview();
            }
            catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
            WaitHandler.CloseWait();
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1201;
            if (detailObj == null) return;

            if (e.Column.FieldName == "WhCode")
            {
                detailObj.PositionCode = null;
            }
        }

        private void PositionCodeEdit_Popup(object sender, EventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1201;
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;
            if (detailObj == null) return;

            lookup.Properties.View.ActiveFilter.NonColumnFilter = "[WhCode] = '" + detailObj.WhCode + "'";
        }

        //거래명세서출력
        private void BarButtonTradingStatePrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null) return;
            var masterList = MasterGridBindingSource.List as List<TN_ORD1200>;
            var checkList = masterList.Where(p => p._Check == "Y" && p.SumOutQty > 0).ToList();
            var costDisplayFlag = true;

            if (checkList.Count > 0)
            {
                if (checkList.GroupBy(p => p.CustomerCode).Count() != 1)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_94));
                    //MessageBoxHandler.Show("거래명세서는 같은 거래처로 출력이 가능합니다. 같은 거래처를 선택해 주세요.", "경고");
                    return;
                }
                var result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_95), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNo);
                //var result = MessageBoxHandler.Show("거래명세서에 단가를 입력하시겠습니까?", "경고", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) costDisplayFlag = true;
                else costDisplayFlag = false;

                try
                {
                    WaitHandler.ShowWait();
                    var groupList = new List<TradingStateGroupModel>();
                    var cultureIndex = DataConvert.GetCultureIndex();

                    if (cultureIndex == 1)
                    {
                        foreach (var v in checkList)
                        {
                            groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemName, p.TN_STD1100.CombineSpec,  p.OutQty }).Select(p => new TradingStateGroupModel
                            {
                                ItemCode = p.Key.ItemCode,
                                ItemName = p.Key.ItemName,
                                Spec = p.Key.CombineSpec,
                                Qty = p.Key.OutQty.GetDecimalNullToZero(),
                                Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                                Count = p.Count()
                            }));
                        }
                    }
                    else if (cultureIndex == 2)
                    {
                        foreach (var v in checkList)
                        {
                            groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNameENG, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
                            {
                                ItemCode = p.Key.ItemCode,
                                ItemName = p.Key.ItemNameENG,
                                Spec = p.Key.CombineSpec,
                                Qty = p.Key.OutQty.GetDecimalNullToZero(),
                                Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                                Count = p.Count()
                            }));
                        }
                    }
                    else
                    {
                        foreach (var v in checkList)
                        {
                            groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new {p.ItemCode,p.TN_ORD1200.TN_STD1100.ItemNameCHN, p.TN_STD1100.CombineSpec, p.OutQty}).Select(p => new TradingStateGroupModel
                            {
                                ItemCode = p.Key.ItemCode,
                                ItemName = p.Key.ItemNameCHN,
                                Spec = p.Key.CombineSpec,
                                Qty = p.Key.OutQty.GetDecimalNullToZero(),
                                Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                                Count = p.Count()
                            }));
                        }
                    }

                    var distinctList = groupList.GroupBy(p => new { p.ItemCode, p.ItemName, p.Spec, p.Qty, p.Cost }).Select(p => new TradingStateGroupModel
                    {
                        ItemCode = p.Key.ItemCode,
                        ItemName = p.Key.ItemName,
                        Spec = p.Key.Spec,
                        Qty = p.Key.Qty.GetDecimalNullToZero(),
                        Cost = p.Key.Cost,
                        Count = p.Sum(c => c.Count)
                    });

                    var _Date = checkList.Max(p => p.OutDate);
                    var printList = new List<TEMP_TRADING_DETAIL>();
                    foreach (var v in distinctList)
                    {
                        var newObj = new TEMP_TRADING_DETAIL()
                        {
                            Date = ((DateTime)_Date).ToString("MM.dd"),
                            ItemCode = v.ItemCode,
                            ItemName = v.ItemName,
                            Spec = v.Spec,
                            //Spec = string.Format("({0}*{1}BOX)", v.Qty, v.Count),
                            Qty = v.Qty * v.Count,
                            Cost = v.Cost,
                        };
                        //newObj.Cost = costDisplayFlag ? checkList.Where(p => p.ItemCode == v.ItemCode).First().TN_STD1100.Cost.GetDecimalNullToZero() : 0
                        newObj.SupplyCost = newObj.Qty * newObj.Cost;
                        newObj.TaxCost = (newObj.Qty * newObj.Cost) / 10;

                        printList.Add(newObj);
                    }
                    if (printList.Count > 0)
                    {
                        var inCustomerCode = checkList.First().CustomerCode;
                        var inCustomer = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == inCustomerCode).FirstOrDefault();
                        var ourCustomer = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();
                        var report = new XRORD1200_TRADING(((DateTime)_Date).ToShortDateString(), ourCustomer, inCustomer, printList);
                        report.CreateDocument();
                        report.PrintingSystem.ShowMarginsWarning = false;
                        report.ShowPrintStatusDialog = false;
                        report.ShowPreview();
                    }
                }
                catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                finally
                {
                    WaitHandler.CloseWait();
                }
            }
        }

        private class TradingStateGroupModel
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Spec { get; set; }
            public decimal Qty { get; set; }
            public decimal Cost { get; set; }
            public decimal Count { get; set; }
        }
    }
}
