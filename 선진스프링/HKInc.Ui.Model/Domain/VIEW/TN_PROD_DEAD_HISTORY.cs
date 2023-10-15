using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>제품재고마감이력</summary>	
    [Table("TN_PROD_DEAD_HISTORY")]
    public class TN_PROD_DEAD_HISTORY : BaseDomain.MES_BaseDomain
    {
        public TN_PROD_DEAD_HISTORY()
        {
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID", Order = 0), Required(ErrorMessage = "RowId is required")] public new decimal RowId { get; set; }
        /// <summary>마감일자</summary>             
        [Column("DEADLINE_DATE"), Required(ErrorMessage = "DeadLineDate")] public DateTime DeadLineDate { get; set; }
        /// <summary>실마감일자</summary>           
        [Column("REAL_DEADLINE_DATE"), Required(ErrorMessage = "RealDeadLineDate")] public DateTime RealDeadLineDate { get; set; }
        /// <summary>구분</summary>           
        [Column("DIVISION"), Required(ErrorMessage = "Division")] public string Division { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }
    }
}