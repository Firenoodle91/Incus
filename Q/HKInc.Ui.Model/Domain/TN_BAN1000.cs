using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_BAN1000T")]
    public class TN_BAN1000 : BaseDomain.MES_BaseDomain
    {
        public TN_BAN1000() { }

        [Key,Column("INPUT_NO",Order =0)] public string InputNo { get; set; }
        [Column("INPUT_DATE")] public Nullable<DateTime> InputDate { get; set; }
        [Column("INPUT_ID")] public string InputId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual ICollection<TN_BAN1001> BAN1001List { get; set; }
    }
}