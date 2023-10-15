using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>품목별 6개월 평균 출고량 뷰</summary>	
    [Table("VI_ITEM_AVG_OUT_QTY")]
    public class VI_ITEM_AVG_OUT_QTY
    {
        public VI_ITEM_AVG_OUT_QTY()
        {
        }

        /// <summary>품목코드</summary>               
        [Key, ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>평균소요량</summary>             
        [Column("AVG_SPEND_QTY")] public decimal AvgSpendQty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}
