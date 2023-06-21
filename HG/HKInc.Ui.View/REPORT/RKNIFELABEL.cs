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
    /// 칼 바코드 화면
    /// </summary>
    public partial class RKNIFELABEL : DevExpress.XtraReports.UI.XtraReport
    {
        public RKNIFELABEL()
        {
            InitializeComponent();
        }

        public RKNIFELABEL(TN_KNIFE001 obj):this()
        {
            Tc_KnifeCode.Text = obj.KnifeCode;
            Tc_KnifeName.Text = obj.KnifeName;

            IService<TN_MOLD001> ModelService = (IService<TN_MOLD001>)ProductionFactory.GetDomainService("TN_MOLD001");
            var WMS1000 = ModelService.GetChildList<TN_WMS1000>(p => p.WhCode == obj.StPostion1).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PosionCode == obj.StPostion2).FirstOrDefault();
            var STD1400 = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == obj.MakeCust).FirstOrDefault();
            
            Tc_Grade.Text = STD1400 == null ? string.Empty : STD1400.CustomerName;
            Tc_W1.Text = WMS1000 == null ? string.Empty : WMS1000.WhName;
            Tc_W2.Text = WMS2000 == null ? string.Empty : WMS2000.PosionName;

            bar_inlot.Text = obj.KnifeCode;
        }
    }
}
