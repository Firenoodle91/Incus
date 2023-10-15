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
            RadioGroupMode.Properties.Columns = 3;
            //byte[] img = FileHandler.FtpToByte(GlobalVariable.HTTP_SERVER + "logo.jpg");
            //pictureEdit2.EditValue = img;
            //GlobalVariable.Culture = "en-US";
            //HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(16));

        }


        #region Event Handler
        void btnLogOn_Click(object sender, EventArgs e)
        {
            DoLoingCheck();
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
                comboBox1.Visible = false;
            }
        
            if (ModeCheck == "P")
            {
                GlobalVariable.DefaultPOPType = "POP1";
                GlobalVariable.KeyPad = true;
                comboBox1.Visible = true;
            }

            if (ModeCheck == "P1")
            {
                GlobalVariable.DefaultPOPType = "POP2";
                GlobalVariable.KeyPad = true;
                txtLoginId.Text = "Admin";
                txtPassword.Text = "1111";
                DoLoingCheck();
               
            }
            if (ModeCheck == "P2")
            {
                GlobalVariable.DefaultPOPType = "POP3";
                GlobalVariable.KeyPad = true;
              
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
                  HKInc.Ui.Model.BaseDomain.GsValue.UserId = UserLogin.UserId;
                  
                    
                   
                    if (UserLogin.IsValidUser())
                        {
                            this.DialogResult = DialogResult.OK;
                       Utils.Interface.Service.IService < Ui.Model.Domain.User > Service =
                                (Utils.Interface.Service.IService<Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");
                        HKInc.Ui.Model.Domain.User User = Service.GetListDetached(p => p.LoginId == UserLogin.UserId).FirstOrDefault();
                        GlobalVariable.SetUser(User);
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
        }

        private void txtLoginId_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            txtLoginId.Text = keypad.returnval;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    GlobalVariable.Culture = "ko-KR";
                    break;
                case 1:
                    GlobalVariable.Culture = "en-US";
                    break;
                case 2:
                    GlobalVariable.Culture = "zh-CN";
                    break;
            }
        }

        private void pictureEdit2_MouseClick(object sender, MouseEventArgs e)
        {
            HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage(16));

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    GlobalVariable.ProcessCode = 0;// "P01";
                    break;
                case 1:
                    GlobalVariable.ProcessCode = 1;// comboBox1.SelectedItem.GetNullToEmpty();// "P03";
                    break;
                case 2:
                    GlobalVariable.ProcessCode = 2;// comboBox1.SelectedItem.GetNullToEmpty();// "P05";
                    break;
                case 3:
                    GlobalVariable.ProcessCode = 3;// comboBox1.SelectedItem.GetNullToEmpty();// "P06";
                    break;
                //case 4:
                //    GlobalVariable.ProcessCode = 5;// comboBox1.SelectedItem.GetNullToEmpty();//"P07";
                //    break;
             
            }
        }
    }
}

