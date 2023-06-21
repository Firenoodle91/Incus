using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW

{   /// 2021-06-03 김진우 주임 생성
    /// <summary>설비등급관리평가 디테일</summary>	
    [Table("TN_MEQ1301T")]
    public class TN_MEQ1301 : BaseDomain.MES_BaseDomain
    {
        public TN_MEQ1301()
        {

        }

        /// <summary> 설비등급평가번호 </summary>
        [ForeignKey("TN_MEQ1300"),  Key, Column("MEA_NO", Order = 0), Required(ErrorMessage = "MeaNo")] public string MeaNo { get; set; }
        /// <summary> 설비등급평가번호 순번 </summary>
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public decimal Seq { get; set; }
        /// <summary> 설비 평가항목 </summary>                   
        [Column("EVALUATION_ITEM")] public string EvaluationItem { get; set; }
        /// <summary> 평가값 </summary>
        [Column("IN_VALUE"), Required(ErrorMessage = "MeaNo")] public int? InValue { get; set; }
        /// <summary> 점수 </summary>
        [Column("SCORE"), Required(ErrorMessage ="Score")] public int Score { get; set; }
        
        public virtual TN_MEQ1300 TN_MEQ1300 { get; set; }
    }
}