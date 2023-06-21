﻿using DevExpress.XtraEditors;
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

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFITEMMOVESCAN_ENG : XtraForm
    {
        TP_POPJOBLIST lsobj;
        public string retutnvalue;
        public string moveno;

        public XPFITEMMOVESCAN_ENG(TP_POPJOBLIST obj)
        {
            InitializeComponent();
            lsobj = obj;
            lup_preprocess.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process));
            lup_process.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process));
            lup_nextprocess.SetDefault(true, "Mcode", "Codename", DbRequesHandler.GetCommCode(MasterCodeSTR.Process));
            textEdit1.ImeMode = ImeMode.Disable;
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_ITEMMOVE_SELECT '"+ textEdit1.Text + "',"+lsobj.PSeq+"");
            if (ds == null) return;
            if (ds.Tables[1].Rows.Count < 1) {

                MessageBox.Show("Check out the document.");
                textEdit1.Text = "";
                textEdit1.Focus();
                return;
            }
            if(ds.Tables[1].Rows[0][0].ToString()!=lsobj.WorkNo)
            {
                MessageBox.Show("It is not a document of the work order.");
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
                MessageBox.Show("Check your previous process results.");
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
                MessageBox.Show("Check out the document.");
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
            textEdit1.BeginInvoke(new MethodInvoker(delegate {
                //txtPassword.SelectionLength = txtPassword.Text.Length;
                textEdit1.SelectionStart = textEdit1.Text.Length;
            }));
        }
    }
}
