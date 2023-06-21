using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 수주현황 차트
    /// </summary>
    public class TEMP_ORDER_STATUS_CHART
    {
        public string Division { get; set; }
        public string Date { get; set; }
        public decimal? Qty{ get; set; }
    }
}
