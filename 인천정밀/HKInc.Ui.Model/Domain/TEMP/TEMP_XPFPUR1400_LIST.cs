using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 외주발주 팝업 리스트
    /// </summary>
    public class TEMP_XPFPUR1400_LIST
    {
        public string _Check { get; set; }
        /// <summary>
        /// 생산계획번호
        /// </summary>
        public string PlanNo { get; set; }
        /// <summary>
        /// 작업지시번호
        /// </summary>
        public string WorkNo { get; set; }
        /// <summary>
        /// 작업지시일
        /// </summary>
        public DateTime WorkDate { get; set; }
        /// <summary>
        /// 공정순번
        /// </summary>
        public int ProcessSeq { get; set; }
        /// <summary>
        /// 공정코드
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 이동표번호
        /// </summary>
        public string ItemMoveNo { get; set; }
        /// <summary>
        /// 생산LOT
        /// </summary>
        public string ProductLotNo { get; set; }
        /// <summary>
        /// 생산수량
        /// </summary>
        public decimal WorkQty { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 설비코드
        /// </summary>
        public string MachineCode { get; set; }
        public string Memo { get; set; }
    }
}
