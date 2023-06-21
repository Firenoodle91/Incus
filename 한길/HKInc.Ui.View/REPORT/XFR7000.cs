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
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 일별총수량현황
    /// </summary>
    public partial class XFR7000 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_LOTTRACKING> ModelService = (IService<VI_LOTTRACKING>)ProductionFactory.GetDomainService("VI_LOTTRACKING");

        public XFR7000()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            MasterGridExControl.MainGrid.MainView.CustomDrawFooterCell += MainView_CustomDrawFooterCell;
            DetailGridExControl.MainGrid.MainView.CustomDrawFooterCell += MainView_CustomDrawFooterCell;

        }

        protected override void InitCombo()
        {
            datePeriodEditEx1.SetTodayIsMonth();
            lupCustomer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").OrderBy(o => o.CustomerName).ToList());
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            MasterGridExControl.MainGrid.AddColumn("Dates", "일자");
            MasterGridExControl.MainGrid.AddColumn("ItemName", "품명");
            MasterGridExControl.MainGrid.AddColumn("OrderQty", "수주량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OkQty", "생산량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("OutQty", "납품량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("StockQty", "재고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridColumnSummaryItem TempSummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TempSummary", "합계 : ");
            GridColumnSummaryItem OrderQtySummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OrderQty", "수주량={0:n0}");
            GridColumnSummaryItem OkQtySummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OkQty", "생산량={0:n0}");
            GridColumnSummaryItem OutQtySummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OutQty", "납품량={0:n0}");
            GridColumnSummaryItem StockQtySummary = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "StockQty", "재고량={0:n0}");
            MasterGridExControl.MainGrid.MainView.Columns["ItemName"].Summary.Add(TempSummary);
            MasterGridExControl.MainGrid.MainView.Columns["OrderQty"].Summary.Add(OrderQtySummary);
            MasterGridExControl.MainGrid.MainView.Columns["OkQty"].Summary.Add(OkQtySummary);
            MasterGridExControl.MainGrid.MainView.Columns["OutQty"].Summary.Add(OutQtySummary);
            MasterGridExControl.MainGrid.MainView.Columns["StockQty"].Summary.Add(StockQtySummary);
            MasterGridExControl.BestFitColumns();

            DetailGridExControl.SetToolbarVisible(false);
            DetailGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            DetailGridExControl.MainGrid.AddColumn("ItemName", "품명");
            DetailGridExControl.MainGrid.AddColumn("OrderQty", "수주량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OkQty", "생산량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("OutQty", "납품량", HorzAlignment.Far, FormatType.Numeric, "n0");
            DetailGridExControl.MainGrid.AddColumn("StockQty", "재고량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridColumnSummaryItem TempSummary2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TempSummary", "합계 : ");
            GridColumnSummaryItem OrderQtySummary2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OrderQty", "수주량={0:n0}");
            GridColumnSummaryItem OkQtySummary2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OkQty", "생산량={0:n0}");
            GridColumnSummaryItem OutQtySummary2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "OutQty", "납품량={0:n0}");
            GridColumnSummaryItem StockQtySummary2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "StockQty", "재고량={0:n0}");
            DetailGridExControl.MainGrid.MainView.Columns["ItemName"].Summary.Add(TempSummary2);
            DetailGridExControl.MainGrid.MainView.Columns["OrderQty"].Summary.Add(OrderQtySummary2);
            DetailGridExControl.MainGrid.MainView.Columns["OkQty"].Summary.Add(OkQtySummary2);
            DetailGridExControl.MainGrid.MainView.Columns["OutQty"].Summary.Add(OutQtySummary2);
            DetailGridExControl.MainGrid.MainView.Columns["StockQty"].Summary.Add(StockQtySummary2);
            DetailGridExControl.BestFitColumns();
        }

        protected override void InitRepository()
        {

        }

        protected override void DataLoad()
        {
            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                var frDate = new SqlParameter("@FrDate", datePeriodEditEx1.DateFrEdit.DateTime);
                var toDate = new SqlParameter("@ToDate", datePeriodEditEx1.DateToEdit.DateTime);
                var customerCode = new SqlParameter("@CustomerCode", lupCustomer.EditValue.GetNullToEmpty());
                var result = context.Database
                      .SqlQuery<MasterModel>("USP_GET_XFR7000 @FrDate, @ToDate, @CustomerCode", frDate, toDate, customerCode).ToList();

                var frDate2 = new SqlParameter("@FrDate", datePeriodEditEx1.DateFrEdit.DateTime);
                var toDate2 = new SqlParameter("@ToDate", datePeriodEditEx1.DateToEdit.DateTime);
                var customerCode2 = new SqlParameter("@CustomerCode", lupCustomer.EditValue.GetNullToEmpty());
                var result2 = context.Database
                      .SqlQuery<MasterModel>("USP_GET_XFR7200 @FrDate, @ToDate, @CustomerCode", frDate2, toDate2, customerCode2).ToList();
                MasterGridBindingSource.DataSource = result.OrderBy(o => o.Dates).ToList();
                DetailGridBindingSource.DataSource = result2.ToList();
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            DetailGridExControl.DataSource = DetailGridBindingSource;
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
            SetRefreshMessage(MasterGridExControl.MainGrid.RecordCount);
        }

        private void MainView_CustomDrawFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            if(e.Column.FieldName == "ItemName")
            {
                e.Appearance.BackColor = Color.Transparent;
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Info.AllowDrawBackground = false;
            }
        }

        class MasterModel
        {
            public DateTime? Dates { get; set; }
            public string ItemName { get; set; }
            public decimal? OrderQty { get; set; }
            public decimal? OkQty { get; set; }
            public decimal? OutQty { get; set; }
            public decimal? StockQty { get; set; }
        }
    }
}