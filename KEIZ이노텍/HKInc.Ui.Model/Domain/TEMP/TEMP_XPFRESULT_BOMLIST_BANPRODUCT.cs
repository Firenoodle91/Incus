using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210621 오세완 차장
    /// 프로시저 USP_GET_XPFRESULT_BOMLIST_BANPRODUCT 결과 반환 객체
    /// </summary>
    public class TEMP_XPFRESULT_BOMLIST_BANPRODUCT
    {
        public TEMP_XPFRESULT_BOMLIST_BANPRODUCT() { }

        /// <summary>
        /// 20210621 오세완 차장
        /// 반제품 하위 품목코드
        /// </summary>
        public string ITEM_CODE { get; set; }

        /// <summary>
        /// 20210621 오세완 차장
        /// 반제품 하위 품목코드의 소요량
        /// </summary>
        public decimal USE_QTY { get; set; }

        /// <summary>
        /// 20210624 오세완 차장
        /// 투입해야 할 공정코드 추가
        /// </summary>
        public string PROCESS_CODE { get; set; }

    }
}
