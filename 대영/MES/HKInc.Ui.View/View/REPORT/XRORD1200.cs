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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 제품출고 바코드라벨 
    /// </summary>
    public partial class XRORD1200 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRORD1200()
        {
            InitializeComponent();
        }

        public XRORD1200(TN_ORD1200 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);
            cell_CustomerName.Text = cultureIndex == 1 ? obj.TN_STD1400.CustomerName : (cultureIndex == 2 ? obj.TN_STD1400.CustomerNameENG : obj.TN_STD1400.CustomerNameCHN);
            cell_OutQty.Text = obj.TN_ORD1201List.Sum(p => p.OutQty).ToString("#,#.##");
            cell_OutDate.Text = obj.OutDate.ToShortDateString();
            bar_outNo.Text = obj.OutNo;
            tx_outNo.Text = obj.OutNo;
        }
    }
}
