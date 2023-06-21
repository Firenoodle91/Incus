using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20220106 오세완 차장 
    /// USP_GET_XFMPS1210_BAN_PRODUCT 결과 반환 객체
    /// </summary>
    public class TEMP_XFMPS1210_BAN_PRODUCT
    {
        public TEMP_XFMPS1210_BAN_PRODUCT()
        {

        }

        /// <summary>
        /// 20220106 오세완 차장
        /// 품목코드
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 20220106 오세완 차장
        /// 품번
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 20220106 오세완 차장
        /// 품명
        /// </summary>
        public string ItemName1 { get; set; }
    }
}
