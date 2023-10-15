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
using HKInc.Ui.Model.Domain;
using HKInc.Ui.View.ProductionService;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.SELECT_Popup
{
    /// <summary>
    /// 20220408 오세완 차장
    /// 거래처 조회 팝업
    /// </summary>
    public partial class XSFSTD1400 : HKInc.Service.Base.PopupCallbackFormTemplate
    {
        #region 전역변수
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        BindingSource bindingSource = new BindingSource();
        private bool IsmultiSelect = true;
        private string Constraint = string.Empty;
        #endregion

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
            lup_Cust.SetDefault(true, "CustomerCode", "CustomerName", ModelService.GetChildList<TN_STD1400>(p => p.UseFlag == "Y").ToList());
        }

        protected override void InitGrid()
        {
            GridExControl.SetToolbarVisible(false);
            GridExControl.MainGrid.CheckBoxMultiSelect(true, "CustomerCode", IsmultiSelect);

            GridExControl.MainGrid.AddColumn("_Check", LabelConvert.GetLabelText("Select"));
            GridExControl.MainGrid.AddColumn("CustomerCode");
            GridExControl.MainGrid.AddColumn("CustomerName");
            GridExControl.MainGrid.AddColumn("NationalCode", LabelConvert.GetLabelText("National"));
            GridExControl.MainGrid.AddColumn("CustType", "거래처구분");

            GridExControl.MainGrid.AddColumn("RegistrationNo");
            GridExControl.MainGrid.AddColumn("CorporationNo");
            GridExControl.MainGrid.AddColumn("CustomerCategoryCode", "업태");
            GridExControl.MainGrid.AddColumn("CustomerCategoryType", "업종");
            GridExControl.MainGrid.AddColumn("Email");

            GridExControl.MainGrid.AddColumn("RepresentativeName");
            GridExControl.MainGrid.AddColumn("ZipCode");
            GridExControl.MainGrid.AddColumn("Address");
            GridExControl.MainGrid.AddColumn("TelePhone");
            GridExControl.MainGrid.AddColumn("Fax");

            GridExControl.MainGrid.AddColumn("CustomerBankCode");
            GridExControl.MainGrid.AddColumn("AccountNumber");
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
            GridExControl.MainGrid.SetEditable("_Check");
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustType", DbRequestHandler.GetCommCode(MasterCodeSTR.CustType), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustomerCategoryCode", DbRequestHandler.GetCommCode(MasterCodeSTR.BusinessType), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemLookUpEdit("CustomerCategoryType", DbRequestHandler.GetCommCode(MasterCodeSTR.BusinessCode), "Mcode", "Codename");
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("NationalCode", DbRequestHandler.GetCommCode(MasterCodeSTR.NationalCode), "Mcode", "Codename");
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

            string sCustomerCode = lup_Cust.EditValue.GetNullToEmpty();

            bindingSource.DataSource = ModelService.GetList(p => (string.IsNullOrEmpty(sCustomerCode) ? true : p.CustomerCode == sCustomerCode) && 
                                                                 p.UseFlag == "Y").OrderBy(p => p.CustomerName).ToList();
            
            GridExControl.DataSource = bindingSource;
            GridExControl.BestFitColumns();
            SetRefreshMessage(GridExControl.MainGrid.RecordCount);
        }

        protected override void Confirm()
        {
            if (bindingSource == null || bindingSource.DataSource == null)
                return;

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