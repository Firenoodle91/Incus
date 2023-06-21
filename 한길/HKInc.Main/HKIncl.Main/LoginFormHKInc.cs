using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using HKInc.Utils.Class;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Handler;
using HKInc.Service.Factory;
using System.Net;
using HKInc.Service.Handler;
using HKInc.Ui.View;

namespace HKInc.Main
{
    public partial class LoginFormHKInc : XtraForm
    {
        IUserLogin UserLogin = LoginFactory.GetUserLogin();

        public LoginFormHKInc()
        {
            InitializeComponent();
            
            InitForm();
            //comboBoxEdit1.SelectedIndex = 0;
        }

        void InitForm()
        {
            UserLogin.Culture = GlobalVariable.DefaultCulture;

            SetControlEditValue();

            lbLogin.Click += btnLogOn_Click;
            lbLogin.MouseHover += LbLogin_MouseHover;
            lbLogin.MouseLeave += LbLogin_MouseLeave;

            lbCancel.Click += lblCancel_Click;            
            lbCancel.MouseEnter += label_MouseEnter;
            lbCancel.MouseLeave += label_MouseLeave;

            txtLoginId.KeyDown += txtEdit_KeyDown;
            txtPassword.KeyDown += txtEdit_KeyDown;

            chkRemember.CheckStateChanged += chkRemember_EditValueChanged;

            RadioGroupMode.EditValueChanged += RadioGroupMode_EditValueChanged;
            Utils.Enum.RadioGroupType radioGroupType = Utils.Enum.RadioGroupType.LoginMode;
            HKInc.Service.Handler.RadioGroupHandler.SetRadioGroup(RadioGroupMode, radioGroupType);
            RadioGroupMode.Properties.Columns = 0;
            RadioGroupMode.Properties.ColumnIndent = 2;
            RadioGroupMode.Properties.Appearance.Font = new Font("맑은 고딕", 13f, FontStyle.Bold);
            //RadioGroupMode.Properties.Items.RemoveAt(2);

            //byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + "logo.jpg"); //서버가 없으므로 주석처리
            //pictureEdit2.EditValue = img;            
            pictureEdit2.ReadOnly = true;
        }


        #region Event Handler
        void btnLogOn_Click(object sender, EventArgs e)
        {
            string ModeCheck = RadioGroupMode.EditValue.GetNullToEmpty();
            if(ModeCheck == "P1")
            {
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else DoLoingCheck();
        }

        void lblCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void label_MouseEnter(object sender, EventArgs e)
        {
            LabelControl label = sender as LabelControl;
            if (label == null) return;

            label.ForeColor = Color.Blue;
        }

        void label_MouseLeave(object sender, EventArgs e)
        {
            LabelControl label = sender as LabelControl;
            if (label == null) return;

            label.ForeColor = Color.Black;
        }

        void chkRemember_EditValueChanged(object sender, EventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            if (edit == null) return;

            if (edit.Checked)
                UserLogin.SaveUserId(true);
            else
                UserLogin.SaveUserId(false);
        }

        void txtEdit_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            if (edit == null) return;

            if (e.KeyCode == Keys.Enter)
            {
                DoLoingCheck();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void LbLogin_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void LbLogin_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void RadioGroupMode_EditValueChanged(object sender, EventArgs e)
        {
            string ModeCheck = RadioGroupMode.EditValue.GetNullToEmpty();

            if (ModeCheck == "M")
            {
                //panel1.Visible = false;
                GlobalVariable.DefaultPOPType = "MES";
                GlobalVariable.KeyPad = false;

                txtLoginId.Enabled = true;
                txtPassword.Enabled = true;
                chkRemember.Enabled = true;
            }
           
            if (ModeCheck == "P")
            {
                GlobalVariable.DefaultPOPType = "POP1";
                GlobalVariable.KeyPad = true;

                txtLoginId.Enabled = true;
                txtPassword.Enabled = true;
                chkRemember.Enabled = true;
            }

            if (ModeCheck == "PENG")
            {
                GlobalVariable.DefaultPOPType = "POP1_ENG";
                GlobalVariable.KeyPad = true;

                txtLoginId.Enabled = true;
                txtPassword.Enabled = true;
                chkRemember.Enabled = true;
            }

            if (ModeCheck == "P1")
            {
                GlobalVariable.DefaultPOPType = "POP2";
                GlobalVariable.KeyPad = false;

                txtLoginId.Enabled = false;
                txtPassword.Enabled = false;
                chkRemember.Enabled = false;
            }

            //panel1.Visible = false;
           
        }

        private void RadioGroupPop_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        //private void BtnDatabaseIP_Click(object sender, EventArgs e)
        //{
        //    using (DatabaseIP_Setting f = new DatabaseIP_Setting())
        //    {
        //        f.Text = "Database IP Setting";
        //        if (f.ShowDialog() == DialogResult.OK)
        //        {
        //        }
        //    }
        //}
        #endregion


        #region Function


        void SetControlEditValue()
        {
            txtPassword.Text = String.Empty;

            if (String.IsNullOrEmpty(UserLogin.UserId))
            {
                chkRemember.Checked = false;
                txtLoginId.Focus();
            }
            else
            {
                chkRemember.Checked = true;
                txtLoginId.Text = UserLogin.UserId;
                             
                txtPassword.Focus();
            }
        }

        void DoLoingCheck()
        {
            HKInc.Service.Handler.WaitHandler waitHandler = new HKInc.Service.Handler.WaitHandler();
                        
            try
            {
                waitHandler.ShowWait();

                if (!string.IsNullOrEmpty(txtLoginId.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (chkRemember.Checked) UserLogin.SaveUserId(true);
                    else UserLogin.SaveUserId(false);

                    UserLogin.UserId = txtLoginId.Text;
                    UserLogin.Password = txtPassword.Text;
                   
                if (UserLogin.IsValidUser())
                    {
                        this.DialogResult = DialogResult.OK;
                        Utils.Interface.Service.IService < Ui.Model.Domain.User > Service =
                            (Utils.Interface.Service.IService<Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");
                        HKInc.Ui.Model.Domain.User User = Service.GetListDetached(p => p.LoginId == UserLogin.UserId).FirstOrDefault();
                        GlobalVariable.SetUser(User);
                        HKInc.Ui.Model.BaseDomain.GsValue.UserId = User.LoginId;
                        waitHandler.CloseWait();
                        Close();
                    }
                    else
                    {
                        HKInc.Service.Handler.MessageBoxHandler.Show("사용자ID와 패스워드를 확인해 주세요.");
                        SetControlEditValue();
                    }
                }
                else if (string.IsNullOrEmpty(txtLoginId.Text))
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("사용자ID를 확인해 주세요.");
                    txtLoginId.Focus();
                }
                else
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show("패스워드를 확인해 주세요.");
                    txtPassword.Focus();
                }
            }
            finally
            {
                waitHandler.CloseWait();
            }
       
        }


        #endregion

        private void txtPassword_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            txtPassword.Text = keypad.returnval;
            txtPassword.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                txtPassword.SelectionStart = txtPassword.Text.Length;
            }));
        }

        private void txtLoginId_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            txtLoginId.Text = keypad.returnval;
            txtLoginId.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                txtLoginId.SelectionStart = txtLoginId.Text.Length;
            }));
        }
    }
}