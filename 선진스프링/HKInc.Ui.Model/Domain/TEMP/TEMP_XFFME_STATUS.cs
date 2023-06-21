using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 초중종현황판 TEMP
    /// </summary>
    public class TEMP_XFFME_STATUS
    {
        /// <summary>품번</summary>
        public string ItemCode { get; set; }
        /// <summary>품번</summary>
        public string ItemName { get; set; }
        /// <summary>작업지시번호</summary>
        public string WorkNo { get; set; }
        /// <summary>생산LotNo</summary>
        public string ProductLotNo { get; set; }
        /// <summary>생산수량</summary>
        public decimal? ResultQty { get; set; }
        /// <summary>작업일</summary>
        public DateTime WorkDate { get; set; }
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>설비코드(설비명)</summary>
        public string MachineCode { get; set; }
        /// <summary>초물검사결과</summary>
        public string FirstResult { get; set; }
        /// <summary>중물검사결과</summary>
        public string MidResult { get; set; }
        /// <summary>종물검사결과</summary>
        public string EndResult { get; set; }
        /// <summary>설비코드</summary>
        public string MachineMcode { get; set; }
        /// <summary></summary>
        public Int64 RowNum { get; set; }
    }
}
