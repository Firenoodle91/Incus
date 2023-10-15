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
        public TN_QCT1001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
            Add_Or_Modify_Flag = "N";
        }
        [Key, Column("SEQ",Order = 0)] public int Seq { get; set; }
        [Key, Column("ITEM_CODE", Order = 1)] public string ItemCode { get; set; }
        [Column("QCNAME")] public string Qcname { get; set; }
        [Column("QCNOTE")] public string Qcnote { get; set; }
        [Column("IMAGEFILE")] public string Imagefile { get; set; }
        [Column("APPEND_DT")] public Nullable<DateTime> AppendDt { get; set; }
        [Column("APPEND_USER")] public string AppendUser { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        
        [NotMapped]
        public string Add_Or_Modify_Flag { get; set; }
    }
}