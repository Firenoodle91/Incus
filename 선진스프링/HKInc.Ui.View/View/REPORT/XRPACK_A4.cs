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
    /// 포장라벨출력/박스라벨출력 A4
    /// </summary>
    public partial class XRPACK_A4 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRPACK_A4()
        {
            InitializeComponent();

            for (int i = 1; i <= 10; i++)
            {
                FindControl("bar_ProductLotNo" + i.ToString(), true).Visible = false;
            }

        }

        public XRPACK_A4(TEMP_XFPOP_PACK TEMP_XFPOP_PACK, int perBoxQty, string printDate, int printQty, string itemNm) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            var itemCode = itemObj.CustomerItemCode;
            //var itemName = itemObj.CustomerItemName;
            var itemName = itemNm;
            var carType = itemObj.CarType;

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CarType" + i.ToString(), true).Text = carType;
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Text = TEMP_XFPOP_PACK.ProductLotNo;
                FindControl("tx_ProductLotNo" + i.ToString(), true).Text = TEMP_XFPOP_PACK.ProductLotNo;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Visible = true;
            }
        }

        public void SetBinding(TEMP_XFPOP_PACK TEMP_XFPOP_PACK, int perBoxQty, string printDate, int printQty, string itemNm)
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            var itemCode = itemObj.CustomerItemCode;
            //var itemName = itemObj.CustomerItemName;
            var itemName = itemNm;
            var carType = itemObj.CarType;

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CarType" + i.ToString(), true).Text = carType;
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Text = TEMP_XFPOP_PACK.ProductLotNo;
                FindControl("tx_ProductLotNo" + i.ToString(), true).Text = TEMP_XFPOP_PACK.ProductLotNo;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Visible = true;
            }
        }

        public XRPACK_A4(VI_MPS1800_LIST VI_MPS1800_LIST, int perBoxQty, string printDate, int printQty) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();


            var itemCode = VI_MPS1800_LIST.ItemCode;
            var itemName = cultureIndex == 1 ? VI_MPS1800_LIST.TN_STD1100.ItemName : (cultureIndex == 2 ? VI_MPS1800_LIST.TN_STD1100.ItemNameENG : VI_MPS1800_LIST.TN_STD1100.ItemNameCHN);
            var carType = VI_MPS1800_LIST.TN_STD1100.CarType;

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CarType" + i.ToString(), true).Text = carType;
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Text = VI_MPS1800_LIST.ProductLotNo;
                FindControl("tx_ProductLotNo" + i.ToString(), true).Text = VI_MPS1800_LIST.ProductLotNo;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Visible = true;
            }
        }
        
        public void SetBinding(VI_MPS1800_LIST VI_MPS1800_LIST, int perBoxQty, string printDate, int printQty)
        {
            var cultureIndex = DataConvert.GetCultureIndex();


            var itemCode = VI_MPS1800_LIST.ItemCode;
            var itemName = cultureIndex == 1 ? VI_MPS1800_LIST.TN_STD1100.ItemName : (cultureIndex == 2 ? VI_MPS1800_LIST.TN_STD1100.ItemNameENG : VI_MPS1800_LIST.TN_STD1100.ItemNameCHN);
            var carType = VI_MPS1800_LIST.TN_STD1100.CarType;

            for (int i = 1; i <= printQty; i++)
            {
                FindControl("cell_ItemCode" + i.ToString(), true).Text = itemCode;
                FindControl("cell_ItemName" + i.ToString(), true).Text = itemName;
                FindControl("cell_CarType" + i.ToString(), true).Text = carType;
                FindControl("cell_Qty" + i.ToString(), true).Text = perBoxQty.ToString("#,0.##");
                FindControl("cell_PrintDate" + i.ToString(), true).Text = printDate;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Text = VI_MPS1800_LIST.ProductLotNo;
                FindControl("tx_ProductLotNo" + i.ToString(), true).Text = VI_MPS1800_LIST.ProductLotNo;
                FindControl("bar_ProductLotNo" + i.ToString(), true).Visible = true;
            }
        }

    }
}
