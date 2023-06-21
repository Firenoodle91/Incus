using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_OUTLIST")]
    public class VI_OUTLIST
    {
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Key,Column("SEQ",Order =1)] public Nullable<int> Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("OUT_DATE")] public DateTime OutDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
    }
}