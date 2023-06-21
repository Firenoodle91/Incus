using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_KNIFE003T")]
    public class TN_KNIFE003 : BaseDomain.MES_BaseDomain
    {
        public TN_KNIFE003()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [ForeignKey("TN_KNIFE001"), Key, Column("KNIFE_MCODE",Order = 0)] public string KnifeMcode { get; set; }
        [ForeignKey("TN_KNIFE001"), Key, Column("KNIFE_CODE", Order = 1)] public string KnifeCode { get; set; }
        [Key,Column("SEQ",Order = 2)] public int Seq { get; set; }
        [Column("KNIFE_NAME")] public string KnifeName { get; set; }
        [Column("INOUT_DT")] public Nullable<DateTime> InoutDt { get; set; }
        [Column("ST_OUTPOSTION1")] public string StOutpostion1 { get; set; }
        [Column("ST_OUTPOSTION2")] public string StOutpostion2 { get; set; }
        [Column("ST_OUTPOSTION3")] public string StOutpostion3 { get; set; }
        [Column("ST_INPOSTION1")] public string StInpostion1 { get; set; }
        [Column("ST_INPOSTION2")] public string StInpostion2 { get; set; }
        [Column("ST_INPOSTION3")] public string StInpostion3 { get; set; }
        [Column("INOUT_ID")] public string InoutId { get; set; }
        public virtual TN_KNIFE001 TN_KNIFE001 { get; set; }
    }
}