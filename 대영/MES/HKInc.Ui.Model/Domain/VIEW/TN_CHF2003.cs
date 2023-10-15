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
    [Table("TN_CHF2003T")]
    public class TN_CHF2003 : BaseDomain.MES_BaseDomain
    {
        public TN_CHF2003()
        {
        }
        /// <summary>
        /// 검사구이력 코드
        /// </summary>
        [ForeignKey("TN_CHF2002"), Key, Column("CHECK_NO",Order =0), Required(ErrorMessage = "CheckNo")] public string CheckNo { get; set; }
        /// <summary>
        /// 2021-10-12 김진우 주임 추가
        /// 검사구이력 순서 채번
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
        /// 평가값
        /// </summary>
        [Column("INVALUE")] public string Invalue { get; set; }
        /// <summary>
        /// 평가
        /// </summary>
        [Column("JUDGEMENT")] public string Judgement { get; set; }

        public virtual TN_CHF2002 TN_CHF2002 { get; set; }
        
    }
}