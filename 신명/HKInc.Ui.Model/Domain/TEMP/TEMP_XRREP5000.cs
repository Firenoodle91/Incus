using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 월별매출관리 TEMP
    /// </summary>
    public class TEMP_XRREP5000
    {
        /// <summary>거래처</summary>
        public string CustomerCode { get; set; }
        /// <summary>영업담당자</summary>
        public string ManagerId { get; set; }
        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>수주단가</summary>
        public Nullable<decimal> OrderCost { get; set; }
        /// <summary>출고수량</summary>
        public Nullable<decimal> Qty { get; set; }
        /// <summary>매출액</summary>
        public Nullable<decimal> Sales { get; set; }
    }
}
