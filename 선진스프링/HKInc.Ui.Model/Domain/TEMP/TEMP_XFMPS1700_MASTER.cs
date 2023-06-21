using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 재공재고현황 마스터
    /// </summary>
    public class TEMP_XFMPS1700_MASTER
    {
        /// <summary>작업지시번호</summary>
        public string WorkNo { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>발행일</summary>
        public DateTime? PublishDate { get; set; }
        /// <summary>초기납기요청일</summary>
        public DateTime? StartDueDate { get; set; }
        /// <summary>생산완료요청일</summary>
        public DateTime? FinishDueDate { get; set; }
        /// <summary>기준월</summary>
        public DateTime? EndMonthDate { get; set; }
        /// <summary>작업의뢰수량</summary>
        public decimal? PlanWorkQty { get; set; }
        /// <summary>포장수량</summary>
        public decimal? PackQty { get; set; }
        /// <summary>비고</summary>
        public string Memo { get; set; }
    }
}
