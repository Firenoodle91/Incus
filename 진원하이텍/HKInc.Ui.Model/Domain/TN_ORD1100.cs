using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1100T")]
    public class TN_ORD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1100()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("SEQ",Order =1)] public decimal Seq { get; set; }
        [Key,Column("ORDER_NO",Order =0)] public string OrderNo { get; set; }
        [Key,Column("DELIV_SEQ",Order =2)] public string DelivSeq { get; set; }
        [Column("DELIV_DATE")] public Nullable<DateTime> DelivDate { get; set; }
        [Column("DELIV_QTY")] public int DelivQty { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }     
        [Column("DELIV_ID")] public string DelivId { get; set; }
        [Column("PROD_YN")] public string ProdYn { get; set; }
        [Column("OUT_CONFIRM_FLAG")] public string OutConfirmflag { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("DELIV_CON_DATE")] public Nullable<DateTime> OutDate { get; set; }
    [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }

}