using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>일별출고관리</summary>	
    [Table("TN_ORD1101T")]
    public class TN_ORD1101 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1101()
        {
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID", Order = 0), Required(ErrorMessage = "RowId is required")] public new decimal RowId { get; set; }
        /// <summary>수주번호</summary>              
        [ForeignKey("TN_ORD1100"), Key, Column("ORDER_NO", Order = 1), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }
        /// <summary>수주순번</summary>              
        [ForeignKey("TN_ORD1100"), Key, Column("ORDER_SEQ", Order = 2), Required(ErrorMessage = "OrderSeq")] public int OrderSeq { get; set; }
        /// <summary>납품계획번호</summary>          
        [ForeignKey("TN_ORD1100"), Key, Column("DELIV_NO", Order = 3), Required(ErrorMessage = "DelivNo")] public string DelivNo { get; set; }
        /// <summary>출고증번호</summary>          
        [ForeignKey("TN_ORD1103"), Key, Column("OUT_REP_NO", Order = 4), Required(ErrorMessage = "OutRepNo")] public string OutRepNo { get; set; }
        /// <summary>품번(도번)</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>           
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        ///// <summary>출고예정수량</summary>              
        [Column("OUT_PLAN_QTY"), Required(ErrorMessage = "OutPlanQty")] public decimal? OutPlanQty { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>생산 LOT NO</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>레포트번호</summary>                 
        [NotMapped]
        public int? ReportNo { get; set; }

        /// <summary>고객사LOTNO</summary>                 
        [NotMapped]
        public string CustomerLotNo { get; set; }

        /// <summary>금액</summary>                 
        [NotMapped]
        public decimal Amt { get; set; }
           
        [NotMapped]
        public string InspAdd
        {
            get
            {
                return "추가";
            }
        }
        
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual TN_ORD1100 TN_ORD1100 { get; set; }
        public virtual TN_ORD1103 TN_ORD1103 { get; set; }
    }
}