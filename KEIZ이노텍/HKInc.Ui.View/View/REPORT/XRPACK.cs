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
using HKInc.Ui.Model.Domain.TEMP;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 포장라벨출력/박스라벨출력
    /// </summary>
    public partial class XRPACK : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRPACK()
        {
            InitializeComponent();
        }

        public XRPACK(TEMP_XFPOP_PACK TEMP_XFPOP_PACK, int perBoxQty, string printDate) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemName.Text = TEMP_XFPOP_PACK.ItemCode;
            cell_ItemName1.Text = cultureIndex == 1 ? TEMP_XFPOP_PACK.ItemName : (cultureIndex == 2 ? TEMP_XFPOP_PACK.ItemNameENG : TEMP_XFPOP_PACK.ItemNameCHN);

            //var customerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == TEMP_XFPOP_PACK.CustomerCode).FirstOrDefault();
            //if (customerObj == null) cell_CarType.Text = string.Empty;
            //else cell_CarType.Text = cultureIndex == 1 ? customerObj.CustomerName : (cultureIndex == 2 ? customerObj.CustomerNameENG : customerObj.CustomerNameCHN);

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            cell_CarType.Text = "(주)인천정밀";// itemObj.CarType;
            cell_Qty.Text = perBoxQty.ToString("#,0.##");
            cell_PrintDate.Text = printDate;
            cell_ItemCode.Text = itemObj.ItemCode;
            cell_ItemName.Text = itemObj.ItemName;
            cell_ItemName1.Text = cultureIndex == 1 ? itemObj.ItemName : (cultureIndex == 2 ? itemObj.ItemNameENG : itemObj.ItemNameCHN);
            cell_ItemName1.Text = itemObj.ItemName;

            bar_ProductLotNo.Text = TEMP_XFPOP_PACK.ProductLotNo;
            tx_ProductLotNo.Text = TEMP_XFPOP_PACK.ProductLotNo;
        }

        public XRPACK(VI_MPS1800_LIST VI_MPS1800_LIST, int perBoxQty, string printDate) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = VI_MPS1800_LIST.TN_STD1100.ItemCode;
            cell_ItemName.Text = VI_MPS1800_LIST.TN_STD1100.ItemName;
            cell_ItemName1.Text = cultureIndex == 1 ? VI_MPS1800_LIST.TN_STD1100.ItemName : (cultureIndex == 2 ? VI_MPS1800_LIST.TN_STD1100.ItemNameENG : VI_MPS1800_LIST.TN_STD1100.ItemNameCHN);
            cell_ItemName1.Text = VI_MPS1800_LIST.TN_STD1100.ItemName;
            //var customerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == TEMP_XFPOP_PACK.CustomerCode).FirstOrDefault();
            //if (customerObj == null) cell_CarType.Text = string.Empty;
            //else cell_CarType.Text = cultureIndex == 1 ? customerObj.CustomerName : (cultureIndex == 2 ? customerObj.CustomerNameENG : customerObj.CustomerNameCHN);

            cell_CarType.Text = "(주)인천정밀";//VI_MPS1800_LIST.TN_STD1100.CarType;
            cell_Qty.Text = perBoxQty.ToString("#,0.##");
            cell_PrintDate.Text = printDate;

            bar_ProductLotNo.Text = VI_MPS1800_LIST.ProductLotNo;
            tx_ProductLotNo.Text = VI_MPS1800_LIST.ProductLotNo;
        }
        public XRPACK(TN_ORD1301 v, TN_ORD1300 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = obj.TN_STD1100.ItemName;
            cell_ItemName1.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_CarType.Text = obj.TN_STD1400.CustomerName;

            xrLabel4.Text = "입고일자";
            cell_Qty.Text = v.InQty.ToString("#,0.##");
            cell_PrintDate.Text = obj.InDate.ToShortDateString();

            bar_ProductLotNo.Text = v.InLotNo;
            tx_ProductLotNo.Text = v.InLotNo;
        }

        public XRPACK(TN_ORD1401 v, TN_ORD1400 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = obj.TN_STD1100.ItemName;
            cell_ItemName1.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_CarType.Text = obj.TN_STD1400.CustomerName;

            xrLabel4.Text = "출고일자";
            cell_Qty.Text = v.OutQty.ToString("#,0.##");
            cell_PrintDate.Text = obj.OutDate.ToShortDateString();

            bar_ProductLotNo.Text = v.OutLotNo;
            tx_ProductLotNo.Text = v.OutLotNo;
        }


    }
}
