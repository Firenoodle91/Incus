using System;

namespace HKInc.Ui.Model.Domain
{
    public class TP_XFQCT1910_DETAIL
    {
        public TP_XFQCT1910_DETAIL()
        {

        }

        public string WORK_NO { get; set; }

        /// <summary>
        /// int인데 int64는 오류라서 변경?
        /// </summary>
        public Int32 SEQ { get; set; }

        public string ITEM_CODE { get; set; }

        public string PROCESS_CODE { get; set; }

        public string PROCESS_NAME { get; set; }

        /// <summary>
        /// int인데 int64는 오류라서 변경?
        /// </summary>
        public Int32 PROCESS_TURN { get; set; }

        public string LOT_NO { get; set; }

        public DateTime? START_DATE { get; set; }

        public DateTime? END_DATE { get; set; }

        public DateTime? RESULT_DATE { get; set; }

        /// <summary>
        /// int인데 int64는 오류라서 변경?
        /// </summary>
        public Int32 RESULT_QTY { get; set; }

        /// <summary>
        /// int인데 int64는 오류라서 변경?
        /// </summary>
        public Int32 FAIL_QTY { get; set; }

        /// <summary>
        /// int인데 int64는 오류라서 변경?
        /// </summary>
        public Int32 OK_QTY { get; set; }

        public string WORK_ID { get; set; }

        public string WORK_NAME { get; set; }

        public string MC_CODE { get; set; }

        public string MACHINE_NAME { get; set; }

        public string SRC_CODE { get; set; }

        public string SRC_NM { get; set; }

        public string SRC_NM1 { get; set; }

        public string SRC_LOT { get; set; }

        public string SRC_CODE1 { get; set; }

        public string SRC1_NM { get; set; }

        public string SRC1_NM1 { get; set; }

        public string SRC_LOT1 { get; set; }

        public string SRC_CODE2 { get; set; }

        public string SRC2_NM { get; set; }

        public string SRC2_NM1 { get; set; }

        public string SRC_LOT2 { get; set; }

        public string SRC_CODE3 { get; set; }

        public string SRC3_NM { get; set; }

        public string SRC3_NM1 { get; set; }

        public string SRC_LOT3 { get; set; }

        public string SRC_CODE4 { get; set; }

        public string SRC4_NM { get; set; }

        public string SRC4_NM1 { get; set; }

        public string SRC_LOT4 { get; set; }

        public string SRC_CODE5 { get; set; }

        public string SRC5_NM { get; set; }

        public string SRC5_NM1 { get; set; }

        public string SRC_LOT5 { get; set; }

        public string SRC_CODE6 { get; set; }

        public string SRC6_NM { get; set; }

        public string SRC6_NM1 { get; set; }

        public string SRC_LOT6 { get; set; }

        public string SRC_CODE7 { get; set; }

        public string SRC7_NM { get; set; }

        public string SRC7_NM1 { get; set; }

        public string SRC_LOT7 { get; set; }
    }
}
