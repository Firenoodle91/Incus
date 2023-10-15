using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD2001T")]
    public class TN_ORD2001 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD2001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("OUTPRT_NO",Order =0)] public string OutprtNo { get; set; }
        [Key,Column("SEQPRT",Order =1)] public Nullable<int> Seqprt { get; set; }
        [Key,Column("ITEM_CODE",Order =2)] public string ItemCode { get; set; }
        [Column("OUT_QTY")] public Nullable<int> OutQty { get; set; }
        [Column("OUT_PRICE")] public Nullable<Decimal> OutPrice { get; set; }  
        [Column("OUT_DATE")] public Nullable<DateTime> OutDate { get; set; }

        [Column("MEMO")] public string Memo { get; set; }
        [Column("OUT_NO")] public string OutNo { get; set; }
        [Column("SEQ")] public Nullable<int> Seq { get; set; }
        [NotMapped]
        public decimal OutAmt
        {
            get
            {
                decimal OutAmt = 0;
                try
                {
                    OutAmt = Convert.ToDecimal(OutQty) * Convert.ToDecimal(OutPrice);
                }
                catch
                {
                    OutAmt = 0;
                }
                return OutAmt;
            }
        }
        public virtual TN_ORD2000 TN_ORD2000 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}