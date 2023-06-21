using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20211214 오세완 차장
    /// USP_GET_XFMPS1200_BOM_INFO 결과 반환 임시 객체
    /// </summary>
    public class TEMP_XFMPS1200_BOM_INFO
    {
        public TEMP_XFMPS1200_BOM_INFO()
        {

        }

        /// <summary>
        /// 20211214 오세완 차장
        /// 대분류
        /// </summary>
        public string TYPE { get; set; }

        /// <summary>
        /// 20211214 오세완 차장
        /// 품목코드
        /// </summary>
        public string ITEM_CODE { get; set; }

        /// <summary>
        /// 20211214 오세완 차장
        /// 품명
        /// </summary>
        public string ITEM_NAME { get; set; }

        /// <summary>
        /// 20211214 오세완 차장
        /// 작업지시여부
        /// </summary>
        public string WORK_ORDER_JUDGE { get; set; }

        /// <summary>
        /// 20211217 오세완 차장
        /// 단위코드
        /// </summary>
        public string UNIT { get; set; }

        /// <summary>
        /// 20211217 오세완 차장
        /// 단위명
        /// </summary>
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// 20211217 오세완 차장
        /// 생산계획 소요량
        /// </summary>
        public decimal USE_QTY { get; set; }

        /// <summary>
        /// 20220313 오세완 차장
        /// tn_std1100t temp1
        /// </summary>
        public string TEMP1 { get; set; }

        /// <summary>
        /// 20220313 오세완 차장
        /// tn_std1100t temp2
        /// </summary>
        public string TEMP2 { get; set; }

        /// <summary>
        /// 20220314 오세완 차장
        /// tn_std1100 colorname
        /// </summary>
        public string COLOR_NAME { get; set; }
    }
}
