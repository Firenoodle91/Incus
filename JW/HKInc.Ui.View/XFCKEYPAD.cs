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
    
    public partial class XFCKEYPAD : XtraForm
    {
        public string returnval;
        public XFCKEYPAD()
        {
            InitializeComponent();
            this.Text = "단순 키보도 입력판입니다. 사칙연산은 이루어지지 않습니다.";
        }

        private void simpleButton13_Click(object sender, EventArgs e)
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
                    this.Close();
                    break;
                default:
                    tx_value.Text += btn.Text;
                    break;
                
            }
        }

        private void tx_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            returnval = tx_value.Text;
            this.Close();
        }
    }
}
