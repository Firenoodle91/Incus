using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table(" VI_MPSPLAN_LIST")]
    public  class VI_MPSPLAN_LIST
    {
        [Key,Column("DELIV_SEQ",Order =0)]
        public string DelivSeq { get; set; }
        [Column("DELIV_DATE")]
        public Nullable<DateTime> DelivDate { get; set; }

        [Column("DELIV_QTY")]
        public int DelivQty { get; set; }
     
        [Key,Column("ITEM_CODE",Order =1)]
        public string ItemCode { get; set; }

        [Column("MEMO")]
        public string Memo { get; set; }

        [Column("DELIV_ID")]
        public string DelivId { get; set; }

        [Column("PLAN_YN")]
        public string PlanYn { get; set; }
        [Column("ORDER_NO")]
        public string OrderNo { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        [ForeignKey("ItemCode")]
   
        public virtual VI_PRODQTY_MST VI_PRODQTY_MST { get; set; }

    }
}
