using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>교육계획관리</summary>	
    [Table("TN_QCT1700T")]
    public class TN_QCT1700 : BaseDomain.MES_BaseDomain2
    {
        public TN_QCT1700()
        {
        }
        /// <summary>순번</summary>                
        [Key, Column("SEQ", Order = 0), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>교육구분</summary>                   
        [Column("EDU_FLAG"), Required(ErrorMessage = "EduFlag")] public string EduFlag { get; set; }
        /// <summary>교육항목</summary>                   
        [Column("EDU_CONTENT"), Required(ErrorMessage = "EduContent")] public string EduContent { get; set; }
        /// <summary>교육기관</summary>                   
        [Column("EDU_ORGAN")] public string EduOrgan { get; set; }
        /// <summary>교육대상</summary>                   
        [Column("EDU_OBJ")] public string EduObj { get; set; }
        /// <summary>교육시간</summary>                   
        [Column("EDU_TIME")] public string EduTime { get; set; }
        /// <summary>교육계획시작일자</summary>                   
        [Column("EDU_PLAN_START"), Required(ErrorMessage = "EduPlanStart")] public DateTime EduPlanStart { get; set; }
        /// <summary>교육계획종료일자</summary>                   
        [Column("EDU_PLAN_END"), Required(ErrorMessage = "EduPlanEnd")] public DateTime EduPlanEnd { get; set; }
        /// <summary>소요예산</summary>                   
        [Column("EDU_BUDGET")] public decimal? EduBudget { get; set; }
        /// <summary>교육시간일자</summary>                    
        [Column("EDU_START")] public DateTime EduStart { get; set; }
        /// <summary>교육종료일자</summary>                   
        [Column("EDU_END")] public DateTime EduEnd { get; set; }
        /// <summary>담당자</summary>                   
        [Column("EDU_ID")] public string EduId { get; set; }
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