using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

using DevExpress.XtraEditors;

using HKInc.Utils.Common;
using HKInc.Utils.Class;

namespace HKInc.Service.SqlNotifier
{
    public class SqlNotifier
    {
        private readonly string connectString;
        private readonly string registQuery;

        private event EventHandler<SQLNotifierArgument> _newMessage;

        private int maxQueId = 0;  // Que Table에서 지난처리 이후의 신규를 읽어와서 처리 한다.

        public event EventHandler<SQLNotifierArgument> NewMessage
        {
            add { this._newMessage += value; }
            remove { this._newMessage -= value; }
        }

        public int MaxQueId { get { return maxQueId; } set { maxQueId = value; } }

        #region Constructor
        public SqlNotifier(HKInc.Utils.Enum.DatabaseCategory database, string sqlQuery)
        {
            this.connectString = ServerInfo.GetConnectString(database);
            this.registQuery = sqlQuery;

            SqlDependency.Start(this.connectString);
        }
        #endregion

        #region SqlCommand Creation and register for notification
        public DataTable RegisterDependency()
        {
            if (!CheckUserPermissions())
                XtraMessageBox.Show("An error has occurred when checking permissions");

            using (SqlConnection sqlConnection = new SqlConnection(this.connectString))
            {
                //select col1, col2 from dbo.tablename
                using (SqlCommand command = new SqlCommand(this.registQuery, sqlConnection))
                {
                    command.Notification = null;

                    //SqlDependency dependency = new SqlDependency(command, this.serviceName, 0);
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += this.dependency_OnChange;

                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();

                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader(CommandBehavior.CloseConnection));

                        this.maxQueId = (maxQueId == 0) ? (dt.Rows.Count > 0 ? dt.AsEnumerable().Max(p => p["QueId"].GetIntNullToZero()) : 0) : maxQueId;

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "SqlDependency.Register");
                        return null;
                    }
                }
            }
        }
        #endregion

        #region event Handler
        public virtual void OnNewMessage(SqlNotificationEventArgs notification)
        {
            if (this._newMessage != null)
            {                
                SQLNotifierArgument arg = new SQLNotifierArgument(notification, GetQueTable());
                this._newMessage(this, arg);
            }

        }

        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = sender as SqlDependency;

            // Notification은 제거후 다시 등록 한다.
            dependency.OnChange -= new OnChangeEventHandler(dependency_OnChange);

            this.OnNewMessage(e);
        }
        #endregion        

        #region Function
        private bool CheckUserPermissions()
        {
            try
            {
                SqlClientPermission permissions = new SqlClientPermission(PermissionState.Unrestricted);

                //if we cann Demand() it will throw an exception if the current user        
                //doesnt have the proper permissions        
                permissions.Demand();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private DataTable GetQueTable()
        {
            using (SqlConnection connection = new SqlConnection(this.connectString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_QUE0001_SEL01";
                    cmd.Parameters.Add(new SqlParameter("@MAX_QUE_ID", maxQueId));

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

                        //this.maxQueId = dt.Rows.Count > 0 ? dt.AsEnumerable().Max(p => p["QUE_ID"].GetIntNullToZero()) : this.maxQueId;

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "SqlDependency.QueTable");
                        return new DataTable();
                    }
                }
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            SqlDependency.Stop(this.connectString);
        }
        #endregion
    }

    #region SQLNotifier Arguement
    public class SQLNotifierArgument : EventArgs
    {
        public DataTable QueTable { get; private set; }
        public SqlNotificationEventArgs Notification { get; private set; }

        public SQLNotifierArgument(SqlNotificationEventArgs notification, DataTable queTable)
        {
            this.Notification = notification;
            this.QueTable = queTable;
        }
    }
    #endregion
}
