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
    /// 20211222 오세완 차장
    /// 반제품재고조정 테이블
    /// </summary>
    [Table("TN_BAN_STOCK_ADJUST")]
    public class TN_BAN_STOCK_ADJUST : BaseDomain.MES_BaseDomain
    {
        public TN_BAN_STOCK_ADJUST()
        {

        }

        /// <summary>
        /// 20211222 오세완 차장
        /// 입/출고구분
        /// </summary>
        [Key, Column("STOCK_TYPE", Order = 0)]
        public string StockType { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 조정일자
        /// </summary>
        [Key, Column("INOUT_DATE", Order = 1)]
        public DateTime InoutDate { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// lotno
        /// </summary>
        [Key, Column("LOTNO", Order = 2)]
        public string LotNo { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 품목코드
        /// </summary>
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 조정수량
        /// </summary>
        [Column("QTY")]
        public decimal? Qty { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 재고조정자
        /// </summary>
        [Column("STOCK_USER")]
        public string StockUser { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 메모
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }
    }
}
