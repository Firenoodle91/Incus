using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW

{   /// 2021-05-28 김진우 주임 생성
    /// <summary>설비 등급기준점수</summary>	
    [Table("TN_MEA1302T")]
    public class TN_MEA1302 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1302()
        { }

        /// <summary>  </summary>
        [Column("GRADE_STAND_CODE"), Key, Required(ErrorMessage = "GradeStandCode")] public string GradeStandCode { get; set; }
        /// <summary> 등급기준 </summary>
        [Column("GRADE_STAND"), Required(ErrorMessage = "GradeStand")] public string GradeStand { get; set; }
        /// <summary> 최대값 </summary>
        [Column("GRADE_VALUE_MAX"), Required(ErrorMessage = "GradeValueMax")] public int GradeValueMax { get; set; }
        /// <summary> 최소값 </summary>
        [Column("GRADE_VALUE_MIN"), Required(ErrorMessage = "GradeValueMin")] public int GradeValueMin { get; set; }
        /// <summary> 표시순서 </summary>
        //[Column("DISPLAY_ORDER")] public decimal DisplayOrder { get; set; }

    }
}