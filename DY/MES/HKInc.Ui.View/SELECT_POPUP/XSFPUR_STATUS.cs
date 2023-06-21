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
    /// 구매현황
    /// </summary>
    public partial class XSFPUR_STATUS : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        private DateTime? searchFrDate = null;
        private DateTime? searchToDate = null;

        public XSFPUR_STATUS()
        {
            InitializeComponent();
        }
        
        public XSFPUR_STATUS(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("PurchaseStatus");
            this.Icon = Icon.FromHandle(((Bitmap)IconImageList.GetIconImage("chart/3dcylinder")).GetHicon());

            GridExControl = gridEx1;
            GridExControl.MainGrid.MainView = new BandedGridView();
            GridExControl.MainGrid.ViewType = Utils.Enum.GridViewType.BandedGridView;
            ((BandedGridView)GridExControl.MainGrid.MainView).FocusedRowChanged += MainView_FocusedRowChanged;

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
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT || p.TopCategory == MasterCodeSTR.TopCategory_BU)).ToList());

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
            ((BandedGridView)GridExControl.MainGrid.MainView).InitEx();
            GridExControl.MainGrid.MainView.OptionsView.ShowIndicator = false;

            GridExControl.MainGrid.Clear();
            chartControl1.Series.Clear();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            ((BandedGridView)GridExControl.MainGrid.MainView).BeginUpdate();
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands.Clear();
            ((BandedGridView)GridExControl.MainGrid.MainView).Columns.Clear();
            ((BandedGridView)GridExControl.MainGrid.MainView).EndUpdate();

            chartControl1.Series.Clear();

            searchFrDate = dt_YearMonth.DateFrEdit.DateTime;
            searchToDate = dt_YearMonth.DateToEdit.DateTime;

            var frDate = new SqlParameter("@FrDate", searchFrDate);
            var toDate = new SqlParameter("@ToDate", searchToDate);
            var itemCode = new SqlParameter("@ItemCode", lup_Item.EditValue.GetNullToEmpty());

            var ds = DbRequestHandler.GetDataSet("USP_GET_PURCHASE_STATUS_GRID", frDate, toDate, itemCode);
            
            SetColumn();

            GridExControl.DataSource = ds.Tables[0];
            GridExControl.BestFitColumns();

            MainView_FocusedRowChanged(null, null);
        }

        private void MainView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var itemCode = ((BandedGridView)GridExControl.MainGrid.MainView).GetFocusedRowCellValue("ItemCode");
            SetChart(itemCode.GetNullToEmpty());
        }
        
        private void SetColumn()
        {
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            GridExControl.MainGrid.AddColumn("CombineSpec", LabelConvert.GetLabelText("Spec"));

            GridExControl.MainGrid.MainView.SetGridBandAddedEx("ItemCode", "ItemCode", "ItemCode");
            GridExControl.MainGrid.MainView.SetGridBandAddedEx(LabelConvert.GetLabelText("ItemName"), "ItemName", DataConvert.GetCultureDataFieldName("ItemName"));
            GridExControl.MainGrid.MainView.SetGridBandAddedEx(LabelConvert.GetLabelText("ItemName1"), "ItemName1", DataConvert.GetCultureDataFieldName("ItemName1"));
            GridExControl.MainGrid.MainView.SetGridBandAddedEx("CombineSpec", "CombineSpec", "CombineSpec");


            List<string> bandList1 = new List<string>();
            var frDate = dt_YearMonth.DateFrEdit.DateTime;
            while (frDate.ToString("yyyyMM").GetIntNullToZero() <= dt_YearMonth.DateToEdit.DateTime.ToString("yyyyMM").GetIntNullToZero())
            {
                bandList1.Clear();
                bandList1.Add(frDate.ToShortDateString().Left(7) + "_PoQty");
                bandList1.Add(frDate.ToShortDateString().Left(7) + "_InQty");
                bandList1.Add(frDate.ToShortDateString().Left(7) + "_OutQty");

                GridExControl.MainGrid.AddColumn(frDate.ToShortDateString().Left(7) + "_PoQty", LabelConvert.GetLabelText("PO"), HorzAlignment.Far, FormatType.Numeric, "#,###,###,##0.##");
                GridExControl.MainGrid.AddColumn(frDate.ToShortDateString().Left(7) + "_InQty", LabelConvert.GetLabelText("IN"), HorzAlignment.Far, FormatType.Numeric, "#,###,###,##0.##");
                GridExControl.MainGrid.AddColumn(frDate.ToShortDateString().Left(7) + "_OutQty", LabelConvert.GetLabelText("OUT"), HorzAlignment.Far, FormatType.Numeric, "#,###,###,##0.##");

                GridExControl.MainGrid.MainView.SetGridBandAddedEx(frDate.ToShortDateString().Left(7), frDate.ToShortDateString().Left(7), bandList1.ToArray());
                frDate = frDate.AddMonths(1);
            }

            BandedGridViewPainter painter = new BandedGridViewPainter((BandedGridView)GridExControl.MainGrid.MainView,
                                               new GridBand[] { (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemCode"],
                                                                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName"],
                                                                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName1"],
                                                                (GridBand)((BandedGridView)GridExControl.MainGrid.MainView).Bands["CombineSpec"],});

            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["ItemName1"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ((BandedGridView)GridExControl.MainGrid.MainView).Bands["CombineSpec"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

        private void SetChart(string itemCode)
        {
            if (searchFrDate == null || searchToDate == null || itemCode.IsNullOrEmpty())
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
                    var _itemCode = new SqlParameter("@ItemCode", itemCode);

                    var result = context.Database.SqlQuery<TEMP_PUR_STATUS_CHART>("USP_GET_PURCHASE_STATUS_CHART @FrDate,@ToDate ,@ItemCode", _frDate, _toDate, _itemCode).OrderBy(p => p.Date).ToList();

                    chartControl1.DataSource = result;
                }

                chartControl1.SeriesDataMember = "Division";
                chartControl1.SeriesTemplate.ArgumentDataMember = "Date";
                chartControl1.SeriesTemplate.ValueDataMembers.AddRange("Qty");
                chartControl1.SeriesTemplate.Label.TextPattern = "{S}:{V:#,###,###,##0.##}";
                chartControl1.CrosshairEnabled = DefaultBoolean.True;
                chartControl1.CrosshairOptions.ShowValueLabels = true;

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

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