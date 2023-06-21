using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraPrinting;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 거래명세서 출력물
    /// </summary>
    public partial class XRORD1200_TRADING_NEW : DevExpress.XtraReports.UI.XtraReport
    {
        //private List<TN_STD1000> CategoryTypeList; //업태
        //private List<TN_STD1000> CategoryCodeList; //업종
        public XRORD1200_TRADING_NEW()
        {
            InitializeComponent();
        }

        public XRORD1200_TRADING_NEW(string printDate, string dueDate, TN_STD1400 ToCustomerObj, TN_STD1400 FrCustomerObj, List<TEMP_TRADING_DETAIL> printList) : this()
        {
            cell_PrintDate_1.Text = printDate;
            cell_PrintDate_2.Text = printDate;

            cell_DueDate_1.Text = dueDate;
            cell_DueDate_2.Text = dueDate;

            var cultureIndex = DataConvert.GetCultureIndex();

            if (ToCustomerObj != null)
            {
                var toCustomerName = cultureIndex == 1 ? ToCustomerObj.CustomerName : (cultureIndex == 2 ? ToCustomerObj.CustomerNameENG : ToCustomerObj.CustomerNameCHN);
                cell_ToCustomerName_1.Text = toCustomerName;
                cell_ToCustomerName_2.Text = toCustomerName;

                cell_ToAddress_1.Text = ToCustomerObj.Address + ToCustomerObj.Address2;
                cell_ToAddress_2.Text = ToCustomerObj.Address + ToCustomerObj.Address2;

                cell_ToPhoneNumber_1.Text = ToCustomerObj.PhoneNumber;
                cell_ToPhoneNumber_2.Text = ToCustomerObj.PhoneNumber;
            }

            if (FrCustomerObj != null)
            {
                cell_FrBusinessNumber_1.Text = FrCustomerObj.RegistrationNo;
                cell_FrBusinessNumber_2.Text = FrCustomerObj.RegistrationNo;

                var frCustomerName = cultureIndex == 1 ? FrCustomerObj.CustomerName : (cultureIndex == 2 ? FrCustomerObj.CustomerNameENG : FrCustomerObj.CustomerNameCHN);
                cell_FrCustomerName_1.Text = frCustomerName;
                cell_FrCustomerName_2.Text = frCustomerName;

                cell_FrRepresidentName_1.Text = FrCustomerObj.RepresentativeName;
                cell_FrRepresidentName_2.Text = FrCustomerObj.RepresentativeName;

                cell_FrAddress_1.Text = FrCustomerObj.Address + FrCustomerObj.Address2;
                cell_FrAddress_2.Text = FrCustomerObj.Address + FrCustomerObj.Address2;

                cell_FrCategoryType_1.Text = FrCustomerObj.CustomerCategoryType;
                cell_FrCategoryType_2.Text = FrCustomerObj.CustomerCategoryType; 

                cell_FrCategoryCode_1.Text = FrCustomerObj.CustomerCategoryCode;
                cell_FrCategoryCode_2.Text = FrCustomerObj.CustomerCategoryCode; 
            }

            for (int i = 1; i <= printList.Count; i++)
            {
                var index = i - 1;
                ((XRTableCell)FindControl(string.Format("cell_No_1_{0}", i), true)).Text = printList[index].No.ToString("N0");
                ((XRTableCell)FindControl(string.Format("cell_No_2_{0}", i), true)).Text = printList[index].No.ToString("N0");

                ((XRTableCell)FindControl(string.Format("cell_ItemCode_1_{0}", i), true)).Text = printList[index].ItemCode;
                ((XRTableCell)FindControl(string.Format("cell_ItemCode_2_{0}", i), true)).Text = printList[index].ItemCode;

                ((XRTableCell)FindControl(string.Format("cell_ItemName_1_{0}", i), true)).Text = printList[index].ItemName;
                ((XRTableCell)FindControl(string.Format("cell_ItemName_2_{0}", i), true)).Text = printList[index].ItemName;

                ((XRTableCell)FindControl(string.Format("cell_Unit_1_{0}", i), true)).Text = printList[index].Unit;
                ((XRTableCell)FindControl(string.Format("cell_Unit_2_{0}", i), true)).Text = printList[index].Unit;

                ((XRTableCell)FindControl(string.Format("cell_Qty_1_{0}", i), true)).Text = printList[index].Qty.ToString("N0");
                ((XRTableCell)FindControl(string.Format("cell_Qty_2_{0}", i), true)).Text = printList[index].Qty.ToString("N0");

                ((XRTableCell)FindControl(string.Format("cell_Cost_1_{0}", i), true)).Text = printList[index].Cost.ToString("#,###,###,###.##");
                ((XRTableCell)FindControl(string.Format("cell_Cost_2_{0}", i), true)).Text = printList[index].Cost.ToString("#,###,###,###.##");

                ((XRTableCell)FindControl(string.Format("cell_Amt_1_{0}", i), true)).Text = printList[index].Amt.ToString("#,###,###,###.##");
                ((XRTableCell)FindControl(string.Format("cell_Amt_2_{0}", i), true)).Text = printList[index].Amt.ToString("#,###,###,###.##");

                ((XRTableCell)FindControl(string.Format("cell_Memo_1_{0}", i), true)).Text = printList[index].Memo;
                ((XRTableCell)FindControl(string.Format("cell_Memo_2_{0}", i), true)).Text = printList[index].Memo;
            }

            cell_SumAmt_1.Text = (printList.Sum(p => p.Amt)).ToString("#,###,###,###.##");
            cell_SumAmt_2.Text = (printList.Sum(p => p.Amt)).ToString("#,###,###,###.##");
        }

        //public XRORD1200_TRADING_NEW(string printDate, TN_STD1400 OurCustomer, TN_STD1400 InCustomer, List<TEMP_TRADING_DETAIL> printList) : this()
        //{
        //    cell_PrintDate_1.Text = printDate;
        //    cell_print_date_2.Text = printDate;

        //    //CategoryCodeList = DbRequesHandler.GetCommCode(MasterCodeSTR.BusinessCode);
        //    //CategoryTypeList = DbRequesHandler.GetCommCode(MasterCodeSTR.BusinessType);

        //    var cultureIndex = DataConvert.GetCultureIndex();

        //    if (OurCustomer != null)
        //    {
        //        cell_our_business_number_1.Text = OurCustomer.RegistrationNo;
        //        cell_our_business_number_2.Text = OurCustomer.RegistrationNo;

        //        var ourCustomerName = cultureIndex == 1 ? OurCustomer.CustomerName : (cultureIndex == 2 ? OurCustomer.CustomerNameENG : OurCustomer.CustomerNameCHN);

        //        cell_our_customer_name_1.Text = ourCustomerName;
        //        cell_our_customer_name_2.Text = ourCustomerName;

        //        cell_our_represident_name_1.Text = OurCustomer.RepresentativeName;
        //        cell_our_represident_name_2.Text = OurCustomer.RepresentativeName;

        //        cell_our_address_1.Text = OurCustomer.Address + OurCustomer.Address2;
        //        cell_our_address_2.Text = OurCustomer.Address + OurCustomer.Address2;

        //        //var categoryTypeObj = CategoryTypeList.Where(p => p.Mcode == OurCustomer.CustomerCategoryType).FirstOrDefault();
        //        cell_our_category_type_1.Text = OurCustomer.CustomerCategoryType; //categoryTypeObj == null ? string.Empty : categoryTypeObj.Codename;
        //        cell_our_category_type_2.Text = OurCustomer.CustomerCategoryType; //categoryTypeObj == null ? string.Empty : categoryTypeObj.Codename;

        //        //var categoryCodeObj = CategoryCodeList.Where(p => p.Mcode == OurCustomer.CustomerCategoryCode).FirstOrDefault();
        //        cell_our_category_code_1.Text = OurCustomer.CustomerCategoryCode; //categoryCodeObj == null ? string.Empty : categoryCodeObj.Codename;
        //        cell_our_category_code_2.Text = OurCustomer.CustomerCategoryCode; //categoryCodeObj == null ? string.Empty : categoryCodeObj.Codename;

        //        cell_our_telephone_1.Text = OurCustomer.PhoneNumber;
        //        cell_our_telephone_2.Text = OurCustomer.PhoneNumber;

        //        cell_our_fax_1.Text = OurCustomer.FaxNumber;
        //        cell_our_fax_2.Text = OurCustomer.FaxNumber;
        //    }

        //    if (InCustomer != null)
        //    {
        //        var inCustomerName = cultureIndex == 1 ? InCustomer.CustomerName : (cultureIndex == 2 ? InCustomer.CustomerNameENG : InCustomer.CustomerNameCHN);

        //        cell_to_customer_name_1.Text = inCustomerName;
        //        cell_to_customer_name_2.Text = inCustomerName;
        //    }

        //    for(int i = 1; i <= printList.Count; i++)
        //    {
        //        var index = i - 1;
        //        ((XRTableCell)FindControl(string.Format("cell_m_{0}", i), true)).Text = printList[index].Date;
        //        ((XRTableCell)FindControl(string.Format("c_cell_m_{0}", i), true)).Text = printList[index].Date;

        //        ((XRTableCell)FindControl(string.Format("cell_ic_{0}", i), true)).Text = printList[index].ItemCode;
        //        ((XRTableCell)FindControl(string.Format("c_cell_ic_{0}", i), true)).Text = printList[index].ItemCode;

        //        ((XRTableCell)FindControl(string.Format("cell_in_{0}", i), true)).Text = printList[index].ItemName;
        //        ((XRTableCell)FindControl(string.Format("c_cell_in_{0}", i), true)).Text = printList[index].ItemName;

        //        ((XRTableCell)FindControl(string.Format("cell_spec_{0}", i), true)).Text = printList[index].Spec;
        //        ((XRTableCell)FindControl(string.Format("c_cell_spec_{0}", i), true)).Text = printList[index].Spec;

        //        ((XRTableCell)FindControl(string.Format("cell_qty_{0}", i), true)).Text = printList[index].Qty.ToString("N0");
        //        ((XRTableCell)FindControl(string.Format("c_cell_qty_{0}", i), true)).Text = printList[index].Qty.ToString("N0");

        //        ((XRTableCell)FindControl(string.Format("cell_cost_{0}", i), true)).Text = printList[index].Cost.ToString("#,###,###,###.##");
        //        ((XRTableCell)FindControl(string.Format("c_cell_cost_{0}", i), true)).Text = printList[index].Cost.ToString("#,###,###,###.##");

        //        ((XRTableCell)FindControl(string.Format("cell_supply_{0}", i), true)).Text = printList[index].SupplyCost.ToString("#,###,###,###.##");
        //        ((XRTableCell)FindControl(string.Format("c_cell_supply_{0}", i), true)).Text = printList[index].SupplyCost.ToString("#,###,###,###.##");

        //        ((XRTableCell)FindControl(string.Format("cell_tax_{0}", i), true)).Text = printList[index].TaxCost.ToString("#,###,###,###.##");
        //        ((XRTableCell)FindControl(string.Format("c_cell_tax_{0}", i), true)).Text = printList[index].TaxCost.ToString("#,###,###,###.##");
        //    }

        //    cell_supply_total_1.Text = printList.Sum(p => p.SupplyCost).ToString("#,###,###,###.##");
        //    cell_supply_total_2.Text = printList.Sum(p => p.SupplyCost).ToString("#,###,###,###.##");

        //    cell_tax_total_1.Text = printList.Sum(p => p.TaxCost).ToString("#,###,###,###.##");
        //    cell_tax_total_2.Text = printList.Sum(p => p.TaxCost).ToString("#,###,###,###.##");

        //    cell_total_amt_1.Text = (printList.Sum(p => p.SupplyCost) + printList.Sum(p => p.TaxCost)).ToString("#,###,###,###.##");
        //    cell_total_amt_2.Text = (printList.Sum(p => p.SupplyCost) + printList.Sum(p => p.TaxCost)).ToString("#,###,###,###.##");
        //}
    }
}
