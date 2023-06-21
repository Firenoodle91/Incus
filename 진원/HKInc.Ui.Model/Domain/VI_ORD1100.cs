using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table(" VI_ORD1100V")]
    public class VI_ORD1100 
    {
        
        [Column("ORDER_DATE")]
        public DateTime OrderDate { get; set; }
        [Key,Column("ORDER_NO",Order =0)]
        public string OrderNo { get; set; }

        [Column("CUST_CODE")]
        public string CustomerCode { get; set; }
        [Column("PERIOD_DATE")]
        public Nullable<DateTime> PeriodDate { get; set; }
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }
        [Column("MEMO")]
        public string Memo { get; set; }
        [Column("ORDER_ID")]
        public string OrderId { get; set; }
        [Column("CUST_ORDER_NO")]
        public string CustOrderno { get; set; }
        [Column("CUST_ORDER_ID")]
         public string CustOrderid { get; set; }
        [Column("ORDER_TYPE")]
        public string OrderType { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}