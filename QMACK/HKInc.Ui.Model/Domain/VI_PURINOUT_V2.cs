using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220414 오세완 차장
    /// 자재재고관리 입출고 상세 내역
    /// </summary>
    [Table("VI_PURINOUT_V2")]
    public class VI_PURINOUT_V2
    {
        public VI_PURINOUT_V2()
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
        /// 입출고일
        /// </summary>
        [Column("INPUT_DATE")]
        public DateTime InputDate { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 품목코드
        /// </summary>
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 품번
        /// </summary>
        [Column("ITEM_NM")]
        public string ItemNm { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 품명
        /// </summary>
        [Column("ITEM_NM1")]
        public string ItemNm1 { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 입고량
        /// </summary>
        [Column("INPUT_QTY")]
        public decimal InputQty { get; set; }

        /// <summary>
        /// 20220414 오세완 차장
        /// 출고량
        /// </summary>
        [Column("OUTPUT_QTY")]
        public decimal OutputQty { get; set; }

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
