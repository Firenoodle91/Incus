using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220420 오세완 차장
    /// USP_GET_XFMACHINE_MONITORING 반환 객체 
    /// </summary>
    public class TP_XFMACHINE_MONITORING
    {
        public TP_XFMACHINE_MONITORING()
        {

        }

        public string MACHINE_GROUP_CODE { get; set; }

        public string MACHINE_GROUP_NAME { get; set; }

        public string MACHINE_CODE { get; set; }

        public string MACHINE_NAME { get; set; }

        public string RUN_STATUS { get; set; }

        public int SUM_STOP_TIME{ get; set; }

        public int MACHINE_RUN_TIME { get; set; }

        public string ITEM_CODE { get; set; }

        public string ITEM_NM { get; set; }

        public string ITEM_NM1 { get; set; }

        public string BOTTOM_CATEGORY { get; set; }

        public string CAR_TYPE { get; set; }

        public string CUST_CODE { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public string WORK_NO { get; set; }

        public string PROCESS { get; set; }

        public string PROCESS_NAME { get; set; }

        public string LOT_NO { get; set; }

        public DateTime START_DATE { get; set; }

        public int OK_QTY { get; set; }

        public int FAIL_QTY { get; set; }

        public string MONITORING_LOCATION { get; set; }

        public string JOB_STATE { get; set; }

        public string JOB_STATE_NAME { get; set; }
    }
}
