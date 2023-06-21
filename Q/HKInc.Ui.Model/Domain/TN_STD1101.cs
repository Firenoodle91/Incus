using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1101T")]
    public class TN_STD1101 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1101()
        {
            OUT1101List = new List<TN_OUT1101>();
        }
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("MAIN_CUST")] public string MainCust { get; set; }
        [Column("SPEC_1")] public string Spec1 { get; set; }
        [Column("SPEC_2")] public string Spec2 { get; set; }
        [Column("SPEC_3")] public string Spec3 { get; set; }
        [Column("SPEC_4")] public string Spec4 { get; set; }
        [Column("SRC_CODE")] public string SrcCode { get; set; }
        [Column("SAFE_QTY")] public Nullable<decimal> SafeQty { get; set; }
        [Column("ITEM_IMG")] public string ItemImg { get; set; }
        [Column("MEMO1")] public string Memo1 { get; set; }
        [Column("MEMO2")] public string Memo2 { get; set; }
        [Column("MEMO3")] public string Memo3 { get; set; }

        public virtual ICollection<TN_OUT1101>  OUT1101List { get; set; }

        [NotMapped]
        public Nullable<decimal> StockQty
        {
            get
            {
                try
                {
                    decimal inqty = OUT1101List.Sum(p => p.Inqty).GetValueOrDefault();
                    decimal outqty = OUT1101List.Sum(p => p.Outqty).GetValueOrDefault();
                    decimal reqty = OUT1101List.Sum(p => p.Retqty).GetValueOrDefault();

                    return inqty - outqty + reqty;
                }
                catch
                {
                    return 0;
                }


            }
        }
    }
}