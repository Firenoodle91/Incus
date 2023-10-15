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
    /// 20210820 오세완 차장 
    /// 공정이동표 바크드 라벨 스타일, 프레스 공정에서만 사용
    /// </summary>
    public partial class XRITEMMOVEDOC_PRESS : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRITEMMOVEDOC_PRESS()
        {
            InitializeComponent();
        }

        public XRITEMMOVEDOC_PRESS(TEMP_ITEM_MOVE_NO_MASTER masterObj) : this()
        {
            cell_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == masterObj.ItemCode).FirstOrDefault();
            var cultureIndex = DataConvert.GetCultureIndex();
            cell_ItemName1.Text = cultureIndex == 1 ? itemObj.ItemName1.GetNullToEmpty() : (cultureIndex == 2 ? itemObj.ItemNameENG.GetNullToEmpty() : itemObj.ItemNameCHN.GetNullToEmpty());
            cell_ItemType.Text = itemObj.CarType.GetNullToEmpty();
            cell_ItemCode.Text = itemObj.ItemCode;
            cell_ItemName.Text = itemObj.ItemName;
            cell_Qty.Text = masterObj.BoxInQty.ToString();

            bar_ItemMoveNo.Text = masterObj.ItemMoveNo;
        }

        /// <summary>
        /// 20210827 오세완 차장 
        /// 외주발주관리에서 출력하는 방법
        /// </summary>
        public XRITEMMOVEDOC_PRESS(string sItemMoveNo, string sItemCode, decimal dOkQty) : this()
        {
            cell_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");

            var itemObj = ModelService.GetChildList<TN_STD1100>(p => p.ItemCode == sItemCode).FirstOrDefault();
            var cultureIndex = DataConvert.GetCultureIndex();
            cell_ItemName1.Text = cultureIndex == 1 ? itemObj.ItemName1.GetNullToEmpty() : (cultureIndex == 2 ? itemObj.ItemNameENG.GetNullToEmpty() : itemObj.ItemNameCHN.GetNullToEmpty());
            cell_ItemType.Text = itemObj.CarType.GetNullToEmpty();
            cell_ItemCode.Text = itemObj.ItemCode;
            cell_ItemName.Text = itemObj.ItemName;
            cell_Qty.Text = Convert.ToInt32(dOkQty).ToString();

            bar_ItemMoveNo.Text = sItemMoveNo;
        }
    }
}
