using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Security.Principal;

using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Handler;

namespace HKInc.Main
{
    public partial class ChangeSetting : DevExpress.XtraEditors.XtraForm
    {
        bool isPasswordChanged = false;

        public ChangeSetting()
        {
            InitializeComponent();
            InitForm();
            InitControls();
        }

        void InitForm()
        {
            SetRadionGroup();
            SetLoginPathRadioGroup();
            SetNotificationPathRadioGroup();
            InitLupSkin();

            lupSkin.SelectedIndexChanged += skin_SelectedIndexChanged;
            rdoGrpCulture.EditValueChanged += rdoGrpCulture_EditValueChanged;
            rdoGrpLogin.EditValueChanged += rdoGrpLogin_EditValueChanged;
            rdoGrpNoti.EditValueChanged += rdoGrpNoti_EditValueChanged;
            
            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void TxtPwd_EditValueChanged(object sender, EventArgs e)
        {
            isPasswordChanged = true;
        }

        void InitControls()
        {
            ILabelConvert LabelConvert = HelperFactory.GetLabelConvert();

            btnConfirm.Text = LabelConvert.GetLabelText("OK");
            btnCancel.Text = LabelConvert.GetLabelText("Cancel");            
            labelNewPassword.Text = LabelConvert.GetLabelText("NewPassword");
            labelConfirmPassword.Text = LabelConvert.GetLabelText("ConfirmPassword");
            labelLogin.Text = LabelConvert.GetLabelText("Login");
            labelLanguage.Text = LabelConvert.GetLabelText("Language");
            labelNotification.Text = LabelConvert.GetLabelText("Notification");
            labelSkin.Text = LabelConvert.GetLabelText("Skin");

            txtCnfPwd.EditValueChanged += TxtPwd_EditValueChanged;
            txtNewPwd.EditValueChanged += TxtPwd_EditValueChanged;
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void btnConfirm_Click(object sender, EventArgs e)
        {
            if (isPasswordChanged)
            { 
                // Password 처리
                if (txtNewPwd.Text.Equals(txtCnfPwd.Text))
                {
                    if(!ValidatePasswordFormat(txtCnfPwd.Text))                    
                    {                        
                        txtCnfPwd.Text = null;
                        txtNewPwd.Text = null;
                        txtNewPwd.Focus();
                        return;
                    }
                }
                else
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(16));
                    txtCnfPwd.Focus();
                    return;
                }
            }
            Close();
        }

        void skin_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit comboBox = sender as ComboBoxEdit;
            string skinName = comboBox.Text;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = skinName;
            HKInc.Service.Handler.RegistryHandler.SetValue(HKInc.Utils.Common.GlobalVariable.SkinPath, "Skin", skinName);
        }

