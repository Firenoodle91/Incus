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
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.BandedGrid;
using HKInc.Service.Handler;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Controls;
using HKInc.Service.Service;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 수주참조
    /// </summary>
    public partial class XSFORDER_REF : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_ORD1001> ModelService = (IService<TN_ORD1001>)ProductionFactory.GetDomainService("TN_ORD1001");
        private List<VI_PUR_STOCK> MatStockList = new List<VI_PUR_STOCK>();

        private BindingSource DetailBindingSource = new BindingSource();

        public XSFORDER_REF()
        {
            InitializeComponent();
        }
        
        public XSFORDER_REF(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("OrderRefer");
            this.Icon = Icon.FromHandle(((Bitmap)IconImageList.GetIconImage("business%20objects/botask")).GetHicon());

            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;
            gridEx2.MainGrid.MainView.CustomColumnDisplayText += GridEx2_CustomColumnDisplayText;


            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitBindingSource(){}

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y").ToList());

            dt_YearMonth.SetFormat(DateFormat.Month);
            dt_YearMonth.DateTime = DateTime.Today;

            dt_YearMonth.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            dt_YearMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"), false);
            GridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("TN_STD1100.TopCategory", LabelConvert.GetLabelText("TopCategory"), false);
            GridExControl.MainGrid.AddColumn("TN_STD1100.MiddleCategory", LabelConvert.GetLabelText("MiddleCategory"), false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            GridExControl.MainGrid.AddColumn("OrderQty", LabelConvert.GetLabelText("OrderQty"));
            GridExControl.MainGrid.AddColumn("OrderCost", LabelConvert.GetLabelText("Cost"));
            GridExControl.MainGrid.AddUnboundColumn("OrderAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([OrderQty],0) * ISNULL([OrderCost],0)", FormatType.Numeric, "#,###,###,###.##");

            gridEx2.SetToolbarVisible(false);
            gridEx2.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx2.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            gridEx2.MainGrid.AddColumn("UseQty", LabelConvert.GetLabelText("UseQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            gridEx2.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            gridEx2.MainGrid.Clear();
            MatStockList.Clear();
            ModelService.ReLoad();

            var searchDate = dt_YearMonth.DateTime;
            var firstDate = searchDate.AddDays(1 - searchDate.Day);
            var lastDate = firstDate.AddMonths(1).AddDays(-1);

            string itemCode = lup_Item.EditValue.GetNullToEmpty();
            ModelBindingSource.DataSource = ModelService.GetList(p => (p.TN_ORD1000.OrderDate >= firstDate
                                                                         && p.TN_ORD1000.OrderDate <= lastDate)
                                                                         && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                         && (p.TN_ORD1000.OrderType == "양산")
                                                                      )
                                                                      .OrderBy(p => p.OrderNo)
                                                                      .ToList();
            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();

            //재고량 가져오기
            MatStockList.AddRange(ModelService.GetChildList<VI_PUR_STOCK>(p => true).ToList());


            MainView_FocusedRowChanged(null, null);
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var masterObj = ModelBindingSource.Current as TN_ORD1001;
            if (masterObj == null)
            {
                gridEx2.MainGrid.Clear();
                return;
            }

            var bomList = ModelService.GetChildList<TN_STD1300>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();

            if (bomList == null) return;

            DetailBindingSource.DataSource = ModelService.GetChildList<TN_STD1300>(p => p.ParentBomCode == bomList.BomCode && p.UseFlag == "Y");
            gridEx2.DataSource = DetailBindingSource;
            gridEx2.BestFitColumns();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            if (ModelBindingSource == null || ModelBindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var masterObj = (TN_ORD1001)ModelBindingSource.Current;
                var DetailObj = DetailBindingSource.DataSource as List<TN_STD1300>;

                param.SetValue(PopupParameter.Value_1, masterObj);
                param.SetValue(PopupParameter.Value_2, DetailObj);
                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }

        private void GridEx2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var itemCode = gridEx2.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var stockObj = MatStockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.SumStockQty.ToString("#,#.##");
                }
            }
        }
    }
}