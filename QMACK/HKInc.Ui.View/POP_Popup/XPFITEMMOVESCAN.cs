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
    public partial class XPFITEMMOVESCAN : XtraForm
    {
        #region 전역변수
        TP_POPJOBLIST lsobj;
       public string retutnvalue;
        public string moveno;

        /// <summary>
        /// 20220223 오세완 차장
        /// 고도화후 변경된 작업지시 형태
        /// </summary>
        TP_XFPOP1000_V2_LIST job_Obj;
        #endregion

        public XPFITEMMOVESCAN(TP_POPJOBLIST obj)
        {
            InitializeComponent();
            lsobj = obj;
            // 20220223 오세완 차장 중복요소 교체
            //lup_preprocess.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            //lup_process.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            //lup_nextprocess.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            InitCombo();
        }

        public XPFITEMMOVESCAN(TP_XFPOP1000_V2_LIST obj)
        {
            InitializeComponent();
            job_Obj = obj;

            InitCombo();
        }

        private void InitCombo()
        {
            lup_preprocess.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            lup_process.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
            lup_nextprocess.SetDefault(true, "Mcode", "Codename", DbRequestHandler.GetCommCode(MasterCodeSTR.Process));
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (job_Obj == null)
                return;

            //DataSet ds = DbRequestHandler.GetDataQury("exec SP_ITEMMOVE_SELECT '"+ textEdit1.Text + "',"+lsobj.PSeq+"");
            DataSet ds = DbRequestHandler.GetDataQury("exec SP_ITEMMOVE_SELECT '" + textEdit1.Text + "'," + job_Obj.PSeq + "");
            if (ds == null)
                return;
            else if (ds.Tables.Count <= 1)
                return; // 20220223 오세완 차장 안정성응 위해 예외조건 추가 

            if (ds.Tables[1].Rows.Count < 1) {

                MessageBox.Show("부품이동표를 확인하세요.");
                textEdit1.Text = "";
                textEdit1.Focus();
                return;
            }
            if(ds.Tables[1].Rows[0][0].ToString()!=lsobj.WorkNo)
            {
                MessageBox.Show("해당작업지시의 부품이동표가 아닙니다.");
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
                MessageBox.Show("이전 공정 실적을 확인하세요.");
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
                MessageBox.Show("부품이동표를 확인하세요");
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
