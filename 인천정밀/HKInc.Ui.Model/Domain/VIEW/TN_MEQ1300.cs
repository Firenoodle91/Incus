using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW

{   /// 2021-06-03 김진우 주임 생성
    /// <summary>설비등급관리평가 마스터</summary>	
    [Table("TN_MEQ1300T")]
    public class TN_MEQ1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MEQ1300()
        {
            TN_MEQ1301List = new List<TN_MEQ1301>();
        }

        /// <summary> 설비등급평가번호 </summary>
        [Key, Column("MEA_NO", Order = 0), Required(ErrorMessage = "MeaNo")] public string MeaNo { get; set; }
        /// <summary> 설비번호 </summary>
        [ForeignKey("TN_MEA1000"), Column("MACHINE_CODE"), Required(ErrorMessage = "MachineCode")] public string MachineMCode { get; set; }
        /// <summary> 등급관리번호 </summary>                   
        [Column("GRADE_MANAGE_NO"), Required(ErrorMessage = "GradeManageNo")] public string GradeManageNo { get; set; }
        /// <summary> 평가일자 </summary>
        [Column("QC_DATE")] public DateTime? QcDate { get; set; }
        /// <summary> 등록자 </summary>     
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary> 총합 </summary>
        [Column("TOT_SCR"), Required(ErrorMessage ="TotalScore")] public int TotalScore { get; set; }
        /// <summary> 등급 </summary>
        [Column("GRADE")] public string Grade { get; set; }
        /// <summary> 메모 </summary>
        [Column("MEMO")] public string Memo { get; set; }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        public virtual ICollection<TN_MEQ1301> TN_MEQ1301List { get; set; }
    }
}