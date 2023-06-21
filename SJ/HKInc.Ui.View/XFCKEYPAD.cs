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
            this.Text = "가상키보드(Virtual keyboard)";
            this.FormClosing += Form1_FormClosing;
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
                    tx_value.Text += btn.Text;
                    break;
                
            }
        }

        private void tx_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            returnval = tx_value.Text;
            DialogResult = DialogResult.OK;
            this.Close();
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
