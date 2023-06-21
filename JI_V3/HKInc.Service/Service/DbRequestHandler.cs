using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Ui.Model.Context;
using HKInc.Utils.Common;
using System.Data;
using System.Data.SqlClient;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Utils.Interface.Service;
using HKInc.Service.Factory;

namespace HKInc.Service.Service

{
   public class DbRequestHandler
    {
        #region 채번

        /// <summary>
        /// 표준 채번 ex) ITEM-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public static string GetSeqStandard(string seqName)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetSeqStandard(seqName);
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// 연도별 채번 ex) ORD-2019-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public static string GetSeqYear(string seqName)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetSeqYear(seqName);
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// 월별 채번 ex) ORD-1901-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public static string GetSeqMonth(string seqName)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetSeqMonth(seqName);
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// 일별 채번 ex) ORD-190101-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public static string GetSeqDay(string seqName)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetSeqDay(seqName);
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// 이동표번호채번
        /// </summary>
        /// <param name="workNo">작업지시번호</param>
        /// <returns></returns>
        public static string GetItemMoveSeq(string workNo)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.GetItemMoveSeq(workNo);
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        public static decimal GetCustItemCost(string CustCode, string ItemCode, string Date, string DateType)
        {
            decimal returnValue = 0;

            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));

                if (DateType == "M")
                {
                    returnValue = db.GetCustItemCostMonth(CustCode, ItemCode, Date.Substring(0, 7));
                }
                else
                {
                    returnValue = db.GetCustItemCost(CustCode, ItemCode, Date.Substring(0, 10));
                }
                    
            }
            catch(Exception ex)
            {
                returnValue = 0;
            }

            return returnValue;
        }


        /// <summary>
        /// 자재입고LOT 채번 
        /// </summary>
        public static string USP_GET_SEQ_IN_LOT_NO(string seqName, string date)
        {
            string returnValue = string.Empty;
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                returnValue = db.USP_GET_SEQ_IN_LOT_NO(seqName, date);
            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }
        
        /// <summary>
        /// 외주발주 저장 시 작업지시상태 갱신
        /// </summary>
        public static void USP_UPD_PUR1400_JOBSTATES()
        {
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                db.USP_UPD_PUR1400_JOBSTATES();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 외주입고 저장 시 작업지시상태 갱신
        /// </summary>
        public static void USP_UPD_PUR1500_JOBSTATES()
        {
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                db.USP_UPD_PUR1500_JOBSTATES();
            }
            catch
            {

            }
        }

        #endregion

        public static DataSet GetDataSet(string procname, params SqlParameter[] parameters)
        {
            try
            {
                SqlDataAdapter da = null;
                DataSet ds = null;
                ds = new DataSet();
                da = new SqlDataAdapter();
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                System.Data.SqlClient.SqlConnection conn = db.Database.Connection as System.Data.SqlClient.SqlConnection;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn ?? throw new InvalidCastException("SqlConnection is invalid for this database");
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
                cmd.Parameters.Clear();
                return ds;

                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable GetDataTableSelect(string lssql)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception ex)
            {
                return 0;
            }


        }

        public static string SetDataQury2(string lssql)
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

                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
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
            catch (Exception)
            {
                return null;
            }


        }

        public static List<TN_STD1000> GetCommCode(string codemain, string codetop, string codemid, string codebottom)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();

            if (codetop == "" && codemid == "" && codebottom == "")
            {
                rtnvalue = ModelService.GetList(p => (p.CodeMain == codemain) 
                                                  && (string.IsNullOrEmpty(codetop) ? p.CodeTop != "00" : p.CodeTop == codetop)
                                                  && (p.CodeMid == "00") 
                                                  && (p.CodeBottom == "00")
                                                  && (p.UseYN != "N")
                                               )
                                               .OrderBy(p => (p.DisplayOrder))
                                               .ToList();
            }
            else if (codemid == "" && codebottom == "")
            {
                rtnvalue = ModelService.GetList(p => (p.CodeMain == codemain) 
                                                  && (p.CodeTop == codetop) 
                                                  && (string.IsNullOrEmpty(codemid) ? p.CodeMid != "00" : p.CodeMid == codemid) 
                                                  && (p.CodeBottom == "00")
                                                  && (p.UseYN != "N")
                                               )
                                               .OrderBy(p => (p.DisplayOrder))
                                               .ToList();
            }
            else if (codebottom == "")
            {
                rtnvalue = ModelService.GetList(p => (p.CodeMain == codemain) 
                                                  && (p.CodeTop == codetop)
                                                  && (p.CodeMid == codemid) 
                                                  && (p.CodeBottom != "00")
                                                  && (p.UseYN != "N")
                                               )
                                               .OrderBy(p => (p.DisplayOrder))
                                               .ToList();
            }
            return rtnvalue;
        }

        /// <summary>
        /// 메인목록
        /// </summary>
        public static List<TN_STD1000> GetCommMainCode(string codeMain)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain)
                                              && (p.UseYN == "Y")
                                              && (p.CodeTop == "00")
                                              && (p.CodeMid == "00")
                                              && (p.CodeBottom == "00")
                                           )
                                           .OrderBy(p => (p.DisplayOrder))
                                           .ToList();
            return rtnvalue;
        }

        /// <summary>
        /// 대분류
        /// </summary>
        public static List<TN_STD1000> GetCommTopCode(string codeMain)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain)
                                                  && (p.CodeTop != "00")
                                                  && (p.CodeMid == "00")
                                                  && (p.CodeBottom == "00")
                                                  && (p.UseYN != "N")
                                           )
                                           .OrderBy(p => (p.DisplayOrder))
                                           .ToList();
            return rtnvalue;
        }

        /// <summary>
        /// 중분류
        /// </summary>
        public static List<TN_STD1000> GetCommMidCode(string codeMain, string topCode)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain) 
                                              && (p.UseYN == "Y") 
                                              && (p.CodeTop == topCode)
                                              && (p.CodeMid != "00")
                                           )
                                           .OrderBy(p => (p.DisplayOrder))
                                           .ToList();
            return rtnvalue;
        }

        /// <summary>
        /// 소분류
        /// </summary>
        public static List<TN_STD1000> GetCommBottomCode(string codeMain, string topCode, string middleCode)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();
            rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain)
                                              && (p.UseYN == "Y")
                                              && (p.CodeTop == topCode)
                                              && (p.CodeMid == middleCode)
                                              && (p.CodeBottom != "00")
                                           )
                                           .OrderBy(p => (p.DisplayOrder))
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
        public static List<TN_STD1000> GetCommCode(string codeMain, string codeVal, int lavel)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();

            TN_STD1000 val1 = ModelService.GetList(p => (p.CodeMain == codeMain) && (p.CodeVal == codeVal)).FirstOrDefault();

            switch (lavel)
            {
                case 1://대분류
                    rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain) && (p.CodeTop != "00") && (p.CodeMid == "00") && (p.CodeBottom == "00") && (p.UseYN == "Y")).OrderBy(o => o.DisplayOrder).ToList();
                    break;
                case 2://중분류
                    if (val1 != null)
                        rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain) && (p.CodeTop == val1.CodeTop) && (p.CodeMid != "00") && (p.CodeBottom == "00") && (p.UseYN == "Y")).OrderBy(o => o.DisplayOrder).ToList();
                    break;
                case 3://소분류
                    if (val1 != null)
                        rtnvalue = ModelService.GetList(p => (p.CodeMain == codeMain) && (p.CodeTop == val1.CodeTop) && (p.CodeMid == val1.CodeMid) && (p.CodeBottom != "00") && (p.UseYN == "Y")).OrderBy(o => o.DisplayOrder).ToList();
                    break;
            }
            return rtnvalue;
        }
        /// <summary>
        /// 실적수정 시 ITEM_MOVE 테이블 누적수량 갱신
        /// </summary>
        public static void USP_UPD_XFMPS1900(string workNo)
        {
            try
            {
                ProductionContext db = new ProductionContext(ServerInfo.GetConnectString(GlobalVariable.ProductionDataBase));
                db.USP_UPD_XFMPS1900(workNo);
            }
            catch
            {

            }
        }


        /// <summary>
        /// 공통코드
        /// </summary>
        /// <param name="codemain">메인코드</param>
        /// <param name="lavel">대분류:1 중분류:2 소분류:3</param>
        /// <returns></returns>
        public static List<TN_STD1000> GetCommCode(string codemain, int lavel)
        {
            IService<TN_STD1000> ModelService = (IService<TN_STD1000>)ProductionFactory.GetDomainService("TN_STD1000");

            List<TN_STD1000> rtnvalue = new List<TN_STD1000>();

            switch (lavel)
            {
                case 1://대분류
                    rtnvalue = ModelService.GetList(p => (p.CodeMain == codemain) && (p.CodeTop != "00") && (p.CodeMid == "00") && (p.CodeBottom == "00") && (p.UseYN == "Y")).OrderBy(o=>o.DisplayOrder).ToList();
                    break;
                case 2://중분류
                    rtnvalue = ModelService.GetList(p => (p.CodeMain == codemain) && (p.CodeTop != "00") && (p.CodeMid != "00") && (p.CodeBottom == "00") && (p.UseYN == "Y")).OrderBy(o => o.DisplayOrder).ToList();
                    break;
                case 3://소분류
                    rtnvalue = ModelService.GetList(p => (p.CodeMain == codemain) && (p.CodeTop != "00") && (p.CodeMid != "00") && (p.CodeBottom != "00") && (p.UseYN == "Y")).OrderBy(o => o.DisplayOrder).ToList();
                    break;
            }
            return rtnvalue;
        }
        
    }
}
