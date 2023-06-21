using System;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220405 오세완 차장
    /// USP_GET_XFRMEA1004_LIST 결과 반환 객체
    /// </summary>
    public class TP_XFRMEA1004_LIST
    {
        public TP_XFRMEA1004_LIST()
        {

        }

        public string MACHINE_CODE { get; set; }

        public string MACHINE_NAME { get; set; }

        public string WORK_NO { get; set; }

        public string ITEM_CODE { get; set; }

        public string ITEM_NM { get; set; }

        public string ITEM_NM1 { get; set; }

        public DateTime STOP_START_TIME { get; set; }

        public DateTime STOP_END_TIME { get; set; }

        public int STOP_TIME { get; set; }

        public string STOP_CODE { get; set; }

        public string STOP_CODE_NAME { get; set; }
    }
}
