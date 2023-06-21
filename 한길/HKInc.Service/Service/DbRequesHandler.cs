using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;
using System.Data;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;

namespace HKInc.Service.Service

{
   public class DbRequesHandler
    {
        public static string GetRequestNumber(string preFix)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetRequestNumber(preFix);
            }
            catch
            {
                returnValue = preFix;
            }

            return returnValue;
        }
        //public static string GetRequestNumber2(string preFix)
        //{
        //    string returnValue = string.Empty;
        //    try
        //    {
        //        ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
        //        returnValue = db.GetRequestNumber2(preFix);
        //    }
        //    catch
        //    {
        //        returnValue = preFix;
        //    }

        //    return returnValue;
        //}
        ///// <summary>
        ///// preFix + YYYYMMdd + No
        ///// </summary>
        ///// <param name="preFix"></param>
        ///// <returns></returns>
        //public static string GetRequestNumber3(string preFix)
        //{
        //    string returnValue = string.Empty;
        //    try
        //    {
        //        ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
        //        returnValue = db.GetRequestNumber3(preFix);
        //    }
        //    catch
        //    {
        //        returnValue = preFix;
        //    }

        //    return returnValue;
        //}

        public static string GetRequestNumberNew(string preFix)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetRequestNumberNew(preFix);
            }
            catch
            {
                returnValue = preFix;
            }

            return returnValue;
        }

        public static int GetPackLotNoMaxSeq(string preFix)
        {
            int returnValue = -1;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetPackLotNoMaxSeq(preFix);
            }
            catch
            {
                returnValue = -1;
            }

            return returnValue;
        }
        //public static string GetRequestNumberPr(string preFix)
        //{
        //    string returnValue = string.Empty;
        //    try
        //    {
        //        ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
        //        returnValue = db.GetRequestNumberPr(preFix);
        //    }
        //    catch
        //    {
        //        returnValue = preFix;
        //    }

        //    return returnValue;
        //}
        //public static string GetRequestLaserMarkingSeq(string LotNo)
        //{
        //    string returnValue = string.Empty;
        //    try
        //    {
        //        ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
        //        returnValue = db.GetRequestLaserMarkingSeq(LotNo);
        //    }
        //    catch
        //    {
        //        returnValue = string.Empty;
        //    }

        //    return returnValue;
        //}
        //public static string GetRequestWashDocNoSeq()
        //{
        //    string returnValue = string.Empty;
        //    try
        //    {
        //        ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
        //        returnValue = db.GetRequestWashDocNoSeq();
        //    }
        //    catch
        //    {
        //        returnValue = string.Empty;
        //    }

        //    return returnValue;
        //}
        //public static string GetRequestWashBatchNoSeq()
        //{
        //    string returnValue = string.Empty;
        //    try
        //    {
        //        ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
        //        returnValue = db.GetRequestWashBatchNoSeq();
        //    }
        //    catch
        //    {
        //        returnValue = string.Empty;
        //    }

        //    return returnValue;
        //}

        public static string GetDepartmentCodeSeq()
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetDepartmentCodeSeq();
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }
        public static DataSet GetDataSet(string procname, System.Data.SqlClient.SqlParameter[] parameters)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                if (conn == null)
                {
                    throw new InvalidCastException("SqlConnection is invalid for this database");
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procname;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds, "list");
                if (ds.Tables.Count > 1)
                {
                    int tcnt = 1;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = "list_" + (k + 1).ToString();
                        tcnt++;
                    }
                }

                return ds;
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public static DataTable GetDTselect(string lssql)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                if (conn == null)
                {
                    throw new InvalidCastException("SqlConnection is invalid for this database");
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = lssql;
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds, "list");
                if (ds.Tables.Count > 1)
                {
                    int tcnt = 1;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = "list_" + (k + 1).ToString();
                        tcnt++;
                    }
                }

                return ds.Tables[0];
            }
            catch (Exception err)
            {
                return null;
            }
        }
        public static string GetCellValue(string lssql, int idx)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                if (conn == null)
                {
                    throw new InvalidCastException("SqlConnection is invalid for this database");
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = lssql;
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds, "list");
                if (ds.Tables.Count > 1)
                {
                    int tcnt = 1;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = "list_" + (k + 1).ToString();
                        tcnt++;
                    }
                }

                return ds.Tables[0].Rows[0][idx].ToString();
            }
            catch (Exception err)
            {
                return null;
            }
        }
       public static int GetRowCount(string lssql)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                if (conn == null)
                {
                    throw new InvalidCastException("SqlConnection is invalid for this database");
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = lssql;
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds, "list");
                if (ds.Tables.Count > 1)
                {
                    int tcnt = 1;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = "list_" + (k + 1).ToString();
                        tcnt++;
                    }
                }

                return  Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception err)
            {
                return 0;
            }


        }
        public static int SetDataQury(string lssql)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                if (conn == null)
                {
                    throw new InvalidCastException("SqlConnection is invalid for this database");
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = lssql;
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds, "list");
                if (ds.Tables.Count > 1)
                {
                    int tcnt = 1;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = "list_" + (k + 1).ToString();
                        tcnt++;
                    }
                }

                return 1;
            }
            catch (Exception err)
            {
                return 0;
            }


        }
        public static DataSet GetDataQury(string lssql)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                if (conn == null)
                {
                    throw new InvalidCastException("SqlConnection is invalid for this database");
                }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = lssql;       
                conn.Open();
                da.SelectCommand = cmd;
                da.Fill(ds, "list");
                if (ds.Tables.Count > 1)
                {
                    int tcnt = 1;
                    for (int k = 0; k < ds.Tables.Count; k++)
                    {
                        ds.Tables[k].TableName = "list_" + (k + 1).ToString();
                        tcnt++;
                    }
                }

                return ds;
            }
            catch (Exception err)
            {
                return null;
            }


        }
        public static List<TN_STD1000> GetCommCode(string codemain, string codemid, string codesub, string codeval)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();

            if (codemid == "" && codesub == "" && codeval == "")
            {
                rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) 
                                                  && (string.IsNullOrEmpty(codemid) ? p.Codemid != "00" : p.Codemid == codemid)
                                                  && (p.Codesub == "00") 
                                                  && (p.Codeval == "00")
                                                  && (p.Useyn != "N")
                                               )
                                               .OrderBy(p => (p.displayorder))
                                               .ToList();
            }
            else if (codesub == "" && codeval == "")
            {
                rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) 
                                                  && (p.Codemid == codemid) 
                                                  && (string.IsNullOrEmpty(codesub) ? p.Codesub != "00" : p.Codesub == codesub) 
                                                  && (p.Codeval == "00")
                                                  && (p.Useyn != "N")
                                               )
                                               .OrderBy(p => (p.displayorder))
                                               .ToList();
            }
            else if (codeval == "")
            {
                rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) 
                                                  && (p.Codemid == codemid)
                                                  && (p.Codesub == codesub) 
                                                  && (p.Codeval != "00")
                                                  && (p.Useyn != "N")
                                               )
                                               .OrderBy(p => (p.displayorder))
                                               .ToList();
            }
            return rtnvalue;
        }

        /// <summary>
        /// 대분류
        /// </summary>
        /// <param name="codemain"></param>
        /// <returns></returns>
        public static List<TN_STD1000> GetCommCode(string codemain)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) 
                                              && (p.Useyn != "N") 
                                              && (p.Codemid != "00")
                                           )
                                           .OrderBy(p => (p.displayorder))
                                           .ToList();
            return rtnvalue;
        }

        /// <summary>
        /// 중분류
        /// </summary>
        /// <param name="codemain"></param>
        /// <returns></returns>
        public static List<TN_STD1000> GetCommCodeVal(string codemain)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) 
                                              && (p.Useyn != "N") 
                                              && (p.Codesub != "00")
                                           )
                                           .OrderBy(p => (p.displayorder))
                                           .ToList();
            return rtnvalue;
        }

        /// <summary>
        /// 공통코드
        /// </summary>
        /// <param name="codemain">메인코드</param>
        /// <param name="codeval">고유코드</param>
        /// <param name="lavel">대분류:1 중분류:2 소분류:3</param>
        /// <returns></returns>
        public static List<TN_STD1000> GetCommCode(string codemain, string codeval, int lavel)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            TN_STD1000 val1 = ModelService.GetList(p => (p.Codemain == codemain) && (p.Mcode == codeval)).FirstOrDefault();
            switch (lavel)
            {
                case 1://대분류
                    rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) && (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Useyn == "Y")).OrderBy(o => o.displayorder).ToList();
                    break;
                case 2://중분류
                    if (val1 != null)
                        rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) && (p.Codemid == val1.Codemid) && (p.Codesub != "00") && (p.Codeval == "00") && (p.Useyn == "Y")).OrderBy(o => o.displayorder).ToList();
                    break;
                case 3://소분류
                    if (val1 != null)
                        rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) && (p.Codemid == val1.Codemid) && (p.Codesub == val1.Codesub) && (p.Codeval != "00") && (p.Useyn == "Y")).OrderBy(o => o.displayorder).ToList();
                    break;
            }
            return rtnvalue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procname"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<TN_STD1000> GetCommCode(string codemain, int lavel)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();

            switch (lavel)
            {
                case 1://대분류
                    rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) && (p.Codemid != "00") && (p.Codesub == "00") && (p.Codeval == "00") && (p.Useyn == "Y")).OrderBy(o=>o.displayorder).ToList();
                    break;
                case 2://중분류
                    rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) && (p.Codemid != "00") && (p.Codesub != "00") && (p.Codeval == "00") && (p.Useyn == "Y")).OrderBy(o => o.displayorder).ToList();
                    break;
                case 3://소분류
                    rtnvalue = ModelService.GetList(p => (p.Codemain == codemain) && (p.Codemid != "00") && (p.Codesub != "00") && (p.Codeval != "00") && (p.Useyn == "Y")).OrderBy(o => o.displayorder).ToList();
                    break;
            }
            return rtnvalue;
        }



    }
}
