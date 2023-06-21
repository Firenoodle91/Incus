using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.Model.Context
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(string connectString) : base(connectString) { }
        //public virtual DbSet<Domain.Bad> Bad { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.UserView> UserView { get; set; }
        //public virtual int SendSqlNotification(string entityName)
        //{
        //    var entityNameParameter = new SqlParameter { ParameterName = "@QUE_TABLE", Value = entityName };
        //    return Database.ExecuteSqlCommand("HKInc.dbo.USP_QUE0001_CUD01 @QUE_TABLE", entityNameParameter);
        //}



        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MTTF1000> TN_MTTF1000 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1000> TN_STD1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1100> TN_STD1100 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1101> TN_STD1101 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_OUT1101> TN_OUT1101 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1200> TN_STD1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1400> TN_STD1400 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1500> TN_STD1500 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1600> TN_STD1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD4M001> TN_STD4M001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_STD1800> TN_STD1800 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1000> TN_ORD1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1002> TN_ORD1002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_ORD1100> VI_ORD1100 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1100> TN_ORD1100 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1200> TN_ORD1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1201> TN_ORD1201 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1600> TN_ORD1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1601> TN_ORD1601 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1700> TN_ORD1700 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD1701> TN_ORD1701 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1000> TN_MPS1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1001> TN_MPS1001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1300> TN_MPS1300 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1400> TN_MPS1400 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1401> TN_MPS1401 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1402> TN_MPS1402 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1404> TN_MPS1404 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1600> TN_MPS1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1700> TN_MPS1700 { get; set; }
        
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPSPLAN_LIST> VI_MPSPLAN_LIST { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1100> TN_PUR1100 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1200> TN_PUR1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1300> TN_PUR1300 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1301> TN_PUR1301 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1500> TN_PUR1500 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1501> TN_PUR1501 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1502> TN_PUR1502 { get; set; }        
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1600> TN_PUR1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1700> TN_PUR1700 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1800> TN_PUR1800 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1801> TN_PUR1801 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR2000> TN_PUR2000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR2001> TN_PUR2001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR1600_LIST> VI_PUR1600_LIST { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURSTOCK> VI_PURSTOCK { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURINOUT> VI_PURINOUT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURSTOCK_LOT> VI_PURSTOCK_LOT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_POLISTPRT> VI_POLISTPRT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR1900V1> VI_PUR1900V1 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR1900V2> VI_PUR1900V2 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1000> TN_MEA1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1100> TN_MEA1100 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1200> TN_MEA1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1300> TN_MEA1300 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MOLD001> TN_MOLD001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MOLD002> TN_MOLD002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MOLD003> TN_MOLD003 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_KNIFE001> TN_KNIFE001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_KNIFE002> TN_KNIFE002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_KNIFE003> TN_KNIFE003 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_INDOO001> TN_INDOO001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_INDOO002> TN_INDOO002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_SRC_STOCK_SUM> VI_SRC_STOCK_SUM { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_SRC_USE> VI_SRC_USE { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT2200> TN_QCT2200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1000> TN_QCT1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1001> TN_QCT1001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1200> TN_QCT1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1201> TN_QCT1201 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1500> TN_QCT1500 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1700> TN_QCT1700 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_QCT1500_LIST> VI_QCT1500_LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_INSPLIST> VI_INSPLIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_LOTTRACKING> VI_LOTTRACKING { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPS1401LIST> VI_MPS1401LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTYMSTLOT> VI_PRODQTYMSTLOT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_MST> VI_PRODQTY_MST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_DTL> VI_PRODQTY_DTL { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_MON> VI_PRODQTY_MON { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_DAY> VI_PRODQTY_DAY { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_BAN_QTYM> VI_BAN_QTYM { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_BAN_QTYSUM> VI_BAN_QTYSUM { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_WHPOSITON_QTY_D1> VI_WHPOSITON_QTY_D1 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_WHPOSITON_QTY_D2> VI_WHPOSITON_QTY_D2 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1000> TN_BAN1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1001> TN_BAN1001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1200> TN_BAN1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1201> TN_BAN1201 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_BANTOCK_MST_Lot> VI_BANTOCK_MST_Lot { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_WMS1000> TN_WMS1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_WMS2000> TN_WMS2000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_TRY001> TN_TRY001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_TRY001> VI_TRY001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPS1700_MASTER> VI_MPS1700_MASTER { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_LOT_MST> TN_LOT_MST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1405> TN_MPS1405 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1406> TN_MPS1406 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1407> TN_MPS1407 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_ADD_QC2000_LIST> VI_ADD_QC2000_LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MEA1000_NOT_FILE_LIST> VI_MEA1000_NOT_FILE_LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_ORD1200_DETAIL_ADD> VI_ORD1200_DETAIL_ADD { get; set; }
        

        /// <summary>
        /// 자동채번 프로시저
        /// </summary>
        /// <param name="preFix"></param>
        /// <returns></returns>
        public virtual string GetRequestNumber(string preFix)
        {
            var preFixParameter = new SqlParameter { ParameterName = "@PreFix", Value = preFix };
            return Database.SqlQuery<string>("exec USP_GET_SEQ @PreFix", preFixParameter).Single();
        }
        public virtual string GetRequestNumberNew(string preFix)
        {
            var preFixParameter = new SqlParameter { ParameterName = "@PreFix", Value = preFix };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_NEW @PreFix", preFixParameter).Single();
        }

        /// <summary>
        /// 포장 LOT MAX + 1 값 
        /// </summary>
        /// <param name="preFix"></param>
        /// <returns></returns>
        public virtual int GetPackLotNoMaxSeq(string preFix)
        {
            var preFixParameter = new SqlParameter { ParameterName = "@PreFix", Value = preFix };
            return Database.SqlQuery<int>("exec USP_GET_PACK_LOT_NO_MAX_SEQ @PreFix", preFixParameter).Single();
        }

        //부서코드 채번
        public virtual string GetDepartmentCodeSeq()
        {
            return Database.SqlQuery<string>("exec USP_GET_DEPARTMENT_SEQ").Single();
        }
        
        /////// <summary>
        /////// ex)FAC-00001
        /////// </summary>
        /////// <param name="preFix"></param>
        /////// <returns></returns>
        ////public virtual string GetRequestNumber2(string preFix)
        ////{
        ////    var preFixParameter = new SqlParameter { ParameterName = "@PreFix", Value = preFix };
        ////    return Database.SqlQuery<string>("exec USP_GET_SEQ2 @PreFix", preFixParameter).Single();
        ////}
        ////public virtual string GetRequestNumber3(string preFix)
        ////{
        ////    var preFixParameter = new SqlParameter { ParameterName = "@PreFix", Value = preFix };
        ////    return Database.SqlQuery<string>("exec USP_GET_SEQ3 @PreFix", preFixParameter).Single();
        ////}
        ////public virtual string GetRequestNumberPr(string preFix)
        ////{
        ////    var preFixParameter = new SqlParameter { ParameterName = "@PreFix", Value = preFix };
        ////    return Database.SqlQuery<string>("exec GET_PR_SEQ @PreFix", preFixParameter).Single();
        ////}
        ////public virtual string GetRequestLaserMarkingSeq(string LotNo)
        ////{
        ////    var preFixParameter = new SqlParameter { ParameterName = "@LotNo", Value = LotNo };
        ////    return Database.SqlQuery<string>("exec USP_GET_LaserMarking_SEQ @LotNo", preFixParameter).SingleOrDefault();
        ////}
        ////public virtual string GetRequestWashDocNoSeq()
        ////{
        ////    return Database.SqlQuery<string>("exec USP_GET_WASH_SHEET_SEQ").SingleOrDefault();
        ////}
        ////public virtual string GetRequestWashBatchNoSeq()
        ////{
        ////    return Database.SqlQuery<string>("exec USP_GET_WASH_BATCH_NO_SEQ").SingleOrDefault();
        ////}

        /// <summary>
        /// 소수점처리 명시
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TN_STD1100>().Property(x => x.SrcQty).HasPrecision(18, 3);
            //modelBuilder.Entity<BomMaster>().Property(x => x.ItemWeightA).HasPrecision(18, 6);
            //modelBuilder.Entity<BomMaster>().Property(x => x.ItemWeightB).HasPrecision(18, 6);
            //modelBuilder.Entity<BomMaster>().Property(x => x.UseQty4).HasPrecision(18, 6);
            //modelBuilder.Entity<BomMaster>().Property(x => x.UseQty5).HasPrecision(18, 6);
       
        }

        public virtual int SendSqlNotification(string entityName)
        {
            var entityNameParameter = new SqlParameter { ParameterName = "@QUE_TABLE", Value = entityName };
            return Database.ExecuteSqlCommand("USP_QUE0001_CUD01 @QUE_TABLE", entityNameParameter);
        }

      //  public virtual DbSet<HKInc.Ui.Model.Domain.AttachedFile> AttachedFile { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.CodeMaster> CodeMaster { get; set; }
       // public virtual DbSet<HKInc.Ui.Model.Domain.CodeSql> CodeSql { get; set; }
        //public virtual DbSet<HKInc.Ui.Model.Domain.FileInfo> FileInfo { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.GroupMenu> GroupMenu { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.FieldLabel> LabelText { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.LoginLog> LoginLog { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.Menu> Menu { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.MenuLog> MenuLog { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.StandardMessage> StandardMessage { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.Module> Module { get; set; }
       // public virtual DbSet<HKInc.Ui.Model.Domain.QueTable> QueTable { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.Screen> Screen { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.SystemLog> SystemLog { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.User> User { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.UserGroup> UserGroup { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.UserUserGroup> UserUserGroup { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.ParameterInfo> ParameterInfo { get; set; }
      //  public virtual DbSet<HKInc.Ui.Model.Domain.ServerAgentJob> ServerAgentJob { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.MenuUserRight> MenuUserRight { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.MenuUserList> MenuUserList { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.MenuBookmark> MenuBookMark { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.MenuFavorite> MenuFavorite { get; set; }
      public virtual DbSet<HKInc.Ui.Model.Domain.CultureField> CultureField { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.Notice> Notice { get; set; }
       // public virtual DbSet<HKInc.Ui.Model.Domain.LicenseKey> LicenseKey { get; set; }
        //public virtual DbSet<HKInc.Ui.Model.Domain.UserDepartment> Department { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.GridLayout> GridLayout { get; set; }

        public virtual int SendDatabaseMail(string toMailAddress, string content, string title, string fromMailAddress, string replyTo, string fileAttachments = "")
        {
            var toMailAddressParameter = new SqlParameter { ParameterName = "@TO_EMAIL_ADDR", Value = toMailAddress };
            var contentParameter = new SqlParameter { ParameterName = "@CONT", Value = content };
            var titleParameter = new SqlParameter { ParameterName = "@TTL", Value = title };
            var fromMailAddressParameter = new SqlParameter { ParameterName = "@FROM_EMAIL_ADDR", Value = fromMailAddress };
            var replyToParameter = new SqlParameter { ParameterName = "@REPLY_TO", Value = replyTo };
            var fileAttachmentsParameter = new SqlParameter { ParameterName = " @file_attachments", Value = fileAttachments };

            return Database.ExecuteSqlCommand("USP_CMN_SEND_MAIL @TO_EMAIL_ADDR, @CONT, @TTL, @FROM_EMAIL_ADDR, @REPLY_TO, @file_attachments",
                                              toMailAddressParameter,
                                              contentParameter,
                                              titleParameter,
                                              fromMailAddressParameter,
                                              replyToParameter,
                                              fileAttachmentsParameter);
        }


     
        public virtual int MakeSystemLogEntry(int logType, string className, int errorCode, string message, string message2, string message3, string message4, string message5, string updateId, string updateClass)
        {
            var logTypeParameter = new SqlParameter { ParameterName = "@LogType", Value = logType };
            var classNameParameter = new SqlParameter { ParameterName = "@ClassName", Value = className };
            var errorCodeParameter = new SqlParameter { ParameterName = "@ErrorCode", Value = errorCode };
            var messageParameter = new SqlParameter { ParameterName = "@Message", Value = message };
            var message2Parameter = new SqlParameter { ParameterName = "@Message2", Value = message2 };
            var message3Parameter = new SqlParameter { ParameterName = "@Message3", Value = message3 };
            var message4Parameter = new SqlParameter { ParameterName = "@Message3", Value = message4 };
            var message5Parameter = new SqlParameter { ParameterName = "@Message4", Value = message5 };
            var updateIdParameter = new SqlParameter { ParameterName = "@UpdateId", Value = updateId };
            var updateClassParameter = new SqlParameter { ParameterName = "@UpdateClass", Value = updateClass };

            return Database.ExecuteSqlCommand("USP_CMN_SYS_LOG @LogType, @ClassName, @ErrorCode, @Message, @Message2, @Message3, @Message4, @Message5, @UpdateId, @UpdateClass",
                                              logTypeParameter,
                                              classNameParameter,
                                              errorCodeParameter,
                                              messageParameter,
                                              message2Parameter,
                                              message3Parameter,
                                              message4Parameter,
                                              message5Parameter,
                                              updateIdParameter,
                                              updateClassParameter);
        }
    }
}



