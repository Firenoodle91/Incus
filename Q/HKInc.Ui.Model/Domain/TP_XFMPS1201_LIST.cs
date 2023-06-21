using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220407 오세완 차장
    /// USP_GET_XFMPS1201_LIST 프로시저 결과 반환 객체
    /// </summary>
    public class TP_XFMPS1201_LIST
    {
        public TP_XFMPS1201_LIST()
        {
            _Check = "N"; // 20220407 오세완 차장 작업지시일 일괄변경 화면에서 마우스 선택 이벤트 때문에 기본값을 정해야 오류가 생기지 않음 
        }

        public string _Check { get; set; }

        public string WORK_NO { get; set; }

        public string ITEM_CODE { get; set; }

        public string ITEM_NM { get; set; }

        public string ITEM_NM1 { get; set; }

        public int P_SEQ {get; set;}

        public string PROCESS { get; set; }

        public string PROCESS_NAME { get; set; }

        public DateTime WORK_DATE { get; set; }

        public string MACHINE_CODE { get; set; }

        public string MACHINE_NAME { get; set; }

        public int PLAN_QTY { get; set; }

        public string OutProc { get; set; }

        public string WORK_ID { get; set; }

        public string UserName { get; set; }

        public string MEMO { get; set; }

        public string CUST_CODE { get; set; }

        public string CUSTOMER_NAME { get; set; }

        public DateTime? WORK_DATE_CHANGE { get; set; }

        public decimal ROW_ID { get; set; }
}
}
