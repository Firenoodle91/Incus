using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;
using System.Linq;
using HKInc.Utils.Class;

namespace HKInc.Ui.View.View.REPORT
{
    public partial class XRQCT1004 : DevExpress.XtraReports.UI.XtraReport
    {
        IService<TN_QCT1100> ModelService = (IService<TN_QCT1100>)ProductionFactory.GetDomainService("TN_QCT1100");

        public XRQCT1004()
        {
            InitializeComponent();
        }

        public XRQCT1004(TN_QCT1100 obj) : this()
        {
            bindingSource1.DataSource = obj;

            cell_CustName.Text = ModelService.GetChildList<TN_STD1400>(p => p.CustomerCode == obj.CustomerCode).FirstOrDefault().CustomerName;
            cell_ItemName.Text = obj.TN_STD1100.ItemName;
            cell_OkQty.Text = obj.OkQty.GetNullToEmpty();
            cell_NgQty.Text = obj.NgQty.GetNullToEmpty();
        }
    }
}
