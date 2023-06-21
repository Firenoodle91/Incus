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
    /// 20210819 오세완 차장
    /// 자재출고 바코드라벨, 10 * 6 크기
    /// </summary>
    public partial class XRPUR1301_V2 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_WMS1000> ModelService = (IService<TN_WMS1000>)ProductionFactory.GetDomainService("TN_WMS1000");

        public XRPUR1301_V2()
        {
            InitializeComponent();
        }

        public XRPUR1301_V2(TN_PUR1301 obj) : this()
        {
            var cultureIndex = DataConvert.GetCultureIndex();

            cell_ItemCode.Text = obj.ItemCode;
            cell_ItemName.Text = obj.TN_STD1100.ItemName;
            cell_ItemName1.Text = cultureIndex == 1 ? obj.TN_STD1100.ItemName1 : (cultureIndex == 2 ? obj.TN_STD1100.ItemNameENG : obj.TN_STD1100.ItemNameCHN);

            cell_OutQty.Text = obj.OutQty.ToString("#,#.##");
            cell_OutDate.Text = obj.TN_PUR1300.OutDate.ToShortDateString();

            cell_InLotNo.Text = obj.InLotNo;
            //cell_InCustomerLotNo.Text = obj.InCustomerLotNo;

            bar_OutLotNo.Text = obj.OutLotNo;
            tx_OutLotNo.Text = obj.OutLotNo;
        }
    }
}
