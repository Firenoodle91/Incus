using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_TRY001T")]
    public class TN_TRY001 : BaseDomain.BaseDomain
    {
        public TN_TRY001() { }

        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("TRY_DATE")] public Nullable<DateTime> TryDate { get; set; }
        [Column("REQ_MEMO")] public string ReqMemo { get; set; }
        [Column("RTN_MEMO")] public string RtnMemo { get; set; }
        [Column("RTN_DATE")] public Nullable<DateTime> RtnDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
      
    }
}