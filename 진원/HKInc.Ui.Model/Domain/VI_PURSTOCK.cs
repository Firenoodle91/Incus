using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PURSTOCK_MST")]
    public class VI_PURSTOCK
    {

        [Key, Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("TOP_CATEGORY")] public string TopCategory { get; set; }
        [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
        [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
        [Column("UNIT")] public string Unit { get; set; }
        [Column("SAFE_QTY")] public decimal SafeQty { get; set; }
        [Column("IN_QTY")] public Nullable<decimal> InQty { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("STOCK_QTY")] public Nullable<decimal> StockQty { get; set; }

        /// <summary>
        /// 재고금액 합계
        /// </summary>
        [NotMapped]
        public decimal? TotalCost
        {
            get {
                if (TN_STD1100.Cost != null)
                    return TN_STD1100.Cost * StockQty;
                else
                    return null;
                }
        }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
    }