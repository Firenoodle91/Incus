

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20211222 오세완 차장
    /// 반제품재고목록 중 USP_GET_BAN_STOCK_MASTER 반환 객체
    /// </summary>
    public class TEMP_BAN_STOCK_MASTER
    {
        public TEMP_BAN_STOCK_MASTER()
        {

        }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string ITEM_CODE { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string ITEM_NAME { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string TOP_CATEGORY_NAME { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string MIDDLE_CATEGORY_NAME { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string BOTTOM_CATEGORY_NAME { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public decimal WEIGHT { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string SPEC_1 { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string SPEC_2 { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string SPEC_3 { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public string SPEC_4 { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public decimal PROD_QTY { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public decimal SAFE_QTY { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public decimal SUM_INQTY { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public decimal SUM_OUTQTY { get; set; }

        /// <summary>
        /// 20211222 오세완 차장
        /// </summary>
        public decimal STOCK_QTY { get; set; }
    }
}
