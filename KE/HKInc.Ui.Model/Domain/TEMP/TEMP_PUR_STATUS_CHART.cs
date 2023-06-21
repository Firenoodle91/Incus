using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 구매현황 차트에서 사용
    /// </summary>
    public class TEMP_PUR_STATUS_CHART
    {
        public string ItemCode { get; set; }
        public string Date { get; set; }
        public string Division { get; set; }
        public decimal? Qty{ get; set; }
    }
}
