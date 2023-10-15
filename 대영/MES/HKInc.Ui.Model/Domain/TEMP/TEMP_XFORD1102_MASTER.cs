using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 납기회의목록
    /// </summary>
    public class TEMP_XFORD1102_MASTER
    {
        /// <summary>납품상태</summary>
        public string DelivStates { get; set; }
        /// <summary>수주번호</summary>
        public string OrderNo { get; set; }
        /// <summary>수주순번</summary>
        public int OrderSeq { get; set; }
        /// <summary>납품계획번호</summary>
        public string DelivNo { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>제조팀</summary>
        public string ProcTeamCode { get; set; }
        /// <summary>수주일</summary>
        public DateTime OrderDate { get; set; }
        /// <summary>납품계획일</summary>
        public DateTime DelivDate { get; set; }
        /// <summary>수주수량</summary>
        public decimal? OrderQty { get; set; }
        /// <summary>납품계획담당자</summary>
        public string DelivId { get; set; }
        /// <summary>수주담당자</summary>
        public string OrderId { get; set; }
        /// <summary>작업상태</summary>
        public string JobStates { get; set; }
        /// <summary>변경수량</summary>
        public int? ChangeQty { get; set; }
        /// <summary>생산수량</summary>
        public decimal? ResultQty { get; set; }
        /// <summary>양품수량</summary>
        public decimal? OkQty { get; set; }
        /// <summary>불량수량</summary>
        public decimal? BadQty { get; set; }
        /// <summary>포장생산수량</summary>
        public decimal? PackResultQty { get; set; }
        /// <summary>포장양품수량</summary>
        public decimal? PackOkQty { get; set; }
        /// <summary>포장불량수량</summary>
        public decimal? PackBadQty { get; set; }
    }
}
