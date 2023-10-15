using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>출고관리 마스터</summary>	
    [Table("TN_ORD1200T")]
    public class TN_ORD1200 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1200()
        {
            TN_ORD1201List = new List<TN_ORD1201>();
        }
        /// <summary>출고번호</summary>              
        [Key, Column("OUT_NO", Order = 0), Required(ErrorMessage = "OutNo")] public string OutNo { get; set; }
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
        /// <summary>출고일</summary>                
        [Column("OUT_DATE"), Required(ErrorMessage = "OutDate")] public DateTime OutDate { get; set; }
        /// <summary>출고예정수량</summary>              
        [Column("OUT_PLAN_QTY")] public decimal? OutPlanQty { get; set; }
        /// <summary>출고예정일</summary>              
        [Column("OUT_PLAN_DATE")] public DateTime? OutPlanDate { get; set; }
        /// <summary>출고자</summary>                
        [Column("OUT_ID")] public string OutId { get; set; }
        /// <summary>계산서월</summary>              
        [Column("Bill_DATE")] public DateTime? BillDate { get; set; }
        /// <summary>고객요청일</summary>              
        [Column("OUT_REQ_DATE")] public DateTime? OutReqDate { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>고객 LOT NO</summary>     
        [NotMapped]
        public string CustomerLotNo { get; set; }

        [NotMapped]
        public decimal? SumOutQty
        {
            get
            {
                return TN_ORD1201List.Count == 0 ? 0 : TN_ORD1201List.Sum(p => p.OutQty);
            }
        }

        /// <summary>수주번호+수주순번</summary>     
        [NotMapped]
        public string OrderNoSeq
        {
            get
            {
                return OrderNo + OrderSeq.ToString();
            }
        }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_ORD1100 TN_ORD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }

        public virtual ICollection<TN_ORD1201> TN_ORD1201List { get; set; }
    }
}