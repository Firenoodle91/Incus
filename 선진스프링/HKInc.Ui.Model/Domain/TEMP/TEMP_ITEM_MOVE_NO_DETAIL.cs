using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 이동표관리 디테일
    /// </summary>
    public class TEMP_ITEM_MOVE_NO_DETAIL
    {
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>외주여부</summary>
        public string OutProcFlag { get; set; }
        /// <summary>누적양품수량</summary>
        public decimal? OkSumQty { get; set; }
        /// <summary>양품수량</summary>
        public decimal? OkQty { get; set; }
        /// <summary>마지막 작업자</summary>
        public string WorkId { get; set; }
        /// <summary>작업시작일</summary>
        public DateTime? ResultStartDate { get; set; }
        /// <summary>작업종료일</summary>
        public DateTime? ResultEndDate { get; set; }
    }
}
