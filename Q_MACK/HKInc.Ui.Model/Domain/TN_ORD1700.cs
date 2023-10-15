using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1700T")]
    public class TN_ORD1700 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1700()
        {
            ORD1701List = new List<TN_ORD1701>();
        }
        [Key,Column("IN_NO",Order =0)] public string InNo { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("IN_QTY")] public decimal InQty { get; set; }
        [Column("IN_DATE")] public Nullable<DateTime> InDate { get; set; }
        [Column("IN_ID")] public string InId { get; set; }     
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
      
        public virtual ICollection<TN_ORD1701> ORD1701List { get; set; }
        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}