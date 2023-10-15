using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 매출실적관리 TEMP
    /// </summary>
    public class TEMP_XRREP5002
    {
        /// <summary>거래처</summary>
        public string CustomerCode { get; set; }
        public decimal? CarryOverCost { get; set; }
        public decimal? TodayCost { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
