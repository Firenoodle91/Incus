using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사구기준관리</summary>	
    [Table("TN_CHF2001T")]
    public class TN_CHF2001 : BaseDomain.MES_BaseDomain
    {
        public TN_CHF2001()
        {
        }
        /// <summary>
        /// 검사구코드
        /// </summary>
        [ForeignKey("TN_CHF2000"), Key, Column("CHFI_CODE", Order = 0), Required(ErrorMessage = "CheckerFixCode")] public string CheckerFixCode { get; set; }
        /// <summary>
        /// 순번
        /// </summary>
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>
        /// 검사포인트
        /// </summary>
        [Column("POINT_NO")] public string PointNo { get; set; }
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
        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }
   
        public virtual TN_CHF2000 TN_CHF2000 { get; set; }
      
    }
}