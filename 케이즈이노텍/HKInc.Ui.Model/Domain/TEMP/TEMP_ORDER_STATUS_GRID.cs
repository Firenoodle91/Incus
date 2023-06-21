using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 수주현황 그리드
    /// </summary>
    public class TEMP_ORDER_STATUS_GRID
    {
        /// <summary>품목코드</summary>
        public string ItemCode { get; set; }
        /// <summary>품번</summary>
        public string ItemName { get; set; }
        /// <summary>품명</summary>
        public string ItemName1 { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>재고량</summary>
        public decimal? StockQty { get; set; }
        /// <summary>3개월평균출고량</summary>
        public decimal? ThirdSpendOutQty { get; set; }
        /// <summary>안전재고량</summary>
        public decimal? SafeQty { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
    }
}
