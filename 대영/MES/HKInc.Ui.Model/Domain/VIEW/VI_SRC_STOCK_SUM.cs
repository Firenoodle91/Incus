using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 자재재공재고 VIEW
    /// </summary>
    [Table("VI_SRC_STOCK_SUM")]
    public class VI_SRC_STOCK_SUM
    {
        [ForeignKey("TN_STD1100"), Key, Column("SRC_ITEM_CODE",Order =0)] public string SrcItemCode { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("USE_QTY")] public Nullable<decimal> UseQty { get; set; }
        [Column("STOCK_QTY")] public Nullable<decimal> StockQty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}