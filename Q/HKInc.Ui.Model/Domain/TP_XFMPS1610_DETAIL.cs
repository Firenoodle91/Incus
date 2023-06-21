using System;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220404 오세완 차장
    /// USP_GET_XFMPS1610_DETAIL 프로시저 반환 객체
    /// </summary>
    public class TP_XFMPS1610_DETAIL
    {
        public TP_XFMPS1610_DETAIL()
        {

        }

        public string STOP_CODE_NAME { get; set; }

        public DateTime? STOP_START_TIME { get; set; }

        public DateTime? STOP_END_TIME { get; set; }

        public int? STOP_TIME { get; set; }
    }
}
