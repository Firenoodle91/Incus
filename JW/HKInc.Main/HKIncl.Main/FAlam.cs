using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;

using HKInc.Ui.Model.Domain;
using HKInc.Service.Factory;
using HKInc.Service.Handler;
using HKInc.Service.Helper;
using HKInc.Utils.Interface.Handler;
using HKInc.Utils.Interface.Service;
using HKInc.Utils.Interface.Forms;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;

using HKInc.Service.Service;
using DevExpress.XtraGrid.Views.Grid;



namespace HKInc.Main
{
    public partial class FAlam : HKInc.Service.Base.ListFormTemplate
    {
        IService<TN_QCT1000> ModelService = (IService<TN_QCT1000>)ProductionFactory.GetDomainService("TN_QCT1000");
        public FAlam()
        {
            InitializeComponent();
            string sql = " SELECT[ST1]      ,[SM1]      ,[ST2]      ,[SM2] FROM[TN_ALAM001T]";
            cb_time1.Text = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 0);
            cb_mm1.Text = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 1);
            cb_time2.Text = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 2);
            cb_mm2.Text = HKInc.Service.Service.DbRequesHandler.GetCellValue(sql, 3);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = "update TN_ALAM001T  set [ST1]='" + cb_time1.Text.GetNullToEmpty() + "',[SM1]='" + cb_mm1.Text.GetNullToEmpty() + "',[ST2]='" + cb_time2.Text.GetNullToEmpty() + "',[SM2]='" + cb_mm2.Text.GetNullToEmpty() + "'";
            HKInc.Service.Service.DbRequesHandler.SetDataQury(sql);
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
