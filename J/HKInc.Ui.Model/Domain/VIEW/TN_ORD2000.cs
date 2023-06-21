using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>출고관리 마스터</summary>	
    [Table("TN_ORD2000T")]
    public class TN_ORD2000 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD2000()
        {
            TN_ORD2001List = new List<TN_ORD2001>();
        }
        [Key, Column("OUTPRT_NO", Order = 0)] public string OutprtNo { get; set; }
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        [Column("OUTPRT_DATE")] public DateTime OutprtDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }


        public virtual ICollection<TN_ORD2001> TN_ORD2001List { get; set; }
    }
}