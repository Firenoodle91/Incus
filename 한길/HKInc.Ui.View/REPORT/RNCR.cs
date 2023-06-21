using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Common;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;
using System.Linq;
using HKInc.Utils.Class;
using System.Collections.Generic;

namespace HKInc.Ui.View.REPORT
{
    /// <summary>
    /// 부적합 출력물
    /// </summary>
    public partial class RNCR : DevExpress.XtraReports.UI.XtraReport
    {
        public RNCR()
        {
            InitializeComponent();
        }

        public RNCR(TN_QCT1500 obj, VI_QCT1500_LIST mObj) : this()
        {
            bindingSource1.DataSource = obj;

            IService<TN_QCT1500> ModelService = (IService<TN_QCT1500>)ProductionFactory.GetDomainService("TN_QCT1500");
            var TN_MPS1400 = ModelService.GetChildList<TN_MPS1400>(p => p.WorkNo == mObj.WorkNo && p.Process == mObj.ProcessCode).FirstOrDefault();
            var TN_ORD1000 = ModelService.GetChildList<TN_ORD1000>(p => p.OrderNo == TN_MPS1400.TN_MPS1300.OrderNo).FirstOrDefault();
            var ReceiptUser = ModelService.GetChildList<UserView>(p => p.LoginId == obj.Fuser).FirstOrDefault();

            cellCustomerName.Text = TN_ORD1000 == null ? string.Empty : TN_ORD1000.TN_STD1400.CustomerName;
            cellItemName.Text = mObj.TN_STD1100.ItemNm;
            cellReceiptId.Text = ReceiptUser == null ? string.Empty : ReceiptUser.UserName;
            cellBadQty.Text = mObj.FailQty.GetIntNullToZero().ToString("n0");

            if (obj.ReceiptWay == "01") //유선
            {
                cellReceiptWay.Text = "☑ 유선   □ FAX   □ 현물   □ 공문서   □ 휴대폰";
            }
            else if (obj.ReceiptWay == "02") //FAX
            {
                cellReceiptWay.Text = "□ 유선   ☑ FAX   □ 현물   □ 공문서   □ 휴대폰";
            }
            else if (obj.ReceiptWay == "03") //현물
            {
                cellReceiptWay.Text = "□ 유선   □ FAX   ☑ 현물   □ 공문서   □ 휴대폰";
            }
            else if (obj.ReceiptWay == "04") //공문서
            {
                cellReceiptWay.Text = "□ 유선   □ FAX   □ 현물   ☑ 공문서   □ 휴대폰";
            }
            else if (obj.ReceiptWay == "05") //휴대폰
            {
                cellReceiptWay.Text = "□ 유선   □ FAX   □ 현물   □ 공문서   ☑ 휴대폰";
            }
            else
            {
                cellReceiptWay.Text = "□ 유선   □ FAX   □ 현물   □ 공문서   □ 휴대폰";
            }
        }
    }
}
