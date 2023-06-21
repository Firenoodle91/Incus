using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220414 오세완 차장
    /// 자재재고관리(lot no) 상세 내역
    /// </summary>
    [Table("VI_PURINOUT_LOT")]
    public class VI_PURINOUT_LOT
    {
        public VI_PURINOUT_LOT()
        {

        }

        /// <summary>
        /// 20220414 오세완 차장
        /// 순번, int로 하면 오류
        /// </summary>
        [Key, Column("ROWNUM", Order = 0)]
        public Int64 RowNum { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 구분
        /// </summary>
        [Column("TYPE")]
        public string Type { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 입출고일
        /// </summary>
        [Column("INOUT_DATE")]
        public DateTime InoutDate { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 입출고량
        /// </summary>
        [Column("QTY")]
        public decimal Qty { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 품목코드
        /// </summary>
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 입출고 lotno
        /// </summary>
        [Column("LOT_NO")]
        public string LotNo { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 입출고자 ID
        /// </summary>
        [Column("WORKER")]
        public string Worker { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 입출고자명
        /// </summary>
        [Column("UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 창고코드
        /// </summary>
        [Column("WH_CODE")]
        public string WhCode { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 창고코드명
        /// </summary>
        [Column("WH_NAME")]
        public string WhCodeName { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 창고위치코드
        /// </summary>
        [Column("WH_POSITION")]
        public string WhPosition { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 창고위치코드명
        /// </summary>
        [Column("WH_POSITION_NAME")]
        public string WhPositionName { get; set; }
    }
}
