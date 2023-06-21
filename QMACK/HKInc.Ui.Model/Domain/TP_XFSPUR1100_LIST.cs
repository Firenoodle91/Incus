using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220217 오세완 차장
    /// 프로시저 USP_GET_XFSPUR1100_LIST 반환 객체
    /// </summary>
    public class TP_XFSPUR1100_LIST
    {
        public TP_XFSPUR1100_LIST()
        {

        }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 발주번호
        /// </summary>
        public string ReqNo { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 발주일
        /// </summary>
        public DateTime? ReqDate { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 납기예정일
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// 20220217 오세완 차장
        /// 발주자 id
        /// </summary>
        public string ReqId { get; set; }

        /// <summary>
        /// 20220217 오세완 차장
        /// 발주자 id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 거래처코드
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 거래처코드명
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 20220217 오세완 차장 
        /// 발주확정여부 
        /// </summary>
        public string Temp2 { get; set; }
    }
}
