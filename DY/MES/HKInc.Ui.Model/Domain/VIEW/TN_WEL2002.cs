using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>용접지그이력관리</summary>	
    [Table("TN_WEL2002T")]
    public class TN_WEL2002 : BaseDomain.MES_BaseDomain
    {
        public TN_WEL2002()
        {
            TN_WEL2003List = new List<TN_WEL2003>();
        }
        /// <summary>
        /// 지그코드
        /// </summary>
        [ForeignKey("TN_WEL2000"), Column("WELD_CODE", Order = 0), Required(ErrorMessage = "WeldingJigCode")] public string WeldingJigCode { get; set; }
        /// <summary>
        /// 용접지그이력코드
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

        public virtual TN_WEL2000 TN_WEL2000 { get; set; }
        public virtual ICollection<TN_WEL2003> TN_WEL2003List { get; set; }
    }
}