using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XRREP7000
    {
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>공정명</summary>
        public string ProcessName { get; set; }
        /// <summary>품목코드</summary>
        public string ItemCode { get; set; }
        /// <summary>품번</summary>
        public string ItemName { get; set; }
        /// <summary>품목명</summary>
        public string ItemName1 { get; set; }
        /// <summary>불량수량</summary>
        public Nullable<decimal> BadQty { get; set; }
        /// <summary>리워크양품수량</summary>
        public Nullable<decimal> ReWorkOkQty { get; set; }
        /// <summary>리워크불량수량</summary>
        public Nullable<decimal> ReWorkBadQty { get; set; }
        /// <summary>불량률</summary>
        public Nullable<decimal> BadRate { get; set; }
    }
}
