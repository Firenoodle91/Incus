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
using HKInc.Ui.Model.Domain.VIEW;
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
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Utils.Common;
using DevExpress.XtraCharts;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 수주관리
    /// </summary>
    public partial class XFORD1000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<TN_ORD1000> ModelService = (IService<TN_ORD1000>)ProductionFactory.GetDomainService("TN_ORD1000");
        private List<VI_PRODBAN_STOCK_ITEM> StockList = new List<VI_PRODBAN_STOCK_ITEM>();
        private List<VI_ITEM_AVG_OUT_QTY> AvgSpendQtyList = new List<VI_ITEM_AVG_OUT_QTY>();
        BindingSource gridEx3BindingSource = new BindingSource();
        List<Holiday> holidayList;

        public XFORD1000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

            dt_OrderDate.DateFrEdit.DateTime = DateTime.Today;
            dt_OrderDate.DateToEdit.DateTime = DateTime.Today;

            MasterGridExControl.MainGrid.MainView.RowClick += MainView_RowClick;

            DetailGridExControl.MainGrid.MainView.CellValueChanged += MainView_CellValueChanged;
            DetailGridExControl.MainGrid.MainView.ShowingEditor += MainView_ShowingEditor;
            DetailGridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
            DetailGridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;

            btn_OrderStatus.Click += Btn_OrdStatus_Click;
        }

        protected override void InitCombo()
        {
            var list = ModelService.GetChildList<VI_BUSINESS_MANAGEMENT_USER>(p => true).ToList();
            lup_OrderId.SetDefault(true, "LoginId", "UserName", list);
            lup_OrderId.EditValueChanged += Lup_Manager_EditValueChanged;
            if (list.Any(p => p.LoginId == GlobalVariable.LoginId))
                lup_OrderId.EditValue = GlobalVariable.LoginId;

            lup_OrderCustomerCode.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Sales || p.CustomerType == null)).ToList());
            lup_OrderCustomerCode.Popup += Lup_OrderCustomerCode_Popup;

            btn_OrderStatus.Text = LabelConvert.GetLabelText("OrderStatus") + "(&O)";

            lacWorkRecordList.Expanded = false;
            lacWorkRecordList.AppearanceGroup.Font = new Font(lacWorkRecordList.AppearanceGroup.Font.FontFamily, 11f);
        }

        protected override void InitGrid()
        {
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("OrderDate", LabelConvert.GetLabelText("OrderDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderCustomerCode", LabelConvert.GetLabelText("OrderCustomerCode"), false);
            MasterGridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("OrderCustomer"));
            MasterGridExControl.MainGrid.AddColumn("OrderManagerName", LabelConvert.GetLabelText("OrderManagerName"));
            MasterGridExControl.MainGrid.AddColumn("OrderDueDate", LabelConvert.GetLabelText("DueDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            MasterGridExControl.MainGrid.AddColumn("OrderId", LabelConvert.GetLabelText("OrderId"));
            MasterGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

            DetailGridExControl.MainGrid.CheckBoxMultiSelect(UserRight.HasEdit, "OrderSeq", UserRight.HasEdit);
            DetailGridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            DetailGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            DetailGridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("Seq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"), false);
            DetailGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"), false);
            DetailGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            DetailGridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            DetailGridExControl.MainGrid.AddColumn("AvgSpendQty", LabelConvert.GetLabelText("AvgSpendQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            DetailGridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"));
            //DetailGridExControl.MainGrid.AddColumn("WorkNoDate", LabelConvert.GetLabelText("PublishDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            //DetailGridExControl.MainGrid.AddColumn("PlanStartDate", LabelConvert.GetLabelText("StartDueDate"));
            //DetailGridExControl.MainGrid.AddColumn("PlanEndDate", LabelConvert.GetLabelText("FinishDueDate"));
            DetailGridExControl.MainGrid.AddColumn("EndMonthDate", LabelConvert.GetLabelText("EndMonthDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            DetailGridExControl.MainGrid.AddColumn("WorkEndMonthDate", LabelConvert.GetLabelText("WorkEndMonthDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            DetailGridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            DetailGridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,##0.##");

            //DetailGridExControl.MainGrid.AddColumn("PlanWorkQty", LabelConvert.GetLabelText("PlanWorkQty"));
            DetailGridExControl.MainGrid.AddColumn("Temp", LabelConvert.GetLabelText("WorkOrderFlag"));
            DetailGridExControl.MainGrid.AddColumn("OutConfirmFlag", LabelConvert.GetLabelText("OutConfirmFlag"));
            DetailGridExControl.MainGrid.AddColumn("TurnKeyFlag", LabelConvert.GetLabelText("TurnKeyFlag"));
            DetailGridExControl.MainGrid.AddColumn("OutRepNo", LabelConvert.GetLabelText("OutRepNo"), HorzAlignment.Center, true);
            //DetailGridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            //DetailGridExControl.MainGrid.AddColumn("WorkPrintFlag", LabelConvert.GetLabelText("WorkPrintFlag"));

            DetailGridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));
            DetailGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "EndMonthDate", "_Check", "OrderQty", "OrderCost", "Memo", "OutConfirmFlag", "TurnKeyFlag", "Temp");

            var barOrderStatus = new DevExpress.XtraBars.BarButtonItem();
            barOrderStatus.Id = 6;
            barOrderStatus.ImageOptions.Image = IconImageList.GetIconImage("chart/3dcylinder");
            barOrderStatus.ItemShortcut = new DevExpress.XtraBars.BarShortcut((Keys.Alt | Keys.U));
            barOrderStatus.Name = "barOrderStatus";
            barOrderStatus.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            barOrderStatus.ShortcutKeyDisplayString = "Alt+U";
            barOrderStatus.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            barOrderStatus.Caption = LabelConvert.GetLabelText("OrderStatus") + "[Alt+U]";
            barOrderStatus.ItemClick += BarOrderStatus_ItemClick;
            DetailGridExControl.BarTools.AddItem(barOrderStatus);

            LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1001>(DetailGridExControl);

            gridEx3.SetToolbarVisible(false);
            gridEx3.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            gridEx3.MainGrid.AddColumn("WorkNoDate", LabelConvert.GetLabelText("PublishDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            gridEx3.MainGrid.AddColumn("PlanWorkQty", LabelConvert.GetLabelText("PlanWorkQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            gridEx3.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            gridEx3.MainGrid.AddColumn("PackQty", LabelConvert.GetLabelText("PackQty2"), HorzAlignment.Far, FormatType.Numeric, "N0");
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OrderId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            MasterGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(MasterGridExControl, "Memo");
            
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("EndMonthDate", DateFormat.Month, false, 80);
            DetailGridExControl.MainGrid.SetRepositoryItemDateEdit("WorkEndMonthDate", DateFormat.Month, false, 80);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TopCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.MiddleCategory", DbRequestHandler.GetCommCode(MasterCodeSTR.ItemType, 2), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommCode(MasterCodeSTR.CarType, 1), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderQty", DefaultBoolean.Default, "n0");
            DetailGridExControl.MainGrid.SetRepositoryItemSpinEdit("OrderCost", DefaultBoolean.Default, "n2");

            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("OutConfirmFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("TurnKeyFlag", "N");
            DetailGridExControl.MainGrid.SetRepositoryItemCheckEdit("Temp", "N");

            DetailGridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new Service.Controls.CommentGridButtonEdit(DetailGridExControl, "Memo", UserRight.HasEdit, 100, 30);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            gridEx3.BestFitColumns();
        }

        protected override void DataLoad()
        {
            if (!IsFirstLoaded)
            {
                holidayList = ModelService.GetChildList<Holiday>(p => true).ToList();
            }

            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("OrderNo", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();
            gridEx3.MainGrid.Clear();
            chartControl1.Series.Clear();

            StockList.Clear();
            AvgSpendQtyList.Clear();
            ModelService.ReLoad();

            string managerId = lup_OrderId.EditValue.GetNullToEmpty();
            string customerCode = lup_OrderCustomerCode.EditValue.GetNullToEmpty();
            MasterGridBindingSource.DataSource = ModelService.GetList(p => (p.OrderDate >= dt_OrderDate.DateFrEdit.DateTime
                                                                         && p.OrderDate <= dt_OrderDate.DateToEdit.DateTime)
                                                                         && (string.IsNullOrEmpty(managerId) ? true : p.OrderId == managerId)
                                                                         && (string.IsNullOrEmpty(customerCode) ? true : p.OrderCustomerCode == customerCode)
                                                                         && (p.OrderType == "양산")
                                                                      )
                                                                      .OrderBy(p => p.OrderNo)
                                                                      .ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            GridRowLocator.SetCurrentRow();
            MasterGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl);

            //재고량 가져오기
            StockList.AddRange(ModelService.GetChildList<VI_PRODBAN_STOCK_ITEM>(p => true).ToList());

            //평균소요량 가져오기
            AvgSpendQtyList.AddRange(ModelService.GetChildList<VI_ITEM_AVG_OUT_QTY>(p => true).ToList());
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                gridEx3.MainGrid.Clear();
                chartControl1.Series.Clear();
                return;
            }

            try
            {
                WaitHandler.ShowWait();
                DetailGridBindingSource.DataSource = masterObj.TN_ORD1001List.OrderBy(p => p.OrderSeq).ToList();
                DetailGridExControl.DataSource = DetailGridBindingSource;
                DetailGridExControl.BestFitColumns();
                MainView_FocusedRowChanged(null, null);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null)
            {
                gridEx3.MainGrid.Clear();
                chartControl1.Series.Clear();
                lacWorkRecordList.AppearanceGroup.ForeColor = Color.Black;
                lacWorkRecordList.AppearanceGroup.Font = new Font(lacWorkRecordList.AppearanceGroup.Font.FontFamily, 11f);
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ItemCode = new SqlParameter("@ItemCode", detailObj.ItemCode.GetNullToEmpty());
                var result = context.Database.SqlQuery<TEMP_WORK_RECORD>("USP_GET_WORK_RECORD @ItemCode", ItemCode).ToList();
                gridEx3BindingSource.DataSource = result.ToList();
            }
            gridEx3.DataSource = gridEx3BindingSource;
            gridEx3.BestFitColumns();

            if (gridEx3BindingSource.List.Count > 0)
            {
                lacWorkRecordList.AppearanceGroup.ForeColor = Color.Red;
                lacWorkRecordList.AppearanceGroup.Font = new Font(lacWorkRecordList.AppearanceGroup.Font.FontFamily, 11f);
            }
            else
            {
                lacWorkRecordList.AppearanceGroup.ForeColor = Color.Black;
                lacWorkRecordList.AppearanceGroup.Font = new Font(lacWorkRecordList.AppearanceGroup.Font.FontFamily, 11f);
            }

            SetChart(detailObj);
        }

        private void SetChart(TN_ORD1001 detailObj)
        {            
            try
            {
                WaitHandler.CloseWait();
                WaitHandler.ShowWait();
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var _frDate = new SqlParameter("@FrDate", DateTime.Today.AddYears(-1));
                    var _toDate = new SqlParameter("@ToDate", DateTime.Today);
                    var _itemCode = new SqlParameter("@ItemCode", detailObj.ItemCode);
                    var _customerCode = new SqlParameter("@CustomerCode", "");

                    var result = context.Database.SqlQuery<TEMP_ORDER_STATUS_CHART>("USP_GET_ORDER_STATUS_CHART @FrDate,@ToDate ,@ItemCode, @CustomerCode", _frDate, _toDate, _itemCode, _customerCode).OrderBy(p => p.Date).ToList();

                    chartControl1.DataSource = result;
                }

                chartControl1.SeriesDataMember = "Division";
                chartControl1.SeriesTemplate.ArgumentDataMember = "Date";
                chartControl1.SeriesTemplate.ValueDataMembers.AddRange("Qty");
                chartControl1.SeriesTemplate.Label.TextPattern = "{A}" + Environment.NewLine + "{V:#,###,###,##0.##}";
                chartControl1.SeriesTemplate.LabelsVisibility = DefaultBoolean.True;
                chartControl1.CrosshairEnabled = DefaultBoolean.True;
                chartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                chartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                chartControl1.Legend.Visibility = DefaultBoolean.False;

                //Show crosshair axis lines and crosshair axis labels to see format values of crosshair labels  
                chartControl1.CrosshairOptions.ShowArgumentLabels = true;
                chartControl1.CrosshairOptions.ShowValueLabels = true;
                chartControl1.CrosshairOptions.ShowValueLine = true;
                chartControl1.CrosshairOptions.ShowArgumentLine = true;

                // Specify the crosshair label pattern for both series. 
                chartControl1.SeriesTemplate.CrosshairLabelPattern = "{S}:{V:#,###,###,##0.##}";
                chartControl1.CrosshairOptions.GroupHeaderPattern = "{A}";

                var view1 = chartControl1.SeriesTemplate.View as SideBySideBarSeriesView;
                view1.BarDistance = 0.1;
                view1.BarDistanceFixed = 2;
                view1.BarWidth = 0.2;
                view1.EqualBarWidth = true;
                var label = chartControl1.SeriesTemplate.Label as SideBySideBarSeriesLabel;
                label.Position = BarSeriesLabelPosition.Top;

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

                diagram.AxisX.CrosshairAxisLabelOptions.Pattern = "{A:F0}";

                diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
                diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
                diagram.AxisY.Label.TextPattern = "{V:#,###,###,##0.##}";
                diagram.AxisY.Label.Font = new Font(@"맑은고딕", 9f);

                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;
                diagram.AxisX.VisualRange.Auto = true;
                diagram.AxisY.VisualRange.Auto = true;
            }
            finally { WaitHandler.CloseWait(); }
        }

        protected override void DataSave()
        {
            MasterGridExControl.MainGrid.PostEditor();
            DetailGridExControl.MainGrid.PostEditor();

            DetailGridBindingSource.EndEdit();
            MasterGridBindingSource.EndEdit();

            #region 품목단가이력 I/F 

            if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            {
                var masterList = MasterGridBindingSource.List as List<TN_ORD1000>;
                if (masterList.Count > 0)
                {
                    var editDetailList = masterList.Where(p => p.TN_ORD1001List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList();

                    if (masterList.Any(p => p.TN_ORD1001List.Any(c => c.OrderQty == 0)))
                    {
                        MessageBoxHandler.Show("수주수량이 없는 항목이 존재합니다. 확인해 주시기 바랍니다.");
                        return;
                    }

                    foreach (var v in masterList.Where(p => p.TN_ORD1001List.Any(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y")).ToList())
                    {
                        foreach (var d in v.TN_ORD1001List.Where(c => c.NewRowFlag == "Y" || c.EditRowFlag == "Y").ToList())
                        {
                            if (d.OrderCost > 0)
                            {
                                TN_STD1103 old = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == v.OrderCustomerCode && p.ItemCode == d.ItemCode && p.ChangeDate == DateTime.Today).FirstOrDefault();
                                if (old == null)
                                {
                                    var newObj = new TN_STD1103()
                                    {
                                        ItemCode = d.ItemCode,
                                        Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
                                        CustomerCode = v.OrderCustomerCode,
                                        ChangeDate = DateTime.Today,
                                        ChangeCost = d.OrderCost
                                    };
                                    d.TN_STD1100.TN_STD1103List.Add(newObj);
                                }
                                else
                                {
                                    old.ChangeCost = d.OrderCost;
                                    d.TN_STD1100.TN_STD1103List.Remove(old);
                                    d.TN_STD1100.TN_STD1103List.Add(old);
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            //#region 품목단가이력 I/F 
            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_ORD1000>;
            //    if (masterList.Count > 0)
            //    {
            //        foreach (var v in masterList.Where(p => p.TN_ORD1001List.Any(c => 1==1)).ToList())
            //        {
            //            foreach (var d in v.TN_ORD1001List.Where(p => 1==1))
            //            {
            //                TN_STD1103 old = ModelService.GetChildList<TN_STD1103>(p =>p.CustomerCode== v.OrderCustomerCode && p.ItemCode == d.ItemCode && p.ChangeDate == DateTime.Today).FirstOrDefault();
            //                if (old == null)
            //                {

            //                    var newObj = new TN_STD1103()
            //                    {
            //                        ItemCode = d.ItemCode,
            //                        Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
            //                        CustomerCode = v.OrderCustomerCode,
            //                        ChangeDate = DateTime.Today,
            //                        ChangeCost = d.OrderCost
            //                    };
            //                    d.TN_STD1100.TN_STD1103List.Add(newObj);
            //                }
            //                else {
            //                    old.ChangeCost = d.OrderCost;
            //                    d.TN_STD1100.TN_STD1103List.Remove(old);
            //                    d.TN_STD1100.TN_STD1103List.Add(old);
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            //#region 품목단가이력 I/F 
            //if (MasterGridBindingSource != null && MasterGridBindingSource.DataSource != null)
            //{
            //    var masterList = MasterGridBindingSource.List as List<TN_ORD1000>;
            //    if (masterList.Count > 0)
            //    {
            //        foreach (var v in masterList.Where(p => p.TN_ORD1001List.Any(c => c.NewRowFlag == "Y")).ToList())
            //        {
            //            foreach (var d in v.TN_ORD1001List.Where(p => p.NewRowFlag == "Y"))
            //            {
            //                var newObj = new TN_STD1103()
            //                {
            //                    ItemCode = d.ItemCode,
            //                    Seq = d.TN_STD1100.TN_STD1103List.Count == 0 ? 1 : d.TN_STD1100.TN_STD1103List.Max(p => p.Seq) + 1,
            //                    CustomerCode = v.OrderCustomerCode,
            //                    ChangeDate = DateTime.Today,
            //                    ChangeCost = d.OrderCost
            //                };
            //                d.TN_STD1100.TN_STD1103List.Add(newObj);
            //            }
            //        }
            //    }
            //}
            //#endregion

            ModelService.Save();
            DataLoad();
        }

        protected override void AddRowClicked()
        {
            if (!UserRight.HasEdit) return;

            // Close Query
            if (IsFormControlChanged)
            {
                DialogResult result = MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_1), LabelConvert.GetLabelText("Confirm"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Cancel)
                    return;
                else if (result == DialogResult.Yes)
                    ActSave();
            }

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.Service, ModelService);
            param.SetValue(PopupParameter.UserRight, UserRight);
            param.SetValue(PopupParameter.EditMode, PopupEditMode.New);
            param.SetValue(PopupParameter.Value_1, "양산");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, AddRowPopupCallback);

            form.ShowPopup(true);
        }

        private void AddRowPopupCallback(object sender, Utils.Common.PopupArgument e)
        {
            if (e == null || e.Map == null)
            {
                ActRefresh();
            }
            else
            {
                PopupDataParam.SetValue(PopupParameter.GridRowId_1, e.Map.GetValue(PopupParameter.GridRowId_1));
                ActRefresh();
            }
        }

        protected override void DeleteRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            if (masterObj.TN_ORD1001List.Count > 0)
            {
                MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OrderMasterInfo"), LabelConvert.GetLabelText("OrderDetailInfo"), LabelConvert.GetLabelText("OrderDetailInfo")));
                return;
            }
            ModelService.Delete(masterObj);
            MasterGridBindingSource.RemoveCurrent();
        }

        protected override void DetailAddRowClicked()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.UserRight, UserRight);
            //param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemWAN);
            param.SetValue(PopupParameter.Constraint, MasterCodeSTR.Contraint_ItemWAN_BAN);
            //Contraint_ItemWAN_BAN
            param.SetValue(PopupParameter.Value_1, masterObj.OrderCustomerCode);
            param.SetValue(PopupParameter.Value_2, "XFORD1000");
            IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_STD1100, param, AddOrderDetailCallback);
            form.ShowPopup(true);
        }

        private void AddOrderDetailCallback(object sender, HKInc.Utils.Common.PopupArgument e)
        {
            if (e == null) return;

            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;
            
            var returnList = (List<TN_STD1100>)e.Map.GetValue(PopupParameter.ReturnObject);
            foreach (var v in returnList)
            {
                var item = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == v.ItemCode).FirstOrDefault();
                TN_STD1103 obj1 = ModelService.GetChildList<TN_STD1103>(p => p.CustomerCode == masterObj.OrderCustomerCode && p.ItemCode ==  v.ItemCode).OrderByDescending(o => o.ChangeDate).FirstOrDefault();
                var newObj = new TN_ORD1001();
                newObj.OrderNo = masterObj.OrderNo;
                newObj.OrderSeq = masterObj.TN_ORD1001List.Count == 0 ? 1 : masterObj.TN_ORD1001List.Max(p => p.OrderSeq) + 1;
                newObj.ItemCode = item.ItemCode;
                newObj.TN_STD1100 = item;
                newObj.OrderCost = obj1 == null ? item.Cost.GetDecimalNullToZero() : obj1.ChangeCost;
                newObj.ProductionFlag = "N";
                newObj.OutConfirmFlag = "Y";
                newObj.TurnKeyFlag = "N";
                newObj.NewRowFlag = "Y";
                newObj.EndMonthDate = masterObj.OrderDueDate.AddDays(1 - masterObj.OrderDueDate.Day);
                newObj.Temp = "N";
                masterObj.TN_ORD1001List.Add(newObj);
                DetailGridBindingSource.Add(newObj);
            }
            if (returnList.Count > 0) SetIsFormControlChanged(true);
            DetailGridExControl.MainGrid.BestFitColumns();
        }

        protected override void DeleteDetailRow()
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var detailList = DetailGridBindingSource.List as List<TN_ORD1001>;
            if (detailList == null) return;

            var checkList = detailList.Where(p => p._Check == "Y").ToList();
            if (checkList.Count == 0) return;
            
            if (checkList.Any(p => !p.WorkNo.IsNullOrEmpty()))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_100));
                return;
            }

            if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_ORD1101List.Count > 0)))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_102));
                return;
            }

            if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_ORD1102List.Count > 0)))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_103));
                return;
            }

            if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_ORD1200List.Count > 0)))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_104));
                return;
            }

            if (checkList.Any(p => p.TN_ORD1100List.Any(c => c.TN_PUR1600List.Count > 0)))
            {
                MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_105));
                return;
            }

            foreach (var v in checkList)
            {
                masterObj.TN_ORD1001List.Remove(v);
                DetailGridBindingSource.Remove(v);
                DetailGridExControl.BestFitColumns();
            }

            //var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            //if (detailObj == null) return;

            //if (detailObj.TN_ORD1100List.Count > 0)
            //{
            //    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_48), LabelConvert.GetLabelText("OrderDetailInfo"), LabelConvert.GetLabelText("DelivInfo"), LabelConvert.GetLabelText("DelivInfo")));
            //    return;
            //}

            //masterObj.TN_ORD1001List.Remove(detailObj);
            //DetailGridBindingSource.RemoveCurrent();
            //DetailGridExControl.BestFitColumns();
        }

        private void MainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
                if (masterObj == null) return;

                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Service, ModelService);
                param.SetValue(PopupParameter.KeyValue, masterObj);
                param.SetValue(PopupParameter.UserRight, UserRight);
                param.SetValue(PopupParameter.EditMode, PopupEditMode.Update);

                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFORD1000, param, PopupRefreshCallback);

                form.ShowPopup(true);
            }
        }

        /// <summary> 수주현황 </summary>
        private void Btn_OrdStatus_Click(object sender, EventArgs e)
        {
            try
            {
                WaitHandler.ShowWait();
                PopupDataParam param = new PopupDataParam();
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD_STATUS, param, null);
                form.ShowPopup(false);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void BarOrderStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            var masterObj = MasterGridBindingSource.Current as TN_ORD1000;
            if (masterObj == null) return;

            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (detailObj == null) return;

            try
            {
                WaitHandler.ShowWait();
                PopupDataParam param = new PopupDataParam();
                param.SetValue(PopupParameter.Value_1, detailObj.ItemCode);
                IPopupForm form = ProductionPopupFactory.GetPopupForm(ProductionPopupView.SELECT_ORD_STATUS, param, null);
                form.ShowPopup(false);
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void MainView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var view = sender as GridView;
            var detailObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (e.Column.FieldName == "OrderCost")
                detailObj.EditRowFlag = "Y";
            else if (e.Column.FieldName == "EndMonthDate") 
            {
                var detailList = DetailGridBindingSource.List as List<TN_ORD1001>;
                var sameOrderNoList = detailList.Where(p => p.OrderNo == detailObj.OrderNo).ToList();
                if (sameOrderNoList.Count > 1)
                {
                    foreach (var v in sameOrderNoList)
                        v.EndMonthDate = (Nullable<DateTime>) e.Value;
                    DetailGridExControl.BestFitColumns();
                }
            }
        }

        private void MainView_ShowingEditor(object sender, CancelEventArgs e)
        {
            var view = sender as GridView;
            var FieldName = view.FocusedColumn.FieldName;
            var SubObj = DetailGridBindingSource.Current as TN_ORD1001;
            if (FieldName == "ProductionFlag")
            {
                if (SubObj.TurnKeyFlag == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("TurnKeyFlag")));
                    e.Cancel = true;
                }
            }
            else if (FieldName == "TurnKeyFlag")
            {
                if (SubObj.ProductionFlag == "Y")
                {
                    //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("ProductionFlag")));
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_100));
                    e.Cancel = true;
                }
                else if(SubObj.Temp == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("WorkOrderFlag")));
                    e.Cancel = true;
                }
                else if (SubObj.TN_ORD1100List.First().TN_PUR1600List.Count > 0)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_52), LabelConvert.GetLabelText("TurnKeyInfo")));
                    e.Cancel = true;
                }
            }
            else if (FieldName == "Temp")
            {
                if (SubObj.ProductionFlag == "Y")
                {
                    //MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("ProductionFlag")));
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_100));
                    e.Cancel = true;
                }
                else if (SubObj.TurnKeyFlag == "Y")
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_55), LabelConvert.GetLabelText("TurnKeyFlag")));
                    e.Cancel = true;
                }
            }
            else if (FieldName == "PlanStartDate" || FieldName == "PlanEndDate" || FieldName == "PlanWorkQty" || FieldName == "EndMonthDate" || FieldName == "WorkEndMonthDate")
            {
                if (!SubObj.WorkNo.IsNullOrEmpty())
                    e.Cancel = true;
            }
        }

        private void Lup_Manager_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_OrderCustomerCode.EditValue = null;
        }

        private void Lup_OrderCustomerCode_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var manager = lup_OrderId.EditValue.GetNullToNull();

            if (manager.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[BusinessManagementId] = '" + manager + "'";
        }

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var itemCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var stockObj = StockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.SumStockQty.ToString("#,0.##");
                }
            }
            else if (e.Column.FieldName == "AvgSpendQty")
            {
                var itemCode = DetailGridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var obj = AvgSpendQtyList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = obj == null ? "0" : obj.AvgSpendQty.ToString("#,0.##");
                }
            }
        }


    }
}