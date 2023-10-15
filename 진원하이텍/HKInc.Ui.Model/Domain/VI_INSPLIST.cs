using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_INSP_LIST")]
    public class VI_INSPLIST
    {
        [Key,Column("IType",Order =0)] public string Itype { get; set; }
        [Column("INPUT_DATE")] public Nullable<DateTime> InputDate { get; set; }
        [Key,Column("INPUT_NO",Order =1)] public string InputNo { get; set; }
        [Key,Column("INPUT_SEQ",Order =2)] public int InputSeq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("INPUT_QTY")] public Nullable<Decimal> InputQty { get; set; }
        [Column("INPUT_ID")] public string InputId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("INLOT")] public string Inlot { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
    }
}