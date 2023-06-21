using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MEA1300T")]
    public class TN_MEA1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1300() { }

        [Key,Column("INSTR_NO",Order =0)] public string InstrNo { get; set; }
        [Key,Column("INSTR_SEQ",Order =1)] public int InstrSeq { get; set; }
        [Column("CHECK_DATE")] public Nullable<DateTime> CheckDate { get; set; }
        [Column("CHECK_GU")] public string CheckGu { get; set; }
        [Column("CHECK_ID")] public string CheckId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [ForeignKey("InstrNo")] public virtual TN_MEA1200 TN_MEA1200 { get; set; }
    }
}
