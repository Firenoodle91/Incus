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
    /// 반제품입고 바코드라벨 
    /// </summary>
    public partial class XRBAN1001 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRBAN1001()
        {
            InitializeComponent();
        }

        public XRBAN1001(TN_BAN1001 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = obj.TN_STD1100.ItemName;
            cell_ItemName1.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName1 : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.Text = obj.InQty.ToString("#,#.##");
            cell_InDate.Text = obj.TN_BAN1000.InDate.ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            cell_WhCode.Text = WMS1000 == null ? string.Empty : cultureIndex == 1 ? WMS1000.WhName : (cultureIndex == 2 ? WMS1000.WhNameENG : WMS1000.WhNameCHN);
            cell_WhPosition.Text = WMS2000 == null ? string.Empty : WMS2000.PositionName;

            bar_inLot.Text = obj.InLotNo;
            tx_inLot.Text = obj.InLotNo;
            
        }

        public XRBAN1001(TN_BAN1001 obj, decimal qty) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemName.Text = obj.ItemCode;
            cell_ItemName1.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.Text = qty.ToString("#,#.##");
            cell_InDate.Text = obj.TN_BAN1000.InDate.ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            cell_WhCode.Text = WMS1000 == null ? string.Empty : cultureIndex == 1 ? WMS1000.WhName : (cultureIndex == 2 ? WMS1000.WhNameENG : WMS1000.WhNameCHN);
            cell_WhPosition.Text = WMS2000 == null ? string.Empty : WMS2000.PositionName;

            bar_inLot.Text = obj.InLotNo;
            tx_inLot.Text = obj.InLotNo;
        }
    }
}
