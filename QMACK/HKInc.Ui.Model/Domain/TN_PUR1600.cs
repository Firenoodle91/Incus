using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1600T")]
    public class TN_PUR1600 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1600()
        {
            PUR1700List = new List<TN_PUR1700>();
        }

        [Key,Column("PO_NO",Order =0)] public string PoNo { get; set; }
        [Column("PO_DATE")] public Nullable<DateTime> PoDate { get; set; }
        [Column("PO_ID")] public string PoId { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("IN_DUE_DATE")] public Nullable<DateTime> InDuedate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_PUR1700> PUR1700List { get; set; }
    }
}