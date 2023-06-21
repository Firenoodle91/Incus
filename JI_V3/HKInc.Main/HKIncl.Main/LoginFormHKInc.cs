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

using HKInc.Service.Controls;
using HKInc.Utils.Interface.Service;
using HKInc.Ui.Model.Domain.VIEW;

namespace HKInc.Main
{
    public partial class LoginFormHKInc : XtraForm
    {
        IUserLogin UserLogin = LoginFactory.GetUserLogin();

        bool MoveFlag = false;
        Point MouseDownPoint;

        IService<TN_MEA1000> ModelService = (IService<TN_MEA1000>)ProductionFactory.GetDomainService("TN_MEA1000");

        public LoginFormHKInc()
        {
            InitializeComponent();

            InitForm();
            InitCombo();
            //comboBoxEdit1.SelectedIndex = 0;
        }

        void InitForm()
        {
            UserLogin.Culture = GlobalVariable.DefaultCulture;

            SetControlEditValue();
            SetRadionGroup();

            lbLogin.Click += btnLogOn_Click;
            //lbLogin.MouseHover += LbLogin_MouseHover;
            //lbLogin.MouseLeave += LbLogin_MouseLeave;

            lbCancel.Click += lblCancel_Click;
            lbCancel.MouseEnter += label_MouseEnter;
            lbCancel.MouseLeave += label_MouseLeave;

            txtLoginId.KeyDown += txtEdit_KeyDown;
            txtPassword.KeyDown += txtEdit_KeyDown;

            chkRemember.CheckStateChanged += chkRemember_EditValueChanged;
            radioGroup1.SelectedIndex = 0;

            lup_Machine.EditValueChanged += lup_Machine_EditValueChanged;

            Utils.Enum.RadioGroupType radioGroupType = Utils.Enum.RadioGroupType.LoginMode;
            HKInc.Service.Handler.RadioGroupHandler.SetRadioGroup(rbo_LoginMode, radioGroupType);
            rbo_LoginMode.EditValueChanged += Rbo_LoginMode_EditValueChanged;
            rbo_LoginMode.Properties.Columns = 3;

            this.MouseMove += LoginFormHKInc_MouseMove;
            this.MouseUp += LoginFormHKInc_MouseUp;
            this.MouseDown += LoginFormHKInc_MouseDown;
        }

        void InitCombo()
        {
            lup_Machine.SetSmall(true, "MachineMCode", "MachineName", ModelService.GetChildList<TN_MEA1000>(p => true).ToList(), DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor);
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

            ////비밀번호 길이, 조합 확인
            //DoLoginCheck_WithLength();
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
        void lup_Machine_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                UserLogin.MachineCode = lup_Machine.EditValue.GetNullToEmpty();
                HKInc.Ui.Model.BaseDomain.GsValue.MachineCode = lup_Machine.EditValue.GetNullToEmpty();
            }
            catch
            { }

        }

        void txtEdit_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit edit = sender as TextEdit;
            if (edit == null) return;

