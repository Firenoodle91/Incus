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
    public partial class RWHPOSITION : DevExpress.XtraReports.UI.XtraReport
    {
        public RWHPOSITION()
        {
            InitializeComponent();
        }
        public RWHPOSITION(TN_WMS2000 obj):this()
        {
            Tc_whcode.Text = obj.WhCode;
            Tc_whname.Text = obj.TN_WMS1000.WhName;
            Tc_positionnote.Text = obj.PosionName;
            Tc_positioncode.Text = obj.PosionCode.ToString();
            bar_position.Text = obj.PosionCode.ToString();

        }
       
    }
}
