using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20220118 오세완 차장 
    /// USP_GET_PUR_STOCK_DETAIL_V2 반환 객체
    /// </summary>
    public class TEMP_PUR_STOCK_DETAIL_V2
    {
        public TEMP_PUR_STOCK_DETAIL_V2()
        {

        }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 입출고구분
        /// </summary>
        public string DIVISION { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 입고일
        /// </summary>
        public DateTime? IN_DATE { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 출고일
        /// </summary>
        public DateTime? OUT_DATE { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 원자재 품목코드 
        /// </summary>
        public string SRC_ITEM_CODE { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 완제품 품목코드
        /// </summary>
        public string WAN_ITEM_CODE { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 완제품 품목명 
        /// </summary>
        public string WAN_ITEM_NAME { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 입출고 lotno
        /// </summary>
        public string INOUT_LOT_NO { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 입고량
        /// </summary>
        public decimal IN_QTY { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 출고량
        /// </summary>
        public decimal OUT_QTY { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 재고량
        /// </summary>
        public decimal STOCK_QTY { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 입출고자
        /// </summary>
        public string INOUT_NAME { get; set; }

        /// <summary>
        /// 20220118 오세완 차장 
        /// 창고명
        /// </summary>
        public string WH_NAME { get; set; }
        
        /// <summary>
        /// 20220118 오세완 차장 
        /// 위치명
        /// </summary>
        public string POSITION_NAME { get; set; }
    }
}
