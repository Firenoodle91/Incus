using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1200T")]
    public class TN_ORD1200 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1200()
        {
            ORD1201List = new List<TN_ORD1201>();
        }

        [Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Column("DELIV_SEQ")] public string DelivSeq { get; set; }
        [Column("ORDER_NO")] public string OrderNo { get; set; }
        [Column("ORDER_SEQ")] public Nullable<int> OrderSeq { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("ORDER_QTY")] public Nullable<decimal> OrderQty { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("OUT_DATE")] public Nullable<DateTime> OutDate { get; set; }
        [Column("OUT_ID")] public string OutId { get; set; }
    //    [Column("OUT_STATE")] public string OutState { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped]
        public string OutState { get
            {
                string val = "출고대기";
                if (OutQty == 0) { val = "출고대기"; }
                else if (OutQty < OrderQty) { val = "출고중"; }
               else if (OutQty >= OrderQty) { val = "출고완료"; }
                return val;
            }
        }
        public virtual ICollection<TN_ORD1201> ORD1201List { get; set; }

        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}