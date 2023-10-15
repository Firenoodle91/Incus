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
    /// 디와이솔루텍 출고 라벨
    /// </summary>
    public partial class XRORD1201_DYS : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
        
        public XRORD1201_DYS()
        {
            InitializeComponent();
        }

        public XRORD1201_DYS(TN_ORD1201 obj) : this()
        {
            var Company = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.DefaultCompanyPlag == "Y").FirstOrDefault();

            cell_ItemCode.Text = obj.TN_ORD1200.TN_STD1100.ItemNm.GetNullToEmpty();
            cell_ItemName.Text = obj.TN_ORD1200.TN_STD1100.ItemNm1.GetNullToEmpty();
            cell_Qty.Text = obj.OutQty.ToString();
            cell_LotNo.Text = obj.LotNo.GetNullToEmpty();
            cell_Date.Text = obj.OutDate?.ToString("yyyy-MM-dd");

            if (Company != null)
            {
                cell_Company.Text = Company.CustomerName.GetNullToEmpty();
            }


            //(유/무) 검사 체크
            var tN_STD1100 = ModelService.GetList(x => x.UseYn == "Y" && x.ItemCode == obj.ItemCode).FirstOrDefault();

            if (tN_STD1100 != null && tN_STD1100.InspectLabelType != null)
            {
                //무검사
                if (tN_STD1100.InspectLabelType == "A00")
                    xrShape2.Visible = true;
                else
                    xrShape1.Visible = true;
            }
        }
    }
}
