using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_BAN_QTYM")]
    public class VI_BAN_QTYM
    {
        [Key,Column("INPUT_DATE",Order =0)] public Nullable<DateTime> InputDate { get; set; }
        [Column("INPUT_ID")] public string InputId { get; set; }
        [Key,Column("INPUT_NO",Order =1)] public string InputNo { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("INPUT_QTY")] public Nullable<int> InputQty { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("LOTNO")] public string LotNo { get; set; }
        [Column("OUT_LOT_NO")] public string OutLotNo { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}