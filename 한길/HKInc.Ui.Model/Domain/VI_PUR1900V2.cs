using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PUR1900V2")]
    public class VI_PUR1900V2
    {
        [Key,Column("PO_NO",Order =0)] public string PoNo { get; set; }
        [Key,Column("PO_SEQ",Order =1)] public int PoSeq { get; set; }
        [Column("IN_NO")] public string InNo { get; set; }
        [Column("IN_SEQ")] public Nullable<int> InSeq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PO_QTY")] public int PoQty { get; set; }
        [Column("IN_QTY")] public Nullable<int> InQty { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("IN_SRE")] public string InSre { get; set; }
        [NotMapped]
        public int Qty {
        get
            {
                if (PoQty - InQty <= 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(PoQty - InQty);

                }
            }
        }
    }
}