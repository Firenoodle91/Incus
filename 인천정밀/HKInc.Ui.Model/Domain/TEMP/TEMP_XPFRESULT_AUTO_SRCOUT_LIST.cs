using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210622 오세완 차장
    /// USP_GET_XPFRESULT_AUTO_SRCOUT_LIST 결과 반환 객체
    /// </summary>
    public class TEMP_XPFRESULT_AUTO_SRCOUT_LIST
    {
        public TEMP_XPFRESULT_AUTO_SRCOUT_LIST()
        {

        }

        /// <summary>
        /// 20210622 오세완 차장
        /// 자동출고할 입고LOT번호
        /// </summary>
        public string IN_LOT_NO { get; set; }

        /// <summary>
        /// 20210622 오세완 차장
        /// 출고할 수 있는 재고량
        /// </summary>
        public decimal? STOCK_QTY { get; set; }

        /// <summary>
        /// 20220127 오세완 차장 
        /// 자동출고할 입고IN_NO 번호, 케이즈이노텍 스타일
        /// </summary>
        public string IN_NO { get; set; }

        /// <summary>
        /// 20220127 오세완 차장 
        /// 자동출고할 입고SEQ, 케이즈이노텍 스타일
        /// </summary>
        public int IN_SEQ { get; set; }

        /// <summary>
        /// 20220127 오세완 차장 
        /// 자동출고할 입고고객사lotNo, 케이즈이노텍 스타일
        /// </summary>
        public string IN_CUSTOMER_LOT_NO { get; set; }
    }
}