        void rdoGrpCulture_EditValueChanged(object sender, EventArgs e)
        {
            HKInc.Service.Handler.RegistryHandler.SetValue(HKInc.Utils.Common.GlobalVariable.CulturePath, "Culture", rdoGrpCulture.EditValue.GetNullToEmpty());
            HKInc.Utils.Common.GlobalVariable.Culture = rdoGrpCulture.EditValue.GetNullToEmpty();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(HKInc.Utils.Common.GlobalVariable.Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(HKInc.Utils.Common.GlobalVariable.Culture);
        }

        void rdoGrpLogin_EditValueChanged(object sender, EventArgs e)
        {
            string domainName, domainUserId;
            if (GetDomainUser(out domainName, out domainUserId))
            {
                SetLoginPathRegistry(domainUserId, rdoGrpLogin.EditValue.GetNullToEmpty());
            }
            else
            {
                if (string.IsNullOrEmpty(HKInc.Utils.Common.GlobalVariable.AdUserId))
                    SetLoginPathRegistry(HKInc.Utils.Common.GlobalVariable.LoginId, rdoGrpLogin.EditValue.GetNullToEmpty());
                else
                    SetLoginPathRegistry(HKInc.Utils.Common.GlobalVariable.AdUserId, rdoGrpLogin.EditValue.GetNullToEmpty());
            }
        }

        void rdoGrpNoti_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HKInc.Utils.Common.GlobalVariable.AdUserId))
                SetNotificationRegistry(HKInc.Utils.Common.GlobalVariable.LoginId, rdoGrpNoti.EditValue.GetNullToEmpty());
            else
                SetNotificationRegistry(HKInc.Utils.Common.GlobalVariable.AdUserId, rdoGrpNoti.EditValue.GetNullToEmpty());
        }

        void InitLupSkin()
        {
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
                lupSkin.Properties.Items.Add(cnt.SkinName);

            lupSkin.EditValue = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
        }


        void SetRadionGroup()
        {
            rdoGrpCulture.Properties.BeginUpdate();
            string[] itemName = new string[2] { HKInc.Utils.Common.GlobalVariable.DefaultCultureName, HKInc.Utils.Common.GlobalVariable.SecondCultureName };
            string[] itemValue = new string[2] { HKInc.Utils.Common.GlobalVariable.DefaultCulture, HKInc.Utils.Common.GlobalVariable.SecondCulture };

            rdoGrpCulture.Properties.BeginUpdate();
            rdoGrpCulture.Properties.Columns = itemName.Count();
            for (int i = 0; i < itemValue.Length; i++)
            {
                rdoGrpCulture.Properties.Items.Add(new RadioGroupItem(itemValue[i], itemName[i]));
            }

            rdoGrpCulture.Properties.EndUpdate();

            string cul = HKInc.Service.Handler.RegistryHandler.GetValue(HKInc.Utils.Common.GlobalVariable.CulturePath, "Culture");
            if (!String.IsNullOrEmpty(cul))
                rdoGrpCulture.EditValue = cul;
            else
                rdoGrpCulture.EditValue = HKInc.Utils.Common.GlobalVariable.DefaultCulture;
        }

        void SetLoginPathRadioGroup()
        {
            rdoGrpLogin.Properties.BeginUpdate();
            string[] itemName = new string[2] { "Domain", "Application" };
            string[] itemValue = new string[2] { "DOMAIN", "APP" };

            rdoGrpLogin.Properties.BeginUpdate();
            rdoGrpLogin.Properties.Columns = itemName.Count();
            for (int i = 0; i < itemValue.Length; i++)
            {
                rdoGrpLogin.Properties.Items.Add(new RadioGroupItem(itemValue[i], itemName[i]));
            }

            rdoGrpLogin.Properties.EndUpdate();

            string domainName, domainUserId, loginPath;

            if (GetDomainUser(out domainName, out domainUserId))
                loginPath = GetLoginPathRegistry(domainUserId);
            else
                loginPath = GetLoginPathRegistry(HKInc.Utils.Common.GlobalVariable.AdUserId);


            if (!String.IsNullOrEmpty(loginPath))
                rdoGrpLogin.EditValue = loginPath;
            else
                rdoGrpLogin.EditValue = "DOMAIN";
        }

        void SetNotificationPathRadioGroup()
        {
            rdoGrpNoti.Properties.BeginUpdate();
            string[] itemName = new string[2] { "Message Bar", "Alert Window" };
            string[] itemValue = new string[2] { "BAR", "WIN" };

            rdoGrpNoti.Properties.BeginUpdate();
            rdoGrpNoti.Properties.Columns = itemName.Count();
            for (int i = 0; i < itemValue.Length; i++)
            {
                rdoGrpNoti.Properties.Items.Add(new RadioGroupItem(itemValue[i], itemName[i]));
            }

            rdoGrpNoti.Properties.EndUpdate();

            string notiMethod = GetNotificationRegistry(string.IsNullOrEmpty(HKInc.Utils.Common.GlobalVariable.AdUserId) ? HKInc.Utils.Common.GlobalVariable.LoginId : HKInc.Utils.Common.GlobalVariable.AdUserId);

            if (!String.IsNullOrEmpty(notiMethod))
                rdoGrpNoti.EditValue = notiMethod;
            else
                rdoGrpNoti.EditValue = "WIN";
        }

        string GetNotificationRegistry(string userId)
        {
            string registryPath = string.Format(@"{0}\{1}", HKInc.Utils.Common.GlobalVariable.ServerConfigPath, userId);
            string key = "Notification";

            string notificationPath = HKInc.Service.Handler.RegistryHandler.GetValue(registryPath, key);

            if (!String.IsNullOrEmpty(notificationPath))
                return notificationPath;
            else
                return String.Empty;
        }

        void SetNotificationRegistry(string userId, string notificationMethod)
        {
            string key = "Notification";
            string registryPath = string.Format(@"{0}\{1}", HKInc.Utils.Common.GlobalVariable.ServerConfigPath, userId);

            HKInc.Service.Handler.RegistryHandler.SetValue(registryPath, key, notificationMethod);
        }

        string GetLoginPathRegistry(string userId)
        {
            string registryPath = string.Format(@"{0}\{1}", HKInc.Utils.Common.GlobalVariable.ServerConfigPath, userId);
            string key = "LoginPath";

            string loginPath = HKInc.Service.Handler.RegistryHandler.GetValue(registryPath, key);

            if (!String.IsNullOrEmpty(loginPath))
                return loginPath;
            else
                return String.Empty;
        }

        void SetLoginPathRegistry(string userId, string loginPath)
        {
            string key = "LoginPath";
            string registryPath = string.Format(@"{0}\{1}", HKInc.Utils.Common.GlobalVariable.ServerConfigPath, userId);

            HKInc.Service.Handler.RegistryHandler.SetValue(registryPath, key, loginPath);
        }        

        bool GetDomainUser(out string domainName, out string domainUserId)
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            domainName = string.Empty;
            domainUserId = string.Empty;

            if (id.Name.GetNullToEmpty().Equals(string.Empty)) return false;            

            domainName = id.Name.Substring(0, id.Name.IndexOf("\\"));
            domainUserId = id.Name.Substring(id.Name.IndexOf("\\") + 1);

            return true;
        }

        bool ValidatePasswordFormat(string password)
        {
            HKInc.Utils.Interface.Handler.IPasswordHandler passwordHandler = Service.Factory.LoginFactory.GetPasswordHandler();
            string errMessage = string.Empty;
            if (passwordHandler.IsValidFormat(password, out errMessage))
            {
                passwordHandler.UpdatePassword(password);
                return true;
            }            
            else
            {
                HKInc.Service.Handler.MessageBoxHandler.Show(errMessage);
                return false;
            }                        
        }
    }
}