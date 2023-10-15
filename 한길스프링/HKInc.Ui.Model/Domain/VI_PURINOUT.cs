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
        public VI_PURINOUT()
        {
            EditRowFlag = "N";
        }
        [Key, Column("Num", Order = 0)] public string Num { get; set; }
        [Key,Column("INPUT_DATE",Order =1)] public DateTime InOutDate { get; set; }
        [ForeignKey("TN_STD1100"), Key,Column("ITEM_CODE",Order =2)] public string ItemCode { get; set; }
        [Key,Column("Division", Order = 3)] public string Division { get; set; }
        [Column("INPUT_QTY")] public Nullable<int> InputQty { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("WORK")] public string Work { get; set; }
        [Column("CHECK_RESULT")] public string CheckResult { get; set; }
        [Key, Column("IN_LOT_NO", Order = 4)] public string InLotNo { get; set; }
        [Column("OUT_LOT_NO")] public string OutLotNo { get; set; }
        [Column("WhCode")] public string WhCode { get; set; }
        [Column("WhPosition")] public string WhPosition { get; set; }
        [Column("InputSeq")] public decimal? InputSeq { get; set; }

        [NotMapped]
        public string EditRowFlag { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }

    }
}