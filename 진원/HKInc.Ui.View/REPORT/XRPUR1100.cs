using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Helper;
using System.Linq;
using HKInc.Utils.Class;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
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

        public XRPUR1100(TN_PUR1100 masterObj, List<VI_POLISTPRT> prt) : this()
        {
            bindingSource1.DataSource = prt;
            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_ItemCode.BeforePrint += cell_ItemCode_BeforePrint;
            xrLabel5.Text = Utils.Common.GlobalVariable.COMPANY_NAME;
            if (masterObj != null)
            {
                //선진업체
                var OurCustomer = ModelService.GetChildList<TN_STD1400>(p => p.DefaultCompanyPlag == "Y").FirstOrDefault();
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
                    cell_OurTel.Text = "TEL:" + OurCustomer.Telephone;
                    cell_OurEmail.Text = "E-mail:" + OurCustomer.Email;
                    cell_OurFax.Text = "FAX:" + OurCustomer.Fax;
                    //  cell_OurHomepage.Text = OurCustomer.;
                }

                //업체명
                if (masterObj.TN_STD1400 == null)
                {
                    cell_PoCustomerName.Text = string.Empty;
                }
                else
                {

                    //국문
                    cell_PoCustomerName.Text = masterObj.TN_STD1400.CustomerName;

                }
                cell_PoNo.Text = masterObj.ReqNo; //주문번호
                cell_PoDate.Text = masterObj.ReqDate.ToString("yyyy-MM-dd"); //주문일자
                xrTableCell21.Text = masterObj.DueDate.ToString("yyyy-MM-dd"); //주문일자
                cell_Memo.Text = masterObj.Memo; //특기사항

                if (prt.Count() > 0)
                {
                    var sumPoQty = prt.Sum(p => p.Qty);
                    var sumPoCost = prt.Sum(p => p.Cost);

                    decimal sumAmt = 0;
                    foreach (var list in prt)
                    {
                        sumAmt += Convert.ToDecimal(list.Qty * list.Cost);
                    }

                    cell_SumPoQty.Text = Convert.ToDecimal(sumPoQty).ToString("#,#.##"); //중량 총 합계
                    cell_SumPoCost.Text = sumPoCost.GetDecimalNullToZero().ToString("#,#.##"); //단가 총 합계
                    cell_SumAmt.Text = sumAmt.GetDecimalNullToZero().ToString("#,#.##"); //금액 총 합계
                }

            }
        }

        /// <summary>
        /// 외주가공품 출력
        /// </summary>
        public XRPUR1100(TN_PUR2100 masterObj, List<TN_PUR2101> list) : this()
        {
            bindingSource2 = new System.Windows.Forms.BindingSource();
            this.bindingSource2.DataSource = typeof(TN_PUR12101_DATA);
            this.DataSource = bindingSource2;

            //cell_No.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PoSeq]") });
            //cell_ItemCode.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PoSeq]") });
            //xrTableCell16.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Spec]") });
            //xrTableCell17.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PoQty]") });
            //xrTableCell18.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PoCost]") });
            //xrTableCell19.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Price]") });
            //xrTableCell22.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] { new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Memo]") });

            AAAA(list);

            cell_No.BeforePrint += Cell_No_BeforePrint;
            cell_ItemCode.BeforePrint += cell_ItemCode_BeforePrint;
            xrLabel5.Text = Utils.Common.GlobalVariable.COMPANY_NAME;

            var OurCustomer = ModelService.GetChildList<TN_STD1400>(p => p.DefaultCompanyPlag == "Y").FirstOrDefault();
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
                cell_OurTel.Text = "TEL:" + OurCustomer.Telephone;
                cell_OurEmail.Text = "E-mail:" + OurCustomer.Email;
                cell_OurFax.Text = "FAX:" + OurCustomer.Fax;
                //  cell_OurHomepage.Text = OurCustomer.;
            }

            //업체명
            if (masterObj.CustomerCode == null)
            {
                cell_PoCustomerName.Text = string.Empty;
            }
            else
            {

                //국문
                //cell_PoCustomerName.Text = masterObj.TN_STD1400.CustomerName;
                var tN_STD1400 = ModelService.GetChildList<TN_STD1400>(x => x.CustomerCode == masterObj.CustomerCode).FirstOrDefault();
                if (tN_STD1400 != null)
                    cell_PoCustomerName.Text = tN_STD1400.CustomerName;

            }
            cell_PoNo.Text = masterObj.PoNo; //주문번호
            cell_PoDate.Text = masterObj.PoDate.ToString("yyyy-MM-dd"); //주문일자
            xrTableCell21.Text = masterObj.DueDate.ToString("yyyy-MM-dd"); //주문일자
            cell_Memo.Text = masterObj.Memo; //특기사항

            if (list.Count() > 0)
            {
                var sumPoQty = list.Sum(p => p.PoQty);
                var sumPoCost = list.Sum(p => p.PoCost);

                decimal sumAmt = 0;
                foreach (var s in list)
                {
                    sumAmt += Convert.ToDecimal(s.PoQty * s.PoCost);
                }

                cell_SumPoQty.Text = Convert.ToDecimal(sumPoQty).ToString("#,#.##"); //중량 총 합계
                cell_SumPoCost.Text = sumPoCost.GetDecimalNullToZero().ToString("#,#.##"); //단가 총 합계
                cell_SumAmt.Text = sumAmt.GetDecimalNullToZero().ToString("#,#.##"); //금액 총 합계
            }
        }

        /// <summary>
        /// 외주가공품 데이터 처리
        /// </summary>
        private void AAAA(List<TN_PUR2101> list)
        {
            List<TN_PUR12101_DATA> dataList = new List<TN_PUR12101_DATA>();

            foreach (var s in list)
            {
                TN_PUR12101_DATA newData = new TN_PUR12101_DATA();
                newData.Seq = s.PoSeq.GetIntNullToZero();
                newData.ItemName = string.Format("{0} [{1}]", s.TN_STD1100.ItemNm, s.TN_STD1100.ItemCode);
                newData.Spec = string.Format("{0} {1} {2} {3}", s.TN_STD1100.Spec1, s.TN_STD1100.Spec2, s.TN_STD1100.Spec3, s.TN_STD1100.Spec4);
                var unit = ModelService.GetChildList<TN_STD1000>(x => x.Codemain == MasterCodeSTR.Unit && x.Mcode == s.TN_STD1100.Unit).FirstOrDefault();
                if (unit != null)
                    newData.Unit = unit.Codename;
                newData.Qty = s.PoQty;
                newData.Cost = s.PoCost.GetDecimalNullToZero();
                newData.Price = s.PoQty * s.PoCost.GetDecimalNullToZero();
                newData.Memo2 = s.Memo;

                dataList.Add(newData);
            }

            bindingSource2.DataSource = dataList;            
        }

        private void Cell_No_BeforePrint(object sender, PrintEventArgs e)
        {
            if (cell_No.Text == "-1")
                cell_No.Text = string.Empty;
        }

        //품명/규격 출력
        private void cell_ItemCode_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //string itemCode = cell_ItemCode.Text;

            //if (!itemCode.Trim().IsNullOrEmpty())
            //{
            //    var itemObj = ModelService.GetList(p => p.ItemCode == itemCode).FirstOrDefault();
            //    if (itemObj == null)
            //    {
            //        cell_ItemCode.Text = string.Empty;
            //    }
            //    else
            //    {

            //            cell_ItemCode.Text = itemObj.ItemNm;

            //        if (!itemObj.Spec1.IsNullOrEmpty())
            //            cell_ItemCode.Text += " / " + itemObj.Spec1;
            //    }
            //}
        }
    }

    /// <summary>
    /// 외주가공품 데이터 처리
    /// </summary>
    public class TN_PUR12101_DATA
    {
        public int Seq { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public decimal Qty { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Memo2 { get; set; }
    }
}
