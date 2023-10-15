using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>용접지그점검기준관리</summary>	
    [Table("TN_WEL2001T")]
    public class TN_WEL2001 : BaseDomain.MES_BaseDomain
    {
        public TN_WEL2001()
        {
        }
        /// <summary>
        /// 용접지그코드
        /// </summary>
        [ForeignKey("TN_WEL2000"), Key, Column("WELD_CODE", Order = 0), Required(ErrorMessage = "WeldingJigCode")] public string WeldingJigCode { get; set; }
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

        public virtual TN_WEL2000 TN_WEL2000 { get; set; }
      
    }
}