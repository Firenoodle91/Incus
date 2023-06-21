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
    /// <summary>턴키 발주관리</summary>	
    [Table("TN_PUR1600T")]
    public class TN_PUR1600 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1600()
        {
            TN_PUR1700List = new List<TN_PUR1700>();
        }
        /// <summary>발주번호</summary>                
        [Key, Column("PO_NO", Order = 0), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>수주번호</summary>                
        [ForeignKey("TN_ORD1100"), Column("ORDER_NO", Order = 1), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }
        /// <summary>수주순번</summary>                
        [ForeignKey("TN_ORD1100"), Column("ORDER_SEQ", Order = 2), Required(ErrorMessage = "OrderSeq")] public int OrderSeq { get; set; }
        /// <summary>납품계획번호</summary>            
        [ForeignKey("TN_ORD1100"), Column("DELIV_NO", Order = 3), Required(ErrorMessage = "DelivNo")] public string DelivNo { get; set; }
        /// <summary>발주일</summary>                  
        [Column("PO_DATE"), Required(ErrorMessage = "PoDate")] public DateTime PoDate { get; set; }
        /// <summary>발주자</summary>                  
        [Column("PO_ID"), Required(ErrorMessage = "PoId")] public string PoId { get; set; }
        /// <summary>발주량</summary>                  
        [Column("PO_QTY"), Required(ErrorMessage = "PoQty")] public decimal PoQty { get; set; }
        /// <summary>발주단가</summary>                
        [Column("PO_COST")] public decimal? PoCost { get; set; }
        /// <summary>메모</summary>                    
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                    
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                   
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                   
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary> 입고미잔량 </summary>
        [NotMapped]
        public decimal PoRemainQty
        {
            get
            {
                if (TN_PUR1700List.Count == 0) return PoQty;
                else return PoQty - TN_PUR1700List.Sum(p => p.InQty);
            }
        }

        public virtual TN_ORD1100 TN_ORD1100 { get; set; }
        public virtual ICollection<TN_PUR1700> TN_PUR1700List { get; set; }
    }
}