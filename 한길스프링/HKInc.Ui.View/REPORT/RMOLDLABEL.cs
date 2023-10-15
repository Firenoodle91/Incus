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
    /// <summary>
    /// 금형 바코드 화면
    /// </summary>
    public partial class RMOLDLABEL : DevExpress.XtraReports.UI.XtraReport
    {
        public RMOLDLABEL()
        {
            InitializeComponent();
        }

        public RMOLDLABEL(TN_MOLD001 obj):this()
        {
            Tc_MoldCode.Text = obj.MoldCode;
            Tc_MoldName.Text = obj.MoldName;
            Tc_CustomerName.Text = obj.TN_STD1400 == null ? string.Empty : obj.TN_STD1400.CustomerName;
            Tc_Spec1.Text = obj.Spec;

            IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
            var WMS1000 = ModelService.GetChildList<TN_WMS1000>(p => p.WhCode == obj.StPostion1).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == obj.StPostion2).FirstOrDefault();
            //Tc_Spec1.Text = WMS1000 == null ? string.Empty : WMS1000.WhName;
            Tc_W2.Text = WMS2000 == null ? string.Empty : WMS2000.PosionName;
            bar_inlot.Text = obj.MoldCode;
        }
    }
}
