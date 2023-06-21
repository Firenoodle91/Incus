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
    public partial class XPFITEMMOVE : XtraForm
    {
        string lworkno;
        string llotno;
        int lpseq;
        public XPFITEMMOVE()
        {
            InitializeComponent();
        }
        public XPFITEMMOVE(string workno,string lotno,int pseq)
        {
            InitializeComponent();
            lworkno = workno;
            llotno = lotno;
            lpseq = pseq;

          
          

            //    printTool.ShowPreviewDialog(UserLookAndFeel.Default);
          
        
        }
        //private void labelControl1_Click(object sender, EventArgs e)
        //{

        //    XRITEMMOVE prt = new XRITEMMOVE("", "", TX_NO.Text);
        //    ReportPrintTool printTool = new ReportPrintTool(prt);

        //    printTool.ShowPreviewDialog();

        //    //    printTool.ShowPreviewDialog(UserLookAndFeel.Default);
        //    this.Close();

        //}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            XRITEMMOVE_100X100 prt;
            if (lpseq != 1) return;
                if (TX_NO.Text != "")
                {
                    prt = new XRITEMMOVE_100X100("", "", TX_NO.Text);

                }
                else
                {
                    prt = new XRITEMMOVE_100X100(lworkno, llotno, "");
               }
         
         
            ReportPrintTool printTool = new ReportPrintTool(prt);
          //  printTool.Print();
              printTool.ShowPreviewDialog();

            //    printTool.ShowPreviewDialog(UserLookAndFeel.Default);
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void XPFITEMMOVE_Load(object sender, EventArgs e)
        {
            if (lpseq != 1)
            {
                string sql = "SELECT      [ITEMMOVENO]      "
                          + " FROM [TN_MPS1401T] where WORK_NO = '" + lworkno + "' and LOT_NO = '" + llotno + "' and PROCESS_TURN =" + lpseq + "";
                string tmoveno = DbRequesHandler.GetCellValue(sql, 0).GetNullToEmpty();
                if (tmoveno != "")
                {
                    XRITEMMOVE_100X100 prt = new XRITEMMOVE_100X100("", "", tmoveno);
                    ReportPrintTool printTool = new ReportPrintTool(prt);
                    printTool.ShowPreviewDialog();
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
        }
    }
    
}
