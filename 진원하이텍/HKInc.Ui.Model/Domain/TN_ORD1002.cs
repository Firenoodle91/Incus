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

        }
        [Key, Column("ORDER_NO",Order =0)] public string OrderNo { get; set; }
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        //[Column("ITEM_NAME")] public string ItemNm { get; set; }
        //[Column("ITEM_NAME1")] public string ItemNm1 { get; set; }
        [Column("COST")] public decimal Cost { get; set; }
        [Column("ORDER_QTY")] public decimal OrderQty { get; set; }
        [Column("TOT_AMT")] public decimal TotAmt { get; set; }
        [Column("PERIOD_DATE")] public Nullable<DateTime> PeriodDate { get; set; }
        [Column("CUST_ITEM_CODE")] public  string CustItemcode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual VI_PRODQTY_MST VI_PRODQTY_MST { get; set; }

   
        public decimal? Amt
        {
            get
            {
                return Cost * OrderQty;
            }
        }


    }
}