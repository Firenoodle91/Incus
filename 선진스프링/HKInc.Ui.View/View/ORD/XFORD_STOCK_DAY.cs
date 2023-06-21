using System;
using System.Data;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using DevExpress.Utils;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using System.Data.SqlClient;
using HKInc.Service.Service;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Enum;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraBars;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Handler;
using HKInc.Utils.Common;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품 일별 재고 조회
    /// </summary>

    public partial class XFORD_STOCK_DAY : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PROD_STOCK_ITEM> ModelService = (IService<VI_PROD_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PROD_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;

        public XFORD_STOCK_DAY()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());

            cbo_YearMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            cbo_YearMonth.Properties.Items.Add("당월");

            var deadLineDateList = ModelService.GetChildList<TN_PROD_DEAD_MST>(p => p.Division == MasterCodeSTR.DeadLineDivision_DeadConfirm).Select(p => p.DeadLineDate).ToList();
            foreach (var v in deadLineDateList)
            {
                cbo_YearMonth.Properties.Items.Add(v.ToString("yyyy-MM"));
            }

            cbo_YearMonth.EditValue = "당월";
            searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();

            var list = ModelService.GetChildList<VI_BUSINESS_MANAGEMENT_USER>(p => true).ToList();
            lup_BusinessManagementId.SetDefault(true, "LoginId", "UserName", list);
            lup_BusinessManagementId.EditValueChanged += Lup_BusinessManagementId_EditValueChanged;
            if (list.Any(p => p.LoginId == GlobalVariable.LoginId))
                lup_BusinessManagementId.EditValue = GlobalVariable.LoginId;

            lup_MainCustomer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && (p.CustomerType == MasterCodeSTR.CustType_Sales || p.CustomerType == null)).ToList());
            lup_MainCustomer.Popup += Lup_MainCustomer_Popup;
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));

            if (searchDivision == "당월")
            {
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TN_STD1400.CustomerName", LabelConvert.GetLabelText("MainCustomer"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.TN_STD1400.BusinessManagementId", LabelConvert.GetLabelText("ManagerName"));
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            }
            else
            {
                MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("Cost", LabelConvert.GetLabelText("Cost"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
                MasterGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("MainCustomer"));
                MasterGridExControl.MainGrid.AddColumn("BusinessManagementId", LabelConvert.GetLabelText("ManagerName"));
                MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "N0");
            }

            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumCarryOverQty", LabelConvert.GetLabelText("SumCarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            if (searchDivision != "당월")
                MasterGridExControl.MainGrid.AddColumn("SumAdjustQty", LabelConvert.GetLabelText("SumAdjustQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            MasterGridExControl.MainGrid.MainView.OptionsView.ShowFooter = true;
            if (searchDivision == "당월")
            {
                MasterGridExControl.MainGrid.AddUnboundColumn("Amt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([SumStockQty],0) * ISNULL([TN_STD1100.Cost],0)", FormatType.Numeric, "#,###,###,##0.##");

                MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.SafeQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.SafeQty"].SummaryItem.FieldName = "TN_STD1100.SafeQty";
                MasterGridExControl.MainGrid.MainView.Columns["TN_STD1100.SafeQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";
            }
            else
            {
                MasterGridExControl.MainGrid.AddUnboundColumn("Amt", LabelConvert.GetLabelText("Amt"), DevExpress.Data.UnboundColumnType.Decimal, "ISNULL([SumStockQty],0) * ISNULL([Cost],0)", FormatType.Numeric, "#,###,###,##0.##");

                MasterGridExControl.MainGrid.MainView.Columns["SafeQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                MasterGridExControl.MainGrid.MainView.Columns["SafeQty"].SummaryItem.FieldName = "SafeQty";
                MasterGridExControl.MainGrid.MainView.Columns["SafeQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

                MasterGridExControl.MainGrid.MainView.Columns["SumAdjustQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                MasterGridExControl.MainGrid.MainView.Columns["SumAdjustQty"].SummaryItem.FieldName = "SumAdjustQty";
                MasterGridExControl.MainGrid.MainView.Columns["SumAdjustQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";
            }

            MasterGridExControl.MainGrid.MainView.Columns["SumInQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumInQty"].SummaryItem.FieldName = "SumInQty";
            MasterGridExControl.MainGrid.MainView.Columns["SumInQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            MasterGridExControl.MainGrid.MainView.Columns["SumOutQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumOutQty"].SummaryItem.FieldName = "SumOutQty";
            MasterGridExControl.MainGrid.MainView.Columns["SumOutQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            MasterGridExControl.MainGrid.MainView.Columns["SumCarryOverQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumCarryOverQty"].SummaryItem.FieldName = "SumOutQty";
            MasterGridExControl.MainGrid.MainView.Columns["SumCarryOverQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            MasterGridExControl.MainGrid.MainView.Columns["SumStockQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["SumStockQty"].SummaryItem.FieldName = "SumStockQty";
            MasterGridExControl.MainGrid.MainView.Columns["SumStockQty"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            MasterGridExControl.MainGrid.MainView.Columns["Amt"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            MasterGridExControl.MainGrid.MainView.Columns["Amt"].SummaryItem.FieldName = "Amt";
            MasterGridExControl.MainGrid.MainView.Columns["Amt"].SummaryItem.DisplayFormat = "{0:#,0.##}";

            DetailGridExControl.MainGrid.AddColumn("Date", LabelConvert.GetLabelText("Date"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            DetailGridExControl.MainGrid.AddColumn("CustomerLot", LabelConvert.GetLabelText("CustomerLotNo"));

        }

        protected override void InitRepository()
        {
            if (searchDivision == "당월")
            {
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.TN_STD1400.BusinessManagementId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            }
            else
            {
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BusinessManagementId", ModelService.GetChildList<User>(p => true), "LoginId", "UserName");
            }
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true).ToList(), "CustomerCode", Service.Helper.DataConvert.GetCultureDataFieldName("CustomerName"));
            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("ItemCode");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();
            searchDivision = cbo_YearMonth.EditValue.GetNullToEmpty();

            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();

            string managerId = lup_BusinessManagementId.EditValue.GetNullToEmpty();
            string customerCode = lup_MainCustomer.EditValue.GetNullToEmpty();

            if (searchDivision == "당월")
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(searchItemCode) ? true : p.ItemCode == searchItemCode)
                                                                            && (string.IsNullOrEmpty(customerCode) ? true : p.TN_STD1100.MainCustomerCode == customerCode)
                                                                            && (string.IsNullOrEmpty(managerId) ? true : p.TN_STD1100.TN_STD1400.BusinessManagementId == managerId)
                                                                         )
                                                                         .OrderBy(p => p.ItemCode)
                                                                         .ToList();
            }
            else
            {
                searchYearMonthDate = new DateTime(searchDivision.Left(4).GetIntNullToZero(), searchDivision.Substring(5).GetIntNullToZero(), 1);
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", searchYearMonthDate);
                    var ItemCode = new SqlParameter("@ItemCode", searchItemCode);
                    var ManagerId = new SqlParameter("@ManagerId", managerId);
                    var CustomerCode = new SqlParameter("@CustomerCode", customerCode);
                    MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_MASTER>("USP_GET_PROD_STOCK_MASTER @Date,@ItemCode,@ManagerId,@CustomerCode", Date, ItemCode, ManagerId, CustomerCode).OrderBy(p => p.ItemCode).ToList();
                }
            }

            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            if (searchDivision == "당월")
            {
                var masterObj = MasterGridBindingSource.Current as VI_PROD_STOCK_ITEM;
                if (masterObj == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", DateTime.Today);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_DETAIL_DAY>("USP_GET_PROD_STOCK_DETAIL_DAY @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
                }
            }
            else
            {
                var masterObj = MasterGridBindingSource.Current as TEMP_PROD_STOCK_MASTER;
                if (masterObj == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }

                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    context.Database.CommandTimeout = 0;
                    var Date = new SqlParameter("@Date", searchYearMonthDate);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_DETAIL_DAY>("USP_GET_PROD_STOCK_DETAIL_DAY @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
                }
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        private void Lup_BusinessManagementId_EditValueChanged(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            lup_MainCustomer.EditValue = null;
        }

        private void Lup_MainCustomer_Popup(object sender, EventArgs e)
        {
            var lookup = sender as SearchLookUpEdit;
            if (lookup == null) return;

            var manager = lup_BusinessManagementId.EditValue.GetNullToNull();

            if (manager.IsNullOrEmpty())
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "";
            else
                lookup.Properties.View.ActiveFilter.NonColumnFilter = "[BusinessManagementId] = '" + manager + "'";
        }
    }
}
