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
        [Key,Column("OUT_QTY",Order =2)] public Nullable<int> OutQty { get; set; }
        [Key,Column("USE_QTY",Order =3)] public decimal UseQty { get; set; }
        [Key,Column("SRC_CODE",Order =0)] public string SrcCode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Key, Column("SRC_LOT_NO", Order = 4)] public string SrcLotNo { get; set; }
        [Column("Seq")] public int? Seq { get; set; }

        [ForeignKey("SrcCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}