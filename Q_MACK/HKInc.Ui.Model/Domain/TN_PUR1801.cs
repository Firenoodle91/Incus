using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1801T")]
    public class TN_PUR1801 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1801() { }

        [ForeignKey("TN_PUR1800"), Key,Column("IN_NO",Order =0)] public string InNo { get; set; }
        [Key,Column("IN_SEQ",Order =1)] public int InSeq { get; set; }
        [Key,Column("IN_LOT_NO",Order =2)] public string InLotno { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PO_QTY")] public int PoQty { get; set; }
        [Column("IN_QTY")] public int InQty { get; set; }
        [Column("COST")] public Nullable<int> Cost { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("PO_NO")] public string PoNo { get; set; }
        [Column("PO_SEQ")] public Nullable<int> PoSeq { get; set; }
        [Column("IN_SRE")] public string InSre { get; set; }
        [Column("TEMP")] public string Temp { get; set; }

        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }

        [NotMapped]
        public decimal Amt
        {
            get
            {

                try
                {
                    int? amt = InQty * Cost;
                    return Convert.ToDecimal(amt);
                }
                catch
                {
                    return 0;
                }

            }
        }

        public virtual TN_PUR1800 TN_PUR1800 { get; set; }

    }
}