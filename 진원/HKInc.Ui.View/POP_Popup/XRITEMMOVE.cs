using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using HKInc.Service.Service;

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XRITEMMOVE : DevExpress.XtraReports.UI.XtraReport
    {
        XRLabel[][] labelval; 
        public XRITEMMOVE()
        {
            InitializeComponent();
        }
        public XRITEMMOVE(string workno,string lotno,string itemmoveno)
        {
            InitializeComponent();
            labelval = new XRLabel[][] {
              new XRLabel[] {  proc0,wkqty0,work0,wkdt0 },
              new XRLabel[] {  proc1,wkqty1,work1,wkdt1 },
              new XRLabel[] {  proc2,wkqty2,work2,wkdt2 },
              new XRLabel[] {  proc3,wkqty3,work3,wkdt3 },
              new XRLabel[] {  proc4,wkqty4,work4,wkdt4 },
              new XRLabel[] {  proc5,wkqty5,work5,wkdt5 },
              new XRLabel[] {  proc6,wkqty6,work6,wkdt6 }
            };

            if (itemmoveno != "")
            {
                initdataR(itemmoveno);
            }
            else
            {
                initdata(workno,lotno);
            }

        }
        private void initdata(string workno,string lotno)
        {
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_ITEM_MOVE_PRT @workno ='"+ workno + "', @lotno='"+ lotno + "'");// @moveno")
            if (ds == null) return;
            if (ds.Tables.Count != 2) return;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                bar_moveno.Text = DbRequesHandler.GetRequestNumber("IMV");
                tx_workno.Text = row[0].ToString();
                // tx_lotno.Text = row[1].ToString();
                xrBarCode1.Text = row[1].ToString();
                tx_itemcode.Text = row[2].ToString();
                tx_itemnm.Text = row[3].ToString();
                tx_custnm.Text = row[4].ToString();
                tx_orderno.Text = row[5].ToString();
                tx_memo.Text = row[6].ToString();
                xrLabel15.Text = row[7].ToString();
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                labelval[i][0].Text = ds.Tables[1].Rows[i][0].ToString();
                labelval[i][1].Text = ds.Tables[1].Rows[i][1].ToString();
                labelval[i][2].Text = ds.Tables[1].Rows[i][2].ToString();
                labelval[i][3].Text = ds.Tables[1].Rows[i][3].ToString();


            }

            int k = DbRequesHandler.SetDataQury("insert into TN_ITEM_MOVE (ITEMMOVENO,WORKNO,LOTNO,QTY) values('"+ bar_moveno.Text+"','"+ workno + "','"+ lotno + "' ,'')");
                
    }
        private void initdataR(string obj)
        {
            DataSet ds = DbRequesHandler.GetDataQury("exec SP_ITEM_MOVE_PRT  @moveno='"+ obj + "'");
            if (ds == null) return;
            if (ds.Tables.Count != 2) return;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                bar_moveno.Text = obj;
                tx_workno.Text = row[0].ToString();
                // tx_lotno.Text = row[1].ToString();
                xrBarCode1.Text = row[1].ToString();
                tx_itemcode.Text = row[2].ToString();
                tx_itemnm.Text = row[3].ToString();
                tx_custnm.Text = row[4].ToString();
                tx_orderno.Text = row[5].ToString();
                tx_memo.Text = row[6].ToString();
                xrLabel15.Text = row[7].ToString();
            }

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                labelval[i][0].Text = ds.Tables[1].Rows[i][0].ToString();
                labelval[i][1].Text = ds.Tables[1].Rows[i][1].ToString();
                labelval[i][2].Text = ds.Tables[1].Rows[i][2].ToString();
                labelval[i][3].Text = ds.Tables[1].Rows[i][3].ToString();


            }

        }
    }
}
