using System;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220404 오세완 차장
    /// USP_GET_XFMPS1610_MASTER 프로시저 결과 반환 객체
    /// </summary>
    public class TP_XFMPS1610_MASTER
    {
        public TP_XFMPS1610_MASTER()
        {

        }

        public DateTime STOP_DATE { get; set; }

        public string MACHINE_CODE { get; set; }

        public string MACHINE_NAME { get; set; }

        public string WORK_NO { get; set; }

        public DateTime? START_DATE_MIN { get; set; }

        public DateTime? END_DATE_MAX { get; set; }

        public int? WORK_TIME { get; set; }
    }
}
