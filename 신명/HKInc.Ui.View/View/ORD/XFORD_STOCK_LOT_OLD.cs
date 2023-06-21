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

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품 LOT별 재고 조회
    /// </summary>
    public partial class XFORD_STOCK_LOT_OLD : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PROD_STOCK_ITEM> ModelService = (IService<VI_PROD_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PROD_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;

        public XFORD_STOCK_LOT_OLD()
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
                MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            }
            else
            {
                MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            }

            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumCarryOverQty", LabelConvert.GetLabelText("SumCarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            if (searchDivision != "당월")
                MasterGridExControl.MainGrid.AddColumn("SumAdjustQty", LabelConvert.GetLabelText("SumAdjustQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("LotNo"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
        }

        protected override void InitRepository()
        {
            if (searchDivision == "당월")
            {
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            }
            else
            {
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            }

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

            if (searchDivision == "당월")
            {
                MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(searchItemCode) ? true : p.ItemCode == searchItemCode
                                                                         )
                                                                         .OrderBy(p => p.ItemCode)
                                                                         .ToList();
            }
            else
            {
                //var yearMonthString = cbo_YearMonth.EditValue.GetNullToEmpty();

                searchYearMonthDate = new DateTime(searchDivision.Left(4).GetIntNullToZero(), searchDivision.Substring(5).GetIntNullToZero(), 1);
                using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
                {
                    var Date = new SqlParameter("@Date", searchYearMonthDate);
                    var ItemCode = new SqlParameter("@ItemCode", searchItemCode);
                    MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_MASTER>("USP_GET_PROD_STOCK_MASTER @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
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
                    var Date = new SqlParameter("@Date", DateTime.Today);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_DETAIL_LOT_NO>("USP_GET_PROD_STOCK_DETAIL_LOT_NO @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
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
                    var Date = new SqlParameter("@Date", searchYearMonthDate);
                    var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                    DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_DETAIL_LOT_NO>("USP_GET_PROD_STOCK_DETAIL_LOT_NO @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
                }
            }
            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

    }
}
