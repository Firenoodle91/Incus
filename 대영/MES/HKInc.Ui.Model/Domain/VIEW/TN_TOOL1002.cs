using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>툴 교체관리</summary>	
    [Table("TN_TOOL1002T")]
    public class TN_TOOL1002 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1002()
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
        /// <summary>교체순번</summary>               
        [Key, Column("CHANGE_SEQ", Order = 5), Required(ErrorMessage = "ChangeSeq")] public decimal ChangeSeq { get; set; }
        /// <summary>툴 순번</summary>               
        [Key, Column("TOOL_SEQ", Order = 6), Required] public int ToolSeq { get; set; }
        /// <summary>교체시수명</summary>               
        [Column("CHANGE_LIFE_QTY")] public decimal? ChangeLifeQty { get; set; }
        /// <summary>교체시수량</summary>               
        [Column("CHANGE_QTY")] public decimal? ChangeQty { get; set; }
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