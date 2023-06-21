using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1201T")]
    public class TN_ORD1201 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1201() { }

        [ForeignKey("TN_ORD1200"),Key, Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("OUT_DATE")] public Nullable<DateTime> OutDate { get; set; }
        [Column("MEMO")]public string Memo { get; set; }

        public virtual TN_ORD1200 TN_ORD1200 { get; set; }
    }
}