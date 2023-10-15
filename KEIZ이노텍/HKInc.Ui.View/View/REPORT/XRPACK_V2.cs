using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using System.Collections.Generic;
using HKInc.Service.Helper;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 20211214 오세완 차장 
    /// 작업지시 후 포장라벨출력, 4*8 크기
    /// </summary>
    public partial class XRPACK_V2 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRPACK_V2()
        {
            InitializeComponent();
        }

        public XRPACK_V2(TN_MPS1200 mps1200, int pi_Qty) : this()
        {
            if(mps1200 != null)
            {
                TN_STD1100 temp_std1100 = mps1200.TN_STD1100;
                if(temp_std1100 != null)
                {
                    cell_ItenName.Text = temp_std1100.ItemName;
                    cell_ItemCode.Text = temp_std1100.ItemCode;
                }

                cell_Workno.Text = mps1200.WorkNo;
                cell_Orderdate.Text = mps1200.WorkDate.ToShortDateString();
                cell_OrderQty.Text = mps1200.WorkQty.ToString("#,###");
                cell_BoxQty.Text = pi_Qty.ToString("#,###");

                string sPackageType = "";
                if (mps1200.Temp.GetNullToEmpty() == "N")
                    sPackageType = "X";
                else
                    sPackageType = "O";

                cell_OrderPackage.Text = sPackageType;

                bar_WorkNo.Text = mps1200.WorkNo;
                tx_WorkNo.Text = mps1200.WorkNo;
            }
        }
    }
}
