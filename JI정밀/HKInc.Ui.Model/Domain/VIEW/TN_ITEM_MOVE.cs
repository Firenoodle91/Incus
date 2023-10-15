using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>이동표관리</summary>	
    [Table("TN_ITEM_MOVE")]
    public class TN_ITEM_MOVE : BaseDomain.MES_BaseDomain
    {
        public TN_ITEM_MOVE()
        {

        }
        /// <summary>이동표번호</summary>               
        [Key, Column("ITEM_MOVE_NO", Order = 0), Required(ErrorMessage = "ItemMoveNo")] public string ItemMoveNo { get; set; }
        /// <summary>작업지시번호</summary>             
        [Key, Column("WORK_NO", Order = 1), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>                 
        [Key, Column("PROCESS_CODE", Order = 2), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>                 
        [Key, Column("PROCESS_SEQ", Order = 3), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>생산 LOT_NO</summary>              
        [Key, Column("PRODUCT_LOT_NO", Order = 4), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>박스내수량</summary>               
        [Column("BOX_IN_QTY")] public decimal? BoxInQty { get; set; }
        /// <summary>총생산수량</summary>               
        [Column("RESULT_SUM_QTY")] public decimal? ResultSumQty { get; set; }
        /// <summary>총양품수량</summary>               
        [Column("OK_SUM_QTY")] public decimal? OkSumQty { get; set; }
        /// <summary>총불량수량</summary>               
        [Column("BAD_SUM_QTY")] public decimal? BadSumQty { get; set; }
        /// <summary>생산수량</summary>               
        [Column("RESULT_QTY")] public decimal? ResultQty { get; set; }
        /// <summary>양품수량</summary>               
        [Column("OK_QTY")] public decimal? OkQty { get; set; }
        /// <summary>불량수량</summary>               
        [Column("BAD_QTY")] public decimal? BadQty { get; set; }
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