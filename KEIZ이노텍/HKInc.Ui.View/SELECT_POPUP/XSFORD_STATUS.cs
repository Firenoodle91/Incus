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

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 수주현황
    /// </summary>
    public partial class XSFORD_STATUS : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        private DateTime? searchFrDate = null;
        private DateTime? searchToDate = null;

        public XSFORD_STATUS()
        {
            InitializeComponent();
        }
        
        public XSFORD_STATUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("OrderStatus");
            this.Icon = Icon.FromHandle(((Bitmap)IconImageList.GetIconImage("chart/3dcylinder")).GetHicon());

            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView.FocusedRowChanged += MainView_FocusedRowChanged;

            chartControl1.AnimationStartMode = ChartAnimationMode.OnDataChanged;
            chartControl1.BoundDataChanged += ChartControl1_BoundDataChanged;

            dt_YearMonth.DateFrEdit.DateTime = DateTime.Today.AddYears(-1);
            dt_YearMonth.DateToEdit.DateTime = DateTime.Today;

            dt_YearMonth.SetDisableBaseEditValueChanged();
            dt_YearMonth.DateFrEdit.EditValueChanged += Dt_YearMonth_EditValueChanged;
            dt_YearMonth.DateToEdit.EditValueChanged += Dt_YearMonth_EditValueChanged;
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
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());

            dt_YearMonth.DateFrEdit.SetFormat(DateFormat.Month);
            dt_YearMonth.DateToEdit.SetFormat(DateFormat.Month);
            //dt_YearMonth.DateFrEdit.DateTime = DateTime.Today.AddYears(-1);
            //dt_YearMonth.DateToEdit.DateTime = DateTime.Today;

            dt_YearMonth.DateFrEdit.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            dt_YearMonth.DateToEdit.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Center;

            dt_YearMonth.DateFrEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            dt_YearMonth.DateToEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("ThirdSpendOutQty", LabelConvert.GetLabelText("ThirdSpendOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));

            chartControl1.Series.Clear();
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            chartControl1.Series.Clear();

            searchFrDate = dt_YearMonth.DateFrEdit.DateTime;
            searchToDate = dt_YearMonth.DateToEdit.DateTime;

            using (var context = new Model.Context.ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var frDate = new SqlParameter("@FrDate", searchFrDate);
                var toDate = new SqlParameter("@ToDate", searchToDate);
                var itemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());
                var customerCode = new SqlParameter("@CustomerCode", lup_Customer.EditValue.GetNullToEmpty());
                
                ModelBindingSource.DataSource = context.Database
                      .SqlQuery<TEMP_ORDER_STATUS_GRID>("USP_GET_ORDER_STATUS_GRID @FrDate,@ToDate ,@ItemCode ,@CustomerCode", frDate, toDate, itemCode, customerCode).ToList();
            }

            GridExControl.DataSource = ModelBindingSource;
            GridExControl.BestFitColumns();
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var masterObj = ModelBindingSource.Current as TEMP_ORDER_STATUS_GRID;
            if (masterObj == null)
            {
                chartControl1.Series.Clear();
                return;
            }

            SetChart(masterObj);
        }
        
        private void SetChart(TEMP_ORDER_STATUS_GRID masterObj)
        {
            if (searchFrDate == null || searchToDate == null)
            {
                return;
            }

            try
            {
                WaitHandler.CloseWait();
                WaitHandler.ShowWait();
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var _frDate = new SqlParameter("@FrDate", searchFrDate);
                    var _toDate = new SqlParameter("@ToDate", searchToDate);
                    var _itemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    var _customerCode = new SqlParameter("@CustomerCode", masterObj.CustomerCode);

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
                //foreach (var v in chartControl1.Series.ToList())
                //{
                //    // Change the first series's view settings. 
                //    SideBySideBarSeriesView view1 = v.SeriesView as SideBySideBarSeriesView;
                //    // The BarDistance, BarDistanceFixed, and EqualBarWidth property values are synchronized  
                //    // in all Side-by-Side Bar Series in a ChartControl. 
                //    // So, you can specify them only for one series view. 
                //    view1.BarDistance = 0.1;
                //    view1.BarDistanceFixed = 2;
                //    view1.BarWidth = 0.2;
                //    view1.EqualBarWidth = true;

                //    //view1.Color = Color.MediumSeaGreen;
                //}

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

                diagram.AxisX.CrosshairAxisLabelOptions.Pattern = "{A:F0}";

                diagram.AxisY.WholeRange.Auto = true;      // y축 범위 자동변경 설정 
                diagram.AxisX.Label.Font = new Font(@"맑은고딕", 9f);
                diagram.AxisY.Label.TextPattern = "{V:#,###,###,##0.##}";

                diagram.EnableAxisXScrolling = true;
                diagram.EnableAxisYScrolling = true;
                diagram.AxisX.VisualRange.Auto = true;
                diagram.AxisY.VisualRange.Auto = true;
            }
            finally { WaitHandler.CloseWait(); }
        }

        private void Dt_YearMonth_EditValueChanged(object sender, EventArgs e)
        {
            var dateEdit = sender as DateEditEx;

            var frDate = dt_YearMonth.DateFrEdit.DateTime;
            var toDate = dt_YearMonth.DateToEdit.DateTime;

            if (DateTime.Compare(frDate, toDate) > 0) //뒷 날짜가 더 작아질 경우
            {
                //MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_12))
                dt_YearMonth.DateFrEdit.DateTime = DateTime.Parse(dt_YearMonth.DateToEdit.DateTime.ToShortDateString());
            }
            else
            {
                var yearSpan = toDate.Year - frDate.Year;

                if (yearSpan > 1)
                {
                    MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_57), LabelConvert.GetLabelText("1Year")));
                    dt_YearMonth.DateFrEdit.EditValueChanged -= Dt_YearMonth_EditValueChanged;
                    dt_YearMonth.DateToEdit.EditValueChanged -= Dt_YearMonth_EditValueChanged;
                    if(dateEdit.Name == "datDateFr")
                        dt_YearMonth.DateFrEdit.DateTime = (DateTime)dt_YearMonth.DateFrEdit.OldEditValue;
                    else
                        dt_YearMonth.DateToEdit.DateTime = (DateTime)dt_YearMonth.DateToEdit.OldEditValue;
                    dt_YearMonth.DateFrEdit.EditValueChanged += Dt_YearMonth_EditValueChanged;
                    dt_YearMonth.DateToEdit.EditValueChanged += Dt_YearMonth_EditValueChanged;
                }
                else if (yearSpan == 1)
                {
                    var monthSpan = toDate.Month - frDate.Month;
                    if (monthSpan > 0)
                    {
                        MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_57), LabelConvert.GetLabelText("1Year")));
                        dt_YearMonth.DateFrEdit.EditValueChanged -= Dt_YearMonth_EditValueChanged;
                        dt_YearMonth.DateToEdit.EditValueChanged -= Dt_YearMonth_EditValueChanged;
                        if (dateEdit.Name == "datDateFr")
                            dt_YearMonth.DateFrEdit.DateTime = (DateTime)dt_YearMonth.DateFrEdit.OldEditValue;
                        else
                            dt_YearMonth.DateToEdit.DateTime = (DateTime)dt_YearMonth.DateToEdit.OldEditValue;
                        dt_YearMonth.DateFrEdit.EditValueChanged += Dt_YearMonth_EditValueChanged;
                        dt_YearMonth.DateToEdit.EditValueChanged += Dt_YearMonth_EditValueChanged;
                    }
                }
            }
        }

        private void ChartControl1_BoundDataChanged(object sender, EventArgs e)
        {
            foreach (Series series in this.chartControl1.Series)
            {
                series.CrosshairLabelPattern = "{S}:{V:#,###,###,##0.##}";
            }
        }

    }
}