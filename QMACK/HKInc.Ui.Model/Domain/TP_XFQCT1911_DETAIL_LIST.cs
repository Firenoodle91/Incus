using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220531 오세완 차장 
    /// USP_GET_XFQCT1911_DETAIL 반환 객체 
    /// </summary>
    public class TP_XFQCT1911_DETAIL_LIST
    {
        public TP_XFQCT1911_DETAIL_LIST()
        {

        }

        public string OUT_NO { get; set; }

        public Int32 OUT_NO_SEQ { get; set; }

        public string OUT_LOT_NO { get; set; }

        public string WORK_NO { get; set; }

        public Int32 SEQ { get; set; }

        public string ITEM_CODE { get; set; }

        public string ITEM_NM { get; set; }

        public string ITEM_NM1 { get; set; }

        public string PROCESS_CODE { get; set; }

        public string PROCESS_NAME { get; set; }

        public Int32? PROCESS_TURN { get; set; }

        public string LOT_NO { get; set; }

        public DateTime? START_DATE { get; set; }

        public DateTime? END_DATE { get; set; }

        public DateTime? RESULT_DATE { get; set; }

        public Int32? RESULT_QTY { get; set; }

        public Int32? OK_QTY { get; set; }

        public Int32? FAIL_QTY { get; set; }

        public string WORK_ID { get; set; }

        public string WORKER_NAME { get; set; }

        public string MACHINE_CODE { get; set; }

        public string MACHINE_NAME { get; set; }

        public decimal? SRC_SEQ { get; set; }

        public string SRC_CODE { get; set; }

        public string SRC_NM { get; set; }

        public string SRC_NM1 { get; set; }

        public string SRC_IN_LOT_NO { get; set; }
    }
}
