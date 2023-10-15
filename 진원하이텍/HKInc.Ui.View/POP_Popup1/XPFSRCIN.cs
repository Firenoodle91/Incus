using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Service.Service;

namespace HKInc.Ui.View.POP_Popup1
{
    public partial class XPFSRCIN : XtraForm
    {
        IService<VI_PURSTOCK_LOT> ModelService = (IService<VI_PURSTOCK_LOT>)ProductionFactory.GetDomainService("VI_PURSTOCK_LOT");
        public string[] returnval  ;
        public XPFSRCIN()
        {
            InitializeComponent();
            this.Text = "원소재투입처리";
        }
     

        private void tx_srcin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            SrcInfo();
        }

        private void SrcInfo()
        {
            //VI_PURSTOCK_LOT obj = ModelService.GetList(p => p.Temp2 == tx_srcin.Text).OrderBy(o => o.Temp2).FirstOrDefault();
            DataTable dt = DbRequesHandler.GetDTselect("exec sp_srcin '" + tx_srcin.Text + "'");
            if (dt == null || dt.Rows.Count==0) {
                MessageBox.Show("Материал неверный.");
                tx_srcname.Text = "";
                tx_srcqty.Text = "";
                lbitem.Text = "";
                lb_lotno.Text = "";
                tx_srcin.Text = "";
            }
            else
            {
                string outf = DbRequesHandler.GetCellValue("exec SP_STOPINOUT '" + tx_srcin.Text + "'", 0);
                if (outf == "Y")
                {
                    MessageBox.Show("Есть предыдущий ЛОТ. Сделать первым, первым вышел");

                }

                if (dt.Rows.Count >= 1)
                {
                    tx_srcname.Text = dt.Rows[0]["ItemName"].ToString();
                    tx_srcqty.Text = dt.Rows[0]["qty"].ToString();
                    lbitem.Text = dt.Rows[0]["ItemCode"].ToString();
                    lb_lotno.Text = dt.Rows[0]["OutLot"].ToString();
              }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tx_srcname.Text == "") {
                MessageBox.Show("Проверьте сырье");
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            returnval = new string[2];
            returnval[0] = lbitem.Text;
            returnval[1] = lb_lotno.Text;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void tx_srcin_Click(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            tx_srcin.Text = keypad.returnval;
            
            SrcInfo();

        }
    }
}
