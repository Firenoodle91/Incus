using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>납기회의관리</summary>	
    [Table("TN_ORD1102T")]
    public class TN_ORD1102 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1102()
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
        /// <summary>회의일</summary>                
        [Column("CONFERENCE_DATE"), Required(ErrorMessage = "ConferenceDate")] public DateTime ConferenceDate { get; set; }
        /// <summary>변경전납품계획일</summary>                
        [Column("BEFORE_DELIV_DATE"), Required(ErrorMessage = "BeforeDelivDate")] public DateTime BeforeDelivDate { get; set; }
        /// <summary>변경후납품계획일</summary>                
        [Column("AFTER_DELIV_DATE"), Required(ErrorMessage = "AfterDelivDate")] public DateTime AfterDelivDate { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_ORD1100 TN_ORD1100 { get; set; }
    }
}