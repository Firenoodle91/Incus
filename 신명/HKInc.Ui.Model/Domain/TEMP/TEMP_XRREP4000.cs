using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 유형별 불량률 TEMP
    /// </summary>
    public class TEMP_XRREP4000
    {
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>공정명</summary>
        public string ProcessName { get; set; }
        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>불량유형코드</summary>
        public string BadTypeCode { get; set; }
        /// <summary>불량유형명</summary>
        public string BadTypeName { get; set; }
        /// <summary>불량수량</summary>
        public Nullable<decimal> BadQty { get; set; }
        /// <summary>총불량수량</summary>
        public Nullable<decimal> BadSumQty { get; set; }
        /// <summary>불량률</summary>
        public Nullable<decimal> BadRate { get; set; }
    }
}
