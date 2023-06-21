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
    /// <summary>외주입고 디테일</summary>	
    [Table("TN_PUR1501T")]
    public class TN_PUR1501 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1501()
        {
        }
        /// <summary>입고번호</summary>                
        [ForeignKey("TN_PUR1500"), Key, Column("IN_NO", Order = 0), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고순번</summary>                
        [Key, Column("IN_SEQ", Order = 1), Required(ErrorMessage = "InSeq")] public int InSeq { get; set; }
        /// <summary>발주번호</summary>                
        [ForeignKey("TN_PUR1401"), Column("PO_NO", Order = 2), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>발주순번</summary>                
        [ForeignKey("TN_PUR1401"), Column("PO_SEQ", Order = 3), Required(ErrorMessage = "PoSeq")] public int PoSeq { get; set; }
        /// <summary>품번(도번)</summary>              
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>입고LOTNO</summary>               
        [Column("IN_LOT_NO"), Required(ErrorMessage = "InLotNo")] public string InLotNo { get; set; }
        /// <summary>입고량</summary>                  
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>입고단가</summary>                
        [Column("IN_COST")] public decimal? InCost { get; set; }
        /// <summary>불량수량</summary>                
        [Column("BAD_QTY")] public decimal? BadQty { get; set; }
        /// <summary>불량유형</summary>                
        [Column("BAD_TYPE")] public string BadType { get; set; }
        /// <summary>메모</summary>                    
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                    
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                   
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                   
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_PUR1401 TN_PUR1401 { get; set; }
        public virtual TN_PUR1500 TN_PUR1500 { get; set; }
    }
}