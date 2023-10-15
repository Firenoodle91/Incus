using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 제품 재고 Select 팝업
    /// </summary>
    public partial class XSFPROD_STOCK : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<VI_PROD_STOCK_PRODUCT_LOT_NO> ModelService = (IService<VI_PROD_STOCK_PRODUCT_LOT_NO>)ProductionFactory.GetDomainService("VI_PROD_STOCK_PRODUCT_LOT_NO");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;

        public XSFPROD_STOCK()
        {
            InitializeComponent();
        }

        public XSFPROD_STOCK(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("StockInfo");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Constraint))
                Constraint = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;

            if (Constraint == "XFORD1200")
            {
                var itemCode = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
                lup_Item.EditValue = itemCode;
                lup_Item.ReadOnly = true;
            }
        }

        protected override void InitToolbarButton()
        {
            SetToolbarButtonVisible(false);
            SetToolbarButtonVisible(ToolbarButton.Refresh, true);
            SetToolbarButtonVisible(ToolbarButton.Confirm, true);
            SetToolbarButtonVisible(ToolbarButton.Close, true);
        }

        protected override void InitCombo()
        {
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            if (IsmultiSelect)
            {
                GridExControl.MainGrid.CheckBoxMultiSelect(true, "RowIndex", IsmultiSelect);

                GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
                GridExControl.MainGrid.SetEditable("_Check");
                GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            }
            
            GridExControl.MainGrid.AddColumn("RowIndex", false);
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("ProductLotNo");
            GridExControl.MainGrid.AddColumn("InQty", LabelConvert.GetLabelText("ResultQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OutQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("CarryOverQty", LabelConvert.GetLabelText("CarryOverQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);

        }

        protected override void InitRepository()
        {
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var productLotNo = tx_ProductLotNo.EditValue.GetNullToEmpty();

            if (Constraint.IsNullOrEmpty())
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(productLotNo) ? true : p.ProductLotNo == productLotNo)
                                                               )
                                                               .OrderBy(p => p.ItemCode)
                                                               .ThenBy(p => p.ProductLotNo)                                                               
                                                               .ToList();
            }
            else if (Constraint == "XFORD1200") //제품출고디테일 추가 시
            {
                var TN_ORD1201List = PopupParam.GetValue(PopupParameter.Value_2) as List<TN_ORD1201>;

                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(productLotNo) ? true : p.ProductLotNo == productLotNo)
                                                                  && p.StockQty > 0
                                                               )
                                                               .Where(p => TN_ORD1201List == null ? true : !TN_ORD1201List.Select(c => c.ProductLotNo).Contains(p.ProductLotNo))
                                                               .OrderBy(p => p.ItemCode)
                                                               .ThenBy(p => p.ProductLotNo)
                                                               .ToList();
            }
         
            GridExControl.DataSource = bindingSource;
            GridExControl.BestFitColumns();
        }

        protected override void Confirm()
        {
            if (bindingSource == null || bindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<VI_PROD_STOCK_PRODUCT_LOT_NO>();
                var dataList = bindingSource.List as List<VI_PROD_STOCK_PRODUCT_LOT_NO>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in dataList.Where(p => p._Check == "Y").ToList())
                {
                    var returnObj = ModelService.GetList(p => p.ItemCode == v.ItemCode && p.ProductLotNo == v.ProductLotNo).FirstOrDefault();
                    if (returnObj != null)
                        returnList.Add(ModelService.Detached(returnObj));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((VI_PROD_STOCK_PRODUCT_LOT_NO)bindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var returnObj = (VI_PROD_STOCK_PRODUCT_LOT_NO)bindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<VI_PROD_STOCK_PRODUCT_LOT_NO>();
                    if (returnObj != null)
                        returnList.Add(ModelService.Detached(returnObj));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(returnObj));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}