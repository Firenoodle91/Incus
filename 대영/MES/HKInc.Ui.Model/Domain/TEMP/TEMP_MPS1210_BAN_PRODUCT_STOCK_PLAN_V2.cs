using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210526 오세완 차장 
    /// 프로시저 USP_GET_MPS1210_BAN_PRODUCT_STOCK_PLAN_V2 반환 객체
    /// </summary>
    public class TEMP_MPS1210_BAN_PRODUCT_STOCK_PLAN_V2
    {
        /// <summary>
        /// 20210526 오세완 차장
        /// 대분류코드
        /// </summary>
        public string TOP_CATEGORY { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 대분류명
        /// </summary>
        public string TOP_CATEGORY_NAME { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 품번
        /// </summary>
        public string ITEM_CODE { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 사내품번
        /// </summary>
        public string ITEM_NAME { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 품명
        /// </summary>
        public string ITEM_NAME1 { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 품명 영문
        /// </summary>
        public string ITEM_NAME_ENG { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 품명 중문
        /// </summary>
        public string ITEM_NAME_CHN { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 안전재고
        /// </summary>
        public decimal? SAFE_QTY { get; set; }

        /// <summary>
        /// 20210526 오세완 차장
        /// 재고량
        /// </summary>
        public decimal? SUM_STOCK_QTY { get; set; }
    }
}
