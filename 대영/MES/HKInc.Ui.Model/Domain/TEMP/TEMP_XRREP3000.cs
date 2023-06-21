using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 작업자별 실적 TEMP
    /// </summary>
    public class TEMP_XRREP3000
    {
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>공정명</summary>
        public string ProcessName { get; set; }
        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>품번</summary>
        public string ItemName { get; set; }
        /// <summary>품목명</summary>
        public string ItemName1 { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameEng { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameChn { get; set; }
        /// <summary>작업자ID</summary>
        public string WorkId { get; set; }
        /// <summary>작업자</summary>
        public string UserName { get; set; }
        /// <summary>생산수량</summary>
        public Nullable<decimal> ResultQty { get; set; }
        /// <summary>불량수량</summary>
        public Nullable<decimal> FQty { get; set; }
        /// <summary>양품수량</summary>
        public Nullable<decimal> OKQty { get; set; }
    }
}
