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
using HKInc.Service.Service;
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 자재입고 바코드라벨 
    /// </summary>
    public partial class XRPUR1201 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRPUR1201()
        {
            InitializeComponent();
        }

        public XRPUR1201(TN_PUR1201 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var unitObj = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).FirstOrDefault();
            var unit = unitObj == null ? string.Empty : unitObj.CodeName;

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell_InQty.Text = obj.InQty.ToString("#,0.##") + unit;
            cell_InDate.Text = obj.TN_PUR1200.InDate.ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InLotNo;
            tx_inlot.Text = obj.InLotNo;
        }

        public XRPUR1201(TN_PUR1302 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var unitObj = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).FirstOrDefault();
            var unit = unitObj == null ? string.Empty : unitObj.CodeName;

            xrLabel3.Text = "재입고량";
            xrLabel4.Text = "재입고일";

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell_InQty.Text = obj.ReturnQty.GetDecimalNullToZero().ToString("#,0.##") + unit;
            if (obj.ReturnDate == null)
                cell_InDate.Text = string.Empty;
            else
                cell_InDate.Text = ((DateTime)obj.ReturnDate).ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.ReturnWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.ReturnWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InLotNo;
            tx_inlot.Text = obj.InLotNo;
        }

        public XRPUR1201(TEMP_PUR_STOCK_DETAIL obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            if (obj.Division == "재입고")
            {
                xrLabel3.Text = "재입고량";
                xrLabel4.Text = "재입고일";
            }

            var TN_STD1100 = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == obj.ItemCode).First();
            var unitObj = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == TN_STD1100.Unit).FirstOrDefault();
            var unit = unitObj == null ? string.Empty : unitObj.CodeName;

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? TN_STD1100.ItemName : (cultureIndex == 2 ? TN_STD1100.ItemNameENG : TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell_InQty.Text = obj.InOutQty.GetDecimalNullToZero().ToString("#,#.##") + unit;
            if (obj.InOutDate == null)
                cell_InDate.Text = string.Empty;
            else
                cell_InDate.Text = ((DateTime)obj.InOutDate).ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InOutLotNo;
            tx_inlot.Text = obj.InOutLotNo;
        }

        public XRPUR1201(TN_PUR1201 obj, decimal qty) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var unitObj = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).FirstOrDefault();
            var unit = unitObj == null ? string.Empty : unitObj.CodeName;

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell_InQty.Text = obj.InQty.ToString("#,0.##") + unit;
            cell_InDate.Text = obj.TN_PUR1200.InDate.ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InLotNo;
            tx_inlot.Text = obj.InLotNo;
        }

        public XRPUR1201(TN_PUR1302 obj, decimal qty) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var unitObj = DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).FirstOrDefault();
            var unit = unitObj == null ? string.Empty : unitObj.CodeName;

            xrLabel3.Text = "재입고량";
            xrLabel4.Text = "재입고일";

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            cell_InQty.Text = obj.ReturnQty.GetDecimalNullToZero().ToString("#,0.##") + unit;
            if(obj.ReturnDate == null)
                cell_InDate.Text = string.Empty;
            else
                cell_InDate.Text = ((DateTime)obj.ReturnDate).ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.ReturnWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.ReturnWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InLotNo;
            tx_inlot.Text = obj.InLotNo;
        }
    }
}
