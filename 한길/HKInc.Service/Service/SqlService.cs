using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HKInc.Utils.Common;

namespace HKInc.Service.Service
{
    public static class SqlService
    {
        private static string connectString = ServerInfo.GetConnectString(GlobalVariable.DefaultProductionDataBase);
        
        public static DataTable ExecuteStoredProcedure(string sp, Dictionary<string, object> sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "dbo." + sp;
                    //cmd.Parameters.Add(new SqlParameter("@MAX_QUE_ID", maxQueId));

                    if (sqlParameters != null)
                    {
                        foreach (KeyValuePair<string, object> sqlParameter in sqlParameters)
                        {
                            cmd.Parameters.Add(new SqlParameter("@" + sqlParameter.Key, sqlParameter.Value));
                        }
                    }

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    try
                    {
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("sp name : " + sp + Environment.NewLine + Environment.NewLine + ex.Message, "ExecuteStoredProcedure Error");
                        return new DataTable();
                    }
                }
            }
        }
    }
}
