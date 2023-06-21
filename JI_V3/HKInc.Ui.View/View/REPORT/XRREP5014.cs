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
using DevExpress.XtraGrid.Views.BandedGrid;

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
    /// 기간별 매입현황
    /// </summary>
    public partial class XRREP5014 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRREP5014()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            OutDate.DateFrEdit.DateTime = DateTime.Today.AddMonths(-1);
            OutDate.DateToEdit.DateTime = DateTime.Today;

        }

        protected override void InitCombo()
        {
            lup_ItemCode.SetDefault(false, true, "ItemCode", "ItemName", ModelService.GetChildList<TN_STD1100>
                (p => p.UseFlag == "Y"
                && (p.TopCategory != MasterCodeSTR.TopCategory_WAN && p.TopCategory != MasterCodeSTR.TopCategory_BAN)
                ).ToList());
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && p.CustomerType == "A01").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.MainGrid.AddColumn("InDate", LabelConvert.GetLabelText("InDate"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"), false);
            GridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"), false);
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            //GridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.###}");
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "{0:#,##0}");
            GridExControl.MainGrid.AddColumn("Sales", "매입액", HorzAlignment.Far, FormatType.Numeric, "{0:#,##0.###}");

            GridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;

            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.DisplayFormat = "{0:#,##0}";

            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Sales"].SummaryItem.DisplayFormat = "{0:#,##0.###}";
        }

        protected override void InitRepository()
        {


        }

        protected override void DataLoad()
        {
            ModelService.ReLoad();

            InitCombo();  
            InitRepository(); 

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var StartDate = new SqlParameter("@StartDate", OutDate.DateFrEdit.DateTime.ToString("yyyy-MM-dd"));
                var EndDate = new SqlParameter("@EndDate", OutDate.DateToEdit.DateTime.ToString("yyyy-MM-dd"));
                var ItemCode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());
                var CustomerCode = new SqlParameter("@CustomerCode", lup_CustomerCode.EditValue.GetNullToEmpty());

                var result = context.Database
                      .SqlQuery<DATA>("USP_GET_XRREP5014 @StartDate, @EndDate, @ItemCode, @CustomerCode", StartDate, EndDate, ItemCode, CustomerCode).ToList();

                GridBindingSource.DataSource = result;
            }

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }
        public class DATA
        {
            public string InDate { get; set; }
            public string CustomerName { get; set; }
            public string ItemName { get; set; }
            //public string CarType { get; set; }

            public decimal? Cost { get; set; }
            public decimal? InQty { get; set; }
            public decimal? Sales { get; set; }


        }
    }
}