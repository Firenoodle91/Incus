using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Service.Helper;
using HKInc.Service.Factory;
using HKInc.Utils.Interface.Service;

namespace HKInc.Ui.View.View.REPORT
{
    /// <summary>
    /// 입고위치 바코드 형식
    /// </summary>
    public partial class XRWMS2000 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRWMS2000()
        {
            InitializeComponent();
        }
        public XRWMS2000(TN_WMS2000 obj) : this()
        {
            var culture = DataConvert.GetCultureIndex();
            Tc_WhName.Text = culture == 1 ? obj.TN_WMS1000.WhName : (culture == 2 ? obj.TN_WMS1000.WhNameENG : obj.TN_WMS1000.WhNameCHN);
            Tc_PositionName.Text = obj.PositionName;
            
            IService<TN_STD1100> ModelService = (IService<TN_STD1100>)ProductionFactory.GetDomainService("TN_STD1100");
            var itemObj = ModelService.GetList(p => p.StockPosition == obj.PositionCode).FirstOrDefault();
            if (itemObj != null)
            {
                Tc_ItemCode.Text = itemObj.ItemCode;
                Tc_ItemName.Text = culture == 1 ? itemObj.ItemName : (culture == 2 ? itemObj.ItemNameENG : itemObj.ItemNameCHN);
            }
            else
            {
                Tc_ItemCode.Text = string.Empty;
                Tc_ItemName.Text = string.Empty;
            }
            bar_position.Text = obj.PositionCode.ToString();
        }

    }
}