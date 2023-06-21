using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 입고위치 바코드 형식
    /// </summary>
    public partial class XRWMS1000 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRWMS1000()
        {
            InitializeComponent();
        }
        public XRWMS1000(TN_WMS2000 obj) : this()
        {
            var culture = DataConvert.GetCultureIndex();
            Tc_whcode.Text = obj.WhCode;
            Tc_whname.Text = culture == 1 ? obj.TN_WMS1000.WhName : (culture == 2 ? obj.TN_WMS1000.WhNameENG : obj.TN_WMS1000.WhNameCHN);
            Tc_positionnote.Text = obj.PositionName;
            Tc_positioncode.Text = obj.PositionCode.ToString();
            bar_position.Text = obj.PositionCode.ToString();
            tx_position.Text = obj.PositionCode.ToString();
        }

    }
}