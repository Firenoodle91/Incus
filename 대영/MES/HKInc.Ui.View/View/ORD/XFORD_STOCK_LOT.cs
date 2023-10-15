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
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;

namespace HKInc.Ui.View.View.ORD
{
    /// <summary>
    /// 제품 LOT별 재고 조회
    /// </summary>
    public partial class XFORD_STOCK_LOT : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        IService<VI_PROD_STOCK_ITEM> ModelService = (IService<VI_PROD_STOCK_ITEM>)ProductionFactory.GetDomainService("VI_PROD_STOCK_ITEM");
        private string searchDivision;
        private DateTime? searchYearMonthDate;
        private string searchItemCode;

        public XFORD_STOCK_LOT()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;
            dateEditEx1.SetFormat(Utils.Enum.DateFormat.Month);
            dateEditEx1.DateTime = DateTime.Today;
            MasterGridExControl.MainGrid.MainView.RowCellStyle += MainView_RowCellStyle;
        }

        

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            DetailGridExControl.SetToolbarVisible(false);

            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemCode"), LabelConvert.GetLabelText("ItemCode"));

            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ItemName1"), LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Spec"), LabelConvert.GetLabelText("Spec"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("Unit"), LabelConvert.GetLabelText("Unit"));
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("SafeQty"), LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ProperQty"), LabelConvert.GetLabelText("ProperQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            

            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("SumInQty"), LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("SumOutQty"), LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            //    MasterGridExControl.MainGrid.AddColumn("SumCarryOverQty", LabelConvert.GetLabelText("SumCarryOverQty") ,HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            MasterGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("SumStockQty"), LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");

            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("ProductLotNo"), LabelConvert.GetLabelText("LotNo"));
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("InQty"), LabelConvert.GetLabelText("ResultQtyTwo"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            DetailGridExControl.MainGrid.AddColumn(DataConvert.GetCultureDataFieldName("OutQty"), LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
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

            InitCombo(); //phs20210624
            InitRepository();//phs20210624

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();


            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();


            //searchYearMonthDate = new DateTime(searchDivision.Left(4).GetIntNullToZero(), searchDivision.Substring(5).GetIntNullToZero(), 1);
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
            DateTime dt = dateEditEx1.DateTime.Date;
            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var Date = new SqlParameter("@Date", dt);
                var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_PROD_STOCK_DETAIL_LOT_NO>("USP_GET_PROD_STOCK_DETAIL_LOT_NO @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }

        private void MainView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.RowHandle >= 0)
            {
                var stockqty = view.GetRowCellValue(e.RowHandle, view.Columns["SumStockQty"]).GetIntNullToZero();

                var safeqty = view.GetRowCellValue(e.RowHandle, view.Columns["SafeQty"]).GetIntNullToZero();
                var properqty = view.GetRowCellValue(e.RowHandle, view.Columns["ProperQty"]).GetIntNullToZero();

                if(Convert.ToDecimal(safeqty) > 0)
                {
                    if (Convert.ToDecimal(stockqty) < Convert.ToDecimal(safeqty))
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }
                
                if(Convert.ToDecimal(properqty) > 0)
                {
                    if (Convert.ToDecimal(stockqty) < Convert.ToDecimal(properqty))
                    {
                        e.Appearance.BackColor = Color.Orange;
                    }
                }


            }
        }

    }
}
