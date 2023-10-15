using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
   /// <summary>납품계획관리</summary>	
   [Table("TN_ORD1100T")]
    public class TN_ORD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1100()
        {
            TN_ORD1101List = new List<TN_ORD1101>();
            TN_ORD1102List = new List<TN_ORD1102>();
            TN_ORD1200List = new List<TN_ORD1200>();
            TN_MPS1100List = new List<TN_MPS1100>();
            TN_PUR1600List = new List<TN_PUR1600>();
        }
        /// <summary>수주번호</summary>                    
        [ForeignKey("TN_ORD1001"), Key, Column("ORDER_NO", Order = 0), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }
        /// <summary>순번</summary>                        
        [ForeignKey("TN_ORD1001"), Key, Column("ORDER_SEQ", Order = 1), Required(ErrorMessage = "OrderSeq")] public int OrderSeq { get; set; }
        /// <summary>납품계획번호</summary>                
        [Key, Column("DELIV_NO", Order = 2), Required(ErrorMessage = "DelivNo")] public string DelivNo { get; set; }
        /// <summary>품번(도번)</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>           
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>납품계획일</summary>                  
        [Column("DELIV_DATE"), Required(ErrorMessage = "DelivDate")] public DateTime DelivDate { get; set; }
        /// <summary>납품계획수(중)량</summary>            
        [Column("DELIV_QTY"), Required(ErrorMessage = "DelivQty")] public decimal DelivQty { get; set; }
        /// <summary>담당자</summary>                      
        [Column("DELIV_ID")] public string DelivId { get; set; }
        /// <summary>생산의뢰</summary>                    
        [Column("PRODUCTION_FLAG"), Required(ErrorMessage = "ProductionFlag")] public string ProductionFlag { get; set; }
        /// <summary>출고의뢰</summary>                    
        [Column("OUT_CONFIRM_FLAG"), Required(ErrorMessage = "OutConfirmFlag")] public string OutConfirmFlag { get; set; }
        /// <summary>턴키의뢰</summary>                    
        [Column("TURN_KEY_FLAG"), Required(ErrorMessage = "TurnKeyFlag")] public string TurnKeyFlag { get; set; }
        /// <summary>메모</summary>                        
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                        
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                       
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                       
        [Column("TEMP2")]	public string Temp2 { get; set; }

        /// <summary> 생산계획여부 </summary>
        [NotMapped]
        public string PlanFlag
        {
            get
            {
                if (TN_MPS1100List.Count == 0) return "N";
                else
                {
                    var CheckQty = TN_MPS1100List.Sum(p => p.PlanQty);
                    if (CheckQty >= DelivQty) return "Y";
                    else return "R";
                }
            }
        }

        /// <summary> 턴키 미발주수량 </summary>
        public decimal TurnKeyRemainQty
        {
            get
            {
                if (TN_PUR1600List.Count == 0) return DelivQty;
                else return DelivQty - TN_PUR1600List.Sum(p => p.PoQty);
            }
        }

        /// <summary> 미출고수량 </summary>
        public decimal OutRemainQty
        {
            get
            {
                if (TN_ORD1200List.Count == 0) return DelivQty;
                else
                {
                    if (TN_ORD1200List.Any(p => p.TN_ORD1201List.Count > 0))
                    {
                        return DelivQty - TN_ORD1200List.Sum(p => p.TN_ORD1201List.Sum(c => c.OutQty));
                    }
                    else
                        return DelivQty;
                }
            }
        }

        public virtual TN_ORD1001 TN_ORD1001 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_ORD1101> TN_ORD1101List { get; set; }
        public virtual ICollection<TN_ORD1102> TN_ORD1102List { get; set; }
        public virtual ICollection<TN_ORD1200> TN_ORD1200List { get; set; }
        public virtual ICollection<TN_MPS1100> TN_MPS1100List { get; set; }
        public virtual ICollection<TN_PUR1600> TN_PUR1600List { get; set; }
        
    }
}