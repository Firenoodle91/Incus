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
using System.Data;
using HKInc.Service.Service;

namespace HKInc.Ui.View.REPORT
{
    public partial class RQCLABLE : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_PUR1301> ModelService = (IService<TN_PUR1301>)ProductionFactory.GetDomainService("TN_PUR1301");
        public RQCLABLE()
        {
            InitializeComponent();
        }
        //public RQCLABLE(TN_QCT1200 obj1):this()
        //{
        //    TN_PUR1301 obj = ModelService.GetList(p => p.Temp2 == obj1.Temp1).FirstOrDefault();
        //    Tc_itemcode.Text = obj.TN_STD1100.ItemNm;// obj.ItemCode;
        //    Tc_itemnm.Text = obj.TN_STD1100.Spec1;
        //    Tc_qty.Text = obj.InputQty.ToString();
        //    try
        //    {
        //        Tc_indate.Text = obj.CreateTime.ToString().Substring(0, 10);
        //    }
        //    catch { Tc_indate.Text = ""; }
        //    try
        //    {
        //        Tc_Whposion.Text = obj.WhPosition.ToString();
        //    }
        //    catch { Tc_Whposion.Text = ""; }
        //    bar_inlot.Text = obj.Temp2;

        //}
        public RQCLABLE(TN_PUR1301 obj) : this()
        {
      //      TN_PUR1301 obj = ModelService.GetList(p => p.Temp2 == obj1.Temp1).FirstOrDefault();
            Tc_itemcode.Text = obj.TN_STD1100.ItemNm1;// obj.ItemCode;
            Tc_itemnm.Text = obj.TN_STD1100.Spec1;
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

    }
}
