using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>작업실적관리 디테일</summary>	
    [Table("TN_MPS1202T")]
    public class TN_MPS1202 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1202()
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
        /// <summary>실적순번</summary>               
        [Key, Column("RESULT_SEQ", Order = 4), Required(ErrorMessage = "ResultSeq")] public int ResultSeq { get; set; }
        /// <summary>품번(도번)</summary>             
        [Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>이동표번호</summary>             
        [Column("ITEM_MOVE_NO")] public string ItemMoveNo { get; set; }
        /// <summary>설비코드</summary>               
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>실적등록일</summary>             
        [Column("RESULT_INS_DATE"), Required(ErrorMessage = "ResultInsDate")] public DateTime ResultInsDate { get; set; }
        /// <summary>생산수량</summary>               
        [Column("RESULT_QTY")] public decimal? ResultQty { get; set; }
        /// <summary>양품수량</summary>               
        [Column("OK_QTY")] public decimal? OkQty { get; set; }
        /// <summary>불량수량</summary>               
        [Column("BAD_QTY")] public decimal? BadQty { get; set; }
        /// <summary>불량유형</summary>               
        [Column("BAD_TYPE")] public string BadType { get; set; }
        /// <summary>작업자</summary>                 
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary>열처리온도</summary>             
        [Column("HEAT")] public decimal? Heat { get; set; }
        /// <summary>열처리속도</summary>             
        [Column("RPM")] public decimal? Rpm { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }
        /// <summary>작업시간</summary>                  
        [Column("WORK_TIME")] public decimal? WorkTime { get; set; }

        public virtual TN_MPS1201 TN_MPS1201 { get; set; }
    }
}