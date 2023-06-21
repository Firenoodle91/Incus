using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 구매미입고현황
    /// </summary>
    public class TEMP_XFPUR_NOT_IN_STATUS
    {
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>발주량</summary>
        public decimal PoQty { get; set; }
        /// <summary>입고량</summary>
        public decimal InQty { get; set; }
        /// <summary>미입고량</summary>
        public decimal NotInQty { get; set; }
    }
}
