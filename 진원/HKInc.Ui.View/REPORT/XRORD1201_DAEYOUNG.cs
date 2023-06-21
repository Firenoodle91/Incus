using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using System.Collections.Generic;
using System.Data;
using HKInc.Service.Service;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 대영 출고 라벨
    /// </summary>
    public partial class XRORD1201_DAEYOUNG : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRORD1201_DAEYOUNG()
        {
            InitializeComponent();
        }


        public XRORD1201_DAEYOUNG(TN_ORD1201 obj) : this()
        {
            var Customer = ModelService.GetChildList<TN_STD1400>(x => x.CustomerCode == obj.TN_ORD1200.CustCode && x.UseFlag == "Y").FirstOrDefault();

            cell_CarTyp.Text = obj.TN_ORD1200.TN_STD1100.BottomCategory;
            cell_ItemName1.Text = obj.TN_ORD1200.TN_STD1100.ItemNm1;
            cell_ItemName2.Text = obj.TN_ORD1200.TN_STD1100.ItemNm;
            cell_LotNo.Text = obj.LotNo;
            cell_Qty.Text = Convert.ToInt32(obj.OutQty).ToString("n0");
            cell_Customer.Text = Customer != null ? Customer.CustomerName.GetNullToEmpty() : string.Empty;
        }
    }
}
