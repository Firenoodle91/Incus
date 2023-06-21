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
    /// 대영, 디와이솔루텍 제외한 나머지 출고라벨
    /// </summary>
    public partial class XRORD1201_NOMAL : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        
        public XRORD1201_NOMAL()
        {
            InitializeComponent();
        }

        public XRORD1201_NOMAL(TN_ORD1201 obj) : this()
        {
            var Company = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.DefaultCompanyPlag == "Y").FirstOrDefault();

            cell_CarTyp.Text = obj.TN_ORD1200.TN_STD1100.BottomCategory;
            cell_ItemCode.Text = obj.TN_ORD1200.TN_STD1100.ItemNm;
            cell_ItemName.Text = obj.TN_ORD1200.TN_STD1100.ItemNm1;
            cell_Qty.Text = obj.OutQty.ToString();
            cell_OutDate.Text = obj.OutDate?.ToString("yyyy-MM-dd");
            cell_LotNo.Text = obj.LotNo;
            cell_Customer.Text = obj.Memo;

            //cell_ItemCode.Text = obj.TN_ORD1200.TN_STD1100.ItemNm.GetNullToEmpty();
            //cell_ItemName.Text = obj.TN_ORD1200.TN_STD1100.ItemNm1.GetNullToEmpty();
            //cell_Qty.Text = obj.OutQty.ToString();
            //cell_LotNo.Text = obj.LotNo.GetNullToEmpty();
            //cell_Date.Text = obj.OutDate?.ToString("yyyy-MM-dd");

            if (Company != null)
            {
                cell_Company.Text = Company.CustomerName.GetNullToEmpty();
            }
        }
    }
}
