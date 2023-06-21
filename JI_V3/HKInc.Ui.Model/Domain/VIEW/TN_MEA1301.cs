using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW

{   /// 2021-05-28 김진우 주임 생성
    /// <summary>설비 등급평가 기준</summary>	
    [Table("TN_MEA1301T")]
    public class TN_MEA1301 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1301()
        {

        }

        /// <summary> 등급관리번호 </summary>
        [ForeignKey("TN_MEA1300"), Key, Column("GRADE_MANAGE_NO", Order = 0), Required(ErrorMessage = "GradeManageNo")] public string GradeManageNo { get; set; }
        /// <summary> 순번 </summary>
        [Column("SEQ", Order = 1), Key, Required(ErrorMessage = "Seq")] public decimal Seq { get; set; }
        /// <summary> 평가항목 </summary>
        [Column("EVALUATION_ITEM"), Required(ErrorMessage = "EvaluationItem")] public string EvaluationItem { get; set; }
        /// <summary> 평가기준 </summary>
        [Column("EVALUATION_STAND")] public string EvaluationStand { get; set; }
        /// <summary> 배점 </summary>
        [Column("SCORE"), Required(ErrorMessage = "Score")] public int Score { get; set; }
        /// <summary>메모</summary>                       
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary> 최대값 </summary>     
        [Column("EVALUATION_VALUE_MAX"), Required(ErrorMessage = "EvaluationValueMax")] public decimal EvaluationValueMax { get; set; }
        /// <summary> 최소값 </summary>     
        [Column("EVALUATION_VALUE_MIN"), Required(ErrorMessage = "EvaluationValueMin")] public decimal EvaluationValueMin { get; set; }
        /// <summary> 표시순서 </summary>
        //[Column("DISPLAY_ORDER")] public decimal DisplayOrder { get; set; }

        public virtual TN_MEA1300 TN_MEA1300 { get; set; }

    }
}