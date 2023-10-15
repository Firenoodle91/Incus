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

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 자재입고 바코드라벨 
    /// </summary>
    public partial class XRPUR1201_SCM : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRPUR1201_SCM()
        {
            InitializeComponent();
        }

        public XRPUR1201_SCM(TN_PUR1201_SCM obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();
            
            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (obj.TN_STD1100.Unit.GetNullToEmpty() != "")
            {
                cell_InQty.Text = obj.InQty.ToString("#,#.##") + DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).First().CodeName;
            }
            else
            {
                cell_InQty.Text = obj.InQty.ToString("#,#.##");// + DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).First().CodeName;
            }
            cell_InDate.Text = obj.TN_PUR1200_SCM.InDate.ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InLotNo;
            tx_inlot.Text = obj.InLotNo;
        }

        public XRPUR1201_SCM(TN_PUR1201 obj, decimal qty) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_InQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            if (obj.TN_STD1100.Unit.GetNullToEmpty() != "")
            {
                cell_InQty.Text = obj.InQty.ToString("#,#.##") + DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).First().CodeName;
            }
            else
            {
                cell_InQty.Text = obj.InQty.ToString("#,#.##");// + DbRequestHandler.GetCommTopCode(MasterCodeSTR.Unit).Where(p => p.CodeVal == obj.TN_STD1100.Unit).First().CodeName;
            }
            cell_InDate.Text = obj.TN_PUR1200.InDate.ToShortDateString();

            var WMS1000 = ModelService.GetList(p => p.WhCode == obj.InWhCode).FirstOrDefault();
            var WMS2000 = ModelService.GetChildList<TN_WMS2000>(p => p.PositionCode == obj.InWhPosition).FirstOrDefault();

            bar_inlot.Text = obj.InLotNo;
            tx_inlot.Text = obj.InLotNo; 
        }
    }
}
