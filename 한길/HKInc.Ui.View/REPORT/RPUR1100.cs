using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Class;
using HKInc.Service.Service;
using HKInc.Service.Factory;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraPrinting;

namespace HKInc.Ui.View.REPORT
{
    public partial class RPUR1100 : DevExpress.XtraReports.UI.XtraReport
    {
        private List<TN_STD1000> UnitList;
        public RPUR1100()
        {
            InitializeComponent();
        }

        public RPUR1100(TN_PUR1100 masterObj, string PoUserName, TN_STD1400 PoCustomer, TN_STD1400 InCustomer) : this()
        {
            bindingSource1.DataSource = masterObj;
            UnitList = DbRequesHandler.GetCommCode(MasterCodeSTR.Unit);

            cell_PoId.Text = PoUserName;

            if (PoCustomer != null)
            {
                cell_PoCustomerName.Text = PoCustomer.CustomerName;
                cell_PoAddress.Text = PoCustomer.Address + PoCustomer.Address2;
                cell_PoPhoneNumber.Text = PoCustomer.Telephone;
                cell_PoFax.Text = PoCustomer.Fax;
            }

            if (InCustomer != null)
            {
                cell_InCustomerName.Text = InCustomer.CustomerName;
                cell_InAddress.Text = InCustomer.Address + PoCustomer.Address2;
                cell_InPhoneNumber.Text = InCustomer.Telephone;
                cell_InFax.Text = InCustomer.Fax;
            }
        }

        // 단위 명 출력
        private void xrTableCell21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrTableCell21.Text.Trim().IsNullOrEmpty())
            {
                var obj = UnitList.Where(p => p.Mcode == xrTableCell21.Text).FirstOrDefault();
                xrTableCell21.Text = obj == null ? string.Empty : obj.Codename;
            }
        }

        // 번호 -1 empty 변환
        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!xrTableCell18.Text.Trim().IsNullOrEmpty())
            {
                if (xrTableCell18.Text == "-1")
                    xrTableCell18.Text = string.Empty;
            }
        }
    }
}
