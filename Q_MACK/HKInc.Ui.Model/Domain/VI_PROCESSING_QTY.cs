using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PROCESSING_QTY")]
    public class VI_PROCESSING_QTY
    {
        [ForeignKey("TN_STD1100"),Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Column("P01")] public Nullable<int> P01 { get; set; }
        [Column("P02")] public Nullable<int> P02 { get; set; }
        [Column("P03")] public Nullable<int> P03 { get; set; }
        [Column("P04")] public Nullable<int> P04 { get; set; }
        [Column("P05")] public Nullable<int> P05 { get; set; }
        [Column("P06")] public Nullable<int> P06 { get; set; }
        [Column("P07")] public Nullable<int> P07 { get; set; }
        [Column("P08")] public Nullable<int> P08 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}