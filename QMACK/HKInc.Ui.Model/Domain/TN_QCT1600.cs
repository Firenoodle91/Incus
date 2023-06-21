using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 안돈
    /// </summary>	

    [Table("TN_QCT1600T")]
    public class TN_QCT1600 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1600() { }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column("ROW_ID", Order = 0), Required(ErrorMessage = "RowId is required")]
        //public new decimal RowId { get; set; }

        /// <summary>지시번호</summary>                  
        [Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNO")] public string WorkNo { get; set; }

        /// <summary>품목코드</summary>                  
        [Key, Column("ITEM_CODE", Order = 1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>공정코드</summary>                  
        [Key, Column("PROCESS_CODE", Order = 2), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }

        /// <summary>설비코드</summary> 
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        //[Key, Column("MACHINE_CODE", Order = 3), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }

        ///// <summary>부서구분</summary>                  
        
        /// <summary>공통코드 안돈 구분</summary>
        [Key, Column("DEP_DIVISION", Order = 3), Required(ErrorMessage = "DepDivision")] public string DepDivision { get; set; }

        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 4), Required(ErrorMessage = "Seq")] public Int32 Seq { get; set; }

        /// <summary>호출일자 2022-04-01 </summary>                      
        [Column("CALL_DATE")] public string CallDate { get; set; }

        /// <summary>점검완료여부</summary>                      
        [Column("CONFIRM_FLAG")] public string ConfirmFlag { get; set; }

        /// <summary>점검일자</summary>                      
        [Column("CHECK_DATE")] public DateTime? CheckDate { get; set; }

        /// <summary>점검내용</summary>                      
        [Column("CHECK_CONTENT")] public string CheckContent { get; set; }

        /// <summary>점검자</summary>                      
        [Column("CHECK_ID")] public string CheckId { get; set; }

    }
}