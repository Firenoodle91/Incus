﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재출고 디테일</summary>	
    [Table("TN_PUR1301T")]
    public class TN_PUR1301 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1301()
        {
        }
        /// <summary>출고번호</summary>              
        [ForeignKey("TN_PUR1300"), Key, Column("OUT_NO", Order = 0), Required(ErrorMessage = "OutNo")] public string OutNo { get; set; }
        /// <summary>출고순번</summary>              
        [Key, Column("OUT_SEQ", Order = 1), Required(ErrorMessage = "OutSeq")] public int OutSeq { get; set; }
        /// <summary>입고번호</summary>              
        [ForeignKey("TN_PUR1201"), Column("IN_NO", Order = 2), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고순번</summary>              
        [ForeignKey("TN_PUR1201"), Column("IN_SEQ", Order = 3), Required(ErrorMessage = "InSeq")] public int InSeq { get; set; }
        /// <summary>품번(도번)</summary>            
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>출고량</summary>                
        [Column("OUT_QTY"), Required(ErrorMessage = "OutQty")] public decimal OutQty { get; set; }
        /// <summary>출고 LOT NO</summary>           
        [Column("OUT_LOT_NO"), Required(ErrorMessage = "OutLotNo")] public string OutLotNo { get; set; }
        /// <summary>입고 LOT NO</summary>           
        [Column("IN_LOT_NO"), Required(ErrorMessage = "InLotNo")] public string InLotNo { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }
        /// <summary>재출고 여부(자동차감)</summary>
        [Column("REOUT_YN")] public string ReOutYn { get; set; }
        /// <summary>자동출고구분(Y:자동출고된 자재)</summary>                  
        [Column("AUTO_FLAG")] public string AutoFlag { get; set; }

        [NotMapped] public decimal? CustomStockQty { get; set; }

        public virtual TN_PUR1201 TN_PUR1201 { get; set; }
        public virtual TN_PUR1300 TN_PUR1300 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}