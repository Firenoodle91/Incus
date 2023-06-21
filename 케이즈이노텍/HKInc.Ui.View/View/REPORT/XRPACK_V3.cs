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
    /// 20211223 오세완 차장 
    /// 포장공정에서 포장라벨출력, 4*8 크기
    /// </summary>
    public partial class XRPACK_V3 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRPACK_V3()
        {
            InitializeComponent();
        }

        public XRPACK_V3(TEMP_XFPOP_PACK TEMP_XFPOP_PACK, int pi_Qty, string sPrintdate) : this()
        {
            if(TEMP_XFPOP_PACK != null)
            {
                cell_ItenName.Text = TEMP_XFPOP_PACK.ItemName;
                cell_ItemCode.Text = TEMP_XFPOP_PACK.ItemCode;

                cell_OwnCompanyName.Text = "케이즈이노텍";
                cell_Productdate.Text = sPrintdate;
                cell_BoxQty.Text = pi_Qty.ToString("#,###");

                string sPackageType = "";
                if (TEMP_XFPOP_PACK.PackageType.GetNullToEmpty() == "N")
                    sPackageType = "X";
                else
                    sPackageType = "O";

                cell_OrderPackage.Text = sPackageType;

                bar_WorkNo.Text = TEMP_XFPOP_PACK.ProductLotNo;
                tx_WorkNo.Text = TEMP_XFPOP_PACK.ProductLotNo;
            }
        }

        public XRPACK_V3(TN_ORD1301 detailObj, TN_ORD1300 masterObj) : this()
        {
            cell_OwnCompanyName.Text = "케이즈이노텍";

            if (detailObj != null)
            {
                bar_WorkNo.Text = detailObj.InLotNo;
                tx_WorkNo.Text = detailObj.InLotNo;
                cell_BoxQty.Text = detailObj.InQty.ToString("#,###");

                string sPackageType = "";
                if (detailObj.Temp.GetNullToEmpty() == "N")
                    sPackageType = "X";
                else
                    sPackageType = "O";

                cell_OrderPackage.Text = sPackageType;
            }

            if(masterObj != null)
            {
                cell_ItemCode.Text = masterObj.ItemCode;
                cell_Productdate.Text = masterObj.InDate.ToShortDateString();

                if (masterObj.TN_STD1100 == null)
                {
                    IService<TN_STD1100> std1100_Service = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
                    List<TN_STD1100> std_Arr = std1100_Service.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                                            p.UseFlag == "Y").ToList();
                    if(std_Arr != null)
                        if(std_Arr.Count > 0)
                        {
                            TN_STD1100 std_Temp = std_Arr.FirstOrDefault();
                            if (std_Temp != null)
                                cell_ItenName.Text = std_Temp.ItemName;
                        }
                }
                else
                    cell_ItenName.Text = masterObj.TN_STD1100.ItemName;
            }
        }

        public XRPACK_V3(TN_ORD1401 detailObj, TN_ORD1400 masterObj) : this()
        {
            cell_OwnCompanyName.Text = "케이즈이노텍";

            if (detailObj != null)
            {
                bar_WorkNo.Text = detailObj.OutLotNo;
                tx_WorkNo.Text = detailObj.OutLotNo;
                cell_BoxQty.Text = detailObj.OutQty.ToString("#,###");

                string sPackageType = "";
                if (detailObj.Temp.GetNullToEmpty() == "N")
                    sPackageType = "X";
                else
                    sPackageType = "O";

                cell_OrderPackage.Text = sPackageType;
            }

            if (masterObj != null)
            {
                cell_ItemCode.Text = masterObj.ItemCode;
                cell_Productdate.Text = masterObj.OutDate.ToShortDateString();
                xrTableCell2.Text = "출고일";

                if (masterObj.TN_STD1100 == null)
                {
                    IService<TN_STD1100> std1100_Service = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
                    List<TN_STD1100> std_Arr = std1100_Service.GetList(p => p.ItemCode == masterObj.ItemCode &&
                                                                            p.UseFlag == "Y").ToList();
                    if (std_Arr != null)
                        if (std_Arr.Count > 0)
                        {
                            TN_STD1100 std_Temp = std_Arr.FirstOrDefault();
                            if (std_Temp != null)
                                cell_ItenName.Text = std_Temp.ItemName;
                        }
                }
                else
                    cell_ItenName.Text = masterObj.TN_STD1100.ItemName;
            }
        }
    }
}
