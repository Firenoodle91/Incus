using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사구이력관리</summary>	
    [Table("TN_CHF2002T")]
    public class TN_CHF2002 : BaseDomain.MES_BaseDomain
    {
        public TN_CHF2002()
        {
            TN_CHF2003List = new List<TN_CHF2003>();
        }
        /// <summary>
        /// 검사구코드
        /// </summary>
        [ForeignKey("TN_CHF2000"), Column("CHFI_CODE", Order = 0), Required(ErrorMessage = "CheckerFixCode")] public string CheckerFixCode { get; set; }
        /// <summary>
        /// 검사구이력 코드
        /// </summary>
        [Key, Column("CHECK_NO", Order = 1), Required(ErrorMessage = "CheckNo")] public string CheckNo { get; set; }
        /// <summary>
        /// 검사일
        /// </summary>
        [Column("CHECK_DATE")] public Nullable<DateTime> CheckDate { get; set; }
        /// <summary>
        /// 검사자
        /// </summary>
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>
        /// 평가
        /// </summary>
        [Column("JUDGEMENT")] public string Judgement { get; set; }
        /// <summary>
        /// 검사방법
        /// </summary>
        [Column("CHECK_TYPE")] public string CheckType { get; set; }
        /// <summary>
        /// 스펙상한
        /// </summary>
        [Column("SPEC_UP")] public string SpecUp { get; set; }
        /// <summary>
        /// 스펙하한
        /// </summary>
        [Column("SPEC_DOWN")] public string SpecDown { get; set; }

        public virtual TN_CHF2000 TN_CHF2000 { get; set; }
        public virtual ICollection<TN_CHF2003> TN_CHF2003List { get; set; }
    }
}