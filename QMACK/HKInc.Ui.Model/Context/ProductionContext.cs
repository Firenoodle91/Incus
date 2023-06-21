using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

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
        /// <summary>
        /// 20220407 오세완 차장
        /// 단가이력관리 거래처 목록 추가 
        /// </summary>
        public virtual DbSet<Domain.TN_STD1120> TN_STD1120 { get; set; }

        /// <summary>
        /// 20220407 오세완 차장
        /// 단가이력관리 단가 목록 추가 
        /// </summary>
        public virtual DbSet<Domain.TN_STD1121> TN_STD1121 { get; set; }
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
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD2000> TN_ORD2000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ORD2001> TN_ORD2001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1000> TN_MPS1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1001> TN_MPS1001 { get; set; }

        /// <summary>
        /// 20220321 오세완 차장 표준별공정타입관리 테이블 추가 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1010> TN_MPS1010 { get; set; }

        /// <summary>
        /// 20220321 오세완 차장 표준별공정타입관리 상세 테이블 추가 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1011> TN_MPS1011 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1300> TN_MPS1300 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1400> TN_MPS1400 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1401> TN_MPS1401 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1404> TN_MPS1404 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1405> TN_MPS1405 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MPS1600> TN_MPS1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPSPLAN_LIST> VI_MPSPLAN_LIST { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MACHINE_PROCESS_QTY> VI_MACHINE_PROCESS_QTY { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_ALAM002> TN_ALAM002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1100> TN_PUR1100 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1200> TN_PUR1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1300> TN_PUR1300 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1301> TN_PUR1301 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1500> TN_PUR1500 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1501> TN_PUR1501 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1600> TN_PUR1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1700> TN_PUR1700 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1800> TN_PUR1800 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR1801> TN_PUR1801 { get; set; }

        /// <summary>
        /// 20220415 오세완 차장 
        /// 자재재고조정 테이블 추가 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_PUR2000> TN_PUR2000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR1600_LIST> VI_PUR1600_LIST { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURSTOCK> VI_PURSTOCK { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURINOUT> VI_PURINOUT { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 자재재고관리 입출고목록 view 추가 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURINOUT_V2> VI_PURINOUT_V2 { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 자재재고관리(lotno) 상세목록 view 추가 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURINOUT_LOT> VI_PURINOUT_LOT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PURSTOCK_LOT> VI_PURSTOCK_LOT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PO_LIST_PRT> VI_PO_LIST_PRT { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR1900V1> VI_PUR1900V1 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPS1401V> VI_MPS1401V { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPS1405V> VI_MPS1405V { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR1900V2> VI_PUR1900V2 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_RESULT_QTY> VI_RESULT_QTY { get; set; }
        

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1000> TN_MEA1000 { get; set; }

        /// <summary>
        /// 설비이력목록      2022-04-19 김진우 추가
        /// </summary>
        public virtual DbSet<Domain.TN_MEA1001> TN_MEA1001 { get; set; }

        /// <summary>
        /// 20220313 오세완 차장 설비일상점김기준관리 추가 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1002> TN_MEA1002 { get; set; }

        /// <summary>
        /// 202203013 오세완 차장 설비일상점검기준이력 추가
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1003> TN_MEA1003 { get; set; }
        /// <summary>
        /// 20220404 오세완 차장 
        /// 비가동 관리 추가
        /// </summary>
        public virtual DbSet<Domain.TN_MEA1004> TN_MEA1004 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1100> TN_MEA1100 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1200> TN_MEA1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1300> TN_MEA1300 { get; set; }

        /// <summary>2021-11-15 김진우 주임 추가</summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1600> TN_MEA1600 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1601> TN_MEA1601 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MEA1700> TN_MEA1700 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MOLD001> TN_MOLD001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MOLD002> TN_MOLD002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_MOLD003> TN_MOLD003 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_INDOO001> TN_INDOO001 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_INDOO002> TN_INDOO002 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_SRC_STOCK_SUM> VI_SRC_STOCK_SUM { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_SRC_USE> VI_SRC_USE { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT2200> TN_QCT2200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1000> TN_QCT1000 { get; set; }
        //public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1001> TN_QCT1001 { get; set; }                    // 2022-03-07 김진우 주석
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1200> TN_QCT1200 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1201> TN_QCT1201 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1500> TN_QCT1500 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1600> TN_QCT1600 { get; set; }                     // 안돈 추가        2022-07-15 김진우
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_QCT1700> TN_QCT1700 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_QCT1500_LIST> VI_QCT1500_LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_INSPLIST> VI_INSPLIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_LOTTRACKING> VI_LOTTRACKING { get; set; }

        /// <summary>
        /// 20220313 오세완 차장
        /// 설비일상점검관리 조회 combo사용 
        /// </summary>
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MEA1003_MASTER_LIST> VI_MEA1003_MASTER_LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_MPS1401LIST> VI_MPS1401LIST { get; set; }
        //  public virtual DbSet<HKInc.Ui.Model.Domain.TP_WORKPLANTORUN> TP_WORKPLANTORUN { get; set; }


        public virtual DbSet<HKInc.Ui.Model.Domain.TN_UPH1000> TN_UPH1000 { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_MST> VI_PRODQTY_MST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_DTL> VI_PRODQTY_DTL { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_MON> VI_PRODQTY_MON { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTY_DAY> VI_PRODQTY_DAY { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PUR_LIST> VI_PUR_LIST { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PRODQTYMSTLOT> VI_PRODQTYMSTLOT { get; set; }
        //public virtual DbSet<HKInc.Ui.Model.Domain.VI_BAN_QTYM> VI_BAN_QTYM { get; set; }                 // 2022-02-23 김진우 주석처리
        //public virtual DbSet<HKInc.Ui.Model.Domain.VI_BAN_QTYSUM> VI_BAN_QTYSUM { get; set; }             // 2022-02-23 김진우 주석처리
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_PROCESSING_QTY> VI_PROCESSING_QTY { get; set; }

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_WHPOSITION_QTY_D1> VI_WHPOSITION_QTY_D1 { get; set; }     // 2021-11-04 김진우 주임 수정
        public virtual DbSet<HKInc.Ui.Model.Domain.VI_WHPOSITION_QTY_D2> VI_WHPOSITION_QTY_D2 { get; set; }     // 2021-11-04 김진우 주임 수정
        //public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1000> TN_BAN1000 { get; set; }                       // 2022-02-23 김진우 주석처리
        //public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1001> TN_BAN1001 { get; set; }                       // 2022-02-23 김진우 주석처리
        //public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1200> TN_BAN1200 { get; set; }                       // 2022-02-23 김진우 주석처리
        //public virtual DbSet<HKInc.Ui.Model.Domain.TN_BAN1201> TN_BAN1201 { get; set; }                       // 2022-02-23 김진우 주석처리
        //public virtual DbSet<HKInc.Ui.Model.Domain.VI_BANTOCK_MST_Lot> VI_BANTOCK_MST_Lot { get; set; }       // 2022-02-23 김진우 주석처리

        public virtual DbSet<HKInc.Ui.Model.Domain.TN_WMS1000> TN_WMS1000 { get; set; }
        public virtual DbSet<HKInc.Ui.Model.Domain.TN_WMS2000> TN_WMS2000 { get; set; }
        //public virtual DbSet<HKInc.Ui.Model.Domain.TN_TRY001> TN_TRY001 { get; set; }                         // 2022-02-23 김진우 주석처리
        //public virtual DbSet<HKInc.Ui.Model.Domain.VI_TRY001> VI_TRY001 { get; set; }                         // 2022-02-23 김진우 주석처리

        public virtual DbSet<HKInc.Ui.Model.Domain.VI_OUTLIST> VI_OUTLIST { get; set; }

        /// <summary>
        /// 20220425 오세완 차장 
        /// 고도화로 인한 로트추적마스터 신규
        /// </summary>
        public virtual DbSet<Domain.TN_LOT_MST_V2> TN_LOT_MST_V2 { get; set; }

        /// <summary>
        /// 20220425 오세완 차장 
        /// 고도화로 인한 로트추적디테일 신규
        /// </summary>
        public virtual DbSet<Domain.TN_LOT_DTL> TN_LOT_DTL { get; set; }
        /// <summary>
        /// 20220321 오세완 차장 휴일관리 추가 
        /// </summary>
        public virtual DbSet<Domain.Holiday> Holiday { get; set; }
        //public virtual DbSet<HKInc.Ui.Model.Domain.TP_POPJOBLIST> TP_POPJOBLIST { get; set; }
        //public virtual int SendSqlNotification(string entityName)
        //{
        //    var entityNameParameter = new SqlParameter { ParameterName = "@QUE_TABLE", Value = entityName };
        //    return Database.ExecuteSqlCommand("HKInc.dbo.USP_QUE0001_CUD01 @QUE_TABLE", entityNameParameter);
        //}

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
            //modelBuilder.Entity<BomMaster>().Property(x => x.UseQty).HasPrecision(18, 6);
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

        /// <summary>
        /// 20220421 오세완 차장 tp보고용 테이블 추가
        /// </summary>
        public virtual DbSet<Domain.MENUTOTABLE> MENUTOTABLE { get; set; }

        /// <summary>
        /// 20220307 오세완 차장 
        /// TP보고용 로그 기록 뷰 추가 
        /// </summary>
        public virtual DbSet<Domain.VI_TABLESIZE> VI_TABLESIZE { get; set; }

        /// <summary>
        /// 20220307 오세완 차장 
        /// TP보고용 로그 기록 테이블 추가 
        /// </summary>
        public virtual DbSet<Domain.MenuEventLog> MenuEventLog { get; set; }

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

        /// <summary>
        /// 20220412 오세완 차장
        /// 단가이력관리에 입력된 단가 가져오기
        /// </summary>
        /// <param name="ps_Itemcode">품목코드</param>
        /// <param name="ps_Customercode">거래처코드</param>
        /// <param name="ps_Time">적용일자 yyyy-MM-dd</param>
        /// <returns>decimal </returns>
        public virtual decimal GetManagedCost(string ps_Itemcode, string ps_Customercode, string ps_Time)
        {
            decimal dResult = 0m;

            SqlParameter sp_ItemCode = new SqlParameter("@ITEM_CODE", ps_Itemcode);
            SqlParameter sp_Customercode = new SqlParameter("@CUSTOMER_CODE", ps_Customercode);
            SqlParameter sp_Date = new SqlParameter("@DATE", ps_Time);

            dResult = Database.SqlQuery<decimal>("USP_GET_XFSTD1120_COST @ITEM_CODE, @CUSTOMER_CODE, @DATE", sp_ItemCode, sp_Customercode, sp_Date).Single();

            return dResult;
        }
    }
}



