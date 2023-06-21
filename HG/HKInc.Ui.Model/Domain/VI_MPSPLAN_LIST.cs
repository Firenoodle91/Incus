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
        public VI_MPSPLAN_LIST()
        {
            TN_MPS1300List = new List<TN_MPS1300>();
            _Check = "N";
        }

        [Key,Column("DELIV_SEQ",Order =0)]
        public string DelivSeq { get; set; }

        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        [Column("DELIV_DATE")]
        public Nullable<DateTime> DelivDate { get; set; }

        [Column("DELIV_QTY")]
        public int DelivQty { get; set; }

        [Column("ITEM_NM")]
        public string ItemNm { get; set; }

        [Column("MEMO")]
        public string Memo { get; set; }

        [Column("DELIV_ID")]
        public string DelivId { get; set; }

        [Column("PLAN_YN")]
        public string PlanYn { get; set; }
        [Column("ORDER_NO")]
        public string OrderNo { get; set; }

        [Column("DELIV_INS_DATE")]
        public DateTime? DelivInsDate { get; set; }

        [Column("ORDER_DATE")]
        public DateTime? OrderDate { get; set; }

        [Column("ORDER_CUST")]
        public string OrderCust { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_MPS1300> TN_MPS1300List { get; set; }

        [NotMapped] public string _Check { get; set; }
    }
}
