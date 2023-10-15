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
    /// 영업계획 대비 매출현황
    /// </summary>
    public partial class XRREP5005 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        //IService<TN_ORD1600> MasterModel = (IService<TN_ORD1600>)ProductionFactory.GetDomainService("TN_ORD1600");
        IService<TN_ORD1601> DetailModel = (IService<TN_ORD1601>)ProductionFactory.GetDomainService("TN_ORD1601");

        IService<TN_STD1100> ItemModel = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        IService<TN_STD1400> CustModel = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");

        public XRREP5005()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;

            dt_OrderDate.DateTime = DateTime.Today;
        }        

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarButtonVisible(false);
            MasterGridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            MasterGridExControl.MainGrid.AddColumn("DelivNo", "납품계획번호");
            //MasterGridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerCode"));
            MasterGridExControl.MainGrid.AddColumn("CustomerName", LabelConvert.GetLabelText("CustomerName"));
            //MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("CarType", LabelConvert.GetLabelText("CarType"));
            
            //MasterGridExControl.MainGrid.AddColumn("OrderItemCost", "수주단가", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DelivDate", "납품예정일");
            MasterGridExControl.MainGrid.AddColumn("DelivQty", "납품예정\n계획수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueQty", "실 납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueMQty", "미납수량", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("DueRate", "달성율", HorzAlignment.Far, FormatType.Numeric, "n2");
            MasterGridExControl.MainGrid.AddColumn("OutYYMM", "납품 월");
            MasterGridExControl.MainGrid.AddColumn("OutQtyYYMM", "월 납품수량", HorzAlignment.Far, FormatType.Numeric, "n0");

            //MasterGridExControl.MainGrid.AddColumn("DuePlanCost", "납품예정계획금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("DueCost", "실 납품금액", HorzAlignment.Far, FormatType.Numeric, "n0");
            //MasterGridExControl.MainGrid.AddColumn("DueMCost", "미 달성금액", HorzAlignment.Far, FormatType.Numeric, "n0");


            MasterGridExControl.MainGrid.AddColumn("A01", "01", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A02", "02", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A03", "03", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A04", "04", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A05", "05", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A06", "06", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A07", "07", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A08", "08", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A09", "09", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A10", "10", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A11", "11", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A12", "12", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A13", "13", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A14", "14", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A15", "15", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A16", "16", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A17", "17", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A18", "18", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A19", "19", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A20", "20", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A21", "21", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A22", "22", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A23", "23", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A24", "24", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A25", "25", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A26", "26", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A27", "27", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A28", "28", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A29", "29", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A30", "30", HorzAlignment.Far, FormatType.Numeric, "n0");
            MasterGridExControl.MainGrid.AddColumn("A31", "31", HorzAlignment.Far, FormatType.Numeric, "n0");

            //MasterGridExControl.MainGrid.SetEditable(UserRight.HasEdit, "OrderItemCost");
            //LayoutControlHandler.SetRequiredGridHeaderColor<TN_ORD1300>(MasterGridExControl);
        }

        protected override void InitRepository()
        {

            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnAutoWidth = false;
            MasterGridExControl.MainGrid.MainView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;

            MasterGridExControl.MainGrid.MainView.Columns["DelivQty"].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //MasterGridExControl.MainGrid.SetRepositoryItemSpinEdit("ItemCost");
            MasterGridExControl.BestFitColumns();
        }

        protected override void InitCombo()
        {
            lup_CustomerCode.SetDefault(true, "CustomerCode", "CustomerName", CustModel.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_ItemCode.SetDefault(true, "ItemCode", "ItemName", ItemModel.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("OrderNo");

            MasterGridExControl.MainGrid.Clear();

            InitCombo();
            InitRepository();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;

                var monthdate = new SqlParameter("@MonthDate", dt_OrderDate.DateTime);
                var customercode = new SqlParameter("@CustomerCode", lup_CustomerCode.EditValue.GetNullToEmpty());
                var itemcode = new SqlParameter("@ItemCode", lup_ItemCode.EditValue.GetNullToEmpty());

                var result = context.Database.SqlQuery<TEMP_XRREP5005>
                    ("USP_GET_XRREP5005 @MonthDate, @CustomerCode, @ItemCode",
                    monthdate, customercode, itemcode).ToList();

                MasterGridBindingSource.DataSource = result.ToList();
                MasterGridExControl.DataSource = MasterGridBindingSource;
                MasterGridExControl.BestFitColumns();

            }

            GridRowLocator.SetCurrentRow();
            
        }



        private void  DetailMainView_RowStyle(object sender, RowStyleEventArgs e)
        {
            /*
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                var ttt = View.GetRowCellValue(e.RowHandle, View.Columns["ItemCost"]);

                int ItemCost = View.GetRowCellValue(e.RowHandle, View.Columns["ItemCost"]).GetIntNullToZero();
                int OutQty = View.GetRowCellValue(e.RowHandle, View.Columns["OutQty"]).GetIntNullToZero();

                int DueCost = ItemCost * OutQty;

                View.SetRowCellValue(e.RowHandle, View.Columns["DueCost"], "111");
            }
            */
        }
       
    }
}
