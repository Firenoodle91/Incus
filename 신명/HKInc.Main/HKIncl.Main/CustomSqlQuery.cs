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
using HKInc.Service.Service;

namespace HKInc.Main
{
    public partial class CustomSqlQuery : DevExpress.XtraEditors.XtraForm
    {
        public CustomSqlQuery()
        {
            InitializeComponent();

            btn_Commit.Click += Btn_Commit_Click;
            btn_Cancel.Click += Btn_Cancel_Click;
        }

        private void Btn_Commit_Click(object sender, EventArgs e)
        {
            var sql = memo_SQL.EditValue.GetNullToEmpty().Trim();
            if (sql.Substring(0, 6).ToUpper().Contains("SELECT"))
            {
                var dataSet = DbRequestHandler.GetDataQury(sql);
                if (dataSet == null || dataSet.Tables.Count == 0)
                {
                    MessageBoxHandler.Show("The SQL statement is invalid.");
                }
                else
                {
                    var gridForm = new CustomSqlQueryGrid();
                    gridForm.ds = dataSet;
                    gridForm.Show();
                }
            }
            else if (sql.Substring(0, 6).ToUpper().Contains("INSERT") || sql.Substring(0, 6).ToUpper().Contains("UPDATE") || sql.Substring(0, 6).ToUpper().Contains("DELETE"))
            {
                var ex = DbRequestHandler.SetDataQury2(sql);
                if (ex != "1")
                {
                    MessageBoxHandler.ErrorShow(ex);
                }
                else
                {
                    MessageBoxHandler.Show("SQL Commit.");
                }
            }
            else
            {
                MessageBoxHandler.Show("The SQL statement is invalid.");
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}