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
using HKInc.Utils.Enum;
using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.View.STD_POPUP
{
    public partial class XPFSTD1400 : HKInc.Service.Base.ListEditFormTemplate
    {
        private IService<TN_STD1400> Std1400Service;

        public XPFSTD1400(PopupDataParam param, PopupCallback callback)
        {
            InitializeComponent();
            PopupParam = param;
            Callback = callback;
            ModelBindingSource = bindingSource1; // BindingSource설정

            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            chk_MyCompanyFlag.EditValueChanged += Chk_MyCompanyFlag_EditValueChanged;
        }

        

        protected override void InitBindingSource()
        {
            // Service설정 부모에게서 넘어온다
            Std1400Service = (IService<TN_STD1400>)PopupParam.GetValue(PopupParameter.Service);
        }

        protected override void AddControlList()
        {
            ControlEnableList.Add("CustomerCode", tx_CustomerCode);
            ControlEnableList.Add("CustomerName", tx_CustomerName);
            ControlEnableList.Add("CustomerNameENG", tx_CustomerNameENG);
            ControlEnableList.Add("CustomerNameCHN", tx_CustomerNameCHN);
            ControlEnableList.Add("CustomerType", lup_CustomerType);
            ControlEnableList.Add("RegistrationNo", tx_RegistrationNo);
            ControlEnableList.Add("MyCompanyFlag", chk_MyCompanyFlag);
            ControlEnableList.Add("CorporationNo", tx_CorporationNo);
            ControlEnableList.Add("Email", tx_Email);
            ControlEnableList.Add("CustomerCategoryType", tx_CustomerCategoryType);
            ControlEnableList.Add("CustomerCategoryCode", tx_CustomerCategoryCode);
            ControlEnableList.Add("NationalCode", lup_NationalCode);
            ControlEnableList.Add("RepresentativeName", tx_RepresentativeName);
            ControlEnableList.Add("ZipCode", tx_ZipCode);
            ControlEnableList.Add("Address", tx_Address);
            ControlEnableList.Add("Address2", tx_Address2);
            ControlEnableList.Add("PhoneNumber", tx_PhoneNumber);
            ControlEnableList.Add("FaxNumber", tx_FaxNumber);
            ControlEnableList.Add("ManagerName", tx_ManagerName);
            ControlEnableList.Add("ManagerPhoneNumber", tx_ManagerPhoneNumber);
            ControlEnableList.Add("CustomerBankCode", tx_CustomerBankCode);
            ControlEnableList.Add("AccountNumber", tx_AccountNumber);
            //ControlEnableList.Add("BusinessManagementId", lup_BusinessManagementId);
            ControlEnableList.Add("BusinessManagementId", lup_BusinessManagementId);
            ControlEnableList.Add("TradingStartDate", dt_TradingStartDate);
            ControlEnableList.Add("TradingEndDate", dt_TradingEndDate);
            ControlEnableList.Add("DeadLine", lup_DeadLine);
            ControlEnableList.Add("Memo", memo_Memo);
            ControlEnableList.Add("Homepage", tx_Homepage);
            ControlEnableList.Add("UseFlag", chk_UseFlag);
            
            #region 데이터 바인딩 연결
            foreach (var control in ControlEnableList)
            {
                control.Value.DataBindings.Clear();
                control.Value.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.ModelBindingSource, control.Key, true));
            }
            #endregion

            LayoutControlHandler.SetRequiredLabelText<TN_STD1400>(new TN_STD1400(), ControlEnableList, this.Controls);
        }

        protected override void InitCombo()
        {
            lup_CustomerType.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.CustomerType));
            lup_NationalCode.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.NationalCode), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
            //lup_BusinessManagementId.SetDefault(true, "LoginId", "UserName", Std1400Service.GetChildList<User>(p => p.Active == "Y" && p.MainYn == "02"));
            lup_DeadLine.SetDefault(true, "CodeVal", DataConvert.GetCultureDataFieldName("CodeName"), DbRequestHandler.GetCommTopCode(MasterCodeSTR.DeadLine), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
        }

        protected override void DataLoad()
        {
            tx_CustomerCode.ReadOnly = true;
            if (EditMode == PopupEditMode.New) // 신규 추가
            {
                ModelBindingSource.Add(new TN_STD1400 { UseFlag = "Y", MyCompanyFlag = "N", CustomerCode = DbRequestHandler.GetSeqStandard("CUST") });
                ModelBindingSource.DataSource = Std1400Service.Insert((TN_STD1400)ModelBindingSource.Current);
            }
            else
            {
                // Update
                TN_STD1400 obj = (TN_STD1400)PopupParam.GetValue(PopupParameter.KeyValue);
                ModelBindingSource.DataSource = obj;
            }
        }

        protected override void DataSave()
        {
            ModelBindingSource.EndEdit(); //저장전 수정사항 Posting
            TN_STD1400 obj = ModelBindingSource.Current as TN_STD1400;

            //string[] rscno = obj.RegistrationNo.Split('-');
            string[] rscno;
            if (obj.RegistrationNo != null)
            {
                rscno = obj.RegistrationNo.Split('-');

                if (rscno.Length < 3)
                {
                    MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_125));
                    return;
                }
            }

            if (obj.MyCompanyFlag == "Y")
            {
                var myCompanyCheckobj = Std1400Service.GetList(p => p.CustomerCode != obj.CustomerCode && p.MyCompanyFlag == "Y").FirstOrDefault();
                if (myCompanyCheckobj != null)
                {
                    myCompanyCheckobj.MyCompanyFlag = "N";
                    Std1400Service.Update(myCompanyCheckobj);
                }
            }

            if (EditMode == PopupEditMode.New)
            {
                ModelBindingSource.DataSource = Std1400Service.Insert(obj);
            }
            else
            {
                Std1400Service.Update(obj);
            }
            Std1400Service.Save();

            #region Grid Focus를 위한 수정 필요
            PopupDataParam param = new PopupDataParam();
            param.SetValue(PopupParameter.GridRowId_1, obj.CustomerCode);
            ReturnPopupArgument = new PopupArgument(param);
            #endregion

            IsFormControlChanged = false;
            EditMode = PopupEditMode.Saved;
        }
        
        private void Chk_MyCompanyFlag_EditValueChanged(object sender, EventArgs e)
        {

            if(EditMode == PopupEditMode.New)
            {
                string MyCompanyFlag_value = chk_MyCompanyFlag.EditValue.GetNullToEmpty();

                if (MyCompanyFlag_value == "Y")
                {
                    var MyCompanyFlag_chk = Std1400Service.GetList(p => p.MyCompanyFlag == "Y").ToList();

                    if (MyCompanyFlag_chk.Count() > 0)
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_154));
                        chk_MyCompanyFlag.EditValue = "N";
                        return;
                    }
                }


            }
            else
            {
                string MyCompanyFlag_value = chk_MyCompanyFlag.EditValue.GetNullToEmpty();

                string customer_code = tx_CustomerCode.EditValue.GetNullToEmpty();

                if (MyCompanyFlag_value == "Y")
                {
                    var MyCompanyFlag_chk = Std1400Service.GetList(p => p.MyCompanyFlag == "Y" && p.CustomerCode != customer_code).ToList();

                    if (MyCompanyFlag_chk.Count() > 0)
                    {
                        MessageBoxHandler.Show(MessageHelper.GetStandardMessage((int)StandardMessageEnum.M_154));
                        chk_MyCompanyFlag.EditValue = "N";
                        return;
                    }
                }
            }
            
        }
    }
}