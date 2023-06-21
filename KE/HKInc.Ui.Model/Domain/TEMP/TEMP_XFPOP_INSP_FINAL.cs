using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFPOP_INSP_FINAL
    {
        /// <summary>고유 ID</summary>
        public decimal RowId { get; set; }
        /// <summary>긴급여부</summary>
        public string EmergencyFlag { get; set; }
        /// <summary>작업상태</summary>
        public string JobStates { get; set; }
        /// <summary>작업지시번호</summary>
        public string WorkNo { get; set; }
        /// <summary>공정순번</summary>
        public int ProcessSeq { get; set; }
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>작업지시일</summary>
        public DateTime WorkDate { get; set; }
        /// <summary>설비코드</summary>
        public string MachineCode { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 20210825 오세완 차장 
        /// 품목명 추가
        /// </summary>
        public string ItemName1 { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>작업지시량</summary>
        public decimal WorkQty { get; set; }
        /// <summary>메모</summary>
        public string Memo { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>생산 LOT NO</summary>
        public string ProductLotNo { get; set; }
        /// <summary>이동표번호</summary>
        public string ItemMoveNo { get; set; }
        /// <summary>생산수량</summary>
        public decimal ResultQty { get; set; }
        /// <summary>양품수량</summary>
        public decimal OkQty { get; set; }
        /// <summary>불량수량</summary>
        public decimal BadQty { get; set; }
        /// <summary>누적생산수량</summary>
        public decimal ResultSumQty { get; set; }
        /// <summary>제조팀코드</summary>
        public string ProcTeamCode { get; set; }
    }
}
