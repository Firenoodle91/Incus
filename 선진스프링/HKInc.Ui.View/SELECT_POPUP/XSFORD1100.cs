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
    /// 납품계획관리 Select 팝업
    /// </summary>
    public partial class XSFORD1100 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_ORD1100> ModelService = (IService<TN_ORD1100>)ProductionFactory.GetDomainService("TN_ORD1100");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;
        private string Value_1 = string.Empty;

        public XSFORD1100()
        {
            InitializeComponent();
        }

        public XSFORD1100(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText("DelivInfo");

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Constraint))
                Constraint = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
            if (parameter.ContainsKey(PopupParameter.Value_1))
                Value_1 = PopupParam.GetValue(PopupParameter.Value_1).GetNullToEmpty();

            GridExControl.MainGrid.MainView.RowClick += MainView_RowClick;
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
            if (Constraint == "XFORD1101") //출고증관리 추가 시
            {
                var customerListCheck = ModelService.GetChildList<TN_ORD1100>(p => p.TN_ORD1101List.Count == 0).Select(p => p.CustomerCode).Distinct().ToArray();
                lup_OrderCustomer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y" && p.BusinessManagementId == Value_1 && customerListCheck.Contains(p.CustomerCode)).ToList());
            }
            else
            {
                lup_OrderCustomer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
            }

            lup_Item.SetDefault(true, "ItemCode", DataConvert.GetCultureDataFieldName("ItemName"), ModelService.GetChildList<TN_STD1100>(p => p.UseFlag == "Y" && (p.TopCategory == MasterCodeSTR.TopCategory_WAN || p.TopCategory == MasterCodeSTR.TopCategory_BAN)).ToList());

            dt_DelivDate.SetTodayIsDay(0);
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);

            if (IsmultiSelect)
            {
                GridExControl.MainGrid.CheckBoxMultiSelect(true, "DelivNo", IsmultiSelect);

                GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
                GridExControl.MainGrid.SetEditable("_Check");
                GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");
            }

            GridExControl.MainGrid.AddColumn("OrderNo");
            GridExControl.MainGrid.AddColumn("OrderSeq", LabelConvert.GetLabelText("OrderSeq"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            GridExControl.MainGrid.AddColumn("TN_ORD1001.EndMonthDate", LabelConvert.GetLabelText("EndMonthDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM");
            GridExControl.MainGrid.AddColumn("CustomerCode", LabelConvert.GetLabelText("OrderCustomerCode"), false);
            GridExControl.MainGrid.AddColumn("TN_STD1400." + DataConvert.GetCultureDataFieldName("CustomerName"), LabelConvert.GetLabelText("OrderCustomer"));
            GridExControl.MainGrid.AddColumn("DelivNo", LabelConvert.GetLabelText("DelivNo"));
            GridExControl.MainGrid.AddColumn("DelivDate", LabelConvert.GetLabelText("DelivDate"), HorzAlignment.Center, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("ItemCode", LabelConvert.GetLabelText("ItemCode"));
            GridExControl.MainGrid.AddColumn("TN_STD1100." + DataConvert.GetCultureDataFieldName("ItemName"), LabelConvert.GetLabelText("ItemName"));
            GridExControl.MainGrid.AddColumn("TN_STD1100.CombineSpec", LabelConvert.GetLabelText("Spec"));
            GridExControl.MainGrid.AddColumn("DelivQty", LabelConvert.GetLabelText("DelivQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            
            if (Constraint == "XFPUR1600")
            {
                GridExControl.MainGrid.AddColumn("TurnKeyRemainQty", LabelConvert.GetLabelText("TurnKeyRemainQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            }
            else if (Constraint == "OutConfirmFlag")
            {
                GridExControl.MainGrid.AddColumn("OutRemainQty", LabelConvert.GetLabelText("OutRemainQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            }
            else if (Constraint == "XFORD1101")
            {
                GridExControl.MainGrid.AddColumn("RemainOutPlanQty", LabelConvert.GetLabelText("RemainQty"), HorzAlignment.Far, FormatType.Numeric, MasterCodeSTR.Numeric_N0);
            }

            GridExControl.MainGrid.AddColumn("DelivId", LabelConvert.GetLabelText("DelivId"));
            //GridExControl.MainGrid.AddColumn("ProductionFlag", LabelConvert.GetLabelText("ProductionFlag"));
            //GridExControl.MainGrid.AddColumn("TurnKeyFlag", LabelConvert.GetLabelText("TurnKeyFlag"));
            //GridExControl.MainGrid.AddColumn("OutConfirmFlag", LabelConvert.GetLabelText("OutConfirmFlag"));
            GridExControl.MainGrid.AddColumn("Memo", LabelConvert.GetLabelText("Memo"));

        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DelivId", ModelService.GetChildList<User>(p => p.Active == "Y").ToList(), "LoginId", "UserName");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("ProductionFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("TurnKeyFlag", "N");
            //GridExControl.MainGrid.SetRepositoryItemCheckEdit("OutConfirmFlag", "N");
            GridExControl.MainGrid.MainView.Columns["Memo"].ColumnEdit = new HKInc.Service.Controls.CommentGridButtonEdit(GridExControl, "Memo");
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
            var customerCode = lup_OrderCustomer.EditValue.GetNullToEmpty();

            if (Constraint.IsNullOrEmpty()) //전체 조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                  && (p.DelivDate >= dt_DelivDate.DateFrEdit.DateTime && p.DelivDate <= dt_DelivDate.DateToEdit.DateTime)
                                                               )
                                                               .OrderBy(p => p.DelivNo)
                                                               .ToList();
            }
            else if (Constraint == "TurkKeyFlag")  //턴키의뢰 조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                  && (p.DelivDate >= dt_DelivDate.DateFrEdit.DateTime && p.DelivDate <= dt_DelivDate.DateToEdit.DateTime)
                                                                  && (p.TurnKeyFlag == "Y")
                                                               )
                                                               .OrderBy(p => p.DelivNo)
                                                               .ToList();
            }
            else if (Constraint == "ProductionFlag")  //생산의뢰 조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                  && (p.DelivDate >= dt_DelivDate.DateFrEdit.DateTime && p.DelivDate <= dt_DelivDate.DateToEdit.DateTime)
                                                                  && (p.ProductionFlag == "Y")
                                                               )
                                                               .OrderBy(p => p.DelivNo)
                                                               .ToList();
            }
            else if (Constraint == "OutConfirmFlag")  //출고의뢰 조회
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                  && (p.DelivDate >= dt_DelivDate.DateFrEdit.DateTime && p.DelivDate <= dt_DelivDate.DateToEdit.DateTime)
                                                                  && (p.OutConfirmFlag == "Y")
                                                               )
                                                               .Where(p => p.OutRemainQty > 0)
                                                               .OrderBy(p => p.DelivNo)
                                                               .ToList();
            }
            else if (Constraint == "XFPUR1600") //턴키발주 추가 시
            {
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(customerCode) ? true : p.CustomerCode == customerCode)
                                                                  && (p.DelivDate >= dt_DelivDate.DateFrEdit.DateTime && p.DelivDate <= dt_DelivDate.DateToEdit.DateTime)
                                                                  && (p.TurnKeyFlag == "Y")
                                                               )
                                                               .Where(p => p.TurnKeyRemainQty > 0)
                                                               .OrderBy(p => p.DelivNo)
                                                               .ToList();
            }
            else if (Constraint == "XFORD1101") //출고증관리 추가 시
            {
                //var TN_ORD1101List = PopupParam.GetValue(PopupParameter.Value_1) as List<TN_ORD1101>;

                var CheckCustomerCodeList = ModelService.GetChildList<TN_STD1400>(p => p.BusinessManagementId == Value_1).ToList().Select(p => p.CustomerCode).ToArray();
                bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(itemCode) ? true : p.ItemCode == itemCode)
                                                                  && (string.IsNullOrEmpty(customerCode) ? (CheckCustomerCodeList.Contains(p.CustomerCode)) : p.CustomerCode == customerCode)
                                                                  && (p.OutConfirmFlag == "Y")
                                                                  && (p.DelivDate >= dt_DelivDate.DateFrEdit.DateTime && p.DelivDate <= dt_DelivDate.DateToEdit.DateTime)
                                                               )
                                                               .Where(p => p.RemainOutPlanQty > 0)
                                                               //.Where(p => TN_ORD1101List == null ? true : !TN_ORD1101List.Select(c=>c.DelivNo).Contains(p.DelivNo))
                                                               .OrderBy(p => p.DelivNo)
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
                var returnList = new List<TN_ORD1100>();
                var dataList = bindingSource.List as List<TN_ORD1100>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();
                foreach (var v in dataList.Where(p => p._Check == "Y").ToList())
                {
                    var returnObj = ModelService.GetList(p => p.DelivNo == v.DelivNo).FirstOrDefault();
                    if (returnObj != null)
                        returnList.Add(ModelService.Detached(returnObj));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_ORD1100)bindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var returnObj = (TN_ORD1100)bindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_ORD1100>();
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