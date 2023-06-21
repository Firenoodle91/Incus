using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PUR_LIST")]
    public class VI_PUR_LIST
    {
        [Key,Column("REQ_NO",Order =0)] public string ReqNo { get; set; }
        [Key,Column("REQ_CUSTCODE",Order =1)] public string ReqCustcode { get; set; }
        [Column("REQ_DATE")] public Nullable<DateTime> ReqDate { get; set; }
        [Key,Column("REQITEM_CODE",Order =2)] public string ReqitemCode { get; set; }
        [Column("REQ_QTY")] public Nullable<decimal> ReqQty { get; set; }
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        [Key,Column("INPUT_DATE",Order =3)] public Nullable<DateTime> InputDate { get; set; }
        [Column("INPUT_ITEM_CODE")] public string InputItemcode { get; set; }
        [Column("INPUT_QTY")] public Nullable<decimal> InputQty { get; set; }
        [Column("REQ_ID")] public string ReqId { get; set; }
        [Column("INPUT_ID")] public string InputId { get; set; }
        [Column("UNIT")] public string Unit { get; set; }
    }
}