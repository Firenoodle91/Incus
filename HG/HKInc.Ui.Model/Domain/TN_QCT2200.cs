using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_QCT2200T")]
    public class TN_QCT2200 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT2200()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("NO",Order =0)] public string No { get; set; }
        [Column("REQ_DATE")] public Nullable<DateTime> ReqDate { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("REQ_USER")] public string ReqUser { get; set; }
        [Column("REQ_QTY")] public string ReqQty { get; set; }
        [Column("RETURN_DATE")] public Nullable<DateTime> ReturnDate { get; set; }
        [Column("REQ_FILE")] public string ReqFile { get; set; }
        [Column("CHECK_DATE")] public Nullable<DateTime> CheckDate { get; set; }
        [Column("CHECK_ID")] public string CheckId { get; set; }
        [Column("CHECK_FILE")] public string CheckFile { get; set; }
        [Column("MEMO")] public string Memo { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }


    }
}