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
    [Table("TN_WEL2003T")]
    public class TN_WEL2003 : BaseDomain.MES_BaseDomain
    {
        public TN_WEL2003()
        {
        }
        /// <summary>
        /// 용접지그이력코드
        /// </summary>
        [ForeignKey("TN_WEL2002"), Key, Column("CHECK_NO",Order =0), Required(ErrorMessage = "CheckNo")] public string CheckNo { get; set; }
        /// <summary>
        /// 검사포인트
        /// </summary>
        //[Key,Column("POINT_NO",Order =1), Required(ErrorMessage = "PointNo")] public Nullable<int> PointNo { get; set; }
        /// <summary>
        /// 2021-10-18 김진우 주임 추가
        /// 용접지그 순서 채번
        /// </summary>
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>
        /// 2021-10-18 김진우 주임 변경
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

        public virtual TN_WEL2002 TN_WEL2002 { get; set; }
        
    }
}