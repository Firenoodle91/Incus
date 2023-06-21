using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 납기준수 TEMP
    /// </summary>
    public class TEMP_XRREP2000
    {
        /// <summary>수주번호</summary>
        public string OrderNo { get; set; }
        /// <summary>순번</summary>
        public int Seq { get; set; }
        /// <summary>거래처코드</summary>
        public string CustCode { get; set; }
        /// <summary>거래처명</summary>
        public string CustNm { get; set; }
        /// <summary>품목코드</summary>
        public string ItemCode { get; set; }
        /// <summary>품번</summary>
        public string ItemName { get; set; }
        /// <summary>품목명</summary>
        public string ItemName1 { get; set; }
        /// <summary>품목명(영문)</summary>
        public string ItemNameEng { get; set; }
        /// <summary>품목명(중문)</summary>
        public string ItemNameChn { get; set; }
        /// <summary>수주수량</summary>
        public Nullable<decimal> OrderQty { get; set; }
        /// <summary>납기일</summary>
        public Nullable<DateTime> DueDate { get; set; }
        /// <summary>출고일</summary>
        public Nullable<DateTime> OutDate { get; set; }
        /// <summary>납기준수출고수량</summary>
        public Nullable<decimal> OkQty { get; set; }
        /// <summary>납기미준수출고수량</summary>
        public Nullable<decimal> FQty { get; set; }
        /// <summary>미납품수량</summary>
        public Nullable<decimal> MQty { get; set; }
        /// <summary>납품률</summary>
        public Nullable<decimal> Rat1 { get; set; }
        /// <summary></summary>
        public string Rat { get; set; }
        /// <summary></summary>
        public string TRat { get; set; }
    }
}
