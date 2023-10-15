using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PURSTOCK_MST_LOT")]
    public class VI_PURSTOCK_LOT
    {
        [ForeignKey("TN_STD1100"), Key,Column("ITEM_CODE", Order = 0)] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Key, Column("TEMP2", Order = 1)] public string Temp2 { get; set; } //입고 LOT NO
        [Column("TOP_CATEGORY")] public string TopCategory { get; set; }
        [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
        [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
        [Column("UNIT")] public string Unit { get; set; }
        [Column("SAFE_QTY")] public decimal SafeQty { get; set; }
        [Column("IN_QTY")] public Nullable<int> InQty { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("STOCK_QTY")] public Nullable<int> StockQty { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}