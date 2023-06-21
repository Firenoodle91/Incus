using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

using HKInc.Ui.Model.Context;
using HKInc.Ui.Model.Domain;


using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Common;

namespace HKInc.Service.Helper
{
    class CodeSqlHelper : HKInc.Utils.Interface.Helper.ICodeSqlHelper
    {
        public DataTable GetCodeSqlDataTable(string sqlId, List<object> parameterList, bool isPopupForm = false,  DatabaseCategory database = DatabaseCategory.DefaultProduction)
        {            
            try
            {                
                DataParam sqlDataParam = GetSqlQuery(sqlId, database);  // CodeSql Table에서 읽어온다.
                string sqlQuery = sqlDataParam.GetValue("sqlQuery").GetNullToEmpty();
                string sqlParameters = sqlDataParam.GetValue("sqlParameters").GetNullToEmpty();
            CodeSql codeSqlObject = (CodeSql)sqlDataParam.GetValue("codeSqlObject"); //CodeSql object

                if (!string.IsNullOrEmpty(sqlQuery))
                {
                    DataParam paramData = GetParameters(sqlParameters, parameterList); // ,를 기준으로 각각의 Parameter 생성
                    if (paramData == null) paramData = new DataParam();
                    
                    using(SqlConnection connection = new SqlConnection(ServerInfo.GetConnectString(database)))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlQuery, connection);

                        if(paramData.Count > 0) // Parameter 가 있으면 SqlParameter를 생성한다.
                        {
                            SqlParameter[] dbParams = GetSqlParameters(paramData);  
                            command.Parameters.AddRange(dbParams);
                        }                        

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {                            
                            DataTable dataTableToReturn = new DataTable();
                            dataTableToReturn.Load(reader);

                            if(isPopupForm)
                            {
                                string[] gridDisplayField = codeSqlObject.DisplayField.Split(',');
                                if (gridDisplayField == null || gridDisplayField.Length == 0)                                
                                    return new DataTable();
                                
                                using (HKInc.Service.Forms.CodeHelpPopupForm form = new HKInc.Service.Forms.CodeHelpPopupForm(dataTableToReturn, gridDisplayField))
                                {
                                    form.Width = (int)codeSqlObject.FormWith;
                                    form.Height = (int)codeSqlObject.FormHeight;
                                    form.Text = codeSqlObject.FormTitle;
                                    form.SearchEnabled = codeSqlObject.FindText == "Y" ? true : false;
                                    form.KeyColumnName = codeSqlObject.KeyField;

                                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        return form.ReturnDataTable;
                                    else
                                        return new DataTable();
                                }
                            }
                            else
                            {
                                return dataTableToReturn;
                            }                                                            
                        }                            
                    }
                }
            }
            catch (Exception ex)
            { 
                HKInc.Service.Handler.MessageBoxHandler.ErrorShow(ex);
            }
            return new DataTable();            
        }

        private SqlParameter[] GetSqlParameters(DataParam paramData)
        {
            SqlParameter[] dbParams = new SqlParameter[paramData.Count];
            int paramIndex = 0;
            foreach (var pair in paramData)
            {
                if (pair.Value == null)
                    dbParams[paramIndex] = CreateSqlParameter("@" + pair.Key, DBNull.Value);
                else
                    dbParams[paramIndex] = CreateSqlParameter("@" + pair.Key, pair.Value);

                paramIndex++;
            }
            return dbParams;
        }

        private DataParam GetSqlQuery(string sqlId, DatabaseCategory database)
        {
            DataParam map = new DataParam();

            using (ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(database)))
            {
                DbSet<CodeSql> dbSetCodeSql = db.Set<CodeSql>();
                CodeSql codeSql = dbSetCodeSql.Where(p => p.SqlId == sqlId).FirstOrDefault();
                if (codeSql != null)
                {
                    map.SetValue("sqlParameters", codeSql.Parameter);
                    map.SetValue("sqlQuery", codeSql.Query);
                    map.SetValue("codeSqlObject", codeSql);
                }
                return map;
            }
        }
        private SqlParameter CreateSqlParameter(string fieldName, object value)
        {
            return new SqlParameter()
            {
                ParameterName = fieldName,
                Value = value
            };
        }

        private DataParam GetParameters(string sqlParams, List<object> reqParams)
        {
            
            if (String.IsNullOrEmpty(sqlParams))
            {
                return null;
            }

            string[] paramList = sqlParams.Split(',');
            if (paramList == null || paramList.Length == 0)
            {
                return null;
            }

            DataParam returnValue = new DataParam();
            for (int i = 0; i < paramList.Length; i++)
            {
                if (!String.IsNullOrEmpty(paramList[i].Trim()))
                {
                    if (reqParams != null && i < reqParams.Count)
                        returnValue.SetValue(paramList[i].Trim(), reqParams[i]);
                    else
                        returnValue.SetValue(paramList[i].Trim(), null);
                }
            }
            return returnValue;            
        }
    }
}
