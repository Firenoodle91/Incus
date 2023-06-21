using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using HKInc.Service.Service;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using System.Collections.Generic;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 거래명세서
    /// </summary>
    public partial class XRORD2001 : DevExpress.XtraReports.UI.XtraReport
    {
        XRLabel[][] labelval;
        XRLabel[][] labelval1;
        public XRORD2001()
        {
            InitializeComponent();
        }
        public XRORD2001(string cust, string prtno) : this()
        {

            string REGISTRATION_NO = string.Empty;
	        string CUSTOMER_NAME = string.Empty;
            string REPRESENTATIVE_NAME = string.Empty;
            string ADDRESS = string.Empty;
            string CUSTOMER_CATEGORY_CODE = string.Empty;
            string CUSTOMER_CATEGORY_TYPE = string.Empty;

            labelval = new XRLabel[][] 
            {
              new XRLabel[] {  tx_no1,tx_item1,tx_spec1,tx_unit1,tx_qty1,tx_price1,tx_amt1,tx_tax1 },
              new XRLabel[] {  tx_no2,tx_item2,tx_spec2,tx_unit2,tx_qty2,tx_price2,tx_amt2,tx_tax2 },
              new XRLabel[] {  tx_no3,tx_item3,tx_spec3,tx_unit3,tx_qty3,tx_price3,tx_amt3,tx_tax3 },
              new XRLabel[] {  tx_no4,tx_item4,tx_spec4,tx_unit4,tx_qty4,tx_price4,tx_amt4,tx_tax4 },
              new XRLabel[] {  tx_no5,tx_item5,tx_spec5,tx_unit5,tx_qty5,tx_price5,tx_amt5,tx_tax5 },
              new XRLabel[] {  tx_no6,tx_item6,tx_spec6,tx_unit6,tx_qty6,tx_price6,tx_amt6,tx_tax6 },
              new XRLabel[] {  tx_no7,tx_item7,tx_spec7,tx_unit7,tx_qty7,tx_price7,tx_amt7,tx_tax7 },
              new XRLabel[] {  tx_no8,tx_item8,tx_spec8,tx_unit8,tx_qty8,tx_price8,tx_amt8,tx_tax8 },
              new XRLabel[] {  tx_no9,tx_item9,tx_spec9,tx_unit9,tx_qty9,tx_price9,tx_amt9,tx_tax9 },
              new XRLabel[] {  tx_no10,tx_item10,tx_spec10,tx_unit10,tx_qty10,tx_price10,tx_amt10,tx_tax10 }
            };
            labelval1 = new XRLabel[][] 
            {
              new XRLabel[] {  ts_no1,ts_item1,ts_spec1,ts_unit1,ts_qty1,ts_price1,ts_amt1,ts_tax1 },
              new XRLabel[] {  ts_no2,ts_item2,ts_spec2,ts_unit2,ts_qty2,ts_price2,ts_amt2,ts_tax2 },
              new XRLabel[] {  ts_no3,ts_item3,ts_spec3,ts_unit3,ts_qty3,ts_price3,ts_amt3,ts_tax3 },
              new XRLabel[] {  ts_no4,ts_item4,ts_spec4,ts_unit4,ts_qty4,ts_price4,ts_amt4,ts_tax4 },
              new XRLabel[] {  ts_no5,ts_item5,ts_spec5,ts_unit5,ts_qty5,ts_price5,ts_amt5,ts_tax5 },
              new XRLabel[] {  ts_no6,ts_item6,ts_spec6,ts_unit6,ts_qty6,ts_price6,ts_amt6,ts_tax6 },
              new XRLabel[] {  ts_no7,ts_item7,ts_spec7,ts_unit7,ts_qty7,ts_price7,ts_amt7,ts_tax7 },
              new XRLabel[] {  ts_no8,ts_item8,ts_spec8,ts_unit8,ts_qty8,ts_price8,ts_amt8,ts_tax8 },
              new XRLabel[] {  ts_no9,ts_item9,ts_spec9,ts_unit9,ts_qty9,ts_price9,ts_amt9,ts_tax9 },
              new XRLabel[] {  ts_no10,ts_item10,ts_spec10,ts_unit10,ts_qty10,ts_price10,ts_amt10,ts_tax10 }
            };

            DataSet ds = DbRequestHandler.GetDataQury("EXEC PRT_ORDER '"+ prtno + "'");

            if (ds.Tables[0] != null)
            {
                tx_date.Text = ds.Tables[0].Rows[0][0].ToString();
                ts_date.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            if (ds.Tables[1] != null)
            {
                tx_cust.Text = ds.Tables[1].Rows[0][0].ToString();
                ts_cust.Text = ds.Tables[1].Rows[0][0].ToString();
            }

            // 2022-04-07 김진우 추가
            int Total_AMT = 0;
            int Total_TAX = 0;

            //ToString()

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j > 3)
                    {
                        labelval[i][j].Text = String.Format("{0:#,0}", ds.Tables[2].Rows[i][j]);
                        labelval1[i][j].Text = String.Format("{0:#,0}", ds.Tables[2].Rows[i][j]);
                    }
                    else
                    {
                        labelval[i][j].Text = ds.Tables[2].Rows[i][j].ToString();
                        labelval1[i][j].Text = ds.Tables[2].Rows[i][j].ToString();
                    }
                }

                var a = ds.Tables[2].Rows[i][6].GetType();


                if (ds.Tables[2].Rows[i][6].ToString() != "")
                    Total_AMT += Convert.ToInt32(ds.Tables[2].Rows[i][6]);

                if (ds.Tables[2].Rows[i][7].ToString() != "")
                    Total_TAX += Convert.ToInt32(ds.Tables[2].Rows[i][7]);
                
            }

            tx_tax.Text = String.Format("{0:#,0}", Total_TAX);
            tx_amt.Text = String.Format("{0:#,0}", Total_AMT);
            ts_tax.Text = String.Format("{0:#,0}", Total_TAX);
            ts_amt.Text = String.Format("{0:#,0}", Total_AMT);

            tx_sumamt.Text = String.Format("{0:#,0}", (Total_AMT + Total_TAX));
            ts_sumamt.Text = String.Format("{0:#,0}", (Total_AMT + Total_TAX));

            REGISTRATION_NO        =  ds.Tables[3].Rows[0][0].ToString();
	        CUSTOMER_NAME          =  ds.Tables[3].Rows[0][1].ToString();
	        REPRESENTATIVE_NAME    =  ds.Tables[3].Rows[0][2].ToString();
	        ADDRESS                =  ds.Tables[3].Rows[0][3].ToString();
	        CUSTOMER_CATEGORY_CODE =  ds.Tables[3].Rows[0][4].ToString();
	        CUSTOMER_CATEGORY_TYPE =  ds.Tables[3].Rows[0][5].ToString();


            xrLabel23.Text = xrLabel12.Text = REGISTRATION_NO;
            xrLabel22.Text = xrLabel19.Text = CUSTOMER_NAME;
            xrLabel20.Text = xrLabel29.Text = REPRESENTATIVE_NAME;
            xrLabel11.Text = xrLabel30.Text = ADDRESS;
            xrLabel8.Text = xrLabel99.Text = CUSTOMER_CATEGORY_CODE;
            xrLabel10.Text = xrLabel97.Text = CUSTOMER_CATEGORY_TYPE;


        #region 이전소스

            //for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            //{
            //    labelval[i][0].Text = ds.Tables[2].Rows[i][0].ToString();
            //    labelval[i][1].Text = ds.Tables[2].Rows[i][1].ToString();
            //    labelval[i][2].Text = ds.Tables[2].Rows[i][2].ToString();
            //    labelval[i][3].Text = ds.Tables[2].Rows[i][3].ToString();
            //    labelval[i][4].Text = ds.Tables[2].Rows[i][4].GetNullToZero().ToString("#,##0");
            //    labelval[i][5].Text = ds.Tables[2].Rows[i][5].ToString();
            //    labelval[i][6].Text = ds.Tables[2].Rows[i][6].ToString();
            //    labelval[i][7].Text = ds.Tables[2].Rows[i][7].ToString();

            //    labelval1[i][0].Text = ds.Tables[2].Rows[i][0].ToString();
            //    labelval1[i][1].Text = ds.Tables[2].Rows[i][1].ToString();
            //    labelval1[i][2].Text = ds.Tables[2].Rows[i][2].ToString();
            //    labelval1[i][3].Text = ds.Tables[2].Rows[i][3].ToString();
            //    labelval1[i][4].Text = ds.Tables[2].Rows[i][4].GetNullToZero().ToString("#,##0");
            //    labelval1[i][5].Text = ds.Tables[2].Rows[i][5].ToString();
            //    labelval1[i][6].Text = ds.Tables[2].Rows[i][6].ToString();
            //    labelval1[i][7].Text = ds.Tables[2].Rows[i][7].ToString();

            //    Total_AMT += Convert.ToInt32(ds.Tables[2].Rows[i][6]);
            //    Total_TAX += Convert.ToInt32(ds.Tables[2].Rows[i][7]);
            //}

            #endregion
        }
    }
}

