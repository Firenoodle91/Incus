using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 일일생산실적 TEMP
    /// </summary>
    public class TEMP_XFMPS1301
    {
        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>작업자</summary>
        public string WorkId { get; set; }
        /// <summary>수주처</summary>
        public string CustomerCode { get; set; }
        /// <summary>작업지시수량</summary>
        public decimal? WorkQty { get; set; }
        /// <summary>설비명</summary>
        public string MachineCode { get; set; }
        /// <summary>공정명</summary>
        public string ProcessCode { get; set; }
        /// <summary>당일양품수량</summary>
        public decimal? OkQty { get; set; }
        /// <summary>당일불량수량</summary>
        public decimal? BadQty { get; set; }
        /// <summary>달성률</summary>
        public decimal? AchieveRate { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>단위</summary>
        public string Unit { get; set; }
    }
}
