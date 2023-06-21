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

        bool MoveFlag = false;
        Point MouseDownPoint;

        public LoginFormHKInc()
        {
            InitializeComponent();
            
            InitForm();
            //comboBoxEdit1.SelectedIndex = 0;
        }

        void InitForm()
        {
            Utils.Enum.RadioGroupType radioGroupType = Utils.Enum.RadioGroupType.LoginMode;
            HKInc.Service.Handler.RadioGroupHandler.SetRadioGroup(rbo_LoginMode, radioGroupType);
            rbo_LoginMode.EditValueChanged += Rbo_LoginMode_EditValueChanged;

            UserLogin.Culture = GlobalVariable.DefaultCulture;

            SetControlEditValue();
            SetRadionGroup();

            lbLogin.Click += btnLogOn_Click;
            lbLogin.MouseHover += LbLogin_MouseHover;
            lbLogin.MouseLeave += LbLogin_MouseLeave;

            lbCancel.Click += lblCancel_Click;            
            lbCancel.MouseEnter += label_MouseEnter;
            lbCancel.MouseLeave += label_MouseLeave;

            txtLoginId.KeyDown += txtEdit_KeyDown;
            txtPassword.KeyDown += txtEdit_KeyDown;

            chkRemember.CheckStateChanged += chkRemember_EditValueChanged;
            radioGroup1.SelectedIndex = 0;

            this.MouseMove += LoginFormHKInc_MouseMove;
            this.MouseUp += LoginFormHKInc_MouseUp;
            this.MouseDown += LoginFormHKInc_MouseDown;
        }

        private void LoginFormHKInc_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveFlag && (e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(this.Left - (MouseDownPoint.X - e.X), this.Top - (MouseDownPoint.Y - e.Y));
        }

        private void LoginFormHKInc_MouseDown(object sender, MouseEventArgs e)
        {
            MoveFlag = true;
            MouseDownPoint = new Point(e.X, e.Y);
        }

        private void LoginFormHKInc_MouseUp(object sender, MouseEventArgs e)
        {
            MoveFlag = false;
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

        //private void RadioGroupMode_EditValueChanged(object sender, EventArgs e)
        //{
        //    string ModeCheck = RadioGroupMode.EditValue.GetNullToEmpty();

        //    if (ModeCheck == "M")
        //    {
        //        //panel1.Visible = false;
        //        GlobalVariable.DefaultPOPType = "MES";
        //        GlobalVariable.KeyPad = false;
        //    }
        
        //    if (ModeCheck == "P")
        //    {
        //        GlobalVariable.DefaultPOPType = "POP1";
        //        GlobalVariable.KeyPad = true;
        //    }

        //    if (ModeCheck == "P1")
        //    {
        //        GlobalVariable.DefaultPOPType = "POP2";
        //        GlobalVariable.KeyPad = true;
        //    }
        //    if (ModeCheck == "P2")
        //    {
        //        GlobalVariable.DefaultPOPType = "POP3";
        //        GlobalVariable.KeyPad = true;
        //    }
        //    if (ModeCheck == "P3")
        //    {
        //        GlobalVariable.DefaultPOPType = "POP4";
        //        GlobalVariable.KeyPad = true;
        //    }
        //    //panel1.Visible = false;

        //}

        private void RadioGroupPop_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void Rbo_LoginMode_EditValueChanged(object sender, EventArgs e)
        {
            string ModeCheck = rbo_LoginMode.EditValue.GetNullToEmpty();

            if (ModeCheck == "MES")
            {
                GlobalVariable.DefaultPOPType = ModeCheck;
                GlobalVariable.KeyPad = false;
            }
            else if (ModeCheck == "POP")
            {
                GlobalVariable.DefaultPOPType = ModeCheck;
                //GlobalVariable.KeyPad = true;
                GlobalVariable.KeyPad = false;
            }
            else
            {
                GlobalVariable.DefaultPOPType = "MES";
                GlobalVariable.KeyPad = false;
            }
        }

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
                rbo_LoginMode.EditValue = UserLogin.Mode;

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
                    UserLogin.Mode = rbo_LoginMode.EditValue.GetNullToEmpty();
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
                        HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_6));
                        SetControlEditValue();
                    }
                }
                else if (string.IsNullOrEmpty(txtLoginId.Text))
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_7));
                    txtLoginId.Focus();
                }
                else
                {
                    HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_8));
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
            //keypad.ShowDialog();
            if (keypad.ShowDialog() != DialogResult.Cancel)
                txtPassword.Text = keypad.returnval;
        }

        private void txtLoginId_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            if (keypad.ShowDialog() != DialogResult.Cancel)
                txtLoginId.Text = keypad.returnval;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;
            GlobalVariable.Culture = radioGroup.EditValue.GetNullToEmpty();
            UserLogin.Culture = radioGroup.EditValue.GetNullToEmpty();
        }

        private void SetRadionGroup()
        {
            radioGroup1.Properties.BeginUpdate();
            string[] itemName = new string[3] { HKInc.Utils.Common.GlobalVariable.DefaultCultureName,
                                                HKInc.Utils.Common.GlobalVariable.SecondCultureName,
                                                HKInc.Utils.Common.GlobalVariable.ThirdCultureName,
                                              };
            string[] itemValue = new string[3] { HKInc.Utils.Common.GlobalVariable.DefaultCulture,
                                                 HKInc.Utils.Common.GlobalVariable.SecondCulture,
                                                 HKInc.Utils.Common.GlobalVariable.ThirdCulture,
                                               };

            radioGroup1.Properties.BeginUpdate();
            radioGroup1.Properties.Columns = itemName.Count();
            for (int i = 0; i < itemValue.Length; i++)
            {
                radioGroup1.Properties.Items.Add(new RadioGroupItem(itemValue[i], itemName[i]));
            }

            radioGroup1.Properties.EndUpdate();
            radioGroup1.EditValue = HKInc.Utils.Common.GlobalVariable.DefaultCulture;
        }

    }
}