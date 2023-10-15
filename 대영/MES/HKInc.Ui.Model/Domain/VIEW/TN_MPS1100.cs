using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
   /// <summary>생산계획관리</summary>	
   [Table("TN_MPS1100T")]
    public class TN_MPS1100 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1100()
        {
            TN_MPS1200List = new List<TN_MPS1200>();
        }
        /// <summary>생산계획번호</summary>        
        [Key, Column("PLAN_NO", Order = 0), Required(ErrorMessage = "PlanNo")] public string PlanNo { get; set; }
        /// <summary>수주번호</summary>            
        [ForeignKey("TN_ORD1100"), Column("ORDER_NO", Order = 1), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }
        /// <summary>수주순번</summary>            
        [ForeignKey("TN_ORD1100"), Column("ORDER_SEQ", Order = 2), Required(ErrorMessage = "OrderSeq")] public int OrderSeq { get; set; }
        /// <summary>납품계획번호</summary>        
        [ForeignKey("TN_ORD1100"), Column("DELIV_NO", Order = 3), Required(ErrorMessage = "DelivNo")] public string DelivNo { get; set; }
        /// <summary>품번(도번)</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>           
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>생산계획수(중)량</summary>        
        [Column("PLAN_QTY"), Required(ErrorMessage = "PlanQty")] public decimal PlanQty { get; set; }
        /// <summary>생산계획시작일</summary>      
        [Column("PLAN_START_DATE"), Required(ErrorMessage = "PlanStartDate")] public DateTime PlanStartDate { get; set; }
        /// <summary>생산계획종료일</summary>      
        [Column("PLAN_END_DATE"), Required(ErrorMessage = "PlanEndDate")] public DateTime PlanEndDate { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")]	public string Temp2 { get; set; }

        /// <summary>
        /// 작업지시여부
        /// </summary>
        [NotMapped]
        public string WorkOrderFlag
        {
            get
            {
                if (TN_MPS1200List.Count == 0)
                    return "N";
                else
                    return "Y";
            }
        }

        public virtual TN_ORD1100 TN_ORD1100 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_MPS1200> TN_MPS1200List { get; set; }
    }
}