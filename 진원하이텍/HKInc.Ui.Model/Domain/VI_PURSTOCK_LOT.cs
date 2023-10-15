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

            [Key,Column("ITEM_CODE")] public string ItemCode { get; set; }
            [Column("ITEM_NM")] public string ItemNm { get; set; }
            [Column("TEMP2")] public string Temp2 { get; set; }
            [Column("TOP_CATEGORY")] public string TopCategory { get; set; }
            [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
            [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
            [Column("UNIT")] public string Unit { get; set; }
            [Column("SAFE_QTY")] public decimal SafeQty { get; set; }
            [Column("IN_QTY")] public Nullable<decimal> InQty { get; set; }
            [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
            [Column("STOCK_QTY")] public Nullable<decimal> StockQty { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }

    }
    }