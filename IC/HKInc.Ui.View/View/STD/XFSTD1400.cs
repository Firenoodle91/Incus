using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Service.Handler;
using HKInc.Utils.Interface.Popup;
using HKInc.Ui.View.PopupFactory;
using DevExpress.Utils;

namespace HKInc.Ui.View.View.STD
{
    public partial class XFSTD1400 : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        public XFSTD1400()
        {
            InitializeComponent();
            GridExControl = gridEx1;

            rbo_UseFlag.SetLabelText(LabelConvert.GetLabelText("Use"), LabelConvert.GetLabelText("NotUse"), LabelConvert.GetLabelText("All"));
        }
        protected override void InitGrid()
        {
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
            GridExControl.MainGrid.AddColumn("ManagerName",false);
            GridExControl.MainGrid.AddColumn("ManagerPhoneNumber");
            GridExControl.MainGrid.AddColumn("CustomerBankCode");
            GridExControl.MainGrid.AddColumn("AccountNumber");
            GridExControl.MainGrid.AddColumn("Homepage");
            GridExControl.MainGrid.AddColumn("Memo");
            GridExControl.MainGrid.AddColumn("UseFlag", LabelConvert.GetLabelText("UseFlag"));
        }

        protected override void InitRepository()
        {
            GridExControl.MainGrid.SetRepositoryItemCheckEdit("UseFlag", "N");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("CustomerType", DbRequestHandler.GetCommTopCode(MasterCodeSTR.CustomerType), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("NationalCode", DbRequestHandler.GetCommTopCode(MasterCodeSTR.NationalCode), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("BusinessManagementId", ModelService.GetChildList<Model.Domain.User>(p => true).ToList(), "LoginId", "UserName");
            GridExControl.MainGrid.SetRepositoryItemSearchLookUpEdit("DeadLine", DbRequestHandler.GetCommTopCode(MasterCodeSTR.DeadLine), "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"));
            GridExControl.BestFitColumns();
        }

        protected override void DataLoad()
        {
            InitRepository();
            #region Grid Focus를 위한 코드
            GridRowLocator.GetCurrentRow("CustomerCode", PopupDataParam.GetValue(PopupParameter.GridRowId_1));

            //refresh 초기화
            PopupDataParam.SetValue(PopupParameter.GridRowId_1, null);
            #endregion

            GridExControl.MainGrid.Clear();
            ModelService.ReLoad();

            var CustomerCodeName = tx_CustomerCodeName.EditValue.GetNullToEmpty();
            var radioValue = rbo_UseFlag.SelectedValue.GetNullToEmpty();

            GridBindingSource.DataSource = ModelService.GetList(p => (p.CustomerName.Contains(CustomerCodeName) || p.CustomerCode.Contains(CustomerCodeName))
                                                                      && (radioValue == "A" ? true : p.UseFlag == radioValue)
                                                                    )
                                                                    .OrderBy(p => p.CustomerName)
                                                                    .ToList();

            GridExControl.DataSource = GridBindingSource;
            GridExControl.BestFitColumns();

            #region Grid Focus를 위한 수정 필요
            GridRowLocator.SetCurrentRow();
            #endregion

            SetRefreshMessage(GridExControl);
        }

        protected override void DataSave()
        {
            GridExControl.MainGrid.PostEditor();
            GridBindingSource.EndEdit();

            ModelService.Save();
            DataLoad();
        }
        
        protected override void DeleteRow()
        {
            TN_STD1400 obj = GridBindingSource.Current as TN_STD1400;

            if (obj != null)
            {
                DialogResult result = MessageBoxHandler.Show(string.Format(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_50), LabelConvert.GetLabelText("CustomerInfo")), LabelConvert.GetLabelText("Warning"), MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    obj.UseFlag = "N";
                    GridExControl.BestFitColumns();
                }
            }
        }

        protected override IPopupForm GetPopupForm(PopupDataParam param)
        {
            return ProductionPopupFactory.GetPopupForm(ProductionPopupView.PFSTD1400, param, PopupRefreshCallback);
        }

        protected override PopupDataParam AddServiceToPopupDataParam(PopupDataParam param)
        {
            param.SetValue(PopupParameter.Service, ModelService);
            return param;
        }
    }
}