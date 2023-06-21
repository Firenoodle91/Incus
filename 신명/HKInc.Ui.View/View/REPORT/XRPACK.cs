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

            cell_ItemCode.Text = TEMP_XFPOP_PACK.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? TEMP_XFPOP_PACK.ItemName : (cultureIndex == 2 ? TEMP_XFPOP_PACK.ItemNameENG : TEMP_XFPOP_PACK.ItemNameCHN);

            //var customerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == TEMP_XFPOP_PACK.CustomerCode).FirstOrDefault();
            //if (customerObj == null) cell_CarType.Text = string.Empty;
            //else cell_CarType.Text = cultureIndex == 1 ? customerObj.CustomerName : (cultureIndex == 2 ? customerObj.CustomerNameENG : customerObj.CustomerNameCHN);

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == TEMP_XFPOP_PACK.ItemCode).FirstOrDefault();
            cell_CarType.Text = "(주)신명유압";// itemObj.CarType;
            cell_Qty.Text = perBoxQty.ToString("#,0.##");
            cell_PrintDate.Text = printDate;

            bar_ProductLotNo.Text = TEMP_XFPOP_PACK.ProductLotNo;
            tx_ProductLotNo.Text = TEMP_XFPOP_PACK.ProductLotNo;
        }

        public XRPACK(VI_MPS1800_LIST VI_MPS1800_LIST, int perBoxQty, string printDate) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = VI_MPS1800_LIST.ItemCode;
            cell_ItemName.Text = cultureIndex == 1 ? VI_MPS1800_LIST.TN_STD1100.ItemName : (cultureIndex == 2 ? VI_MPS1800_LIST.TN_STD1100.ItemNameENG : VI_MPS1800_LIST.TN_STD1100.ItemNameCHN);

            //var customerObj = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == TEMP_XFPOP_PACK.CustomerCode).FirstOrDefault();
            //if (customerObj == null) cell_CarType.Text = string.Empty;
            //else cell_CarType.Text = cultureIndex == 1 ? customerObj.CustomerName : (cultureIndex == 2 ? customerObj.CustomerNameENG : customerObj.CustomerNameCHN);

            cell_CarType.Text = "(주)신명유압";//VI_MPS1800_LIST.TN_STD1100.CarType;
            cell_Qty.Text = perBoxQty.ToString("#,0.##");
            cell_PrintDate.Text = printDate;

            bar_ProductLotNo.Text = VI_MPS1800_LIST.ProductLotNo;
            tx_ProductLotNo.Text = VI_MPS1800_LIST.ProductLotNo;
        }
        
    }
}
