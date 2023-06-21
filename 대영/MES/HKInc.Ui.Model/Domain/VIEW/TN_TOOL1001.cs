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
    /// <summary>툴 입출고관리</summary>
    [Table("TN_TOOL1001T")]
    public class TN_TOOL1001 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1001()
        {
        }
        /// <summary>품번(도번)</summary>        
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>              
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>구분</summary>              
        [Column("DIVISION"), Required(ErrorMessage = "Division")] public string Division { get; set; }
        /// <summary>입출고일자</summary>        
        [Column("IN_OUT_DATE"), Required(ErrorMessage = "InoutDate")] public DateTime InOutDate { get; set; }
        /// <summary>수량</summary>              
        [Column("QTY"), Required(ErrorMessage = "Qty")] public decimal Qty { get; set; }
        /// <summary>입출고자</summary>          
        [Column("IN_OUT_ID")] public string InOutId { get; set; }
        /// <summary>메모</summary>              
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>              
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>             
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>             
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}