using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Utils.Common;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.BAN
{
    /// <summary>
    /// 20210616 오세완 차장
    /// 대영 스타일 반제품재고관리, 이월없이 간단하게
    /// </summary>
    public partial class XFBAN_STOCK_V2 : HKInc.Service.Base.ListMasterDetailFormTemplate
    {
        #region 전역변수
        IService<VI_BAN_STOCK_ITEM_V2> ModelService = (IService<VI_BAN_STOCK_ITEM_V2>)ProductionFactory.GetDomainService("VI_BAN_STOCK_ITEM_V2");
        private string searchItemCode;
        #endregion

        public XFBAN_STOCK_V2()
        {
            InitializeComponent();
            MasterGridExControl = gridEx1;
            DetailGridExControl = gridEx2;

        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품(타사) 조회 조건 추가, 자재입고로 받은 반제품(타사)를 입고 받을 수 있게 안배

        }

        protected override void InitGrid()
        {
            MasterGridExControl.SetToolbarVisible(false);
            MasterGridExControl.MainGrid.AddColumn("RowIndex", false);
            MasterGridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.ItemName1", LabelConvert.GetLabelText("ItemName1"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.Unit", LabelConvert.GetLabelText("Unit"));
            MasterGridExControl.MainGrid.AddColumn("TN_STD1100.SafeQty", LabelConvert.GetLabelText("SafeQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumInQty", LabelConvert.GetLabelText("SumInQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumOutQty", LabelConvert.GetLabelText("SumOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
            MasterGridExControl.MainGrid.AddColumn("SumStockQty", LabelConvert.GetLabelText("SumStockQty"), HorzAlignment.Far, FormatType.Numeric, "#,0.##");
         
            DetailGridExControl.SetToolbarButtonVisible(false);
            DetailGridExControl.MainGrid.AddColumn("Division", LabelConvert.GetLabelText("Division"), HorzAlignment.Center, true);
            DetailGridExControl.MainGrid.AddColumn("InOutDate", LabelConvert.GetLabelText("InOutDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            DetailGridExControl.MainGrid.AddColumn("InOutLotNo", LabelConvert.GetLabelText("InOutLotNo"));
            DetailGridExControl.MainGrid.AddColumn("InOutQty", LabelConvert.GetLabelText("InOutQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            DetailGridExControl.MainGrid.AddColumn("InOutId", LabelConvert.GetLabelText("InOutId"));
            DetailGridExControl.MainGrid.AddColumn("InWhCode", LabelConvert.GetLabelText("InWhCode"));
            DetailGridExControl.MainGrid.AddColumn("InWhPosition", LabelConvert.GetLabelText("InWhPosition"));
        }

        protected override void InitRepository()
        {
            MasterGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.Unit", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));

            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InOutId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), true);
            DetailGridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("InWhPosition", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", true);

            MasterGridExControl.BestFitColumns();
            DetailGridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow("RowIndex");

            MasterGridExControl.MainGrid.Clear();
            DetailGridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            searchItemCode = lup_Item.EditValue.GetNullToEmpty();

            MasterGridExControl.MainGrid.Columns.Clear();
            DetailGridExControl.MainGrid.Columns.Clear();
            InitGrid();
            InitRepository();
            
            MasterGridBindingSource.DataSource = ModelService.GetList(p => string.IsNullOrEmpty(searchItemCode) ? true : p.ItemCode == searchItemCode).OrderBy(p => p.ItemCode).ToList();
            MasterGridExControl.DataSource = MasterGridBindingSource;
            MasterGridExControl.BestFitColumns();
            GridRowLocator.SetCurrentRow();
        }

        protected override void MasterFocusedRowChanged()
        {
            var masterObj = MasterGridBindingSource.Current as VI_BAN_STOCK_ITEM_V2;
            if (masterObj == null)
            {
                DetailGridExControl.MainGrid.Clear();
                return;
            }

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                //var Date = new SqlParameter("@Date", DateTime.Today);
                var ItemCode = new SqlParameter("@ItemCode", masterObj.ItemCode);
                //DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_BAN_STOCK_DETAIL>("USP_GET_BAN_STOCK_DETAIL @Date,@ItemCode", Date, ItemCode).OrderBy(p => p.ItemCode).ToList();
                DetailGridBindingSource.DataSource = context.Database.SqlQuery<TEMP_BAN_STOCK_DETAIL>("USP_GET_BAN_STOCK_DETAIL_V2 @ItemCode", ItemCode).OrderBy(p => p.ItemCode).ToList(); // 20210709 오세완 차장 월별조건 없는 프로시저로 교체
            }

            DetailGridExControl.DataSource = DetailGridBindingSource;
            DetailGridExControl.BestFitColumns();
        }
    }
}