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

namespace HKInc.Ui.View.SELECT_POPUP
{
    /// <summary>
    /// 품목 Select 팝업
    /// </summary>
    public partial class XSFSTD1400 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;

        public XSFSTD1400()
        {
            InitializeComponent();
        }

        public XSFSTD1400(PopupDataParam parameter, PopupCallback callback) : this()
        {
            this.PopupParam = parameter;
            this.Callback = callback;
            this.Text = LabelConvert.GetLabelText(this.Name);

            GridExControl = gridEx1;

            if (parameter.ContainsKey(PopupParameter.IsMultiSelect))
                IsmultiSelect = (bool)parameter.GetValue(PopupParameter.IsMultiSelect);
            if (parameter.ContainsKey(PopupParameter.Constraint))
                Constraint = parameter.GetValue(PopupParameter.Constraint).GetNullToEmpty();
            
            if (parameter.ContainsKey(PopupParameter.Value_1))
                tx_CustCodeName.EditValue = parameter.GetValue(PopupParameter.Value_1).GetNullToEmpty();
            
            

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
            //lup_Customer.SetDefault(true, "CustomerCode", DataConvert.GetCultureDataFieldName("CustomerName"), ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.CheckBoxMultiSelect(true, "ItemCode", IsmultiSelect);

            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("CustomerCode");
            GridExControl.MainGrid.AddColumn("CustomerName");
            GridExControl.MainGrid.AddColumn("CustomerNameENG");
            GridExControl.MainGrid.AddColumn("CustomerNameCHN");
            GridExControl.MainGrid.AddColumn("NationalCode", LabelConvert.GetLabelText("National"));
            GridExControl.MainGrid.AddColumn("CustomerType");
            GridExControl.MainGrid.AddColumn("RegistrationNo");
            GridExControl.MainGrid.AddColumn("CorporationNo");
            GridExControl.MainGrid.AddColumn("CustomerCategoryCode");
            GridExControl.MainGrid.AddColumn("CustomerCategoryType");
            GridExControl.MainGrid.AddColumn("Email");
            GridExControl.MainGrid.AddColumn("RepresentativeName");
            GridExControl.MainGrid.AddColumn("ZipCode");
            GridExControl.MainGrid.AddColumn("Address");
            //GridExControl.MainGrid.AddColumn("Address2");
            GridExControl.MainGrid.AddColumn("PhoneNumber");
            GridExControl.MainGrid.AddColumn("FaxNumber");
            GridExControl.MainGrid.AddColumn("TradingStartDate", LabelConvert.GetLabelText("TradingStartDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("TradingEndDate", LabelConvert.GetLabelText("TradingEndDate"), HorzAlignment.Default, FormatType.DateTime, "yyyy-MM-dd");
            GridExControl.MainGrid.AddColumn("DeadLine");
            GridExControl.MainGrid.AddColumn("BusinessManagementId", LabelConvert.GetLabelText("ManagerName"));
            GridExControl.MainGrid.AddColumn("ManagerName", false);
            GridExControl.MainGrid.AddColumn("ManagerPhoneNumber");
            GridExControl.MainGrid.AddColumn("CustomerBankCode");
            GridExControl.MainGrid.AddColumn("AccountNumber");
            GridExControl.MainGrid.AddColumn("Homepage");
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.SetEditable("_Check");
        }

        protected override void InitRepository()
        {

            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CustomerType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("NationalCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.NationalCode), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BusinessManagementId", ModelService.GetChildList<Model.Domain.User>(p => true).ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLine", DbRequestHandler.GetCommTopCode(MasterCodeSTR.DeadLine), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));


            GridExControl.MainGrid.SetRepositoryItemCheckEdit("_Check", "N");

        }

        protected override void InitDataLoad()
        {
            DataLoad();
        }

        protected override void DataLoad()
        {
            GridExControl.MainGrid.Clear();

            ModelService.ReLoad();

            var CustomerCode = tx_CustCodeName.EditValue.GetNullToEmpty();


            bindingSource.DataSource = ModelService.GetList(p => (p.CustomerName.Contains(CustomerCode) || p.CustomerCode.Contains(CustomerCode))
                                                                    )
                                                                    .OrderBy(p => p.CustomerName)
                                                                    .ToList();
            
            GridExControl.DataSource = bindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl);
        }

        protected override void Confirm()
        {
            if (bindingSource == null || bindingSource.DataSource == null) return;

            PopupDataParam param = new PopupDataParam();

            if (IsmultiSelect)
            {
                var returnList = new List<TN_STD1400>();
                var dataList = bindingSource.List as List<TN_STD1400>;
                var checkList = dataList.Where(p => p._Check == "Y").ToList();

                foreach (var v in dataList.Where(p => p._Check == "Y").ToList())
                {
                    var returnObj = ModelService.GetList(p => p.CustomerCode == v.CustomerCode).FirstOrDefault();
                    if (returnObj != null)
                        returnList.Add(ModelService.Detached(returnObj));
                }
                param.SetValue(PopupParameter.ReturnObject, returnList);
            }
            else
            {
                param.SetValue(PopupParameter.ReturnObject, ModelService.Detached((TN_STD1400)bindingSource.Current));
            }
            ReturnPopupArgument = new PopupArgument(param);
            ActClose();
        }

        private void MainView_RowClick(object sender, RowClickEventArgs e)
        {
            PopupDataParam param = new PopupDataParam();

            if (e.Clicks == 2)
            {
                var itemMaster = (TN_STD1400)bindingSource.Current;
                if (IsmultiSelect)
                {
                    var returnList = new List<TN_STD1400>();
                    if (itemMaster != null)
                        returnList.Add(ModelService.Detached(itemMaster));
                    param.SetValue(PopupParameter.ReturnObject, returnList);
                }
                else
                {
                    param.SetValue(PopupParameter.ReturnObject, ModelService.Detached(itemMaster));
                }

                ReturnPopupArgument = new PopupArgument(param);

                ActClose();
            }

        }
    }
}