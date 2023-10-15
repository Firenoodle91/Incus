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
    /// 제품 일별 재고 조회
    /// </summary>

    public partial class XFORD_STOCK_MM : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PROD_STOCK_ITEM> ModelService = (IService<VI_PROD_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PROD_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;

        public XFORD_STOCK_MM()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateEditEx1.SetFormat(Utils.Enum.DateFormat.Year);
            dateEditEx1.DateTime = DateTime.Today;
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());

          
        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            
                MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
                MasterGridExControl.MainGrid.AddColumn("Spec", LabelConvert.GetLabelText("Spec"));
                MasterGridExControl.MainGrid.AddColumn("Unit", LabelConvert.GetLabelText("Unit"));
                MasterGridExControl.MainGrid.AddColumn("SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
          
            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
     //      MasterGridExControl.MainGrid.AddColumn("SumCarryOverQty", LabelConvert.GetLabelText("SumCarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
         
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.MainGrid.AddColumn("YYYYMM", LabelConvert.GetLabelText("Date"));
            DetailGridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
       //     DetailGridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
        }

        protected override void InitRepository()
        {
           
                MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
           

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
           

            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();


            //string yyyy = dateEditEx1.DateTime.Year.ToString();
            //MasterGridBindingSource.DataSource = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_MM>(p => p.YYYY == yyyy).OrderBy(o => o.YYYYMM).ToList();


            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var ItemCode = new SqlParameter("@ItemCode", searchItemCode);
                MasterGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_MASTER>("USP_GET_PROD_STOCK_MASTER @ItemCode", ItemCode).OrderBy(p => p.ItemCode).ToList();
            }


            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
        
                var masterObj = MasterGridBindingSource.Current as TEMP_PROD_STOCK_MASTER;
                if (masterObj == null)
                {
                    DetailGridExControl.MainGrid.Clear();
                    return;
                }
            string yyyy = dateEditEx1.DateTime.Year.ToString();
            DetailGridBindingSource.DataSource = ModelService.GetChildList<VI_PROD_STOCK_PRODUCT_MM>(p => p.YYYY == yyyy&&p.ItemCode==masterObj.ItemCode).OrderBy(o => o.YYYYMM).ToList();
            //DateTime dt = dateEditEx1.DateTime.Date;
            //    using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            //    {
            //        var Date = new SqlParameter("@Date", dt);
            //        var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
            //        DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_DETAIL_DAY>("USP_GET_PROD_STOCK_DETAIL_DAY @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
            //    }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
    }
}
