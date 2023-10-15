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
    /// 자재재고관리 자재재고현황
    /// </summary>
    [Table("VI_PURSTOCK_MST")]
    public class VI_PURSTOCK
    {
        [Key, Column("ITEM_CODE")] public string ItemCode { get; set; }

        // 2022-04-01 김진우 주석
        //[Column("ITEM_NM")] public string ItemNm { get; set; }
        //[Column("TOP_CATEGORY")] public string TopCategory { get; set; }
        //[Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
        //[Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
        //[Column("UNIT")] public string Unit { get; set; }
        //[Column("SAFE_QTY")] public decimal SafeQty { get; set; }
        [Column("IN_QTY")] public Nullable<decimal> InQty { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("STOCK_QTY")] public Nullable<decimal> StockQty { get; set; }
        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}