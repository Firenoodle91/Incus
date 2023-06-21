using System;
using System.Data;
using System.Linq;
using DevExpress.Utils;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Class;
using HKInc.Service.Helper;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using HKInc.Service.Service;
using HKInc.Utils.Enum;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain.TEMP;
using System.Collections.Generic;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.PUR
{
    /// <summary>
    /// 구매입고현황
    /// </summary>
    public partial class XFPUR_NOT_IN_STATUS_MONTH : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        private BindingSource BindingSource = new BindingSource();

        private DateTime searchDate;

        public XFPUR_NOT_IN_STATUS_MONTH()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            dt_Date.SetFormat(DateFormat.Month);
            dt_Date.DateTime = DateTime.Today;

            lup_Customer.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Purchase || p.CustomerType == null)).ToList());
            lup_Item.SetDefault(false, true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetList(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_MAT)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("Customer"));
            GridExControl.MainGrid.AddColumn("PoQty", LabelConvert.GetLabelText("PoQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddUnboundColumn("InAmt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([InQty],0) * ISNULL([Cost],0)", FormatType.Numeric, "#,0.##");
            GridExControl.MainGrid.AddColumn("NotInQty", LabelConvert.GetLabelText("NotInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            GridExControl.MainGrid.ShowFooter = true;
            GridExControl.MainGrid.MainView.Columns["PoQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["PoQty"].SummaryItem.FieldName = "PoQty";
            GridExControl.MainGrid.MainView.Columns["PoQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.FieldName = "InQty";
            GridExControl.MainGrid.MainView.Columns["InQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["Cost"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["Cost"].SummaryItem.FieldName = "Cost";
            GridExControl.MainGrid.MainView.Columns["Cost"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["InAmt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["InAmt"].SummaryItem.FieldName = "InAmt";
            GridExControl.MainGrid.MainView.Columns["InAmt"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            GridExControl.MainGrid.MainView.Columns["NotInQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            GridExControl.MainGrid.MainView.Columns["NotInQty"].SummaryItem.FieldName = "NotInQty";
            GridExControl.MainGrid.MainView.Columns["NotInQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            searchDate = new DateTime(dt_Date.DateTime.Year, dt_Date.DateTime.Month, 1);

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var SearchDateFr = new SqlParameter("@SearchDateFr", searchDate);
                //var SearchDateTo = new SqlParameter("@SearchDateTo", dt_SearchDate.DateToEdit.DateTime);
                var ItemCode = new SqlParameter("@SrcItemCode", lup_Item.EditValue.GetNullToEmpty());
                var customerCode = lup_Customer.EditValue.GetNullToEmpty();
                BindingSource.DataSource = context
                    .Database
                    .SqlQuery<TEMP_XFPUR_NOT_IN_STATUS>("USP_GET_PUR_NOT_IN_STATUS_MONTH @SearchDateFr,@SrcItemCode", SearchDateFr, ItemCode)
                    .Where(p => string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                    .ToList();
            }

            GridExControl.DataSource = BindingSource;
            GridExControl.BestFitColumns();
        }
    }
}
