using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 자재발주참고 입고 가능한 수량
    /// </summary>
    [Table("VI_PUR_INPUT_ALLOW_QTY")]
    public class VI_PUR_INPUT_ALLOW_QTY
    {
        /// <summary>
        /// ROW_NUMBER
        /// </summary>
        [Key, Column("ROW_NO")] public long ROW_NO { get; set; }
        /// <summary>
        /// 발주번호
        /// </summary>
        [Column("REQ_NO")] public string REQ_NO { get; set; }
        /// <summary>
        /// 발주일
        /// </summary>
        [Column("REQ_DATE")] public DateTime REQ_DATE { get; set; }
        /// <summary>
        /// 납기예정일
        /// </summary>
        [Column("DUE_DATE")] public DateTime DUE_DATE { get; set; }
        /// <summary>
        /// 발주 고객사코드
        /// </summary>
        [Column("CUSTOM_CODE")] public string CUSTOM_CODE { get; set; }
        /// <summary>
        /// 발주자
        /// </summary>
        [Column("REQ_ID")] public string REQ_ID { get; set; }
        /// <summary>
        /// 발주메모
        /// </summary>
        [Column("MEMO")] public string MEMO { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        [Column("ITEM_CODE")] public string ITEM_CODE { get; set; }
        /// <summary>
        /// 발주수량합
        /// </summary>
        [Column("SUM_REQ_QTY")] public decimal SUM_REQ_QTY { get; set; }
        /// <summary>
        /// 입고수량합
        /// </summary>
        [Column("SUM_INPUT_QTY")] public decimal SUM_INPUT_QTY { get; set; }
        /// <summary>
        /// 남은 발주수량
        /// (발주수량합 - 입고수량합)
        /// </summary>
        [Column("ALLOW_QTY")] public decimal ALLOW_QTY { get; set; }

        /// <summary>
        /// 체크박스 처리
        /// </summary>
        [NotMapped] public string _Check { get; set; }
    }
}
