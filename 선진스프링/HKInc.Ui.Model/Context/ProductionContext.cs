﻿using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using HKInc.Ui.Model.Domain.VIEW;
using HKInc.Ui.Model.Domain;

namespace HKInc.Ui.Model.Context
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(string connectString) : base(connectString) { }

        #region SYS
        public virtual DbSet<CodeMaster> CodeMaster { get; set; }
        public virtual DbSet<GroupMenu> GroupMenu { get; set; }
        public virtual DbSet<FieldLabel> LabelText { get; set; }
        public virtual DbSet<LoginLog> LoginLog { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuLog> MenuLog { get; set; }
        public virtual DbSet<StandardMessage> StandardMessage { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Screen> Screen { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserUserGroup> UserUserGroup { get; set; }
        public virtual DbSet<MenuUserRight> MenuUserRight { get; set; }
        public virtual DbSet<MenuUserList> MenuUserList { get; set; }
        public virtual DbSet<MenuBookmark> MenuBookMark { get; set; }
        public virtual DbSet<MenuFavorite> MenuFavorite { get; set; }
        public virtual DbSet<CultureField> CultureField { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<GridLayout> GridLayout { get; set; }
        public virtual DbSet<Holiday> Holiday { get; set; }
        
        #endregion

        #region STD

        public virtual DbSet<TN_STD1000> TN_STD1000 { get; set; }
        public virtual DbSet<TN_STD1100> TN_STD1100 { get; set; }
        public virtual DbSet<TN_STD1101> TN_STD1101 { get; set; }
        public virtual DbSet<TN_STD1102> TN_STD1102 { get; set; }
        public virtual DbSet<TN_STD1103> TN_STD1103 { get; set; }
        public virtual DbSet<TN_STD1104> TN_STD1104 { get; set; }
        public virtual DbSet<TN_STD1200> TN_STD1200 { get; set; }
        public virtual DbSet<TN_STD1300> TN_STD1300 { get; set; }
        public virtual DbSet<TN_STD1400> TN_STD1400 { get; set; }
        public virtual DbSet<TN_STD4M001> TN_STD4M001 { get; set; }
        #endregion

        #region ORD
        public virtual DbSet<TN_ORD1000> TN_ORD1000 { get; set; }
        public virtual DbSet<TN_ORD1001> TN_ORD1001 { get; set; }
        public virtual DbSet<TN_ORD1100> TN_ORD1100 { get; set; }
        public virtual DbSet<TN_ORD1101> TN_ORD1101 { get; set; }
        public virtual DbSet<TN_ORD1102> TN_ORD1102 { get; set; }
        public virtual DbSet<TN_ORD1103> TN_ORD1103 { get; set; }        
        public virtual DbSet<TN_ORD1200> TN_ORD1200 { get; set; }
        public virtual DbSet<TN_ORD1201> TN_ORD1201 { get; set; }
        public virtual DbSet<TN_ORD1300> TN_ORD1300 { get; set; }
        public virtual DbSet<TN_ORD1301> TN_ORD1301 { get; set; }
        public virtual DbSet<TN_ORD1400> TN_ORD1400 { get; set; }
        public virtual DbSet<TN_ORD1401> TN_ORD1401 { get; set; }
        public virtual DbSet<TN_ORD1500> TN_ORD1500 { get; set; }
        public virtual DbSet<TN_ORD1600> TN_ORD1600 { get; set; }        
        public virtual DbSet<TN_PROD_DEAD_MST> TN_PROD_DEAD_MST { get; set; }
        public virtual DbSet<TN_PROD_DEAD_DTL> TN_PROD_DEAD_DTL { get; set; }
        public virtual DbSet<TN_PROD_DEAD_HISTORY> TN_PROD_DEAD_HISTORY { get; set; }
        public virtual DbSet<TN_BAN_DEAD_MST> TN_BAN_DEAD_MST { get; set; }
        public virtual DbSet<TN_BAN_DEAD_DTL> TN_BAN_DEAD_DTL { get; set; }
        public virtual DbSet<TN_BAN_DEAD_HISTORY> TN_BAN_DEAD_HISTORY { get; set; }
        public virtual DbSet<TN_MAT_DEAD_MST> TN_MAT_DEAD_MST { get; set; }
        public virtual DbSet<TN_MAT_DEAD_DTL> TN_MAT_DEAD_DTL { get; set; }
        public virtual DbSet<TN_MAT_DEAD_HISTORY> TN_MAT_DEAD_HISTORY { get; set; }
        #endregion

        #region PUR
        public virtual DbSet<TN_PUR1100> TN_PUR1100 { get; set; }
        public virtual DbSet<TN_PUR1101> TN_PUR1101 { get; set; }
        public virtual DbSet<TN_PUR1200> TN_PUR1200 { get; set; }
        public virtual DbSet<TN_PUR1201> TN_PUR1201 { get; set; }
        public virtual DbSet<TN_PUR1202> TN_PUR1202 { get; set; }        
        public virtual DbSet<TN_PUR1300> TN_PUR1300 { get; set; }
        public virtual DbSet<TN_PUR1301> TN_PUR1301 { get; set; }
        public virtual DbSet<TN_PUR1302> TN_PUR1302 { get; set; }
        public virtual DbSet<TN_PUR1303> TN_PUR1303 { get; set; }
        public virtual DbSet<TN_PUR1400> TN_PUR1400 { get; set; }
        public virtual DbSet<TN_PUR1401> TN_PUR1401 { get; set; }
        public virtual DbSet<TN_PUR1500> TN_PUR1500 { get; set; }
        public virtual DbSet<TN_PUR1501> TN_PUR1501 { get; set; }
        public virtual DbSet<TN_PUR1600> TN_PUR1600 { get; set; }
        public virtual DbSet<TN_PUR1700> TN_PUR1700 { get; set; }
        #endregion

        #region MPS
        public virtual DbSet<TN_MPS1000> TN_MPS1000 { get; set; }
        public virtual DbSet<TN_MPS1001> TN_MPS1001 { get; set; }
        public virtual DbSet<TN_MPS1002> TN_MPS1002 { get; set; }
        public virtual DbSet<TN_MPS1100> TN_MPS1100 { get; set; }
        public virtual DbSet<TN_MPS1200> TN_MPS1200 { get; set; }
        public virtual DbSet<TN_MPS1201> TN_MPS1201 { get; set; }
        public virtual DbSet<TN_MPS1202> TN_MPS1202 { get; set; }
        public virtual DbSet<TN_MPS1203> TN_MPS1203 { get; set; }
        public virtual DbSet<TN_MPS1300> TN_MPS1300 { get; set; }
        public virtual DbSet<TN_MPS1302> TN_MPS1302 { get; set; }
        public virtual DbSet<TN_SRC1000> TN_SRC1000 { get; set; }
        public virtual DbSet<TN_SRC1001> TN_SRC1001 { get; set; }
        public virtual DbSet<TN_LOT_MST> TN_LOT_MST { get; set; }
        public virtual DbSet<TN_LOT_DTL> TN_LOT_DTL { get; set; }
        public virtual DbSet<TN_ITEM_MOVE> TN_ITEM_MOVE { get; set; }
        #endregion

        #region BAN
        public virtual DbSet<TN_BAN1000> TN_BAN1000 { get; set; }
        public virtual DbSet<TN_BAN1001> TN_BAN1001 { get; set; }
        public virtual DbSet<TN_BAN1100> TN_BAN1100 { get; set; }
        public virtual DbSet<TN_BAN1101> TN_BAN1101 { get; set; }
        public virtual DbSet<TN_BAN1102> TN_BAN1102 { get; set; }
        #endregion

        #region QCT
        public virtual DbSet<TN_QCT1000> TN_QCT1000 { get; set; }
        public virtual DbSet<TN_QCT1001> TN_QCT1001 { get; set; }
        public virtual DbSet<TN_QCT1100> TN_QCT1100 { get; set; }
        public virtual DbSet<TN_QCT1101> TN_QCT1101 { get; set; }
        public virtual DbSet<TN_QCT1400> TN_QCT1400 { get; set; }
        public virtual DbSet<TN_QCT1300> TN_QCT1300 { get; set; }
        public virtual DbSet<TN_QCT1500> TN_QCT1500 { get; set; }
        public virtual DbSet<TN_QCT1501> TN_QCT1501 { get; set; }
        public virtual DbSet<TN_QCT1600> TN_QCT1600 { get; set; }
        #endregion

        #region MEA
        public virtual DbSet<TN_MEA1000> TN_MEA1000 { get; set; }
        public virtual DbSet<TN_MEA1001> TN_MEA1001 { get; set; }
        public virtual DbSet<TN_MEA1002> TN_MEA1002 { get; set; }
        public virtual DbSet<TN_MEA1003> TN_MEA1003 { get; set; }
        public virtual DbSet<TN_MEA1004> TN_MEA1004 { get; set; }
        public virtual DbSet<TN_MEA1100> TN_MEA1100 { get; set; }
        public virtual DbSet<TN_MEA1101> TN_MEA1101 { get; set; }
        public virtual DbSet<TN_MEA1201> TN_MEA1201 { get; set; }
        #endregion

        #region WMS
        public virtual DbSet<TN_WMS1000> TN_WMS1000 { get; set; }
        public virtual DbSet<TN_WMS2000> TN_WMS2000 { get; set; }
        #endregion

        #region TRY
        public virtual DbSet<TN_TRY1000> TN_TRY1000 { get; set; }
        #endregion

        #region TOOL
        public virtual DbSet<TN_TOOL1001> TN_TOOL1001 { get; set; }
        public virtual DbSet<TN_TOOL1002> TN_TOOL1002 { get; set; }
        public virtual DbSet<TN_TOOL1003> TN_TOOL1003 { get; set; }
        #endregion

        #region DEV
        public virtual DbSet<TN_DEV1000> TN_DEV1000 { get; set; }
        #endregion

        #region VIEW
        public virtual DbSet<VI_INSP_IN_OBJECT> VI_INSP_IN_OBJECT { get; set; }
        public virtual DbSet<VI_INSP_FME_OBJECT> VI_INSP_FME_OBJECT { get; set; }
        public virtual DbSet<VI_INSP_FREQNTLY_OBJECT> VI_INSP_FREQNTLY_OBJECT { get; set; }
        public virtual DbSet<VI_INSP_PROCESS_OBJECT> VI_INSP_PROCESS_OBJECT { get; set; }
        public virtual DbSet<VI_INSP_SETTING_OBJECT> VI_INSP_SETTING_OBJECT { get; set; }
        public virtual DbSet<VI_INSP_SHIPMENT_OBJECT> VI_INSP_SHIPMENT_OBJECT { get; set; }
        public virtual DbSet<VI_PUR_STOCK_IN_LOT_NO> VI_PUR_STOCK_IN_LOT_NO { get; set; }
        public virtual DbSet<VI_PUR_STOCK_ITEM> VI_PUR_STOCK_ITEM { get; set; }
        public virtual DbSet<VI_WH_STOCK_QTY> VI_WH_STOCK_QTY { get; set; }
        public virtual DbSet<VI_WH_POSITON_QTY> VI_WH_POSITON_QTY { get; set; }
        public virtual DbSet<VI_RETURN_OBJECT> VI_RETURN_OBJECT { get; set; }
        public virtual DbSet<VI_LOT_TRACKING> VI_LOT_TRACKING { get; set; }
        public virtual DbSet<VI_QCT1300_LIST> VI_QCT1300_LIST { get; set; }
        public virtual DbSet<VI_PROD_STOCK_ITEM> VI_PROD_STOCK_ITEM { get; set; }
        public virtual DbSet<VI_PRODBAN_STOCK_ITEM> VI_PRODBAN_STOCK_ITEM { get; set; }
        public virtual DbSet<VI_PROD_STOCK_PRODUCT_LOT_NO> VI_PROD_STOCK_PRODUCT_LOT_NO { get; set; }
        public virtual DbSet<VI_SRC_STOCK_SUM> VI_SRC_STOCK_SUM { get; set; }
        public virtual DbSet<VI_SRC_USE> VI_SRC_USE { get; set; }
        public virtual DbSet<VI_SRC_STOCK> VI_SRC_STOCK { get; set; }
        public virtual DbSet<VI_OUT_PROC_STOCK_MASTER> VI_OUT_PROC_STOCK_MASTER { get; set; }
        public virtual DbSet<VI_MEA1003_MASTER_LIST> VI_MEA1003_MASTER_LIST { get; set; }
        public virtual DbSet<VI_BAN_STOCK_IN_LOT_NO> VI_BAN_STOCK_IN_LOT_NO { get; set; }
        public virtual DbSet<VI_BAN_STOCK_PRODUCT_LOT_NO> VI_BAN_STOCK_PRODUCT_LOT_NO { get; set; }
        public virtual DbSet<VI_BAN_STOCK_PRODUCT_LOT_NO_NEW> VI_BAN_STOCK_PRODUCT_LOT_NO_NEW { get; set; }
        public virtual DbSet<VI_BAN_STOCK_ITEM> VI_BAN_STOCK_ITEM { get; set; }
        public virtual DbSet<VI_XFORD1102_MASTER_VIEW> VI_XFORD1102_MASTER_VIEW { get; set; }
        public virtual DbSet<VI_MPS1800_LIST> VI_MPS1800_LIST { get; set; }
        public virtual DbSet<VI_XRREP6000_LIST> VI_XRREP6000_LIST { get; set; }
        public virtual DbSet<VI_TOOL1002_LIST> VI_TOOL1002_LIST { get; set; }
        public virtual DbSet<VI_BUSINESS_MANAGEMENT_USER> VI_BUSINESS_MANAGEMENT_USER { get; set; }
        public virtual DbSet<VI_DISPOSAL_OBJECT> VI_DISPOSAL_OBJECT { get; set; }
        public virtual DbSet<VI_ITEM_AVG_OUT_QTY> VI_ITEM_AVG_OUT_QTY { get; set; }
        public virtual DbSet<VI_MACHINE_DAILY_CHECK> VI_MACHINE_DAILY_CHECK { get; set; }
        
        #endregion

        #region 채번프로시저

        /// <summary>
        /// 표준 채번 ex) ITEM-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public virtual string GetSeqStandard(string seqName)
        {
            var _seqName = new SqlParameter { ParameterName = "@SeqName", Value = seqName };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_STANDARD @SeqName", _seqName).Single();
        }

        /// <summary>
        /// 연도별 채번 ex) ORD-2019-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public virtual string GetSeqYear(string seqName)
        {
            var _seqName = new SqlParameter { ParameterName = "@SeqName", Value = seqName };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_YEAR @SeqName", _seqName).Single();
        }

        /// <summary>
        /// 월별 채번 ex) ORD-1901-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public virtual string GetSeqMonth(string seqName)
        {
            var _seqName = new SqlParameter { ParameterName = "@SeqName", Value = seqName };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_MONTH @SeqName", _seqName).Single();
        }

        /// <summary>
        /// 작업지시번호 채번
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public virtual string GetSeqWorkNo(DateTime endMonthDate)
        {
            var _endMonthDate = new SqlParameter { ParameterName = "@EndMonthDate", Value = endMonthDate };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_WORK_NO @EndMonthDate", _endMonthDate).Single();
        }        

        /// <summary>
        /// 일별 채번 ex) ORD-190101-00001
        /// </summary>
        /// <param name="seqName">채번명</param>
        /// <returns></returns>
        public virtual string GetSeqDay(string seqName)
        {
            var _seqName = new SqlParameter { ParameterName = "@SeqName", Value = seqName };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_DAY @SeqName", _seqName).Single();
        }

        /// <summary>
        /// 이동표번호채번
        /// </summary>
        /// <param name="workNo">작업지시번호</param>
        /// <returns></returns>
        public virtual string GetItemMoveSeq(string workNo)
        {
            var _workNo = new SqlParameter { ParameterName = "@WorkNo", Value = workNo };
            return Database.SqlQuery<string>("exec USP_GET_ITEM_MOVE_SEQ @WorkNo", _workNo).Single();
        }

        /// <summary>
        /// 입고LOT채번
        /// </summary>
        /// <param name="workNo">작업지시번호</param>
        /// <returns></returns>
        public virtual string USP_GET_SEQ_IN_LOT_NO(string seqName, string date)
        {
            var _seqName = new SqlParameter { ParameterName = "@SeqName", Value = seqName };
            var _date = new SqlParameter { ParameterName = "@Date", Value = date };
            return Database.SqlQuery<string>("exec USP_GET_SEQ_IN_LOT_NO @SeqName, @Date", _seqName, _date).Single();
        }
        
        /// <summary>
        /// 외주발주 저장 시 작업지시상태 갱신
        /// </summary>
        public virtual int USP_UPD_PUR1400_JOBSTATES()
        {
            return Database.SqlQuery<int>("exec USP_UPD_PUR1400_JOBSTATES").Single();
        }

        /// <summary>
        /// 외주입고 저장 시 작업지시상태 갱신
        /// </summary>
        public virtual int USP_UPD_PUR1500_JOBSTATES()
        {
            return Database.SqlQuery<int>("exec USP_UPD_PUR1500_JOBSTATES").Single();
        }

        /// <summary>
        /// 실적수정 시 ITEM_MOVE 테이블 누적수량 갱신
        /// </summary>
        public virtual object USP_UPD_XFMPS1900(string workNo)
        {
            var _workNo = new SqlParameter { ParameterName = "@WorkNo", Value = workNo };
            return Database.SqlQuery<string>("exec USP_UPD_XFMPS1900 @WorkNo", _workNo).SingleOrDefault();
        }

        #endregion

        /// <summary>
        /// 소수점처리 명시
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TN_STD1100>().Property(x => x.Weight).HasPrecision(18, 5);
            modelBuilder.Entity<TN_STD1100>().Property(x => x.SrcWeight).HasPrecision(18, 5);
            modelBuilder.Entity<TN_STD1300>().Property(x => x.UseQty).HasPrecision(18, 3);

            //modelBuilder.Entity<BomMaster>().Property(x => x.ItemWeightB).HasPrecision(18, 6);
            //modelBuilder.Entity<BomMaster>().Property(x => x.UseQty4).HasPrecision(18, 6);
            //modelBuilder.Entity<BomMaster>().Property(x => x.UseQty5).HasPrecision(18, 6);       
        }
    }
}



