using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Enum;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using HKInc.Utils.Class;
using HKInc.Ui.View.PopupFactory;
using HKInc.Utils.Interface.Popup;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using HKInc.Service.Service;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 외주발주서 출력(변경)
    /// </summary>
    public partial class XRPUR1600 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_PUR1600> ModelService = (IService<TN_PUR1600>)ProductionFactory.GetDomainService("TN_PUR1600");

        public XRPUR1600()
        {
            InitializeComponent();
        }

        public XRPUR1600(TN_PUR1600 masterObj) : this()
        {
            TN_STD1400 customer = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.CustomerCode == masterObj.CustCode).FirstOrDefault();
            TN_STD1400 company = ModelService.GetChildList<TN_STD1400>(x => x.UseFlag == "Y" && x.DefaultCompanyPlag == "Y").FirstOrDefault();

            //업체명
            cell_PoCustomerName.Text = customer.CustomerName.GetNullToEmpty();
            //주문번호
            cell_PoNo.Text = masterObj.PoNo.GetNullToEmpty();
            //주문일자
            cell_PoDate.Text = masterObj.PoDate?.ToString("yyyy-MM-dd").GetNullToEmpty();
            //납품일자
            xrTableCell21.Text = masterObj.InDuedate?.ToString("yyyy-MM-dd").GetNullToEmpty();

            if (company != null)
            {
                cell_OurAddress.Text = string.Format("Address: {0}", company.Address.GetNullToEmpty());
                cell_OurEmail.Text = string.Format("Email: {0}", company.Email.GetNullToEmpty());
                cell_OurFax.Text = string.Format("Fax: {0}", company.Fax.GetNullToEmpty());
                cell_OurTel.Text = string.Format("Tel: {0}", company.Telephone.GetNullToEmpty());
                //cell_OurHomepage.Text = company.Address2.GetNullToEmpty();
                ////진원와이어텍
                //lbl_Company.Text = HKInc.Utils.Common.GlobalVariable.COMPANY_NAME;
            }



            var result = masterObj.PUR1700List as List<TN_PUR1700>;
            bindingSource1.DataSource = result;


            decimal sumAmt = 0;

            foreach (var s in masterObj.PUR1700List)
            {
                sumAmt += s.Amt;
            }

            var sumPoQty = masterObj.PUR1700List.Sum(x => x.PoQty);

            cell_SumPoQty.Text = sumPoQty?.ToString("#,#.##");
            cell_SumAmt.Text = sumAmt.GetDecimalNullToZero().ToString("#,#.##");
        }

        public XRPUR1600(string cust, string emp, DateTime? dt) : this()
        {
            //담당자
            //xrLabel18.Text = DbRequesHandler.GetCellValue("SELECT       UserName FROM VI_USER where loginid='" + emp + "'", 0);

            //납기일자
            //xrLabel24.Text = Convert.ToDateTime(dt).ToString("yyyyMMdd");
            string sql = "  select cust,emp,tel,fax from(  "
                       + " SELECT 1 seq,isnull([CUSTOMER_NAME],' ') cust      ,isnull([EMP_CODE],' ') emp      ,isnull([TELEPHONE],' ') tel,isnull([FAX],' ') fax FROM TN_STD1400T where CUSTOMER_CODE = '" + cust + "' "
                       + " union all "
                       + " SELECT 2 seq,isnull([CUSTOMER_NAME],' ') cust      ,isnull([EMP_CODE],' ') emp      ,isnull([TELEPHONE],' ') tel,isnull([FAX],' ') fax FROM TN_STD1400T where DEFAULT_COMPANY_PLAG = 'Y' "
                       + " ) aa order by seq ";

            DataSet ds = DbRequesHandler.GetDataQury(sql);
            try
            {
                //xrLabel8.Text = ds.Tables[0].Rows[0][0].ToString();
                //xrLabel9.Text = ds.Tables[0].Rows[0][1].ToString();
                //xrLabel10.Text = ds.Tables[0].Rows[0][2].ToString();
                //xrLabel11.Text = ds.Tables[0].Rows[0][3].ToString();

                //xrLabel19.Text = ds.Tables[0].Rows[1][0].ToString();
                //xrLabel18.Text = ds.Tables[0].Rows[1][1].ToString();
                //xrLabel17.Text = ds.Tables[0].Rows[1][2].ToString();
                //xrLabel16.Text = ds.Tables[0].Rows[1][3].ToString();
                //xrLabel28.Text = ds.Tables[0].Rows[1][0].ToString();
            }
            catch { }
            // this.DataSource = ModelService.GetList(p => p.Pono == pono);

            //업체명
            cell_PoCustomerName.Text = "";
            //주문번호
            cell_PoNo.Text = "";
            //주문일자
            cell_PoDate.Text = "";
            //납품일자
            xrTableCell21.Text = "";
        }
    }
}
