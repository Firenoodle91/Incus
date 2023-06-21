using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>작업실적 일시정지관리</summary>	
    [Table("TN_MPS1203T")]
    public class TN_MPS1203 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1203()
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID", Order = 4), Required(ErrorMessage = "RowId")] public new decimal RowId { get; set; }        
        /// <summary>설비코드</summary>               
        [ForeignKey("TN_MEA1000"), Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>일시정지 시작일</summary>             
        [Column("PAUSE_START_DATE")] public DateTime? PauseStartDate { get; set; }
        /// <summary>일시정지 종료일</summary>             
        [Column("PAUSE_END_DATE")] public DateTime? PauseEndDate { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MPS1201 TN_MPS1201 { get; set; }
        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
    }
}