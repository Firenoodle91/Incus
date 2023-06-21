using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비이력관리</summary>	
    [Table("TN_MPS1302T")]
    public class TN_MPS1302 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1302()
        {
        }
        /// <summary>설비고유코드</summary>         
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_MCODE", Order = 0), Required(ErrorMessage = "MachineMCode")] public string MachineMCode { get; set; }
        /// <summary>고유ID</summary>        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID", Order = 1)] public new decimal RowId { get; set; }
        /// <summary>일자</summary>           
        [Column("DATE")] public DateTime Date { get; set; }
        /// <summary>작업지시번호</summary>         
        [ForeignKey("TN_MPS1200"), Column("WORK_NO", Order = 2)] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>         
        [ForeignKey("TN_MPS1200"), Column("PROCESS_CODE", Order = 3)] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>         
        [ForeignKey("TN_MPS1200"), Column("PROCESS_SEQ", Order = 4)] public int? ProcessSeq { get; set; }
        /// <summary>계획정지</summary>         
        [Column("PLAN_STOP")] public decimal? PlanStop { get; set; }
        /// <summary>품번교체</summary>         
        [Column("ITEM_CODE_CHANGE")] public decimal? ItemCodeChange { get; set; }
        /// <summary>자재품절</summary>         
        [Column("SRC_SOLD_OUT")] public decimal? SrcSoldOut { get; set; }
        /// <summary>재조정</summary>         
        [Column("READJUST")] public decimal? ReAdjust { get; set; }
        /// <summary>기계고장</summary>         
        [Column("MACHINE_BROKEN")] public decimal? MachineBroken { get; set; }
        /// <summary>공수부족</summary>         
        [Column("WORK_LACK")] public decimal? WorkLack { get; set; }
        /// <summary>기타</summary>         
        [Column("ETC")] public decimal? Etc { get; set; }        
        /// <summary>메모</summary>             
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>             
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>            
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>            
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        public virtual TN_MPS1200 TN_MPS1200 { get; set; }
    }
}