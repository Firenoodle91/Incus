using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_BAN_QTYSUM")]
    public class VI_BAN_QTYSUM
    {
        [Key,Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("IN_QTY")] public Nullable<int> InQty { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("STOCK_QTY")] public Nullable<int> StockQty { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}