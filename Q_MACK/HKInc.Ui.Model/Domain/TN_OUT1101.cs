using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_OUT1101T")]
    public class TN_OUT1101 : BaseDomain.BaseDomain
    {
        public TN_OUT1101() { }

        [ForeignKey("TN_STD1101"), Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "RowId is required")] public decimal Seq { get; set; }
        [Column("INOUT_DT")] public Nullable<DateTime> InoutDt { get; set; }
        [Column("INQTY")] public Nullable<decimal> Inqty { get; set; }
        [Column("OUTQTY")] public Nullable<decimal> Outqty { get; set; }
        [Column("RETQTY")] public Nullable<decimal> Retqty { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        public virtual TN_STD1101 TN_STD1101 { get; set; }
    }
}