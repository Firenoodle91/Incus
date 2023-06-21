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
//using HKInc.Ui.Model.Domain.VIEW;
//using HKInc.Ui.Model.Domain.TEMP;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 거래명세서 출력물
    /// </summary>
    public partial class XRORD1200_TRADING : DevExpress.XtraReports.UI.XtraReport
    {
        //private List<TN_STD1000> CategoryTypeList; //업태
        //private List<TN_STD1000> CategoryCodeList; //업종
        public XRORD1200_TRADING()
        {
            InitializeComponent();
        }

        public XRORD1200_TRADING(string printDate, TN_STD1400 OurCustomer, TN_STD1400 InCustomer, List<TEMP_TRADING_DETAIL> printList) : this()
        {
            cell_print_date_1.Text = printDate;
            cell_print_date_2.Text = printDate;

            //CategoryCodeList = DbRequesHandler.GetCommCode(MasterCodeSTR.BusinessCode);
            //CategoryTypeList = DbRequesHandler.GetCommCode(MasterCodeSTR.BusinessType);

            //var cultureIndex = DataConvert.GetCultureIndex();

            if (OurCustomer != null)
            {
                cell_our_business_number_1.Text = OurCustomer.RegistrationNo;
                cell_our_business_number_2.Text = OurCustomer.RegistrationNo;

                //var ourCustomerName = cultureIndex == 1 ? OurCustomer.CustomerName : (cultureIndex == 2 ? OurCustomer.CustomerNameENG : OurCustomer.CustomerNameCHN);
                var ourCustomerName = OurCustomer.CustomerName;

                cell_our_customer_name_1.Text = ourCustomerName;
                cell_our_customer_name_2.Text = ourCustomerName;

                cell_our_represident_name_1.Text = OurCustomer.RepresentativeName;
                cell_our_represident_name_2.Text = OurCustomer.RepresentativeName;

                cell_our_address_1.Text = OurCustomer.Address + OurCustomer.Address2;
                cell_our_address_2.Text = OurCustomer.Address + OurCustomer.Address2;

                //var categoryTypeObj = CategoryTypeList.Where(p => p.Mcode == OurCustomer.CustomerCategoryType).FirstOrDefault();
                cell_our_category_type_1.Text = OurCustomer.CustomerCategoryType; //categoryTypeObj == null ? string.Empty : categoryTypeObj.Codename;
                cell_our_category_type_2.Text = OurCustomer.CustomerCategoryType; //categoryTypeObj == null ? string.Empty : categoryTypeObj.Codename;

                //var categoryCodeObj = CategoryCodeList.Where(p => p.Mcode == OurCustomer.CustomerCategoryCode).FirstOrDefault();
                cell_our_category_code_1.Text = OurCustomer.CustomerCategoryCode; //categoryCodeObj == null ? string.Empty : categoryCodeObj.Codename;
                cell_our_category_code_2.Text = OurCustomer.CustomerCategoryCode; //categoryCodeObj == null ? string.Empty : categoryCodeObj.Codename;

                //cell_our_telephone_1.Text = OurCustomer.PhoneNumber;
                //cell_our_telephone_2.Text = OurCustomer.PhoneNumber;
                cell_our_telephone_1.Text = OurCustomer.Telephone;
                cell_our_telephone_2.Text = OurCustomer.Telephone;

                //cell_our_fax_1.Text = OurCustomer.FaxNumber;
                //cell_our_fax_2.Text = OurCustomer.FaxNumber;
                cell_our_fax_1.Text = OurCustomer.Fax;
                cell_our_fax_2.Text = OurCustomer.Fax;
            }

            if (InCustomer != null)
            {
                //var inCustomerName = cultureIndex == 1 ? InCustomer.CustomerName : (cultureIndex == 2 ? InCustomer.CustomerNameENG : InCustomer.CustomerNameCHN);

                var inCustomerName = InCustomer.CustomerName;

                cell_to_customer_name_1.Text = inCustomerName;
                cell_to_customer_name_2.Text = inCustomerName;
            }

            for(int i = 1; i <= printList.Count; i++)
            {
                var index = i - 1;
                ((XRTableCell)FindControl(string.Format("cell_m_{0}", i), true)).Text = printList[index].Date;
                ((XRTableCell)FindControl(string.Format("c_cell_m_{0}", i), true)).Text = printList[index].Date;

                ((XRTableCell)FindControl(string.Format("cell_ic_{0}", i), true)).Text = printList[index].ItemName;
                ((XRTableCell)FindControl(string.Format("c_cell_ic_{0}", i), true)).Text = printList[index].ItemName;

                //((XRTableCell)FindControl(string.Format("cell_in_{0}", i), true)).Text = printList[index].ItemName2;
                //((XRTableCell)FindControl(string.Format("c_cell_in_{0}", i), true)).Text = printList[index].ItemName2;

                ((XRTableCell)FindControl(string.Format("cell_spec_{0}", i), true)).Text = printList[index].Spec;
                ((XRTableCell)FindControl(string.Format("c_cell_spec_{0}", i), true)).Text = printList[index].Spec;

                ((XRTableCell)FindControl(string.Format("cell_qty_{0}", i), true)).Text = printList[index].Qty.ToString("N0");
                ((XRTableCell)FindControl(string.Format("c_cell_qty_{0}", i), true)).Text = printList[index].Qty.ToString("N0");

                ((XRTableCell)FindControl(string.Format("cell_cost_{0}", i), true)).Text = printList[index].Cost.ToString("#,###,###,###.##");
                ((XRTableCell)FindControl(string.Format("c_cell_cost_{0}", i), true)).Text = printList[index].Cost.ToString("#,###,###,###.##");

                ((XRTableCell)FindControl(string.Format("cell_supply_{0}", i), true)).Text = printList[index].SupplyCost.ToString("#,###,###,###.##");
                ((XRTableCell)FindControl(string.Format("c_cell_supply_{0}", i), true)).Text = printList[index].SupplyCost.ToString("#,###,###,###.##");

                ((XRTableCell)FindControl(string.Format("cell_tax_{0}", i), true)).Text = printList[index].TaxCost.ToString("#,###,###,###.##");
                ((XRTableCell)FindControl(string.Format("c_cell_tax_{0}", i), true)).Text = printList[index].TaxCost.ToString("#,###,###,###.##");
            }

            cell_supply_total_1.Text = printList.Sum(p => p.SupplyCost).ToString("#,###,###,###.##");
            cell_supply_total_2.Text = printList.Sum(p => p.SupplyCost).ToString("#,###,###,###.##");

            cell_tax_total_1.Text = printList.Sum(p => p.TaxCost).ToString("#,###,###,###.##");
            cell_tax_total_2.Text = printList.Sum(p => p.TaxCost).ToString("#,###,###,###.##");

            cell_total_amt_1.Text = (printList.Sum(p => p.SupplyCost) + printList.Sum(p => p.TaxCost)).ToString("#,###,###,###.##");
            cell_total_amt_2.Text = (printList.Sum(p => p.SupplyCost) + printList.Sum(p => p.TaxCost)).ToString("#,###,###,###.##");
        }
    }
}
