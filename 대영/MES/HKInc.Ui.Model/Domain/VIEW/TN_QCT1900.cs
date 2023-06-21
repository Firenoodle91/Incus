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
    /// 20210917 오세완 차장
    /// 출하검사 마스터
    /// </summary>
    [Table("TN_QCT1900T")]
    public class TN_QCT1900 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1900()
        {
            TN_QCT1901List = new List<TN_QCT1901>();
        }

        /// <summary>
        /// 출하검사번호
        /// </summary>
        [Key, Column("SI_NO", Order = 0)]
        public string ShipmentInspectionNo { get; set; }

        /// <summary>
        /// 출고번호
        /// </summary>
        [Column("OUT_NO")]
        public string OutNo { get; set; }

        /// <summary>
        /// 검사일
        /// </summary>
        [Column("CHECK_DATE")]
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }

        public virtual ICollection<TN_QCT1901> TN_QCT1901List { get; set; }
    }
}
