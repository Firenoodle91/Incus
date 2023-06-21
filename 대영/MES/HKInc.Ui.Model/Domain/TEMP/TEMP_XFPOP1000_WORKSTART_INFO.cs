using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210608 오세완 차장
    /// USP_GET_XFPOP1000_WORKSTART_INFO 반환 객체
    /// </summary>
    public class TEMP_XFPOP1000_WORKSTART_INFO
    {
        public TEMP_XFPOP1000_WORKSTART_INFO()
        {

        }

        /// <summary>
        /// 20210608 오세완 차장
        /// bom에 투입되어야 하는 자재 혹은 반제품의 타입
        /// </summary>
        public string TYPE { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 품번
        /// </summary>
        public string ITEM_CODE { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 품명
        /// </summary>
        public string ITEM_NAME { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 품목명
        /// </summary>
        public string ITEM_NAME1 { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// bom에서 투입되는 공정으로 지정한 공정코드
        /// </summary>
        public string PROCESS_CODE { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 수요량
        /// </summary>
        public decimal USE_QTY { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 수동관리여부
        /// </summary>
        public string MG_FLAG { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 출고LOTNO
        /// </summary>
        public string OUT_LOT_NO { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 출고량
        /// </summary>
        public decimal OUT_QTY { get; set; }
    }
}
