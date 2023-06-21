﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>설비점검기준관리</summary>	
    [Table("TN_MEA1002T")]
    public class TN_MEA1002 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1002()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            TN_MEA1003List = new List<TN_MEA1003>();
        }
        /// <summary>설비코드</summary>            
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_CODE", Order = 0), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        /// <summary>점검순번</summary>            
        [Key, Column("CHECK_SEQ", Order = 1), Required(ErrorMessage = "CheckSeq")] public int CheckSeq { get; set; }
        /// <summary>점검구분</summary>            
        [Column("DIVISION"), Required(ErrorMessage = "Division")] public string Division { get; set; }
        /// <summary>점검위치</summary>            
        [Column("CHECK_POSITION"), Required(ErrorMessage = "CheckPosition")] public string CheckPosition { get; set; }
        /// <summary>점검항목</summary>            
        [Column("CHECK_LIST")] public string CheckList { get; set; }
        /// <summary>점검방법</summary>            
        [Column("CHECK_WAY")] public string CheckWay { get; set; }
        /// <summary>점검주기</summary>            
        [Column("CHECK_CYCLE")] public string CheckCycle { get; set; }
        /// <summary>점검기준일</summary>          
        [Column("CHECK_STANDARD_DATE")] public string CheckStandardDate { get; set; }
        /// <summary>관리기준</summary>            
        [Column("MANAGEMENT_STANDARD")] public string ManagementStandard { get; set; }
        /// <summary>표시순서</summary>            
        [Column("DISPLAY_ORDER")] public int? DisplayOrder { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>육안검사여부</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        public virtual ICollection<TN_MEA1003> TN_MEA1003List { get; set; }
    }
}