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
    public partial class XRPUR1700 : DevExpress.XtraReports.UI.XtraReport
    {
      
        public XRPUR1700()
        {
            InitializeComponent();
        }
        public XRPUR1700(string cust,string emp,DateTime? dt) : this()
        {
            xrLabel18.Text= DbRequesHandler.GetCellValue("SELECT       UserName FROM VI_USER where loginid='" + emp + "'",0);
            xrLabel24.Text = Convert.ToDateTime(dt).ToString("yyyyMMdd");
            DataSet ds = DbRequesHandler.GetDataQury("SELECT isnull([CUSTOMER_NAME],' ') cust      ,isnull([EMP_CODE],' ') emp      ,isnull([TELEPHONE],' ') tel,isnull([FAX],' ') fax FROM TN_STD1400T where CUSTOMER_CODE='" + cust + "'");
            xrLabel8.Text = ds.Tables[0].Rows[0][0].ToString();
            xrLabel9.Text = ds.Tables[0].Rows[0][1].ToString();
            xrLabel10.Text = ds.Tables[0].Rows[0][2].ToString();
            xrLabel11.Text = ds.Tables[0].Rows[0][3].ToString();
            // this.DataSource = ModelService.GetList(p => p.Pono == pono);
        }
    }
}
