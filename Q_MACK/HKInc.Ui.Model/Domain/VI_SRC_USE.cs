using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_SRC_USE")]
    public class VI_SRC_USE
    {
        [Key,Column("RESULT_DATE",Order =1)] public Nullable<DateTime> ResultDate { get; set; }
     //   [Key,Column("LOT_NO",Order =1)] public string LotNo { get; set; }
        [Key,Column("OUT_QTY",Order =2)] public Nullable<decimal> OutQty { get; set; }
     //   [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
     //   [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
     //   [Column("OKQTY")] public Nullable<int> Okqty { get; set; }
        [Key,Column("USE_QTY",Order =3)] public decimal UseQty { get; set; }
      //  [Key,Column("ITEM_CODE",Order =2)] public string ItemCode { get; set; }
        [Key,Column("SRC_CODE",Order =0)] public string SrcCode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        //  [Key,Column("SRC_LOT",Order =4)] public string SrcLot { get; set; }
    }
}