            if (e.KeyCode == Keys.Enter)
            {

                DoLoingCheck();

                ////비밀번호 길이, 조합 확인
                //DoLoginCheck_WithLength();
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
            //else if (ModeCheck == "PLC") // 20210715 오세완 차장 REWORK -> PLC수정
            //{
            //    GlobalVariable.DefaultPOPType = ModeCheck;
            //    GlobalVariable.KeyPad = true;
            //}
            else if (ModeCheck == "POP")
            {
                GlobalVariable.DefaultPOPType = ModeCheck;
                GlobalVariable.KeyPad = true;
                lup_Machine.Visible = true;
            }
            else if (ModeCheck == "IFPOP")
            {
                GlobalVariable.DefaultPOPType = ModeCheck;
                GlobalVariable.KeyPad = true;
                lup_Machine.Visible = true;
            }
            else if (ModeCheck == "RUS_IFPOP")          // 러시아 POP 추가 2022-10-24 김진우 추가
            {
                GlobalVariable.DefaultPOPType = ModeCheck;
                GlobalVariable.KeyPad = true;
                lup_Machine.Visible = true;
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

                txtPassword.Focus();
            }

            lup_Machine.EditValue = UserLogin.MachineCode;

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
                    UserLogin.MachineCode = lup_Machine.EditValue.GetNullToEmpty();

                    HKInc.Ui.Model.BaseDomain.GsValue.UserId = UserLogin.UserId;
                    HKInc.Ui.Model.BaseDomain.GsValue.MachineCode = UserLogin.MachineCode;

                    if (UserLogin.IsValidUser())
                    {
                        //this.DialogResult = DialogResult.OK;
                        Utils.Interface.Service.IService<Ui.Model.Domain.User> Service =
                            (Utils.Interface.Service.IService<Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");
                        HKInc.Ui.Model.Domain.User User = Service.GetListDetached(p => p.LoginId == UserLogin.UserId).FirstOrDefault();


                        Utils.Interface.Service.IService<Ui.Model.Domain.VIEW.TN_MEA1000> Service2 =
                            (Utils.Interface.Service.IService<Ui.Model.Domain.VIEW.TN_MEA1000>)ServiceFactory.GetDomainService("Machine");
                        HKInc.Ui.Model.Domain.VIEW.TN_MEA1000 Machine = Service2.GetListDetached(p => p.MachineMCode == UserLogin.MachineCode).FirstOrDefault();

                        if (User.Active == "Y")
                        {
                            this.DialogResult = DialogResult.OK;
                            GlobalVariable.SetUser(User);
                            GlobalVariable.SetMachine(Machine); //로그인 창 설비코드 선택 저장
                            waitHandler.CloseWait();
                            Close();
                        }
                        else
                        {
                            HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_6));
                            SetControlEditValue();
                        }
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

        /// <summary>
        /// 20210818 오세완 차장 비번이 7글자 이하면 변경하는 창을 출력하는 로직
        /// </summary>
        private void DoLoginCheck_WithLength()
        {
            HKInc.Service.Handler.WaitHandler waitHandler = new HKInc.Service.Handler.WaitHandler();

            try
            {
                waitHandler.ShowWait();

                if (!string.IsNullOrEmpty(txtLoginId.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (chkRemember.Checked)
                        UserLogin.SaveUserId(true);
                    else
                        UserLogin.SaveUserId(false);

                    UserLogin.UserId = txtLoginId.Text;
                    UserLogin.Password = txtPassword.Text;
                    UserLogin.MachineCode = lup_Machine.EditValue.GetNullToEmpty();

                    HKInc.Ui.Model.BaseDomain.GsValue.UserId = UserLogin.UserId;
                    HKInc.Ui.Model.BaseDomain.GsValue.MachineCode = UserLogin.MachineCode;

                    short sReturn = UserLogin.IsValidUser_WithLength();
                    switch(sReturn)
                    {
                        // 20210818 오세완 차장 예외상항
                        case 0:
                            HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_6));
                            SetControlEditValue();
                            break;

                        // 20210818 오세완 차장 id틀림
                        case 1:
                            HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_7));
                            txtLoginId.Focus();
                            break;

                        // 20210818 오세완 차장 비번틀림
                        case 2:
                            HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_8));
                            txtPassword.Focus();
                            break;

                        // 20210818 오세완 차장 비밀번호 7자 이하라 변경처리 해야 함
                        case 3:
                            Utils.Interface.Service.IService<Ui.Model.Domain.User> Service1 =
                                (Utils.Interface.Service.IService<Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");
                            HKInc.Ui.Model.Domain.User User1 = Service1.GetListDetached(p => p.LoginId == UserLogin.UserId).FirstOrDefault();

                            if (User1.Active == "Y")
                            {
                                MessageBoxHandler.Show("비밀번호가 7자 이하라서 변경을 해야 프로그램 사용이 가능합니다.");

                                this.DialogResult = DialogResult.Cancel;
                                GlobalVariable.SetUser(User1);
                                waitHandler.CloseWait();

                                ChangeSettingKor form = new ChangeSettingKor();
                                form.Owner = this;
                                form.ShowDialog();
                                Close();
                            }
                            else
                            {
                                HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_6));
                                SetControlEditValue();
                            }
                            break;

                        // 20210818 오세완 차장 정상
                        case 4:
                            Utils.Interface.Service.IService<Ui.Model.Domain.User> Service =
                                (Utils.Interface.Service.IService<Ui.Model.Domain.User>)ServiceFactory.GetDomainService("User");
                            HKInc.Ui.Model.Domain.User User = Service.GetListDetached(p => p.LoginId == UserLogin.UserId).FirstOrDefault();

                            if (User.Active == "Y")
                            {
                                this.DialogResult = DialogResult.OK;
                                GlobalVariable.SetUser(User);
                                waitHandler.CloseWait();
                                Close();
                            }
                            else
                            {
                                HKInc.Service.Handler.MessageBoxHandler.Show(HelperFactory.GetStandardMessage().GetStandardMessage((int)StandardMessageEnum.M_6));
                                SetControlEditValue();
                            }
                            break;
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
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad)
                return;

            // 20210812 오세완 차장 감리 시정조치 때문에 password를 특수문자 및 대소문자를 입력할 수 있는 키패드로 변경
            //XFCKEYPAD keypad = new XFCKEYPAD();
            XFCKEYPAD_LOGIN keypad = new XFCKEYPAD_LOGIN(false);
            
            //keypad.ShowDialog();
            if (keypad.ShowDialog() != DialogResult.Cancel)
                txtPassword.Text = keypad.returnval;
        }

        private void txtLoginId_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad)
                return;

            // 20210812 오세완 차장 감리 시정조치 때문에 password를 특수문자 및 대소문자를 입력할 수 있는 키패드로 변경
            //XFCKEYPAD keypad = new XFCKEYPAD();
            XFCKEYPAD_LOGIN keypad = new XFCKEYPAD_LOGIN(false);
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