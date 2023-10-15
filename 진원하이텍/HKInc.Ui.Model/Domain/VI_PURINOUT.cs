using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PURINOUT")]
    public class VI_PURINOUT
    {
        [Key, Column("RowIndex", Order = 0)] public long RowIndex { get; set; }
        [Column("Num")] public string Num { get; set; }
        [Column("INPUT_DATE")] public DateTime InOutDate { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("INPUT_QTY")] public Nullable<decimal> InputQty { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("WORK")] public string Work { get; set; }
        [Column("CHECK_RESULT")] public string CheckResult { get; set; }
    }
}