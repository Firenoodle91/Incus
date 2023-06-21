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
    /// 출하검사 디테일
    /// </summary>
    [Table("TN_QCT1901T")]
    public class TN_QCT1901 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1901()
        {

        }

        /// <summary>
        /// 출하검사번호
        /// </summary>
        [Key, Column("SI_NO", Order = 0)]
        public string ShipmentInspectionNo { get; set; }

        /// <summary>
        /// 출하검사 순번
        /// </summary>
        [Key, Column("SI_SEQ", Order = 1)]
        public int ShipmentInspectionSeq { get; set; }

        /// <summary>
        /// 생산lotno
        /// </summary>
        [Column("PRODUCT_LOT_NO")]
        public string ProductLotNo { get; set; }

        /// <summary>
        /// 출고량
        /// </summary>
        [Column("OUT_QTY")]
        public decimal? OutQty { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }

        /// <summary>
        /// 검사번호
        /// </summary>
        [ForeignKey("TN_QCT1100"), Column("INSP_NO")]
        public string InspNo { get; set; }

        public virtual TN_QCT1900 TN_QCT1900 { get; set; }
        
        public virtual TN_QCT1100 TN_QCT1100 { get; set; }
    }
}
