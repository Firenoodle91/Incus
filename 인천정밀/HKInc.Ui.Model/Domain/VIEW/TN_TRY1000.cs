using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>TRY관리</summary>	
    [Table("TN_TRY1000T")]
    public class TN_TRY1000 : BaseDomain.MES_BaseDomain
    {
        public TN_TRY1000()
        {
        }
        /// <summary>품번(도번)</summary>            
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>공정</summary>                  
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>발생일</summary>                
        [Column("OCCUR_DATE")] public DateTime? OccurDate { get; set; }
        /// <summary>발생사항</summary>              
        [Column("OCCUR_MEMO")] public string OccurMemo { get; set; }
        /// <summary>조치사항</summary>              
        [Column("ACTION_MEMO")] public string ActionMemo { get; set; }
        /// <summary>조치예정일</summary>            
        [Column("ACTION_DATE")] public DateTime? ActionDate { get; set; }
        /// <summary>거래처코드</summary>            
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>설비코드</summary>              
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>TRY작업자</summary>             
        [Column("TRY_ID")] public string TryId { get; set; }
        /// <summary>TRY수량</summary>               
        [Column("TRY_QTY")] public decimal? TryQty { get; set; }
        /// <summary>TRY시간</summary>               
        [Column("TRY_TIME")] public string TryTime { get; set; }
        /// <summary>외관</summary>                  
        [Column("QC1")] public string Qc1 { get; set; }
        /// <summary>치수</summary>                  
        [Column("QC2")] public string Qc2 { get; set; }
        /// <summary>TRY결과</summary>               
        [Column("TRY_RESULT")] public string TryResult { get; set; }
        /// <summary>승인여부</summary>              
        [Column("CONFIRM_FLAG")] public string ConfirmFlag { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}