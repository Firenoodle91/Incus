using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 기간별입고현황
    /// </summary>
    public class TEMP_XFPUR_IN_STATUS
    {
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>거래처명</summary>
        public string CustomerName { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 20210924 오세완 차장
        /// 품목명 추가
        /// </summary>
        public string ItemName1 { get; set; }
        /// <summary>입고량</summary>
        public decimal InQty { get; set; }
    }
}
