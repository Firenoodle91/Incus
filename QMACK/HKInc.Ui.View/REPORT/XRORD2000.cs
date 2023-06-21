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

namespace HKInc.Ui.View.REPORT
{
    public partial class XRORD2000 : DevExpress.XtraReports.UI.XtraReport
    {
     
        public XRORD2000()
        {
            InitializeComponent();
        }
        public XRORD2000(string cust,string emp) : this()
        {

            xrLabel8.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
            DataSet ds = DbRequestHandler.GetDataQury("SELECT isnull([CUSTOMER_NAME],' ') cust      ,isnull([EMP_CODE],' ') emp      ,isnull([TELEPHONE],' ') tel,isnull([FAX],' ') fax FROM TN_STD1400T where CUSTOMER_CODE='" + cust + "'");
            xrLabel10.Text = ds.Tables[0].Rows[0][0].ToString();
            decimal amt = DbRequestHandler.GetCellValue("SELECT     sum(isnull([OUT_QTY], 0) * isnull([OUT_PRICE],0))   FROM [TN_ORD2001T] where OUTPRT_NO = '" + emp + "' group by OUTPRT_NO", 0).GetDecimalNullToZero();
            //xrLabel10.Text = ds.Tables[0].Rows[0][2].ToString();
            //xrLabel11.Text = ds.Tables[0].Rows[0][3].ToString();
            xrLabel21.Text = amt.ToString("#,##0");
            // this.DataSource = ModelService.GetList(p => p.Pono == pono);
        }
    }
}
