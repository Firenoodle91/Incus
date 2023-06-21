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
using DevExpress.XtraReports.UI;
using HKInc.Service.Service;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFITEMMOVE_ENG : XtraForm
    {
        string lworkno;
        string llotno;
        int lpseq;

        public XPFITEMMOVE_ENG()
        {
            InitializeComponent();
        }

        public XPFITEMMOVE_ENG(string workno,string lotno,int pseq)
        {
            InitializeComponent();
            lworkno = workno;
            llotno = lotno;
            lpseq = pseq;      
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XRITEMMOVE prt;
            if (lpseq != 1) return;

            if (TX_NO.Text != "")
            {
                prt = new XRITEMMOVE("", "", TX_NO.Text);
            }
            else
            {
                prt = new XRITEMMOVE(lworkno, llotno, "");
            }

            //ReportPrintTool printTool = new ReportPrintTool(prt);            
            //printTool.ShowPreviewDialog();            
            prt.PrintingSystem.ShowMarginsWarning = false;
            prt.ShowPrintStatusDialog = false;
            prt.Print();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void XPFITEMMOVE_ENG_Load(object sender, EventArgs e)
        {
            if (lpseq != 1)
            {
                string sql = "SELECT      [ITEMMOVENO]      "
                          + " FROM [TN_MPS1401T] where WORK_NO = '" + lworkno + "' and LOT_NO = '" + llotno + "' and PROCESS_TURN =" + lpseq + "";
                string tmoveno = DbRequesHandler.GetCellValue(sql, 0).GetNullToEmpty();
                if (tmoveno != "")
                {
                    XRITEMMOVE prt = new XRITEMMOVE("", "", tmoveno);
                    //ReportPrintTool printTool = new ReportPrintTool(prt);            
                    //printTool.ShowPreviewDialog();            
                    prt.PrintingSystem.ShowMarginsWarning = false;
                    prt.ShowPrintStatusDialog = false;
                    prt.Print();
                    this.Close();
                }
            }
        }

        private void TX_NO_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            TX_NO.Text = keypad.returnval;
            TX_NO.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                TX_NO.SelectionStart = TX_NO.Text.Length;
            }));
        }
    }
    
}
