using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
namespace HKInc.Ui.View
{
    
    public partial class XFCKEYPAD_LOGIN : XtraForm
    {
        public string returnval;
        private bool shiftYN = true;


        public XFCKEYPAD_LOGIN()
        {
            InitializeComponent();
            this.Text = "가상키보드(Virtual keyboard)";
            this.FormClosing += Form1_FormClosing;
        }

        /// <summary>
        /// 20210817 오세완 차장
        /// 특수문자만 입력하고 대문자 기본으로 사용하기 위해 생성
        /// </summary>
        /// <param name="bVisiableShift">false - shiftkey 안보이기, true - shift 보이기</param>
        public XFCKEYPAD_LOGIN(bool bVisiableShift)
        {
            InitializeComponent();
            this.Text = "가상키보드(Virtual keyboard)";
            this.FormClosing += Form1_FormClosing;

            this.simpleButton68.Visible = bVisiableShift;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            switch (btn.Text.TrimEnd())
            {
                case "SPACE":
                    tx_value.Text += "  ";
                    break;
                case "<-":
                    try
                    {
                        tx_value.Text = tx_value.Text.Substring(0, tx_value.Text.Length - 1);
                    }
                    catch { }
                    break;
                case "CLR":
                    tx_value.Text = "";
                    break;
                case "ENTER":
                    returnval = tx_value.Text;
                    DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                default:
                    if (!shiftYN)
                    {
                        tx_value.Text += btn.Text.ToLower();
                    }
                    else
                    {
                        tx_value.Text += btn.Text.ToUpper();
                    }
                    break;
            }
            //tx_value.Focus();

        }

        private void tx_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            returnval = tx_value.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ShiftButton_Click(object sender, EventArgs e)
        {
            shiftYN = shiftYN == true ? false : true;

            Console.WriteLine(shiftYN);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && DialogResult != DialogResult.OK) 
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
