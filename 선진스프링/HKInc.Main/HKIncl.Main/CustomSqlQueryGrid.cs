using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Security.Principal;

using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using HKInc.Service.Factory;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Helper;
using HKInc.Utils.Interface.Handler;
using System.Data.SqlClient;
using HKInc.Utils.Common;
using HKInc.Service.Handler;

namespace HKInc.Main
{
    public partial class CustomSqlQueryGrid : DevExpress.XtraEditors.XtraForm
    {
        public DataSet ds;

        public CustomSqlQueryGrid()
        {
            InitializeComponent();

            gridEx1.SetToolbarVisible(false);
            this.Load += CustomSqlQueryGrid_Load;
        }

        private void CustomSqlQueryGrid_Load(object sender, EventArgs e)
        {
            if (ds != null)
            {
                gridEx1.DataSource = ds.Tables[0];
                gridEx1.BestFitColumns();
            }
        }
    }
}