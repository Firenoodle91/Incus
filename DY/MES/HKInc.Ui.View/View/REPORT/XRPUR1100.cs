using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Drawing.Printing;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 자재발주서 형식
    /// </summary>
    public partial class XRPUR1100 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRPUR1100()
        {
            InitializeComponent();
            xrLabel5.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
        }

        public XRPUR1100(TN_PUR1100 masterObj) : this()
        {
            bindingSource1.DataSource = masterObj;
            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_ItemCode.BeforePrint += cell_ItemCode_BeforePrint;
            xrLabel5.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
            if (masterObj != null)
            {
                //선진업체
                var OurCustomer = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();
                if (OurCustomer == null)
                {
                    cell_OurAddress.Text = "";
                    cell_OurTel.Text = "";
                    cell_OurEmail.Text = "";
                    cell_OurFax.Text = "";
                    cell_OurHomepage.Text = "";
                }
                else
                {
                    cell_OurAddress.Text = string.Format("우){0} {1}", OurCustomer.ZipCode, OurCustomer.Address + OurCustomer.Address2); 
                    cell_OurTel.Text = "TEL:" + OurCustomer.PhoneNumber;
                    cell_OurEmail.Text = "E-mail:" + OurCustomer.Email;
                    cell_OurFax.Text = "FAX:" + OurCustomer.FaxNumber;
                    cell_OurHomepage.Text = OurCustomer.Homepage;
                }

                //업체명
                if (masterObj.TN_STD1400 == null)
                {
                    cell_PoCustomerName.Text = string.Empty;
                }
                else
                {
                    if (DataConvert.GetCultureIndex() == 1)
                    {
                        //국문
                        cell_PoCustomerName.Text = masterObj.TN_STD1400.CustomerName;
                    }
                    else if (DataConvert.GetCultureIndex() == 2)
                    {
                        //영문
                        cell_PoCustomerName.Text = masterObj.TN_STD1400.CustomerNameENG;
                    }
                    else if (DataConvert.GetCultureIndex() == 3)
                    {
                        //중문
                        cell_PoCustomerName.Text = masterObj.TN_STD1400.CustomerNameCHN;
                    }
                }
                cell_PoNo.Text = masterObj.PoNo; //주문번호
                cell_PoDate.Text = masterObj.PoDate.ToString("yyyy-MM-dd"); //주문일자
                cell_Memo.Text = masterObj.Memo; //특기사항

                if(masterObj.TN_PUR1101List.Count > 0)
                {
                    var sumPoQty = masterObj.TN_PUR1101List.Sum(p => p.PoQty);
                    var sumPoCost = masterObj.TN_PUR1101List.Sum(p => p.PoCost);

                    decimal sumAmt = 0;
                    foreach (var list in masterObj.TN_PUR1101List)
                    {
                        sumAmt += Convert.ToDecimal(list.PoQty * list.PoCost);
                    }

                    cell_SumPoQty.Text = sumPoQty.ToString("#,#.##"); //중량 총 합계
                    cell_SumPoCost.Text = sumPoCost.GetDecimalNullToZero().ToString("#,#.##"); //단가 총 합계
                    cell_SumAmt.Text = sumAmt.GetDecimalNullToZero().ToString("#,#.##"); //금액 총 합계
                }

            }
        }

        private void Cell_No_BeforePrint(object sender, PrintEventArgs e)
        {
            if (cell_No.Text == "-1")
                cell_No.Text = string.Empty;
        }

        //품명/규격 출력
        private void cell_ItemCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string itemCode = cell_ItemCode.Text;

            if (!itemCode.Trim().IsNullOrEmpty())
            {
                var itemObj = ModelService.GetList(p => p.ItemCode == itemCode).FirstOrDefault();
                if (itemObj == null)
                {
                    cell_ItemCode.Text = string.Empty;
                }
                else
                {
                    if (DataConvert.GetCultureIndex() == 1)
                    {
                        //국문
                        cell_ItemCode.Text = itemObj.ItemName;
                    }
                    else if (DataConvert.GetCultureIndex() == 2)
                    {
                        //영문
                        cell_ItemCode.Text = itemObj.ItemNameENG;
                    }
                    else if (DataConvert.GetCultureIndex() == 3)
                    {
                        //중문
                        cell_ItemCode.Text = itemObj.ItemNameCHN;
                    }
                    if (!itemObj.CombineSpec.IsNullOrEmpty())
                        cell_ItemCode.Text += " / " + itemObj.CombineSpec;
                }
            }
        }
    }
}
