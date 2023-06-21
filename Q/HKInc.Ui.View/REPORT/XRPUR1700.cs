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
using HKInc.Utils.Common;
using HKInc.Service.Handler;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 자재발주관리 및 외주발주관리 리포트
    /// </summary>
    public partial class XRPUR1700 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_STD1400> ModelService = (IService<TN_STD1400>)ProductionFactory.GetDomainService("TN_STD1400");
        public XRPUR1700()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 2022-03-28 김진우 생성
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="dt"></param>
        public XRPUR1700(string cust, DateTime? dt) : this()
        {
            xrLabel18.Text = GlobalVariable.UserName;
            xrLabel24.Text = Convert.ToDateTime(dt).ToString("yyyyMMdd");

            TN_STD1400 Data = ModelService.GetList(p => p.CustomerCode == cust).FirstOrDefault();
            if (Data != null)
            {
                xrLabel8.Text = Data.CustomerName;
                xrLabel9.Text = Data.EmpCode;
                xrLabel10.Text = Data.Telephone;
                xrLabel11.Text = Data.Fax;
            }
        }

        #region 이전소스
        //public XRPUR1700(string cust, string emp, DateTime? dt) : this()        // 추후 수정 PUR1100이랑 PUR1600에서 받아오는 값이 다름 EMP 값이 다름 그리고 다른 정보가 표시하는것도 없음
        //{
        //    xrLabel18.Text = DbRequestHandler.GetCellValue("SELECT UserName FROM VI_USER where loginid='" + emp + "'", 0);
        //    DataSet ds = DbRequestHandler.GetDataQury("SELECT isnull([CUSTOMER_NAME],' ') cust," +
        //                                                     "isnull([EMP_CODE],' ') emp," +
        //                                                     "isnull([TELEPHONE],' ') tel," +
        //                                                     "isnull([FAX],' ') fax " +
        //                                              "FROM   TN_STD1400T " +
        //                                              "WHERE  CUSTOMER_CODE = '" + cust + "'");
        //    xrLabel8.Text = ds.Tables[0].Rows[0][0].ToString();
        //    xrLabel9.Text = ds.Tables[0].Rows[0][1].ToString();
        //    xrLabel10.Text = ds.Tables[0].Rows[0][2].ToString();
        //    xrLabel11.Text = ds.Tables[0].Rows[0][3].ToString();
        //    //this.DataSource = ModelService.GetList(p => p.Pono == pono);
        //}
        #endregion
    }
}
