using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("VI_PROD_STOCK_PRODUCT_MM")]
    public class VI_PROD_STOCK_PRODUCT_MM
    {
        [Key, Column("RowIndex", Order = 0)] public Int64 RowIndex { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }

        [Column("YYYY")] public string YYYY { get; set; }
        [Column("YYYYMM")] public string YYYYMM { get; set; }
        [Column("IN_QTY")] public Nullable<decimal> InQty { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
    }
}