using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PUR1900V1")]
    public class VI_PUR1900V1
    {
        [Key,Column("PO_NO",Order =0)] public string PoNo { get; set; }
        [Column("PO_DATE")] public Nullable<DateTime> PoDate { get; set; }
        [Column("PO_ID")] public string PoId { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("IN_DUE_DATE")] public Nullable<DateTime> InDuedate { get; set; }
        [Column("IN_NO")] public string InNo { get; set; }
        [Column("IN_DATE")] public Nullable<DateTime> InDate { get; set; }
        [Column("IN_ID")] public string InId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("IN_SRE")] public string InSre { get; set; }
    }
}
