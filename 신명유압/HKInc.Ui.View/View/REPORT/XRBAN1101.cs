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
    /// 반제품출고 바코드라벨 
    /// </summary>
    public partial class XRBAN1101 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRBAN1101()
        {
            InitializeComponent();
        }

        public XRBAN1101(TN_BAN1101 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_OutQty.Text = obj.OutQty.ToString("#,#.##");
            cell_OutDate.Text = obj.TN_BAN1100.OutDate.ToShortDateString();

            cell_InLotNo.Text = obj.InLotNo;

            bar_OutLotNo.Text = obj.OutLotNo;
        }
    }
}
