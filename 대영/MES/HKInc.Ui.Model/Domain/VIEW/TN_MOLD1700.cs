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
    [Table("TN_MOLD1700T")]
    public class TN_MOLD1700 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1700()
        {
            TN_MOLD1701List = new List<TN_MOLD1701>();
        }

        /// <summary>관리번호</summary>
        [Key, Column("MEA_NO")] public string MeaNo { get; set; }
        /// <summary>금형코드</summary>
        [ForeignKey("TN_MOLD1100"), Column("MOLD_MCODE")] public string MoldMCode { get; set; }
        /// <summary>금형등급관리번호</summary>
        [Column("GRADE_MANAGE_NO")] public string GradeManageNo { get; set; }
        /// <summary>개정일자</summary>
        [Column("REVISION_DATE")] public DateTime RevisionDate { get; set; }
        /// <summary>평가일</summary>
        [Column("QC_DATE")] public DateTime? QcDate { get; set; }

        /// <summary>평가자</summary>
        [Column("WORK_ID")] public string WorkId { get; set; }

        /// <summary>등급</summary>
        [Column("GRADE")] public string Grade { get; set; }

        /// <summary>점수</summary>
        [Column("TOT_SCR")] public int? TotalScore { get; set; }

        public virtual TN_MOLD1100 TN_MOLD1100 { get; set; }
        public virtual ICollection<TN_MOLD1701> TN_MOLD1701List { get; set; }
    }
}
