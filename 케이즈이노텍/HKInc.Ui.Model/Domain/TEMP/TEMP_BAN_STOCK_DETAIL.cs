using System;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20211222 오세완 차장
    /// 반제품재고관리 디테일, USP_GET_BAN_STOCK_DETAIL 반환 임시 객체
    /// </summary>	
    public class TEMP_BAN_STOCK_DETAIL
    {
        public TEMP_BAN_STOCK_DETAIL()
        {

        }

        /// <summary>
        /// 20211222 오세완 차장
        /// 구분
        /// </summary>                 
        public string TYPE { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 입출고일
        /// </summary>                 
        public DateTime INOUT_DATE { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 입/출고 LOT NO
        /// </summary>             
        public string LOTNO { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 입/출고량
        /// </summary>                 
        public decimal? QTY { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// 입/출고자
        /// </summary>             
        public string WORKER { get; set; }
        
    }
}