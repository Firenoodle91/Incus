using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Collections.Generic;
using HKInc.Service.Service;
using HKInc.Service.Helper;
using HKInc.Utils.Class;
using System.Linq;
using System.Data;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 업체발주서
    /// 2023-01-10 김진우
    /// </summary>
    /// 
    public partial class XRPUR1100_V2 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<VI_PO_REPORT> ModelService = (IService<VI_PO_REPORT>)ProductionFactory.GetDomainService("VI_PO_REPORT");

        public XRPUR1100_V2()
        {
            InitializeComponent();
        }

        public XRPUR1100_V2(TN_PUR1400 obj) : this()
        {
            Po_Report(obj.PoNo, obj.PoId, obj.PoCustomerCode);
        }

        public XRPUR1100_V2(TN_PUR1100 obj) : this()
        {
            Po_Report(obj.PoNo, obj.PoId, obj.PoCustomerCode);
        }

        private void Po_Report(string PoNo, string ID, string Customer)
        {
            bindingSource1.DataSource = ModelService.GetList(p => p.PoNo == PoNo).ToList();

            #region 작업자
            User Worker = ModelService.GetChildList<User>(p => p.LoginId == ID).FirstOrDefault();

            string WorkerName = Worker.UserName;
            string Email = Worker.Email;
            string Phone = Worker.CellPhone;

            lblCo.Text += "담당 : " + WorkerName + "\n" +
                          "Tel : " + Phone + "\n" +
                          "E_mail : " + Email;
            #endregion

            #region 고객사
            TN_STD1400 STD1400 = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == Customer).FirstOrDefault();

            Cell_CoName.Text = STD1400.CustomerName;
            Cell_Phone.Text = STD1400.PhoneNumber;
            Cell_Fax.Text = STD1400.FaxNumber;
            Cell_PoWorker.Text = Worker.UserName;
            #endregion
        }
    }
}
