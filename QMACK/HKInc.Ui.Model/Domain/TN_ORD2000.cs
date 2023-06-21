using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD2000T")]
    public class TN_ORD2000 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD2000()
        {
            ORD2001List = new List<TN_ORD2001>();
        }
        [Key,Column("OUTPRT_NO",Order =0)] public string OutprtNo { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("OUTPRT_DATE")] public DateTime OutprtDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_ORD2001> ORD2001List { get; set; }
    }
}