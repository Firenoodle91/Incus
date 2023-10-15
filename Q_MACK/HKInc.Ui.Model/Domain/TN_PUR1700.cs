using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1700T")]
    public class TN_PUR1700 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1700()
        {
            _Check = "N";
        }

        [ForeignKey("TN_PUR1600"), Key,Column("PO_NO",Order =0)] public string PoNo { get; set; }
        [Key,Column("PO_SEQ",Order =1)] public int PoSeq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("COST")] public Nullable<int> Cost { get; set; }
        [Column("PO_QTY")] public Nullable<int> PoQty { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("MAT_LOT_NO")] public string MatLotno { get; set; }
        [Column("IN_YN")] public string InYn { get; set; }
        [Column("WORKNO")] public string WorkNo { get; set; }
        [Column("PSEQ")] public int Pseq { get; set; }
        [Column("LOTNO")] public string LotNo { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }

        [NotMapped]
       
        public Decimal  Amt {
            get {
                Decimal val = 0;
                int? qty = 0;
                int? amt = 0;
                qty = PoQty == null ? 0 : PoQty;
                amt= Cost==null?0:Cost;
                val = Convert.ToDecimal(qty) * Convert.ToDecimal(amt);
                return val;
            }
        }

        public virtual TN_PUR1600 TN_PUR1600 { get; set; }
    }
}
