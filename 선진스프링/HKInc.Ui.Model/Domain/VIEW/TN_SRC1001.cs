using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재재공재고 재고조정</summary>	
    [Table("TN_SRC1001T")]
    public class TN_SRC1001 : BaseDomain.MES_BaseDomain2
    {
        public TN_SRC1001()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID"), Required(ErrorMessage = "RowId is required")]
        public decimal RowId { get; set; }
        /// <summary>재고조정일</summary>                  
        [Column("STOCK_ADJUST_DATE"), Required(ErrorMessage = "StockAdjustDate")] public DateTime StockAdjustDate { get; set; }
        /// <summary>원자재코드</summary>                    
        [Column("SRC_CODE"), Required(ErrorMessage = "SrcCode")] public string SrcCode { get; set; }
        /// <summary>입고수량</summary>                          
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>사용수량</summary>                          
        [Column("USE_QTY"), Required(ErrorMessage = "UseQty")] public decimal UseQty { get; set; }
        /// <summary>메모</summary>                        
        [Column("MEMO")] public string Memo { get; set; }
    }
}