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
    public partial class XPFITEMMOVESCAN : XtraForm
    {
        TP_POPJOBLIST lsobj;
       public string retutnvalue;
        public string moveno;
        public XPFITEMMOVESCAN(TP_POPJOBLIST obj)
        {
            InitializeComponent();
            lsobj = obj;
            lup_preprocess.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            lup_process.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            lup_nextprocess.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            DataSet ds = DbRequestHandler.GetDataQury("exec SP_ITEMMOVE_SELECT '"+ textEdit1.Text + "',"+lsobj.PSeq+"");
            if (ds == null) return;
            if (ds.Tables[1].Rows.Count < 1) {

                MessageBox.Show("Проверьте таблицу движения деталей.");
                textEdit1.Text = "";
                textEdit1.Focus();
                return;
            }
            if(ds.Tables[1].Rows[0][0].ToString()!=lsobj.WorkNo)
            {
                MessageBox.Show("Это не таблица перемещения деталей рабочего задания.");
                tx_workno.Text = "";
                tx_lotno.Text = "";
                lup_preprocess.EditValue = "";
                lup_process.EditValue = "";
                lup_nextprocess.EditValue = "";
                textEdit1.Text = "";
                textEdit1.Focus();
                return;
            }
            if (ds.Tables[0].Rows[0][0].ToString() != "T")
            {
                MessageBox.Show("Подтверждение предыдущей производительности процесса.");
                tx_workno.Text = "";
                tx_lotno.Text = "";
                lup_preprocess.EditValue = "";
                lup_process.EditValue = "";
                lup_nextprocess.EditValue = "";
                textEdit1.Text = "";
                textEdit1.Focus();
                return;
            }
            tx_workno.Text = ds.Tables[1].Rows[0][0].ToString();
            tx_lotno.Text= ds.Tables[1].Rows[0][1].ToString();
            lup_preprocess.EditValue= ds.Tables[1].Rows[0][2].ToString();
            lup_process.EditValue= ds.Tables[1].Rows[0][3].ToString();
            lup_nextprocess.EditValue = ds.Tables[1].Rows[0][4].ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tx_lotno.Text != "")
            {
                DialogResult = DialogResult.OK;
                retutnvalue = tx_lotno.Text;
                moveno = textEdit1.Text.ToUpper();
                this.Close();
            }
            else
            {
                MessageBox.Show("Проверьте таблицу движения деталей.");
                return;
                //DialogResult = DialogResult.Cancel;
            }
            
        }

        private void textEdit1_DoubleClick(object sender, EventArgs e)
        {
            if (!HKInc.Utils.Common.GlobalVariable.KeyPad) return;
            XFCKEYPAD keypad = new XFCKEYPAD();
            keypad.ShowDialog();
            textEdit1.Text = keypad.returnval;
        }
    }
}
