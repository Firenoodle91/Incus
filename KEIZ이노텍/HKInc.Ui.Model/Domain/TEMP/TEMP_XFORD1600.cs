using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 영업판매계획관리 TEMP
    /// </summary>
    public class TEMP_XFORD1600
    {
        public TEMP_XFORD1600()
        {
            Decision = "N";
        }

        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>거래처</summary>
        public string MainCustomerCode { get; set; }
        /// <summary>판매수량(3개월전)</summary>
        public Nullable<decimal> QtyMon3 { get; set; }
        /// <summary>판매수량(2개월전)</summary>
        public Nullable<decimal> QtyMon2 { get; set; }
        /// <summary>판매수량(1개월전)</summary>
        public Nullable<decimal> QtyMon1 { get; set; }
        /// <summary>3개월 평균수량</summary>
        public Nullable<decimal> QtyMonAvg { get; set; }
        /// <summary>계획수량</summary>
        public Nullable<decimal> PlanQty { get; set; }
        /// <summary>단가</summary>
        public Nullable<decimal> Cost { get; set; }
        /// <summary>실적</summary>
        public Nullable<decimal> ResultQty { get; set; }
        /// <summary>메모</summary>
        public string Memo { get; set; }

        /// <summary>확정</summary>
        [NotMapped] public string Decision { get; set; }
    }
}
