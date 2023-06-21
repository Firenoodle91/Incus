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
using System.Data.SqlClient;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품출고관리
    /// </summary>
    public partial class XFORD1200 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1200> ModelService = (IService<TN_ORD1200>)ProductionFactory.GetDomainService("TN_ORD1200");
        IService<TN_BAN1100> ModelServiceBAN = (IService<TN_BAN1100>)ProductionFactory.GetDomainService("TN_BAN1100");
        bool detailFirstLoadCheck = false;

        private BindingSource gridEx3BindingSource = new BindingSource();
        private BindingSource BANBindingSource = new BindingSource();
        string banf = "T";
        List<VI_PROD_STOCK_PRODUCT_LOT_NO> VI_PROD_STOCK_PRODUCT_LOT_NO_LIST = new List<VI_PROD_STOCK_PRODUCT_LOT_NO>();

        public XFORD1200()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            gridEx3.ActAddRowClicked += GridEx3_ActAddRowClicked;
            gridEx3.MainGrid.MainView.CustomColumnDisplayText += GridEx3MainView_CustomColumnDisplayText1;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;

            btn_OrderStatus.Click += Btn_OrdStatus_Click;
        }
       
        protected override void InitCombo()
        {
            dateOrderDate.SetTodayIsWeek();
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Sales)).ToList());

            btn_OrderStatus.Text = LabelConvert.GetLabelText("OrderStatus") + "(&O)";
        }

        protected override void InitGrid()
        {
            IsMasterGridButtonFileChooseEnabled = true;
            MasterGridExControl.SetToolbarButtonCaption(GridToolbarButton.FileChoose, LabelConvert.GetLabelText("ShipmentReportRef") + "[F10]", IconImageList.GetIconImage("business%20objects/botask"));
            MasterGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OutNo", true);
            MasterGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            MasterGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far,FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            //MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ProcTeamCode", LabelConvert.GetLabelText("ProductTeam"));
            MasterGridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("OrderQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutPlanQty", LabelConvert.GetLabelText("OutPlanQty"), HorzAlignment.Far, FormatType.Numeric, "#,#");
            MasterGridExControl.MainGrid.AddColumn("OutPlanDate", LabelConvert.GetLabelText("OutDatePlan"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");            
            MasterGridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDate"));
            MasterGridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"));
            MasterGridExControl.MainGrid.AddColumn("OutReqDate", LabelConvert.GetLabelText("SalesDate"));
            MasterGridExControl.MainGrid.AddColumn("BillDate", LabelConvert.GetLabelText("BillDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM");
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            MasterGridExControl.MainGrid.AddColumn("OrderNoSeq", LabelConvert.GetLabelText("OrderNoSeq"), false);
            
            MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "_Check", "OutDate", "BillDate", "Memo", "OutReqDate");
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

            var dateObj = new RepositoryItemDateEdit();
            dateObj.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            dateObj.VistaDisplayMode = DefaultBoolean.True;
            dateObj.Mask.EditMask = "yyyy-MM";
            dateObj.Mask.UseMaskAsDisplayFormat = true;
            dateObj.AllowNullInput = DefaultBoolean.True;
            dateObj.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            var barDateEditBillDate = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, dateObj);
            barDateEditBillDate.Id = 9;
            barDateEditBillDate.Enabled = UserRight.HasEdit;
            barDateEditBillDate.Name = "barDateEditBillDate";
            barDateEditBillDate.EditWidth = 120;
            MasterGridExControl.BarTools.AddItem(barDateEditBillDate);

            (MasterGridExControl.BarTools.ItemLinks[5].Item as BarEditItem).EditValue = DateTime.Today;

            var barButtonBillDate = new DevExpress.XtraBars.BarButtonItem();
            barButtonBillDate.Id = 8;
            barButtonBillDate.ImageOptions.Image = IconImageList.GetIconImage("navigation/next");
            barButtonBillDate.Name = "barButtonBillDate";
            barButtonBillDate.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barButtonBillDate.Caption = "마감월 일괄변경";
            barButtonBillDate.ItemClick += BarButtonBillDate_ItemClick; ;
            barButtonBillDate.Visibility = BarItemVisibility.Always;
            barButtonBillDate.Enabled = UserRight.HasEdit;
            MasterGridExControl.BarTools.AddItem(barButtonBillDate);

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
            
            var barTextEditBarCodeScanStaticItem = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            //barTextEditBarCodeScanStaticItem.Id = 11;
            barTextEditBarCodeScanStaticItem.Name = "barTextEditBarCodeScanStaticItem";
            barTextEditBarCodeScanStaticItem.Edit.NullText = "출고 바코드:";
            barTextEditBarCodeScanStaticItem.EditWidth = 85;
            barTextEditBarCodeScanStaticItem.Enabled = false;
            barTextEditBarCodeScanStaticItem.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeScanStaticItem.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeScanStaticItem.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeScanStaticItem.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeScanStaticItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeScanStaticItem.Alignment = BarItemLinkAlignment.Left;
            MasterGridExControl.BarTools.AddItem(barTextEditBarCodeScanStaticItem);

            var barBarCodeScan = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            //barBarCodeScan.Id = 10;
            barBarCodeScan.Enabled = UserRight.HasEdit;
            barBarCodeScan.Name = "barBarCodeScan";
            barBarCodeScan.EditWidth = 130;
            barBarCodeScan.Edit.KeyDown += Edit_KeyDown2;
            MasterGridExControl.BarTools.AddItem(barBarCodeScan);
            
            //DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "ProductLotNo", true);
            //DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OutNo", LabelConvert.GetLabelText("OutNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OutSeq", LabelConvert.GetLabelText("OutSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            DetailGridExControl.MainGrid.AddColumn("CustomerLotNo", LabelConvert.GetLabelText("CustomerLotNo"));
            DetailGridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhCode"));
            DetailGridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("PositionName"));
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"));            
            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OutQty", "Memo", "PositionCode", "WhCode");
            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1201>(DetailGridExControl);

            var barTextEditBarCode = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode.Id = 5;
            barTextEditBarCode.Enabled = UserRight.HasEdit;
            barTextEditBarCode.Name = "barTextEditBarCode";
            barTextEditBarCode.EditWidth = 130;
            barTextEditBarCode.Edit.KeyDown += Edit_KeyDown; ;
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

            var barTextEditBarCode2 = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCode2.Id = 7;
            barTextEditBarCode2.Enabled = UserRight.HasEdit;
            barTextEditBarCode2.Name = "barTextEditBarCode2";
            barTextEditBarCode2.EditWidth = 130;
            barTextEditBarCode2.Edit.KeyDown += Edit_KeyDown1;

            var barTextEditBarCodeStaticItem2 = new DevExpress.XtraBars.BarEditItem(DetailGridExControl.BarTools.Manager, new RepositoryItemTextEdit());
            barTextEditBarCodeStaticItem2.Id = 8;
            barTextEditBarCodeStaticItem2.Name = "barTextEditBarCodeStaticItem";
            barTextEditBarCodeStaticItem2.Edit.NullText = LabelConvert.GetLabelText("CustomerLotNo") + ":";
            barTextEditBarCodeStaticItem2.EditWidth = barTextEditBarCodeStaticItem2.Edit.NullText.Length * 9;
            barTextEditBarCodeStaticItem2.Enabled = false;
            barTextEditBarCodeStaticItem2.Edit.AppearanceDisabled.ForeColor = Color.Black;
            barTextEditBarCodeStaticItem2.Edit.AppearanceDisabled.TextOptions.HAlignment = HorzAlignment.Far;
            barTextEditBarCodeStaticItem2.Edit.AppearanceDisabled.BackColor = Color.Transparent;
            barTextEditBarCodeStaticItem2.Edit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            barTextEditBarCodeStaticItem2.Alignment = BarItemLinkAlignment.Left;

            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCode2);
            DetailGridExControl.BarTools.ItemLinks.Insert(0, barTextEditBarCodeStaticItem2);

            gridEx3.SetToolbarButtonVisible(false);
            gridEx3.SetToolbarButtonVisible(GridToolbarButton.AddRow, UserRight.HasEdit);
            gridEx3.SetToolbarButtonCaption(GridToolbarButton.AddRow, "출고 일괄 확정[Alt+G]");
            gridEx3.SetToolbarShotKeyChange(GridToolbarButton.AddRow, new BarShortcut((Keys.Alt | Keys.G)));            

            gridEx3.MainGrid.AddColumn("RowIndex", false);
            gridEx3.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            gridEx3.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            gridEx3.MainGrid.AddColumn("CustomerLotNo", LabelConvert.GetLabelText("CustomerLotNo"));
            gridEx3.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            gridEx3.MainGrid.AddColumn("StockQty2", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
        }

        private void Edit_KeyDown2(object sender, KeyEventArgs e)
        {
            var textEdit = sender as TextEdit;
            if (textEdit == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1200;
            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null)
            {
                ActRefresh();
            }

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var barcode = textEdit.EditValue.GetNullToEmpty().ToUpper();
                    if (barcode.IsNullOrEmpty()) return;
                    var masterlist = MasterGridBindingSource.List as List<TN_ORD1200>;
                    
                    var list = barcode.Split('&').ToList();

                    DateTime searchOutDate;
                    string searchCustomerCode;

                    if (list.Count < 2) return;

                    var orderNo = list[0];
                    var orderSeq = list[1].GetIntNullToZero();
                    var returnedPart = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == orderNo && p.OrderSeq == orderSeq).FirstOrDefault();
                    if (returnedPart == null) return;

                    if (masterlist.Any(p => p.OrderNoSeq == orderNo + orderSeq.ToString()))
                    {
                        MasterGridExControl.MainGrid.MainView.FocusedRowHandle = MasterGridExControl.MainGrid.MainView.LocateByValue("OrderNoSeq", orderNo + orderSeq.ToString());
                        return;
                    }

                    var outPlanObj = returnedPart.TN_ORD1101List.FirstOrDefault();

                    searchOutDate = outPlanObj == null ? DateTime.Today : outPlanObj.TN_ORD1103.OutDatePlan;
                    searchCustomerCode = returnedPart.CustomerCode;

                    if ((cbo_SearchDivision.SelectedIndex == 0) || 
                        (dateOrderDate.DateFrEdit.DateTime != searchOutDate || dateOrderDate.DateToEdit.DateTime != searchOutDate) || 
                        (lup_CustomerCode.EditValue.GetNullToEmpty() != searchCustomerCode)
                       )
                    {
                        ActSave();

                        dateOrderDate.DateFrEdit.DateTime = searchOutDate;
                        dateOrderDate.DateToEdit.DateTime = searchOutDate;
                        lup_CustomerCode.EditValue = searchCustomerCode;
                        cbo_SearchDivision.SelectedIndex = 1;

                        ActRefresh();

                        returnedPart = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == orderNo && p.OrderSeq == orderSeq).FirstOrDefault();
                    }

                    outPlanObj = returnedPart.TN_ORD1101List.FirstOrDefault();

                    if (list.Count == 2)
                    {
                        TN_ORD1200 newObj = new TN_ORD1200();
                        newObj.OutNo = DbRequestHandler.GetSeqDay("POUT");
                        newObj.OrderNo = returnedPart.OrderNo;
                        newObj.OrderSeq = Convert.ToInt32(returnedPart.OrderSeq);
                        newObj.DelivNo = returnedPart.DelivNo;
                        newObj.ItemCode = returnedPart.ItemCode;
                        newObj.CustomerCode = returnedPart.CustomerCode;
                        if (outPlanObj == null)
                        {
                            newObj.OutDate = DateTime.Today;//DateTime.Today;
                            newObj.OutPlanDate = null;
                            newObj.OutReqDate = DateTime.Today;
                            newObj.OutPlanQty = masterObj.TN_ORD1100.DelivQty;
                        }
                        else
                        {
                            newObj.OutDate = DateTime.Today;
                            newObj.OutPlanDate = outPlanObj.TN_ORD1103.OutDatePlan;
                            newObj.OutReqDate = outPlanObj.TN_ORD1103.OutDatePlan;
                            newObj.OutPlanQty = outPlanObj.OutPlanQty;
                        }
                        newObj.OutId = GlobalVariable.LoginId;
                        newObj.Memo = returnedPart.Memo;
                        newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                        newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.OrderSeq == returnedPart.OrderSeq && p.DelivNo == returnedPart.DelivNo).First();
                        newObj.BillDate = newObj.TN_ORD1100.TN_ORD1001.EndMonthDate;

                        MasterGridBindingSource.Add(newObj);
                        MasterGridBindingSource.MoveLast();
                        ModelService.Insert(newObj);
                        PopupDataParam.SetValue(PopupParameter.GridRowId_1, newObj.OutNo);
                    }
                    else if (list.Count > 2)
                    {
                        TN_ORD1200 newObj = new TN_ORD1200();
                        newObj.OutNo = DbRequestHandler.GetSeqDay("POUT");
                        newObj.OrderNo = returnedPart.OrderNo;
                        newObj.OrderSeq = Convert.ToInt32(returnedPart.OrderSeq);
                        newObj.DelivNo = returnedPart.DelivNo;
                        newObj.ItemCode = returnedPart.ItemCode;
                        newObj.CustomerCode = returnedPart.CustomerCode;
                        //newObj.OutDate = outPlanObj == null ? DateTime.Today : outPlanObj.TN_ORD1103.OutDatePlan;//DateTime.Today;
                        newObj.OutId = GlobalVariable.LoginId;
                        newObj.Memo = returnedPart.Memo;
                        newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                        newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.OrderSeq == returnedPart.OrderSeq && p.DelivNo == returnedPart.DelivNo).First();
                        newObj.BillDate = newObj.TN_ORD1100.TN_ORD1001.EndMonthDate;
                        if (outPlanObj == null)
                        {
                            newObj.OutDate = DateTime.Today;//DateTime.Today;
                            newObj.OutPlanDate = null;
                            newObj.OutReqDate = DateTime.Today;
                            newObj.OutPlanQty = masterObj.TN_ORD1100.DelivQty;
                        }
                        else
                        {
                            newObj.OutDate = DateTime.Today;
                            newObj.OutPlanDate = outPlanObj.TN_ORD1103.OutDatePlan;
                            newObj.OutReqDate = outPlanObj.TN_ORD1103.OutDatePlan;
                            newObj.OutPlanQty = outPlanObj.OutPlanQty;
                        }
                        TN_BAN1100 bannewobj = ModelServiceBAN.GetList(p => p.Temp == newObj.OutNo).FirstOrDefault();
                        if (returnedPart.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                        {
                            if (bannewobj == null)
                            {
                                banf = "N";
                                bannewobj = new TN_BAN1100()
                                {
                                    OutNo = DbRequestHandler.GetSeqMonth("BOUT"),
                                    OutDate = DateTime.Today,
                                    OutId = GlobalVariable.LoginId,
                                    Temp = newObj.OutNo,
                                    Memo = "제품출고"
                                };
                            }
                        }

                        MasterGridBindingSource.Add(newObj);
                        MasterGridBindingSource.MoveLast();
                        ModelService.Insert(newObj);
                        PopupDataParam.SetValue(PopupParameter.GridRowId_1, newObj.OutNo);

                        for (int i = 2; i < list.Count; i++)
                        {
                            var split1 = list[i].Split(';');
                            foreach (var c in split1)
                            {
                                var lotNo = c.Substring(0, c.IndexOf('('));
                             
                                if (returnedPart.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                                {
                                   var  stockObj = ModelService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == lotNo).Where(p => p.StockQty > 0).OrderBy(p => p.ProductLotNo).FirstOrDefault();
                                    if (stockObj == null)
                                    {
                                        //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
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
                                            OutNo = newObj.OutNo,
                                            OutSeq = newObj.TN_ORD1201List.Count == 0 ? 1 : newObj.TN_ORD1201List.Max(o => o.OutSeq) + 1,
                                            ItemCode = stockObj.ItemCode,
                                            ProductLotNo = stockObj.ProductLotNo,
                                            OutQty = c.Substring(c.IndexOf('(') + 1, c.IndexOf(')') - 1 - c.IndexOf('(')).GetDecimalNullToZero(), //stockObj.StockQty.GetDecimalNullToZero(),
                                            WhCode = whCode,
                                            PositionCode = positionCode,
                                        };

                                        if (returnedPart.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                                        {
                                            var TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == stockObj.ProductLotNo).FirstOrDefault();
                                            var newObjBan = new TN_BAN1101();
                                            newObjBan.OutNo = bannewobj.OutNo;
                                            newObjBan.OutSeq = bannewobj.TN_BAN1101List.Count == 0 ? 1 : bannewobj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                                            newObjBan.InNo = TN_BAN1001.InNo;
                                            newObjBan.InSeq = TN_BAN1001.InSeq;
                                            newObjBan.ItemCode = TN_BAN1001.ItemCode;
                                            newObjBan.OutQty = c.Substring(c.IndexOf('(') + 1, c.IndexOf(')') - 1 - c.IndexOf('(')).GetDecimalNullToZero();
                                            newObjBan.OutLotNo = stockObj.ProductLotNo;
                                            newObjBan.InLotNo = stockObj.ProductLotNo;
                                            newObjBan.Temp = newobj.OutNo;
                                            newObjBan.Temp1 = newobj.OutSeq.ToString();
                                            newObjBan.NewRowFlag = "Y";
                                            newObjBan.TN_BAN1100 = bannewobj;
                                            newObjBan.TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == stockObj.ProductLotNo).FirstOrDefault();
                                            newObjBan.TN_STD1100 = ModelServiceBAN.GetChildList<TN_STD1100>(p => p.ItemCode == returnedPart.ItemCode).FirstOrDefault();
                                            bannewobj.TN_BAN1101List.Add(newObjBan);
                                        }
                                        DetailGridBindingSource.Add(newobj);
                                        newObj.TN_ORD1201List.Add(newobj);
                                        DetailGridExControl.BestFitColumns();
                                    }
                                }
                                else {
                                     var stockObj1 = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == lotNo).Where(p => p.StockQty > 0).OrderBy(p => p.ProductLotNo).FirstOrDefault();
                                    if (stockObj1 == null)
                                    {
                                        //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
                                    }
                                    else
                                    {
                                        var positionObj = ModelService.GetChildList<TN_MPS1300>(p => p.ProductLotNo == stockObj1.ProductLotNo).OrderBy(p => p.InQty).LastOrDefault();
                                        string whCode = null;
                                        string positionCode = null;
                                        if (positionObj != null)
                                        {
                                            whCode = positionObj.WhCode;
                                            positionCode = positionObj.PositionCode;
                                        }
                                        TN_ORD1201 newobj = new TN_ORD1201()
                                        {
                                            OutNo = newObj.OutNo,
                                            OutSeq = newObj.TN_ORD1201List.Count == 0 ? 1 : newObj.TN_ORD1201List.Max(o => o.OutSeq) + 1,
                                            ItemCode = stockObj1.ItemCode,
                                            ProductLotNo = stockObj1.ProductLotNo,
                                            OutQty = c.Substring(c.IndexOf('(') + 1, c.IndexOf(')') - 1 - c.IndexOf('(')).GetDecimalNullToZero(), //stockObj.StockQty.GetDecimalNullToZero(),
                                            WhCode = whCode,
                                            PositionCode = positionCode,
                                        };

                                       
                                        DetailGridBindingSource.Add(newobj);
                                        newObj.TN_ORD1201List.Add(newobj);
                                        DetailGridExControl.BestFitColumns();
                                    }
                                }
                             
                            }
                            if (returnedPart.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                            {
                                if (banf == "N")
                                {
                                    ModelServiceBAN.Insert(bannewobj);
                                }
                                else { ModelServiceBAN.Update(bannewobj); }
                            }
                        } 
                    }
                }
                finally
                {
                    textEdit.EditValue = "";
                    e.Handled = true;
                }
            }
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(MasterGridExControl,"Memo", UserRight.HasEdit);
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            MasterGridExControl.MainGrid.SetRepositoryItemDateEdit("OutReqDate");
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
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => (p.Temp == MasterCodeSTR.WhCodeDivision_WAN || p.Temp == null) && p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p =>  p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionCode", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName");

            var PositionCodeEdit = DetailGridExControl.MainGrid.Columns["PositionCode"].ColumnEdit as RepositoryItemSearchLookUpEdit;
            PositionCodeEdit.Popup += PositionCodeEdit_Popup;
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OutNo");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            VI_PROD_STOCK_PRODUCT_LOT_NO_LIST.Clear();

            detailFirstLoadCheck = false;

            ModelService.ReLoad();
            ModelServiceBAN.ReLoad();

            string customerCode = lup_CustomerCode.EditValue.GetNullToEmpty();
            var selectedIndex = cbo_SearchDivision.SelectedIndex;

            if (selectedIndex == 0)
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= dateOrderDate.DateFrEdit.DateTime
                                                                            && p.OutDate <= dateOrderDate.DateToEdit.DateTime)
                                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                         )
                                                                         .OrderBy(p => p.OutDate)
                                                                         .ToList();
            }
            else
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OutReqDate >= dateOrderDate.DateFrEdit.DateTime
                                                                            && p.OutReqDate <= dateOrderDate.DateToEdit.DateTime)
                                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                         )
                                                                         .OrderBy(p => p.OutReqDate)
                                                                         .ToList();
            }
            MasterGridExControl.DataSource = MasterGridBindingSource;

            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
            SetRefreshMessage(MasterGridExControl);
        }

        protected override void MasterFocusedRowChanged()
        {
            DetailGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            VI_PROD_STOCK_PRODUCT_LOT_NO_LIST.Clear();

            detailFirstLoadCheck = false;

            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null)
            {
                return;
            }

            DetailGridBindingSource.DataSource = obj.TN_ORD1201List.OrderBy(o => o.OutSeq).ToList();
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ItemCode = new SqlParameter("@ItemCode", obj.ItemCode);
                var CheckQty = new SqlParameter("@CheckQty", obj.TN_ORD1100.DelivQty - obj.SumOutQty.GetDecimalNullToZero());
                var result = context.Database.SqlQuery<TEMP_AUTO_SHIPMENT>("USP_GET_AUTO_SHIPMENT_LIST_NEW @ItemCode ,@CheckQty", ItemCode, CheckQty).OrderBy(p => p.RowIndex).ToList();
                gridEx3BindingSource.DataSource = result;
            }
            gridEx3.DataSource = gridEx3BindingSource;
            gridEx3.BestFitColumns();

            VI_PROD_STOCK_PRODUCT_LOT_NO_LIST.AddRange(ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ItemCode == obj.ItemCode&&p.StockQty>0).ToList());
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();
            ModelServiceBAN.Save();
            ModelService.Save();
            banf = "T";
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
                newObj.OutReqDate = DateTime.Today;
                newObj.OutId = GlobalVariable.LoginId;
                newObj.Memo = returnedPart.Memo;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.OrderSeq == returnedPart.OrderSeq && p.DelivNo == returnedPart.DelivNo).First();
                newObj.BillDate = newObj.TN_ORD1100.TN_ORD1001.EndMonthDate;

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

        private void BarButtonBillDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!UserRight.HasEdit) return;

            if (MasterGridBindingSource == null || MasterGridBindingSource.DataSource == null) return;

            var list = MasterGridBindingSource.DataSource as List<TN_ORD1200>;
            var checkList = list.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;

            var date = (DateTime)(MasterGridExControl.BarTools.ItemLinks[5].Item as BarEditItem).EditValue;
            var billDate = new DateTime(date.Year, date.Month, 1);

            foreach (var v in checkList)
            {
                v.BillDate = billDate;
            }

            MasterGridExControl.BestFitColumns();
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
                newObj.CustomerCode = returnedPart.CustomerCode; //DateTime.Today;
                newObj.OutId = GlobalVariable.LoginId;
                newObj.Memo = returnedPart.Memo;
                newObj.TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == newObj.ItemCode).First();
                newObj.TN_ORD1100 = ModelService.GetChildList<TN_ORD1100>(p => p.OrderNo == returnedPart.OrderNo && p.OrderSeq == returnedPart.OrderSeq && p.DelivNo == returnedPart.DelivNo).First();
                newObj.OutPlanDate = ModelService.GetChildList<TN_ORD1103>(p => p.OutRepNo == returnedPart.OutRepNo).First().OutDatePlan;
                newObj.OutDate = DateTime.Today;
                newObj.OutReqDate = ModelService.GetChildList<TN_ORD1103>(p => p.OutRepNo == returnedPart.OutRepNo).First().OutDatePlan;
                newObj.OutPlanQty = returnedPart.OutPlanQty;
                newObj.BillDate = newObj.TN_ORD1100.TN_ORD1001.EndMonthDate;

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
                TN_BAN1100 BAN = ModelServiceBAN.GetList(p => p.Temp == tn.OutNo && p.Memo == "제품출고").FirstOrDefault();
                if (BAN != null)
                {
                    ModelServiceBAN.Delete(BAN);
                }
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
            if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
            {
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_BANPROD_STOCK, param, BANAddDetailCallBack);
                form.ShowPopup(true);
            }
            else
            {
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_PROD_STOCK, param, AddDetailCallBack);
                form.ShowPopup(true);
            }
        }

        private void AddDetailCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            if (e == null) return;

            detailFirstLoadCheck = false;
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
                };
                DetailGridBindingSource.Add(newobj);
                obj.TN_ORD1201List.Add(newobj);
            }
            DetailGridExControl.BestFitColumns();
        }
        private void BANAddDetailCallBack(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            if (e == null) return;

            detailFirstLoadCheck = false;
            var returnList = (List<VI_BAN_STOCK_PRODUCT_LOT_NO>)e.Map.GetValue(PopupParameter.ReturnObject);
            TN_BAN1100 bannewobj = ModelServiceBAN.GetList(p => p.Temp == obj.OutNo).FirstOrDefault();
            if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
            {
                if (bannewobj == null)
                {
                    banf = "N";
                    bannewobj = new TN_BAN1100()
                    {
                        OutNo = DbRequestHandler.GetSeqMonth("BOUT"),
                        OutDate = DateTime.Today,
                        OutId = GlobalVariable.LoginId,
                        Temp = obj.OutNo,
                        Memo = "제품출고"
                    };
                }
            }
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
                };
                if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                {
                    var TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == v.ProductLotNo).FirstOrDefault();
                    var newObjBan = new TN_BAN1101();
                    newObjBan.OutNo = bannewobj.OutNo;
                    newObjBan.OutSeq = bannewobj.TN_BAN1101List.Count == 0 ? 1 : bannewobj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                    newObjBan.InNo = TN_BAN1001.InNo;
                    newObjBan.InSeq = TN_BAN1001.InSeq;
                    newObjBan.ItemCode = TN_BAN1001.ItemCode;
                    newObjBan.OutQty = v.StockQty.GetDecimalNullToZero();
                    newObjBan.OutLotNo = v.ProductLotNo;
                    newObjBan.InLotNo = v.ProductLotNo;
                    newObjBan.Temp = newobj.OutNo;
                    newObjBan.Temp1 = newobj.OutSeq.ToString();
                    newObjBan.NewRowFlag = "Y";
                    newObjBan.TN_BAN1100 = bannewobj;
                    newObjBan.TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == v.ProductLotNo).FirstOrDefault();
                    newObjBan.TN_STD1100 = ModelServiceBAN.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                    bannewobj.TN_BAN1101List.Add(newObjBan);
                }
                DetailGridBindingSource.Add(newobj);
                obj.TN_ORD1201List.Add(newobj);
            }
            if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
            {
                if (banf == "N")
                {
                    ModelServiceBAN.Insert(bannewobj);
                }
                else { ModelServiceBAN.Update(bannewobj); }
            }
            DetailGridExControl.BestFitColumns();
        }
        private void GridEx3_ActAddRowClicked(object sender, ItemClickEventArgs e)
        {
            TN_ORD1200 obj = MasterGridBindingSource.Current as TN_ORD1200;
            if (obj == null) return;

            var grid3List = gridEx3BindingSource.List as List<TEMP_AUTO_SHIPMENT>;
            if (grid3List.Count > 0)
            {
                if (MessageBoxHandler.Show("출고 일괄 확정 시 모든 작업이 저장됩니다. 정말로 확정하시겠습니까?", "경고", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TN_BAN1100 bannewobj = ModelServiceBAN.GetList(p => p.Temp == obj.OutNo).FirstOrDefault();
                    if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                    {
                        if (bannewobj == null)
                        {
                            banf = "N";
                            bannewobj = new TN_BAN1100()
                            {
                                OutNo = DbRequestHandler.GetSeqMonth("BOUT"),
                                OutDate = DateTime.Today,
                                OutId = GlobalVariable.LoginId,
                                Temp = obj.OutNo,
                                Memo = "제품출고"
                            };
                        }
                    }

                    foreach (var v in grid3List)
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
                        };
                        if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                        {
                            var TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == v.ProductLotNo).FirstOrDefault();
                            var newObjBan = new TN_BAN1101();
                            newObjBan.OutNo = bannewobj.OutNo;
                            newObjBan.OutSeq = bannewobj.TN_BAN1101List.Count == 0 ? 1 : bannewobj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                            newObjBan.InNo = TN_BAN1001.InNo;
                            newObjBan.InSeq = TN_BAN1001.InSeq;
                            newObjBan.ItemCode = TN_BAN1001.ItemCode;
                            newObjBan.OutQty = v.StockQty.GetDecimalNullToZero();
                            newObjBan.OutLotNo = v.ProductLotNo;
                            newObjBan.InLotNo = v.ProductLotNo;
                            newObjBan.Temp = newobj.OutNo;
                            newObjBan.Temp1 = newobj.OutSeq.ToString();
                            newObjBan.NewRowFlag = "Y";
                            newObjBan.TN_BAN1100 = bannewobj;
                            newObjBan.TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == v.ProductLotNo).FirstOrDefault();
                            newObjBan.TN_STD1100 = ModelServiceBAN.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).FirstOrDefault();
                            bannewobj.TN_BAN1101List.Add(newObjBan);
                        }
                        DetailGridBindingSource.Add(newobj);
                        obj.TN_ORD1201List.Add(newobj);
                    }
                    if (obj.TN_STD1100.TopCategory.GetNullToEmpty() == "A01")
                    {
                        if (banf == "N")
                        {
                            ModelServiceBAN.Insert(bannewobj);
                        }
                        else { ModelServiceBAN.Update(bannewobj); }
                    }
                    ActSave();
                }
            }
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
                        var banstock = ModelService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == productLotNo).FirstOrDefault();
                        if (banstock == null || banstock.StockQty == 0)
                        {
                           
                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
                        }
                        else
                        {
                          TN_BAN1100 bannewobj = ModelServiceBAN.GetList(p => p.Temp == masterObj.OutNo).FirstOrDefault();
                            if (bannewobj == null )
                            {
                                banf = "N";
                                bannewobj = new TN_BAN1100()
                                {
                                    OutNo = DbRequestHandler.GetSeqMonth("BOUT"),
                                    OutDate = DateTime.Today,
                                    OutId = GlobalVariable.LoginId,
                                    Temp = masterObj.OutNo,
                                    Memo = "제품출고"
                                };
                            }
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
                                ItemCode = banstock.ItemCode,
                                ProductLotNo = banstock.ProductLotNo,
                                OutQty = banstock.StockQty.GetDecimalNullToZero(),
                                WhCode = whCode,
                                PositionCode = positionCode,
                            };
                            var TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == banstock.ProductLotNo).FirstOrDefault();
                            var newObjBan = new TN_BAN1101();
                            newObjBan.OutNo = bannewobj.OutNo;
                            newObjBan.OutSeq = bannewobj.TN_BAN1101List.Count == 0 ? 1 : bannewobj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                            newObjBan.InNo = TN_BAN1001.InNo;
                            newObjBan.InSeq = TN_BAN1001.InSeq;
                            newObjBan.ItemCode = TN_BAN1001.ItemCode;
                            newObjBan.OutQty = banstock.StockQty.GetDecimalNullToZero();
                            newObjBan.OutLotNo = banstock.ProductLotNo;
                            newObjBan.InLotNo = banstock.ProductLotNo;
                            newObjBan.NewRowFlag = "Y";
                            newObjBan.Temp = newobj.OutNo;
                            newObjBan.Temp1 = newobj.OutSeq.ToString();
                            newObjBan.TN_BAN1100 = bannewobj;
                            newObjBan.TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == banstock.ProductLotNo).FirstOrDefault();
                            newObjBan.TN_STD1100 = ModelServiceBAN.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();
                            bannewobj.TN_BAN1101List.Add(newObjBan);
                            if (banf == "N")
                            {
                                ModelServiceBAN.Insert(bannewobj);
                            }
                            else { ModelServiceBAN.Update(bannewobj); }
                            DetailGridBindingSource.Add(newobj);
                            masterObj.TN_ORD1201List.Add(newobj);
                            DetailGridExControl.BestFitColumns();
                        }

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
                        var banstock = ModelService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == stockObj.ProductLotNo).FirstOrDefault();
                        if (banstock == null || banstock.StockQty == 0)
                        {

                            MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
                        }
                        else
                        {
                            TN_BAN1100 bannewobj = ModelServiceBAN.GetList(p => p.Temp == masterObj.OutNo).FirstOrDefault();
                            if (bannewobj == null)
                            {
                                banf = "N";
                                bannewobj = new TN_BAN1100()
                                {
                                    OutNo = DbRequestHandler.GetSeqMonth("BOUT"),
                                    OutDate = DateTime.Today,
                                    OutId = GlobalVariable.LoginId,
                                    Temp = masterObj.OutNo,
                                    Memo = "제품출고"
                                };
                            }
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
                                ItemCode = banstock.ItemCode,
                                ProductLotNo = banstock.ProductLotNo,
                                OutQty = banstock.StockQty.GetDecimalNullToZero(),
                                WhCode = whCode,
                                PositionCode = positionCode,
                            };
                            var TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == banstock.ProductLotNo).FirstOrDefault();
                            var newObjBan = new TN_BAN1101();
                            newObjBan.OutNo = bannewobj.OutNo;
                            newObjBan.OutSeq = bannewobj.TN_BAN1101List.Count == 0 ? 1 : bannewobj.TN_BAN1101List.Max(o => o.OutSeq) + 1;
                            newObjBan.InNo = TN_BAN1001.InNo;
                            newObjBan.InSeq = TN_BAN1001.InSeq;
                            newObjBan.ItemCode = TN_BAN1001.ItemCode;
                            newObjBan.OutQty = banstock.StockQty.GetDecimalNullToZero();
                            newObjBan.OutLotNo = banstock.ProductLotNo;
                            newObjBan.InLotNo = banstock.ProductLotNo;
                            newObjBan.NewRowFlag = "Y";
                            newObjBan.Temp = newobj.OutNo;
                            newObjBan.Temp1 = newobj.OutSeq.ToString();
                            newObjBan.TN_BAN1100 = bannewobj;
                            newObjBan.TN_BAN1001 = ModelServiceBAN.GetChildList<TN_BAN1001>(p => p.InLotNo == banstock.ProductLotNo).FirstOrDefault();
                            newObjBan.TN_STD1100 = ModelServiceBAN.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();
                            bannewobj.TN_BAN1101List.Add(newObjBan);
                            if (banf == "N")
                            {
                                ModelServiceBAN.Insert(bannewobj);
                            }
                            else { ModelServiceBAN.Update(bannewobj); }
                            DetailGridBindingSource.Add(newobj);
                            masterObj.TN_ORD1201List.Add(newobj);
                            DetailGridExControl.BestFitColumns();
                        }
                       // MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_34), LabelConvert.GetLabelText("StockQty")));
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
            string seq = delobj.OutSeq.ToString();
            TN_BAN1101 bans = ModelServiceBAN.GetChildList<TN_BAN1101>(p => p.Temp == delobj.OutNo && p.Temp1 == seq).FirstOrDefault();
            if (bans != null)
            {
                ModelServiceBAN.RemoveChild<TN_BAN1101>(bans);
            }
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
            
            var mainobj =MasterGridBindingSource.Current as  TN_ORD1200;
            var detailObj = DetailGridBindingSource.Current as TN_ORD1201;
            if (detailObj == null) return;
          
            if (e.Column.FieldName == "WhCode")
            {
                detailObj.PositionCode = null;
            }

            if (e.Column.FieldName == "OutQty")
            {
                if (mainobj.TN_STD1100.TopCategory==MasterCodeSTR.TopCategory_BAN)
                {
                    var stockObj = ModelService.GetChildList<VI_BAN_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == detailObj.ProductLotNo).Where(p => p.StockQty > 0).OrderBy(p => p.ProductLotNo).FirstOrDefault();
                   
                    string lsseq = detailObj.OutSeq.ToString();
                    TN_BAN1100 ban = ModelServiceBAN.GetList(p => p.Temp == detailObj.OutNo).FirstOrDefault();//ModelServiceBAN.GetChildList<TN_BAN1101>(p => p.Temp == detailObj.OutNo && p.Temp1 == lsseq).FirstOrDefault();
                        TN_BAN1101 bans = ban.TN_BAN1101List.Where(p => p.Temp == ban.Temp && p.Temp1 == lsseq).FirstOrDefault(); 
                    if (bans != null)
                    {
                        bans.OutQty = detailObj.OutQty;// gridEx2.MainGrid.MainView.GetRowCellValue(e.RowHandle, "OutQty").GetDecimalNullToZero();
                    }
                    int rowcnt = gridEx2.MainGrid.MainView.RowCount;

                    decimal tot_qut_qty = 0;

                    for (int i = 0; i < rowcnt; i++)
                    {
                        tot_qut_qty = tot_qut_qty + Convert.ToDecimal(gridEx2.MainGrid.MainView.GetRowCellValue(i, "OutQty"));
                    }
                    

                    if (stockObj.StockQty < tot_qut_qty)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_120)));
                        for (int i = 0; i < rowcnt; i++)
                        {
                            gridEx2.MainGrid.MainView.SetRowCellValue(i, "OutQty", 0);

                        }
                        if (bans != null)
                        {
                            bans.OutQty = 0;
                        }
                    }
                    if (bans != null)
                     {

                        ModelServiceBAN.Update(ban);
                       }
                     }
                else
                { 
                         var stockObj = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_LOT_NO>(p => p.ProductLotNo == detailObj.ProductLotNo).Where(p => p.StockQty > 0).OrderBy(p => p.ProductLotNo).FirstOrDefault();
                       
                         int rowcnt = gridEx2.MainGrid.MainView.RowCount;
                       
                         decimal tot_qut_qty = 0;
                       
                         for (int i = 0; i < rowcnt; i++)
                         {
                        if (gridEx2.MainGrid.MainView.GetRowCellValue(i, "ProductLotNo").ToString() == detailObj.ProductLotNo)
                        {
                            tot_qut_qty = tot_qut_qty + Convert.ToDecimal(gridEx2.MainGrid.MainView.GetRowCellValue(i, "OutQty"));
                        }
                    }
                       
                       
                         if (stockObj.StockQty < tot_qut_qty)
                         {
                             MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_120)));
                             for (int i = 0; i < rowcnt; i++)
                             {
                                 gridEx2.MainGrid.MainView.SetRowCellValue(i, "OutQty", 0);
                             }
                         }
                 }

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
                if (checkList.Any(p => p.NewRowFlag == "Y"))
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_99));
                    return;
                }

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

                    groupList.AddRange(checkList.GroupBy(p => new { p.TN_STD1100, p.TN_ORD1100, p.SumOutQty }).Select(p => new TradingStateGroupModel
                    {
                        ItemCode = p.Key.TN_STD1100.CustomerItemCode,
                        ItemName = p.Key.TN_STD1100.CustomerItemName,
                        Unit = p.Key.TN_STD1100.Unit,
                        Qty = p.Key.SumOutQty.GetDecimalNullToZero(),
                        Cost = costDisplayFlag ? (p.Key.TN_ORD1100.TN_ORD1001.OrderCost == null ? p.Key.TN_STD1100.Cost.GetDecimalNullToZero() : p.Key.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                        Count = p.Count()
                    }));

                    var unitList = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit);
                    var printList = new List<TEMP_TRADING_DETAIL>();
                    int n = 0;
                    foreach (var v in groupList)
                    {
                        n++;
                        var newObj = new TEMP_TRADING_DETAIL()
                        {
                            No = n,
                            ItemCode = v.ItemCode,
                            ItemName = v.ItemName,
                            Unit = unitList.Where(p => p.CodeVal == v.Unit).First().CodeName,
                            Qty = v.Qty,
                            Cost = v.Cost,
                        };
                        newObj.Amt = newObj.Qty * newObj.Cost;
                        printList.Add(newObj);
                    }

                    if (printList.Count > 0)
                    {
                        var ToCustomerCode = checkList.First().CustomerCode;
                        var ToCustomerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == ToCustomerCode).FirstOrDefault();
                        var FrCustomerObj = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();

                        var mainReport = new XRORD1200_TRADING_NEW();

                        var rowCount = 8;
                        var valueCount = printList.Count / rowCount;
                        var modCount = printList.Count % rowCount;

                        if (valueCount == 0)
                        {
                            var report = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), DateTime.Today.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.OrderBy(p => p.No).ToList());
                            report.CreateDocument();
                            mainReport.Pages.AddRange(report.Pages);
                        }
                        else if (valueCount > 0 && modCount == 0)
                        {
                            for (int i = 1; i <= valueCount; i++)
                            {
                                var min = i * 8 - 7;
                                var max = i * 8;
                                var report = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), DateTime.Today.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.Where(p => p.No >= min && p.No <= max).OrderBy(p => p.No).ToList());
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= valueCount; i++)
                            {
                                var min = i * 8 - 7;
                                var max = i * 8;
                                var report = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), DateTime.Today.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.Where(p => p.No >= min && p.No <= max).OrderBy(p => p.No).ToList());
                                report.CreateDocument();
                                mainReport.Pages.AddRange(report.Pages);
                            }

                            var report2 = new XRORD1200_TRADING_NEW(DateTime.Today.ToShortDateString(), DateTime.Today.ToShortDateString(), ToCustomerObj, FrCustomerObj, printList.Where(p => p.No > valueCount * 8).OrderBy(p => p.No).ToList());
                            report2.CreateDocument();
                            mainReport.Pages.AddRange(report2.Pages);
                        }

                        mainReport.PrintingSystem.ShowMarginsWarning = false;
                        mainReport.ShowPrintStatusDialog = false;
                        mainReport.ShowPreview();

                        foreach (var v in checkList)
                            v._Check = "N";

                        DetailGridExControl.BestFitColumns();
                    }
                }
                catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                finally
                {
                    WaitHandler.CloseWait();
                }
                //try
                //{
                //    WaitHandler.ShowWait();
                //    var groupList = new List<TradingStateGroupModel>();
                //    var cultureIndex = DataConvert.GetCultureIndex();

                //    if (cultureIndex == 1)
                //    {
                //        foreach (var v in checkList)
                //        {
                //            groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemName, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
                //            {
                //                ItemCode = p.Key.ItemCode,
                //                ItemName = p.Key.ItemName,
                //                Spec = p.Key.CombineSpec,
                //                Qty = p.Key.OutQty.GetDecimalNullToZero(),
                //                Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                //                Count = p.Count()
                //            }));
                //        }
                //    }
                //    else if (cultureIndex == 2)
                //    {
                //        foreach (var v in checkList)
                //        {
                //            groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNameENG, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
                //            {
                //                ItemCode = p.Key.ItemCode,
                //                ItemName = p.Key.ItemNameENG,
                //                Spec = p.Key.CombineSpec,
                //                Qty = p.Key.OutQty.GetDecimalNullToZero(),
                //                Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                //                Count = p.Count()
                //            }));
                //        }
                //    }
                //    else
                //    {
                //        foreach (var v in checkList)
                //        {
                //            groupList.AddRange(v.TN_ORD1201List.GroupBy(p => new { p.ItemCode, p.TN_ORD1200.TN_STD1100.ItemNameCHN, p.TN_STD1100.CombineSpec, p.OutQty }).Select(p => new TradingStateGroupModel
                //            {
                //                ItemCode = p.Key.ItemCode,
                //                ItemName = p.Key.ItemNameCHN,
                //                Spec = p.Key.CombineSpec,
                //                Qty = p.Key.OutQty.GetDecimalNullToZero(),
                //                Cost = costDisplayFlag ? (v.TN_ORD1100.TN_ORD1001.OrderCost == null ? v.TN_STD1100.Cost.GetDecimalNullToZero() : v.TN_ORD1100.TN_ORD1001.OrderCost.GetDecimalNullToZero()) : 0,
                //                Count = p.Count()
                //            }));
                //        }
                //    }

                //    var distinctList = groupList.GroupBy(p => new { p.ItemCode, p.ItemName, p.Spec, p.Qty, p.Cost }).Select(p => new TradingStateGroupModel
                //    {
                //        ItemCode = p.Key.ItemCode,
                //        ItemName = p.Key.ItemName,
                //        Spec = p.Key.Spec,
                //        Qty = p.Key.Qty.GetDecimalNullToZero(),
                //        Cost = p.Key.Cost,
                //        Count = p.Sum(c => c.Count)
                //    });

                //    var _Date = checkList.Max(p => p.OutDate);
                //    var printList = new List<TEMP_TRADING_DETAIL>();
                //    foreach (var v in distinctList)
                //    {
                //        var newObj = new TEMP_TRADING_DETAIL()
                //        {
                //            Date = ((DateTime)_Date).ToString("MM.dd"),
                //            ItemCode = v.ItemCode,
                //            ItemName = v.ItemName,
                //            Spec = v.Spec,
                //            //Spec = string.Format("({0}*{1}BOX)", v.Qty, v.Count),
                //            Qty = v.Qty * v.Count,
                //            Cost = v.Cost,
                //        };
                //        //newObj.Cost = costDisplayFlag ? checkList.Where(p => p.ItemCode == v.ItemCode).First().TN_STD1100.Cost.GetDecimalNullToZero() : 0
                //        newObj.SupplyCost = newObj.Qty * newObj.Cost;
                //        newObj.TaxCost = (newObj.Qty * newObj.Cost) / 10;

                //        printList.Add(newObj);
                //    }
                //    if (printList.Count > 0)
                //    {
                //        var inCustomerCode = checkList.First().CustomerCode;
                //        var inCustomer = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == inCustomerCode).FirstOrDefault();
                //        var ourCustomer = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();
                //        var report = new XRORD1200_TRADING(((DateTime)_Date).ToShortDateString(), ourCustomer, inCustomer, printList);
                //        report.CreateDocument();
                //        report.PrintingSystem.ShowMarginsWarning = false;
                //        report.ShowPrintStatusDialog = false;
                //        report.ShowPreview();
                //    }
                //}
                //catch (Exception ex) { MessageBoxHandler.ErrorShow(ex.Message); }
                //finally
                //{
                //    WaitHandler.CloseWait();
                //}
            }
        }

        private void MainView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (!detailFirstLoadCheck)
            {
                var view = sender as GridView;
                if (e.Column.FieldName == "CustomerLotNo")
                {
                    var lotNo = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProductLotNo").GetNullToEmpty();
                    if (!lotNo.IsNullOrEmpty())
                    {
                        var obj = ModelService.GetChildList<TN_MPS1201>(p => p.ProductLotNo == lotNo).FirstOrDefault();
                        if (obj != null)
                        {
                            e.DisplayText = obj.TN_MPS1200.Temp1;
                        }
                    }
                }
                //detailFirstLoadCheck = true;
            }
        }

        private void GridEx3MainView_CustomColumnDisplayText1(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty2")
            {
                var productLotNo = gridEx3.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ProductLotNo").GetNullToEmpty();
                var stockObj = VI_PROD_STOCK_PRODUCT_LOT_NO_LIST.Where(p => p.ProductLotNo == productLotNo).FirstOrDefault();
                e.DisplayText = stockObj == null ? "0" : stockObj.StockQty.ToString("#,0.##");
            }
        }

        private class TradingStateGroupModel
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Spec { get; set; }
            public string Unit { get; set; }
            public decimal Qty { get; set; }
            public decimal Cost { get; set; }
            public decimal Count { get; set; }
        }
    }
}
