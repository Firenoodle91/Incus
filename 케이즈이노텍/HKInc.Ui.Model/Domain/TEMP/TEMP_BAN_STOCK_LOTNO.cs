using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 반제품 ITEM, LOT 별 재고
    /// </summary>
    public class TEMP_BAN_STOCK_LOTNO
    {
        /// <summary>
        /// 반제품 생산LOTNO
        /// </summary>
        public string ProductLotNo { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        public string ItemCode { get; set; }
        public string InNo { get; set; }
        public int InSeq { get; set; }
        /// <summary>
        /// 재고수량
        /// </summary>
        public decimal StockQty { get; set; }
    }
}
