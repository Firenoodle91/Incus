using System;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220418 오세완 차장
    /// USP_GET_XFQCT1910_LIST 프로시저 결과 
    /// lot역추적 출고목록
    /// </summary>
    public class TP_XFQCT1910_LIST
    {
        public TP_XFQCT1910_LIST()
        {

        }

        public string ITEM_CODE { get; set; }

        public string ITEM_NM { get; set; }

        public string ITEM_NM1 { get; set; }

        public string CUSTOMER_CODE { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string OUT_NO { get; set; }

        public DateTime OUT_DATE { get; set; }


    }
}
