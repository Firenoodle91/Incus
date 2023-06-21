using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 작업지시대비실적 TEMP
    /// </summary>
    public class TEMP_XRREP1000
    {
        /// <summary>품목코드</summary>
        public string ItemCode { get; set; }
        /// <summary>품목</summary>
        public string ItemName { get; set; }
        /// <summary>품번명</summary>
        public string ItemName1 { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameEng { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameChn { get; set; }
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>공정명</summary>
        public string ProcessName { get; set; }
        /// <summary>지시수량</summary>
        public decimal? WorkQty { get; set; }
        /// <summary>실적수량</summary>
        public decimal? ResultQty { get; set; }
        /// <summary>달성률</summary>
        public decimal? AchieveRate { get; set; }
    }
}
