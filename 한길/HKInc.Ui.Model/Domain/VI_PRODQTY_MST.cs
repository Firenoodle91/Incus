using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PROD_QTY_MST")]
    public class VI_PRODQTY_MST
    {
        [ForeignKey("TN_STD1100"), Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Column("InQty")] public Nullable<int> Inqty { get; set; }
        [Column("OutQty")] public Nullable<int> Outqty { get; set; }
        [Column("StockQty")] public Nullable<int> Stockqty { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}