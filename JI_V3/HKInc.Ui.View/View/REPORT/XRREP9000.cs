using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Entity;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars;

using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Enum;


namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 생산계획대비실적
    /// </summary>
    public partial class XRREP9000 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRREP9000()
        {
            InitializeComponent();
            GridExControl = gridEx1;


            dt_Date.SetTodayIsMonth();
        }        

        protected override void InitGrid()
        {
            GridExControl.SetToolbarButtonVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", "품목코드");
            GridExControl.MainGrid.AddColumn("ItemName", "품명");
            GridExControl.MainGrid.AddColumn("DelivNo", "납품계획번호");
            GridExControl.MainGrid.AddColumn("PlanNo", "생산계획번호");
            GridExControl.MainGrid.AddColumn("WorkNo", "작업지시번호");
            GridExControl.MainGrid.AddColumn("PlanStartDate", "계획시작일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            GridExControl.MainGrid.AddColumn("PlanEndDate", "계획종료일", HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd", false);
            GridExControl.MainGrid.AddColumn("PlanQty", "생산계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("WorkQty", "작업지시수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ResultSumQty", "생산수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("ReadyQty", "작업대기수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("Rate", "계획대비생산수량", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.##}%");
        }

        protected override void InitRepository()
        {

            GridExControl.BestFitColumns();

            /* 요청시 넣을거
            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            GridExControl.MainGrid.MainView.Columns["OutQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["OutQty"].SummaryItem.DisplayFormat = "{0:#,#}";
            GridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["DueCost"].SummaryItem.DisplayFormat = "{0:#,#}";
            */
        }

        protected override void InitCombo()
        {
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("DelivNo");

            GridExControl.MainGrid.Clear();
            GridExControl.MainGrid.Clear();

            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var startdate = new SqlParameter("@StartDate", dt_Date.DateFrEdit.DateTime);
                var enddate = new SqlParameter("@EndDate", dt_Date.DateToEdit.DateTime);
                var itemcode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());

                var DelivNo = new SqlParameter("@DelivNo", tx_DelivNo.EditValue.GetNullToEmpty());
                var PlanNo = new SqlParameter("@PlanNo", tx_PlanNo.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XRREP9000>
                    ("USP_GET_XRREP9000 @StartDate, @EndDate, @ItemCode, @DelivNo, @PlanNo", startdate, enddate, itemcode, DelivNo, PlanNo).ToList();

                GridBindingSource.DataSource = result.ToList();
                GridExControl.DataSource = GridBindingSource;
                GridExControl.BestFitColumns();

            }

            GridRowLocator.SetCurrentRow();
            
            //SetRefreshMessage(GridExControl);
        }


    }
}
