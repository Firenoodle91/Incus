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
            InitLupSkin();

            lupSkin.SelectedIndexChanged += skin_SelectedIndexChanged;
            rdoGrpCulture.EditValueChanged += rdoGrpCulture_EditValueChanged;
            
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
            this.Text = LabelConvert.GetLabelText(this.Name);

            btnConfirm.Text = LabelConvert.GetLabelText("OK");
            btnCancel.Text = LabelConvert.GetLabelText("Cancel");            
            lcItemNewPassword.Text = LabelConvert.GetLabelText("NewPassword");
            lcItemConfirmPassword.Text = LabelConvert.GetLabelText("ConfirmPassword");
            lcItemLanguage.Text = LabelConvert.GetLabelText("Language");
            lcItemSkin.Text = LabelConvert.GetLabelText("Skin");

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

        void rdoGrpCulture_EditValueChanged(object sender, EventArgs e)
        {
            HKInc.Service.Handler.RegistryHandler.SetValue(HKInc.Utils.Common.GlobalVariable.CulturePath, "Culture", rdoGrpCulture.EditValue.GetNullToEmpty());
            HKInc.Utils.Common.GlobalVariable.Culture = rdoGrpCulture.EditValue.GetNullToEmpty();

            Thread.CurrentThread.CurrentCulture = new CultureInfo(HKInc.Utils.Common.GlobalVariable.Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(HKInc.Utils.Common.GlobalVariable.Culture);

            var owner = this.Owner as MainForm;
            owner.SetMainFormCultureChange();
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
            string[] itemName = new string[3] { HKInc.Utils.Common.GlobalVariable.DefaultCultureName,
                                                HKInc.Utils.Common.GlobalVariable.SecondCultureName,
                                                HKInc.Utils.Common.GlobalVariable.ThirdCultureName,
                                              };
            string[] itemValue = new string[3] { HKInc.Utils.Common.GlobalVariable.DefaultCulture,
                                                 HKInc.Utils.Common.GlobalVariable.SecondCulture,
                                                 HKInc.Utils.Common.GlobalVariable.ThirdCulture,
                                               };

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