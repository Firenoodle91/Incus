using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_QCT1001T")]
    public class TN_QCT1001 : BaseDomain.BaseDomain
    {
        public TN_QCT1001() { }

        [Key,Column("SEQ",Order =0)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("QCNAME")] public string Qcname { get; set; }
        [Column("QCNOTE")] public string Qcnote { get; set; }
        [Column("IMAGEFILE")] public string Imagefile { get; set; }
        [Column("APPEND_DT")] public Nullable<DateTime> AppendDt { get; set; }
        [Column("APPEND_USER")] public string AppendUser { get; set; }
    }
}