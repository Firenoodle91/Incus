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
using HKInc.Utils.Class;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFSRCIN2 : XtraForm
    {
        IService<VI_PURSTOCK_LOT> ModelService = (IService<VI_PURSTOCK_LOT>)ProductionFactory.GetDomainService("VI_PURSTOCK_LOT");
        public string[] returnval  ;

        public XPFSRCIN2()
        {
            InitializeComponent();
            this.Text = "원소재투입처리";
        }
        public XPFSRCIN2(string lot)
        {
            InitializeComponent();
            this.Text = "원소재투입처리";

            string sql = "SELECT [SRC_LOT],[SRC_LOT1],[SRC_LOT2],[SRC_LOT3],[SRC_LOT4],[SRC_LOT5],[SRC_LOT6],[SRC_LOT7] "
                       + " FROM[TN_LOT_MST] where lot_no = '" + lot + "'";
            DataSet ds = DbRequesHandler.GetDataQury(sql);
            if (ds != null)
            {
              if(ds.Tables.Count>=1)
                {
                    if (ds.Tables[0].Rows.Count >= 1)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            if (ds.Tables[0].Rows[0][i].GetNullToEmpty() != "")
                            {
                                SrcInfoNew(ds.Tables[0].Rows[0][i].GetNullToEmpty(), Convert.ToString(i + 1));
                            }
                        }


                    }

                }

            }
        }

        private void tx_srcin_KeyDown(object sender, KeyEventArgs e)
       {

            TextEdit tx = sender as TextEdit;
            if (e.KeyCode != Keys.Enter) return;
            string idx = tx.Name.ToString().Substring(tx.Name.ToString().Length-1,1);
            
            SrcInfo(tx.Text,idx);
        }
        private void SrcInfoNew(string tx, string idx)
        {
            //VI_PURSTOCK_LOT obj = ModelService.GetList(p => p.Temp2 == tx_srcin.Text).OrderBy(o => o.Temp2).FirstOrDefault();
            DataTable dt = DbRequesHandler.GetDTselect("exec sp_srcin '" + tx + "'");

            if (dt == null)
            {
            
            }
            else
            {

                switch (idx)
                {
                    case "1":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname1.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty1.Text = dt.Rows[0]["qty"].ToString();
                            lbitem1.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno1.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin1.Text = tx;
                        }
                        else
                        {
                          
                            tx_srcname1.Text = "";
                            tx_srcqty1.Text = "";
                            lbitem1.Text = "";
                            lb_lotno1.Text = "";
                            tx_srcin1.Text = tx;
                        }
                        break;
                    case "2":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname2.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty2.Text = dt.Rows[0]["qty"].ToString();
                            lbitem2.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno2.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin2.Text = tx;
                        }
                        else
                        {
                          
                            tx_srcname2.Text = "";
                            tx_srcqty2.Text = "";
                            lbitem2.Text = "";
                            lb_lotno2.Text = "";
                            tx_srcin2.Text = tx;
                        }
                        break;
                    case "3":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname3.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty3.Text = dt.Rows[0]["qty"].ToString();
                            lbitem3.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno3.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin3.Text = tx;
                        }
                        else
                        {
                           
                            tx_srcname3.Text = "";
                            tx_srcqty3.Text = "";
                            lbitem3.Text = "";
                            lb_lotno3.Text = "";
                            tx_srcin3.Text = tx;
                        }
                        break;
                    case "4":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname4.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty4.Text = dt.Rows[0]["qty"].ToString();
                            lbitem4.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno4.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin4.Text = tx;
                        }
                        else
                        {
                           
                            tx_srcname4.Text = "";
                            tx_srcqty4.Text = "";
                            lbitem4.Text = "";
                            lb_lotno4.Text = "";
                            tx_srcin4.Text = tx;
                        }
                        break;
                    case "5":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname5.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty5.Text = dt.Rows[0]["qty"].ToString();
                            lbitem5.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno5.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin5.Text = tx;
                        }
                        else
                        {
                           
                            tx_srcname5.Text = "";
                            tx_srcqty5.Text = "";
                            lbitem5.Text = "";
                            lb_lotno5.Text = "";
                            tx_srcin5.Text = tx;
                        }
                        break;
                    case "6":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname6.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty6.Text = dt.Rows[0]["qty"].ToString();
                            lbitem6.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno6.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin6.Text = tx;
                        }
                        else
                        {
                           
                            tx_srcname6.Text = "";
                            tx_srcqty6.Text = "";
                            lbitem6.Text = "";
                            lb_lotno6.Text = "";
                            tx_srcin6.Text = tx;
                        }
                        break;
                    case "7":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname7.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty7.Text = dt.Rows[0]["qty"].ToString();
                            lbitem7.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno7.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin7.Text = tx;
                        }
                        else
                        {
                          
                            tx_srcname7.Text = "";
                            tx_srcqty7.Text = "";
                            lbitem7.Text = "";
                            lb_lotno7.Text = "";
                            tx_srcin7.Text = tx;
                        }
                        break;
                    case "8":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname8.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty8.Text = dt.Rows[0]["qty"].ToString();
                            lbitem8.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno8.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin8.Text = tx;

                        }
                        else
                        {
                           
                            tx_srcname8.Text = "";
                            tx_srcqty8.Text = "";
                            lbitem8.Text = "";
                            lb_lotno8.Text = "";
                            tx_srcin8.Text = tx;
                        }
                        break;
                }

            }
        }
        private void SrcInfo(string tx,string idx)
        {
            //VI_PURSTOCK_LOT obj = ModelService.GetList(p => p.Temp2 == tx_srcin.Text).OrderBy(o => o.Temp2).FirstOrDefault();
            DataTable dt = DbRequesHandler.GetDTselect("exec sp_srcin '" + tx + "'");
            
            if (dt == null) {
                MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
          
            }
            else
            {
                
                    switch (idx)
                    {
                        case "1":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname1.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty1.Text = dt.Rows[0]["qty"].ToString();
                            lbitem1.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno1.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin2.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname1.Text = "";
                            tx_srcqty1.Text = "";
                            lbitem1.Text = "";
                            lb_lotno1.Text = "";
                            tx_srcin1.Focus();
                        }
                        break;
                    case "2":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname2.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty2.Text = dt.Rows[0]["qty"].ToString();
                            lbitem2.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno2.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin3.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname2.Text = "";
                            tx_srcqty2.Text = "";
                            lbitem2.Text = "";
                            lb_lotno2.Text = "";
                            tx_srcin2.Focus();
                        }
                        break;
                    case "3":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname3.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty3.Text = dt.Rows[0]["qty"].ToString();
                            lbitem3.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno3.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin4.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname3.Text = "";
                            tx_srcqty3.Text = "";
                            lbitem3.Text = "";
                            lb_lotno3.Text = "";
                            tx_srcin3.Focus();
                        }
                        break;
                    case "4":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname4.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty4.Text = dt.Rows[0]["qty"].ToString();
                            lbitem4.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno4.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin5.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname4.Text = "";
                            tx_srcqty4.Text = "";
                            lbitem4.Text = "";
                            lb_lotno4.Text = "";
                            tx_srcin4.Focus();
                        }
                        break;
                    case "5":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname5.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty5.Text = dt.Rows[0]["qty"].ToString();
                            lbitem5.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno5.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin6.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname5.Text = "";
                            tx_srcqty5.Text = "";
                            lbitem5.Text = "";
                            lb_lotno5.Text = "";
                            tx_srcin5.Focus();
                        }
                        break;
                    case "6":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname6.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty6.Text = dt.Rows[0]["qty"].ToString();
                            lbitem6.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno6.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin7.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname6.Text = "";
                            tx_srcqty6.Text = "";
                            lbitem6.Text = "";
                            lb_lotno6.Text = "";
                            tx_srcin6.Focus();
                        }
                        break;
                    case "7":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname7.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty7.Text = dt.Rows[0]["qty"].ToString();
                            lbitem7.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno7.Text = dt.Rows[0]["OutLot"].ToString();
                            tx_srcin8.Focus();
                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname7.Text = "";
                            tx_srcqty7.Text = "";
                            lbitem7.Text = "";
                            lb_lotno7.Text = "";
                            tx_srcin7.Focus();
                        }
                        break;
                    case "8":
                        if (dt.Rows.Count >= 1)
                        {
                            tx_srcname8.Text = dt.Rows[0]["ItemName"].ToString();
                            tx_srcqty8.Text = dt.Rows[0]["qty"].ToString();
                            lbitem8.Text = dt.Rows[0]["ItemCode"].ToString();
                            lb_lotno8.Text = dt.Rows[0]["OutLot"].ToString();

                        }
                        else
                        {
                            MessageBox.Show("원소재 품목코드가 잘못되었습니다.");
                            tx_srcname8.Text = "";
                            tx_srcqty8.Text = "";
                            lbitem8.Text = "";
                            lb_lotno8.Text = "";
                            tx_srcin8.Focus();
                        }
                        break;
                }
            
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tx_srcname1.Text == "") {
                MessageBox.Show("원소재를 확인하세요");
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            returnval = new string[16];
            returnval[0] = lbitem1.Text.GetNullToEmpty()== "lbitem1"? null : lbitem1.Text.GetNullToEmpty();
            returnval[1] = lb_lotno1.Text.GetNullToEmpty() == "lb_lotno1" ? null : lb_lotno1.Text.GetNullToEmpty();
            returnval[2] = lbitem2.Text.GetNullToEmpty() == "lbitem2" ? null : lbitem2.Text.GetNullToEmpty();
            returnval[3] = lb_lotno2.Text.GetNullToEmpty() == "lb_lotno2" ? null : lb_lotno2.Text.GetNullToEmpty();
            returnval[4] = lbitem3.Text.GetNullToEmpty() == "lbitem3" ? null : lbitem3.Text.GetNullToEmpty();
            returnval[5] = lb_lotno3.Text.GetNullToEmpty() == "lb_lotno3" ? null : lb_lotno3.Text.GetNullToEmpty();
            returnval[6] = lbitem4.Text.GetNullToEmpty() == "lbitem4" ? null : lbitem4.Text.GetNullToEmpty();
            returnval[7] = lb_lotno4.Text.GetNullToEmpty() == "lb_lotno4" ? null : lb_lotno4.Text.GetNullToEmpty();
            returnval[8] = lbitem5.Text.GetNullToEmpty() == "lbitem5" ? null : lbitem5.Text.GetNullToEmpty();
            returnval[9] = lb_lotno5.Text.GetNullToEmpty() == "lb_lotno5" ? null : lb_lotno5.Text.GetNullToEmpty();
            returnval[10] = lbitem6.Text.GetNullToEmpty() == "lbitem6" ? null : lbitem6.Text.GetNullToEmpty();
            returnval[11] = lb_lotno6.Text.GetNullToEmpty() == "lb_lotno6" ? null : lb_lotno6.Text.GetNullToEmpty();
            returnval[12] = lbitem7.Text.GetNullToEmpty() == "lbitem7" ? null : lbitem7.Text.GetNullToEmpty();
            returnval[13] = lb_lotno7.Text.GetNullToEmpty() == "lb_lotno7" ? null : lb_lotno7.Text.GetNullToEmpty();
            returnval[14] = lbitem8.Text.GetNullToEmpty() == "lbitem8" ? null : lbitem8.Text.GetNullToEmpty();
            returnval[15] = lb_lotno8.Text.GetNullToEmpty() == "lb_lotno8" ? null : lb_lotno8.Text.GetNullToEmpty();

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
            tx_srcin1.Text = keypad.returnval;
            
         //   SrcInfo();

        }
    }
}
