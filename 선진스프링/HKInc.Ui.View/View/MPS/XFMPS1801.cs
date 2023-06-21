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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using HKInc.Service.Handler;
using System.Collections.Generic;
using HKInc.Utils.Enum;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.TEMP;
using System.Windows.Forms;

namespace HKInc.Ui.View.View.MPS
{
    /// <summary>
    /// 포장이력
    /// </summary>
    public partial class XFMPS1801 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_MPS1202> ModelService = (IService<TN_MPS1202>)ProductionFactory.GetDomainService("TN_MPS1202");
        private BindingSource gridEx2BindingSource = new BindingSource();

        public XFMPS1801()
        {
            InitializeComponent();
            GridExControl = gridEx1;
        }

        protected override void InitCombo()
        {
            dt_WorkDate.SetTodayIsDay(0);

            lup_ItemCode.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.AddColumn("WorkNo", LabelConvert.GetLabelText("WorkNo"));
            GridExControl.MainGrid.AddColumn("CreateTime", LabelConvert.GetLabelText("PackDateTime"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd HH:mm:ss");
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_MPS1201.TN_MPS1200.TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_MPS1201.TN_MPS1200.TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ProductLotNo", LabelConvert.GetLabelText("ProductLotNo"));
            GridExControl.MainGrid.AddColumn("TN_MPS1201.TN_MPS1200.Temp1", LabelConvert.GetLabelText("CustomerLotNo"));
            GridExControl.MainGrid.AddColumn("OkQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");
            //GridExControl.MainGrid.AddColumn("WhCode", LabelConvert.GetLabelText("WhName"));
            //GridExControl.MainGrid.AddColumn("PositionCode", LabelConvert.GetLabelText("PositionName"));
            //GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("InQty"), HorzAlignment.Far, FormatType.Numeric, "#,#.##");

            gridEx2.SetToolbarVisible(false);
            gridEx2.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            gridEx2.MainGrid.AddColumn("ItemName", LabelConvert.GetLabelText("ItemName"));
            gridEx2.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            gridEx2.MainGrid.AddColumn("OkQty", "입고량합계", HorzAlignment.Far, FormatType.Numeric, "#,#.##");
        }

        protected override void InitRepository()
        {
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("ProcessCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.Process), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("WhCode", ModelService.GetChildList<TN_WMS1000>(p => p.UseFlag == "Y").ToList(), "WhCode", DataConvert.GetCultureDataFieldName("WhName"), false);
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("PositionCode", ModelService.GetChildList<TN_WMS2000>(p => p.UseFlag == "Y").ToList(), "PositionCode", "PositionName", false);

            gridEx2.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => true), "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"));
        }

        protected override void DataLoad()
        {
            GridRowLocator.GetCurrentRow();
            GridExControl.MainGrid.Clear();
            gridEx2.MainGrid.Clear();

            ModelService.ReLoad();

            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();
            var itemCode = lup_ItemCode.EditValue.GetNullToEmpty();

            var toDate = new DateTime(dt_WorkDate.DateToEdit.DateTime.Year, dt_WorkDate.DateToEdit.DateTime.Month, dt_WorkDate.DateToEdit.DateTime.Day, 23, 59, 59);
            GridBindingSource.DataSource = ModelService.GetList(p => (p.CreateTime >= dt_WorkDate.DateFrEdit.DateTime
                                                                        && p.CreateTime <= toDate)
                                                                    && (p.ProcessCode == "P100")
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (p.ProductLotNo.Contains(productLotNo))
                                                                 )
                                                                 .OrderBy(p=>p.CreateTime)
                                                                 .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            GridRowLocator.SetCurrentRow();

            using (var context = new HKInc.Ui.Model.Context.ProductionContext(HKInc.Utils.Common.ServerInfo.GetConnectString(HKInc.Utils.Common.GlobalVariable.ProductionDataBase)))
            {
                context.Database.CommandTimeout = 0;
                var _frDate = new SqlParameter("@FrDate", dt_WorkDate.DateFrEdit.DateTime);
                var _toDate = new SqlParameter("@ToDate", toDate);
                var _itemCode = new SqlParameter("@ItemCode", itemCode);
                var _productLotNo = new SqlParameter("@ProductLotNo", productLotNo);

                var result = context.Database.SqlQuery<TEMP_MPS1801_SUM>("USP_GET_MPS1801_SUM @FrDate, @ToDate, @ItemCode, @ProductLotNo", _frDate, _toDate, _itemCode, _productLotNo).ToList();
                if (result == null) return;
                gridEx2BindingSource.DataSource = result;
            }

            gridEx2.DataSource = gridEx2BindingSource;
            gridEx2.BestFitColumns();
        }

        protected override void GridRowDoubleClicked(){}
    }
}
