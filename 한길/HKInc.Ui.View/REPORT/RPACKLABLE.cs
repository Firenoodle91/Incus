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
    public partial class RPACKLABLE : DevExpress.XtraReports.UI.XtraReport
    {
        public RPACKLABLE()
        {
            InitializeComponent();
        }
        public RPACKLABLE(PRT_OUTLABLE obj):this()
        {
            Tc_itemcode.Text = obj.ItemCode;
            Tc_itemnm.Text = obj.ItemNm;
            Tc_qty.Text = obj.Qty.GetDecimalNullToZero().ToString("n0");
            bar_inlot.Text = obj.LotNo;
            lbl_PackLotNo.Text = obj.LotNo;
        }
    }
}
