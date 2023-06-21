using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 금형등급관리평가
    /// </summary>
    [Table("TN_MOLD1701T")]
    public class TN_MOLD1701 : BaseDomain.MES_BaseDomain
    {
        /// <summary> 금형등급평가번호 </summary>
        [ForeignKey("TN_MOLD1700"), Key, Column("MEA_NO", Order = 0), Required(ErrorMessage = "MeaNo")] public string MeaNo { get; set; }
        /// <summary> 설비번호 </summary>
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public decimal Seq { get; set; }
        /// <summary> 금형 평가항목 </summary>                   
        [Column("EVALUATION_ITEM")] public string EvaluationItem { get; set; }
        /// <summary> 평가값 </summary>
        [Column("IN_VALUE"), Required(ErrorMessage = "InValue")] public int? InValue { get; set; }
        /// <summary> 점수 </summary>
        [Column("SCORE"), Required(ErrorMessage = "Score")] public int Score { get; set; }

        public virtual TN_MOLD1700 TN_MOLD1700 { get; set; }

    }
}
