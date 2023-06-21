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
    public partial class ChangeSettingKor : DevExpress.XtraEditors.XtraForm
    {
        bool isPasswordChanged = false;

        public ChangeSettingKor()
        {
            InitializeComponent();
            InitForm();
            InitControls();
        }

        void InitForm()
        {           
            SetNotificationPathRadioGroup();
            InitLupSkin();

            lupSkin.SelectedIndexChanged += skin_SelectedIndexChanged;            
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
                    HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_16));
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
        
        void rdoGrpNoti_EditValueChanged(object sender, EventArgs e)
        {
            SetNotificationRegistry(HKInc.Utils.Common.GlobalVariable.LoginId, rdoGrpNoti.EditValue.GetNullToEmpty());
        }

        void InitLupSkin()
        {
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
                lupSkin.Properties.Items.Add(cnt.SkinName);
                        
            lupSkin.EditValue = DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName;
            lupSkin.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
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

            string notiMethod = GetNotificationRegistry(HKInc.Utils.Common.GlobalVariable.LoginId);

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