using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1002T")]
    public class TN_ORD1002 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1002() {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            TN_ORD1100List = new List<TN_ORD1100>();
        }
        [ForeignKey("TN_ORD1000"), Key, Column("ORDER_NO",Order =0)] public string OrderNo { get; set; }
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("COST")] public decimal Cost { get; set; }
        [Column("ORDER_QTY"), Required(ErrorMessage = "수주수량은 필수입니다.")] public decimal OrderQty { get; set; }
        [Column("TOT_AMT")] public decimal TotAmt { get; set; }
        [Column("PERIOD_DATE")] public Nullable<DateTime> PeriodDate { get; set; }
        [Column("CUST_ITEM_CODE")] public  string CustItemcode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; } // 수주처
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_ORD1000 TN_ORD1000 { get; set; }

        public virtual ICollection<TN_ORD1100> TN_ORD1100List { get; set; }

        public decimal? Amt
        {
            get
            {
                return Cost * OrderQty;
            }
        }

        public string DelivFlag
        {
            get
            {
                return TN_ORD1100List.Count > 0 ? "Y" : "N";
            }
        }

    }
}