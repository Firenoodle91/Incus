using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class SqlQuery
    {

        public static string Query1(string strtDt, string endDt)
        {
            string query = null;

            string st = strtDt + " 00:00:00";
            string et = endDt + " 23:59:59";

            query += "SELECT 'IN' AS Typ, UserId, LoginTime, IpAddress, PcName "; 
            query += "FROM LoginLog ";
            query += "WHERE LoginTime BETWEEN '" + st + "' AND '" + et + "' ";

            query += "UNION ALL ";

            query += "SELECT 'OUT' AS Typ, UserId, LogoutTime, IpAddress, PcName ";
            query += "FROM LoginLog ";
            query += "WHERE LogoutTime BETWEEN '" + st + "' AND '" + et + "' ";

            return query;
        }

        public static string Proc1(string workDt, string customer, int? okCnt = null, int? badCnt = null)
        {
            string procedure = null;

            procedure = string.Format("EXEC USP_SET_LOGAPI_HISTORY '{0}', '{1}', '{2}', '{3}'", workDt, customer, okCnt, badCnt);

            return procedure;
        }

        /// <summary>
        /// 일별 데이터 프로시저명
        /// </summary>
        /// <returns></returns>
        public static string Proc2()
        {
            return "USP_GET_LOGAPI_HISTORY";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource">Ip</param>
        /// <param name="applicationDatabase">DataBase</param>
        /// <param name="userId">userid</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public static string ConnectionString(string dataSource, string applicationDatabase, string userId, string password)
        {
            string server = null;

            server = string.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}", dataSource, applicationDatabase, userId, password);

            return server;
        }

        
    }
}
