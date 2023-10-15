using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 202120117 오세완 차장
    /// 반제품재고관리 디테일, USP_GET_BAN_STOCK_DETAIL_V2 반환 임시 객체
    /// </summary>	
    public class TEMP_BAN_STOCK_DETAIL_V2
    {
        public TEMP_BAN_STOCK_DETAIL_V2()
        {

        }

        /// <summary>
        /// 20220117 오세완 차장
        /// 구분
        /// </summary>                 
        public string TYPE { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 반제품 입고일
        /// </summary>                 
        public DateTime? BAN_INDATE { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 반제품 출고일
        /// </summary>                 
        public DateTime? BAN_OUT_DATE { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 입/출고 LOT NO
        /// </summary>             
        public string LOTNO { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 반제품 입고량
        /// </summary>                 
        public decimal? BAN_IN_QTY { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 반제품 출고량
        /// </summary>                 
        public decimal? BAN_OUT_QTY { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 반제품 재고량
        /// </summary>                 
        public decimal? STOCK_QTY { get; set; }

        /// <summary>
        /// 20220117 오세완 차장
        /// 입/출고자
        /// </summary>             
        public string WORKER { get; set; }
    }
}
