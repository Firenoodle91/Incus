using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
{
    public partial class RINPUTLABLE : DevExpress.XtraReports.UI.XtraReport
    {
        public RINPUTLABLE()
        {
            InitializeComponent();
        }
        public RINPUTLABLE(TN_PUR1301 obj):this()
        {
            Tc_itemcode.Text = obj.ItemCode;
            Tc_itemnm.Text = obj.TN_STD1100.ItemNm1;
            Tc_qty.Text = obj.InputQty.ToString();
            try
            {
                Tc_indate.Text = obj.TN_PUR1300.InputDate.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                Tc_Whposition.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposition.Text = ""; }
            bar_inlot.Text = obj.Temp2;

        }
        public RINPUTLABLE(TN_PUR1501 obj) : this()
        {
            xrLabel3.Text = "출고량";
            xrLabel4.Text = "출고일";
            xrLabel5.Text = "출고위치";
            Tc_itemcode.Text = obj.ItemCode;
            Tc_itemnm.Text = obj.TN_STD1100.ItemNm1;
            Tc_qty.Text = obj.OutQty.ToString();
            try
            {
                Tc_indate.Text = obj.TN_PUR1500.OutDate.ToString().Substring(0, 10);
            }
            catch { Tc_indate.Text = ""; }
            try
            {
                Tc_Whposition.Text = obj.WhPosition.ToString();
            }
            catch { Tc_Whposition.Text = ""; }
            bar_inlot.Text = obj.Temp1;

        }

        //  2022-02-23 반제품 미사용으로 주석

        //public RINPUTLABLE(TN_BAN1001 obj) : this()
        //{
        //    Tc_itemcode.Text = obj.ItemCode;
        //    Tc_itemnm.Text = obj.TN_STD1100.ItemNm1;
        //    Tc_qty.Text = obj.InputQty.ToString();
        //    try
        //    {
        //        Tc_indate.Text = obj.TN_BAN1000.InputDate.ToString().Substring(0, 10);
        //    }
        //    catch { Tc_indate.Text = ""; }
        //    try
        //    {
        //        Tc_Whposition.Text = obj.WhPosition.ToString();
        //    }
        //    catch { Tc_Whposition.Text = ""; }
        //    bar_inlot.Text = obj.Temp2;

        //}
        //public RINPUTLABLE(TN_BAN1201 obj) : this()
        //{
        //    xrLabel3.Text = "출고량";
        //    xrLabel4.Text = "출고일";
        //    xrLabel5.Text = "출고위치";
        //    Tc_itemcode.Text = obj.ItemCode;
        //    Tc_itemnm.Text = obj.TN_STD1100.ItemNm1;
        //    Tc_qty.Text = obj.OutQty.ToString();
        //    try
        //    {
        //        Tc_indate.Text = obj.TN_BAN1200.OutDate.ToString().Substring(0, 10);
        //    }
        //    catch { Tc_indate.Text = ""; }
        //    try
        //    {
        //        Tc_Whposition.Text = obj.WhPosition.ToString();
        //    }
        //    catch { Tc_Whposition.Text = ""; }
        //    bar_inlot.Text = obj.Temp2;

        //}
    }
}
