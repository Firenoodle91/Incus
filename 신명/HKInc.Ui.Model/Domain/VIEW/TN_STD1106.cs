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
    /// <summary>품목/거래처단가이력관리</summary>	
    [Table("TN_STD1106T")]
    public class TN_STD1106 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1106()
        {
        }
        /// <summary>품목코드</summary>              
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>            
        [ForeignKey("TN_STD1400"), Key, Column("CUSTOMER_CODE", Order = 1)] public string CustomerCode { get; set; }
        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 2), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>
        /// 공정코드
        /// </summary>
        [Key, Column("PROCESS_CODE", Order =3), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }

        /// <summary>변경 시작일</summary>                
        [Column("START_DATE")] public DateTime? StartDate { get; set; }
        /// <summary>변경 종료일</summary>                
        [Column("END_DATE")] public DateTime? EndDate { get; set; }

        /// <summary>변경단가</summary>              
        [Column("ITEM_COST")] public decimal? ItemCost { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}