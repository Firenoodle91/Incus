using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>설비점검이력관리 / 예방보전</summary>	
    [Table("TN_MEA1003T")]
    public class TN_MEA1003 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1003()
        {
        }

        /// <summary>설비코드</summary>              
        //[Key, Column("MACHINE_CODE", Order = 0), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        [ForeignKey("TN_MEA1002"), Key, Column("MACHINE_CODE", Order = 0), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        
        /// <summary>점검순번</summary>
        //[Key, Column("CHECK_SEQ", Order = 2), Required(ErrorMessage = "CheckSeq")] public int CheckSeq { get; set; }
        [ForeignKey("TN_MEA1002"), Key, Column("CHECK_SEQ", Order = 1), Required(ErrorMessage = "CheckSeq")] public int CheckSeq { get; set; }
        
        /// <summary>점검일</summary>
        [Key, Column("CHECK_DATE", Order = 2), Required(ErrorMessage = "CheckDate")] public DateTime CheckDate { get; set; }
        
        /// <summary>점검자</summary>                
        [Column("CHECK_ID")] public string CheckId { get; set; }
        
        /// <summary>점검값</summary>                
        [Column("CHECK_VALUE")] public string CheckValue { get; set; }
        
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MEA1002 TN_MEA1002 { get; set; }
    }
}