using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW

{   /// 2021-05-28 김진우 주임 생성
    /// <summary>설비 등급관리</summary>	
    [Table("TN_MEA1300T")]
    public class TN_MEA1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1300()
        {
            TN_MEA1301List = new List<TN_MEA1301>();
            TN_MEQ1300List = new List<TN_MEQ1300>();

        }

        /// <summary> 등급관리번호 </summary>                   
        [Column("GRADE_MANAGE_NO"), Key, Required(ErrorMessage = "GradeManageNo")] public string GradeManageNo { get; set; }
        /// <summary> 개정일자 </summary>
        [Column("REV_DATE")] public DateTime RevDate { get; set; }
        /// <summary> 등록일자 </summary>
        [Column("REG_DATE")] public DateTime RegDate { get; set; }
        /// <summary> 등록자 </summary>     
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary> 사용여부 </summary>
        [Column("USE_FLAG"), Required(ErrorMessage ="UseFlag")] public string UseFlag { get; set; }
        /// <summary>메모</summary>                       
        [Column("MEMO")] public string Memo { get; set; }
        
        public virtual ICollection<TN_MEA1301> TN_MEA1301List { get; set; }
        public virtual ICollection<TN_MEQ1300> TN_MEQ1300List { get; set; }
    }
}