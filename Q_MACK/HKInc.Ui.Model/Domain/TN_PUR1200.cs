using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1200T")]
    public class TN_PUR1200 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1200() { }

        [ForeignKey("TN_PUR1100"),Key, Column("REQ_NO", Order = 0)] public string ReqNo { get; set; }
        [Column("REQ_SEQ")] public int ReqSeq { get; set; }       
        [ForeignKey("TN_STD1100"),Key, Column("ITEM_CODE",Order =1), Required(ErrorMessage = "품목코드")] public string ItemCode { get; set; }
        [Column("REQ_QTY")] public Nullable<Decimal> ReqQty { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped]
        public Decimal Amt
        {
            get
            {
                decimal amt = 0;
                decimal price = 0;
                try
                {
                    price = Convert.ToDecimal(Temp1);
                }
                catch { price = 0; }
                amt = Convert.ToDecimal(ReqQty) * price;
                return amt;
            }
        }
        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
        //[ForeignKey("ReqNo")]
        public virtual TN_PUR1100 TN_PUR1100 { get; set; }
    }
}