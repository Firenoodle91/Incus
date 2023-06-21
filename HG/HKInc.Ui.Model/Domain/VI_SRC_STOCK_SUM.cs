using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_SRC_STOCK_SUM")]
    public class VI_SRC_STOCK_SUM
    {
        [ForeignKey("TN_STD1100"), Key, Column("SRC_CODE",Order =0)] public string SrcCode { get; set; }
        //[Key, Column("SRC_CODE1", Order = 1)] public string SrcNm { get; set; }
        [Column("IN_QTY")] public Nullable<int> InQty { get; set; }
        [Column("USEQTY")] public Nullable<decimal> Useqty { get; set; }
        [Column("STOCKQTY")] public Nullable<decimal> Stockqty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}