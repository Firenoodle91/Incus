using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>턴키 입고관리</summary>	
    [Table("TN_PUR1700T")]
    public class TN_PUR1700 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1700()
        {
        }

        /// <summary>발주번호</summary>              
        [ForeignKey("TN_PUR1600"), Key, Column("PO_NO", Order = 0), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>입고 LOT NO</summary>    
        [Key, Column("IN_LOT_NO", Order = 1), Required(ErrorMessage = "InLotNo")] public string InLotNo { get; set; }
        /// <summary>입고일</summary>                
        [Column("IN_DATE"), Required(ErrorMessage = "InDate")] public DateTime InDate { get; set; }
        /// <summary>입고자</summary>                
        [Column("IN_ID"), Required(ErrorMessage = "InId")] public string InId { get; set; }
        /// <summary>입고량</summary>                
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>입고단가</summary>            
        [Column("IN_COST")] public decimal? InCost { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }
               
        public virtual TN_PUR1600 TN_PUR1600 { get; set; }
    }
}