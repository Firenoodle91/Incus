using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220413 오세완 차장
    /// USP_GET_XFR5000_LIST 프로시저 반환 객체
    /// </summary>
    public class TP_XFR5000_LIST
    {
        public TP_XFR5000_LIST()
        {

        }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string OUT_NO { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public DateTime OUT_DATE { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string CUST_CODE { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string CUSTOMER_NAME { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string ITEM_CODE { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string ITEM_NM { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string ITEM_NM1 { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public decimal OUT_QTY { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public string ORDER_NO { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public int SEQ { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public decimal COST { get; set; }

        /// <summary>
        /// 20220413 오세완 차장
        /// </summary>
        public decimal SALES { get; set; }
    }
}
