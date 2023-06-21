using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>툴 교체관리 디테일</summary>	
    [Table("TN_TOOL1003T")]
    public class TN_TOOL1003 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1003()
        {
        }
        /// <summary>작업지시번호</summary>           
        [ForeignKey("TN_MPS1201"), Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>               
        [ForeignKey("TN_MPS1201"), Key, Column("PROCESS_CODE", Order = 1), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>               
        [ForeignKey("TN_MPS1201"), Key, Column("PROCESS_SEQ", Order = 2), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>생산 LOT_NO</summary>            
        [ForeignKey("TN_MPS1201"), Key, Column("PRODUCT_LOT_NO", Order = 3), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>툴 순번</summary>               
        [Key, Column("TOOL_SEQ", Order = 6), Required] public int ToolSeq { get; set; }
        /// <summary>툴 사용수량</summary>               
        [Column("TOOL_USE_QTY")] public decimal? ToolUseQty { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MPS1201 TN_MPS1201 { get; set; }
    }
}