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
    /// 출고증 Select 팝업
    /// </summary>
    public partial class XSFORD1101 : Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_ORD1101> ModelService = (IService<TN_ORD1101>)ProductionFactory.GetDomainService("TN_ORD1101");
        private List<VI_PROD_STOCK_ITEM> StockList = new List<VI_PROD_STOCK_ITEM>();
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;

        public XSFORD1101()
        {
            InitializeComponent();
        }

        public XSFORD1101(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("ShipmentReportRef");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Constraint))
                Constraint = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
            GridExControl.MainGrid.MainView.CustomColumnDisplayText += MainView_CustomColumnDisplayText;
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
            dt_OutDatePlan.SetTodayIsDay();
            dt_OutDatePlan.DateFrEdit.EditValue = DateTime.Today;

            lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && 
                (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN_Outsourcing)).ToList()); // 20210524 오세완 차장 반제품도 출고를 할 수 있어서 반제품(타사)를 추가 처리
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            if (IsmultiSelect)
            {
                GridExControl.MainGrid.CheckBoxMultiSelect(true, "RowId", IsmultiSelect);

                GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
                GridExControl.MainGrid.SetEditable("_Check");
                GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            }
            GridExControl.MainGrid.AddColumn("RowId", false);
            GridExControl.MainGrid.AddColumn("OrderNo", LabelConvert.GetLabelText("OrderNo"));
            GridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, "n0");
            GridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("CustomerName"));
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CarType", LabelConvert.GetLabelText("CarType"));
            GridExControl.MainGrid.AddColumn("StockQty", LabelConvert.GetLabelText("StockQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("TN_ORD1100.DelivQty", LabelConvert.GetLabelText("DelivQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("TN_ORD1100.DelivDate", LabelConvert.GetLabelText("DelivDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("OutDate", LabelConvert.GetLabelText("OutDatePlan"));
            GridExControl.MainGrid.AddColumn("OutId", LabelConvert.GetLabelText("OutId"), false);
            //GridExControl.MainGrid.AddColumn("OutQty", LabelConvert.GetLabelText("OuQtyPlan"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N2);
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"), false);
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
            GridExControl.MainGrid.SetRepositoryItemDateEdit("OutDate");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("OutId", ModelService.GetChildList<User>(p => p.Active == "Y"), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerCode", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList(), "CustomerCode", "CustomerName");
            //GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("TN_STD1100.CarType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CarType), "CodeVal", Service.Helper.DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.BestFitColumns();
        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();
            StockList.Clear();

            ModelService.ReLoad();

            var frDate = dt_OutDatePlan.DateFrEdit.DateTime;
            var toDate = dt_OutDatePlan.DateToEdit.DateTime;
            var itemCode = lup_Item.EditValue.GetNullToEmpty();
            var customerCode = lup_Customer.EditValue.GetNullToEmpty();

            bindingSource.DataSource = ModelService.GetList(p => (p.OutDate >= frDate && p.OutDate <= toDate)
                                                                    && (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                    && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                               )
                                                               .ToList();
            GridExControl.DataSource = bindingSource;
            GridExControl.BestFitColumns();

            //재고량 가져오기
            StockList.AddRange(ModelService.GetChildList<VI_PROD_STOCK_ITEM>(p => true).ToList());
        }

        protected override void Confirm()
        {
            if (bindingSource == null || bindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<TN_ORD1101>();
                var dataList = bindingSource.List as List<TN_ORD1101>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in dataList.Where(p => p._Check == "Y").ToList())
                {
                    var returnObj = ModelService.GetList(p => p.RowId == v.RowId).FirstOrDefault();
                    if (returnObj != null)
                        returnList.Add(ModelService.Detached(returnObj));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_ORD1101)bindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var returnObj = (TN_ORD1101)bindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_ORD1101>();
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

        private void MainView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "StockQty")
            {
                var itemCode = GridExControl.MainGrid.MainView.GetListSourceRowCellValue(e.ListSourceRowIndex, "ItemCode").GetNullToEmpty();
                if (!itemCode.IsNullOrEmpty())
                {
                    var stockObj = StockList.Where(p => p.ItemCode == itemCode).FirstOrDefault();
                    e.DisplayText = stockObj == null ? "0" : stockObj.SumStockQty.ToString("#,#.##");
                }
            }
        }
    }
}