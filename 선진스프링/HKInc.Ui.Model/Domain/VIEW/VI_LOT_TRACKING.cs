using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>LOT추적관리 뷰</summary>	
    [Table("VI_LOT_TRACKING")]
    public class VI_LOT_TRACKING
    {        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>작업지시번호</summary>             
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>품목코드</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>공정코드</summary>             
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>             
        [Column("PROCESS_SEQ")] public int ProcessSeq { get; set; }
        /// <summary>생산 LOT NO</summary>             
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>작업일</summary>             
        [Column("RESULT_DATE")] public DateTime? ResultDate { get; set; }
        /// <summary>생산수량</summary>             
        [Column("RESULT_QTY")] public decimal? ResultQty { get; set; }
        /// <summary>양품수량</summary>             
        [Column("OK_QTY")] public decimal? OkQty { get; set; }
        /// <summary>불량수량</summary>             
        [Column("BAD_QTY")] public decimal? BadQty { get; set; }
        /// <summary>불량유형</summary>             
        [Column("BAD_TYPE")] public string BadType { get; set; }
        /// <summary>열처리온도</summary>             
        [Column("HEAT")] public decimal? Heat { get; set; }
        /// <summary>열처리RPM</summary>             
        [Column("RPM")] public decimal? Rpm { get; set; }
        /// <summary>작업자</summary>             
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary>설비코드</summary>             
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>원자재품목코드</summary>               
        [ForeignKey("TN_STD1100_SRC"), Column("SRC_CODE")] public string SrcItemCode { get; set; }
        /// <summary>원자재 LOT NO</summary>             
        [Column("SRC_IN_LOT_NO")] public string SrcInLotNo { get; set; }
        /// <summary>외주여부</summary>             
        [Column("OUT_PROC_FLAG")] public string OutProcFlag { get; set; }
        /// <summary>고객 LOT NO</summary>             
        [Column("CUSTOMER_LOT_NO")] public string CustomerLotNo { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1100 TN_STD1100_SRC { get; set; }
    }
}