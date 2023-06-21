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
using HKInc.Ui.Model.Domain.TEMP;
using System.Collections.Generic;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 외주발주서 형식
    /// </summary>
    public partial class XRPUR1400 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");

        public XRPUR1400()
        {
            InitializeComponent();
            xrLabel5.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
            xrLabel4.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
        }

        public XRPUR1400(TN_PUR1400 masterObj, List<TEMP_PUR1400_REPORT> TEMP_PUR1400_REPORT_List) : this()
        {
            bindingSource1.DataSource = TEMP_PUR1400_REPORT_List;
            xrLabel5.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
            xrLabel4.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_ItemCode.BeforePrint += cell_ItemCode_BeforePrint;

            if (masterObj != null)
            {
                //선진업체
                var OurCustomer = ModelService.GetChildList<TN_STD1400>(p => p.MyCompanyFlag == "Y").FirstOrDefault();
                if (OurCustomer == null)
                {
                    cell_OurAddress.Text = "우)420-801 경기도 부천시 원미구 옥산로 254";
                    cell_OurTel.Text = "TEL:032-675-1321";
                    cell_OurEmail.Text = "E-mail:webmaster@sunjinspring.com";
                    cell_OurFax.Text = "FAX:032-675-1324";
                    cell_OurHomepage.Text = "http://sunjinspring.com";
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
                if (masterObj.PoCustomerCode == null)
                {
                    cell_PoCustomerName.Text = string.Empty;
                }
                else
                {
                    //선진업체
                    var PoCustomer = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == masterObj.PoCustomerCode).FirstOrDefault();
                    if (PoCustomer != null)
                    {
                        if (DataConvert.GetCultureIndex() == 1)
                        {
                            //국문
                            cell_PoCustomerName.Text = PoCustomer.CustomerName;
                        }
                        else if (DataConvert.GetCultureIndex() == 2)
                        {
                            //영문
                            cell_PoCustomerName.Text = PoCustomer.CustomerNameENG;
                        }
                        else if (DataConvert.GetCultureIndex() == 3)
                        {
                            //중문
                            cell_PoCustomerName.Text = PoCustomer.CustomerNameCHN;
                        }
                    }
                }
                cell_PoNo.Text = masterObj.PoNo; //주문번호
                cell_PoDate.Text = masterObj.PoDate.ToString("yyyy-MM-dd"); //주문일자
                cell_Memo.Text = masterObj.Memo; //특기사항

                //if(masterObj.TN_PUR1401List.Count > 0)
                //{
                //    var sumPoQty = masterObj.TN_PUR1401List.Sum(p => p.PoQty);
                //    var sumPoCost = masterObj.TN_PUR1401List.Sum(p => p.PoCost);

                //    decimal sumAmt = 0;
                //    foreach (var list in masterObj.TN_PUR1401List)
                //    {
                //        sumAmt += Convert.ToDecimal(list.PoQty * list.PoCost);
                //    }

                //    cell_SumPoQty.Text = sumPoQty.ToString("#,#.##"); //중량 총 합계
                //    cell_SumPoCost.Text = sumPoCost.GetDecimalNullToZero().ToString("#,#.##"); //단가 총 합계
                //    cell_SumAmt.Text = sumAmt.GetDecimalNullToZero().ToString("#,#.##"); //금액 총 합계
                //}

                if (TEMP_PUR1400_REPORT_List.Count > 0)
                {
                    var sumPoQty = TEMP_PUR1400_REPORT_List.Sum(p => p.PoQty);
                    var sumPoCost = TEMP_PUR1400_REPORT_List.Sum(p => p.PoCost);

                    decimal sumAmt = 0;
                    foreach (var list in TEMP_PUR1400_REPORT_List)
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
