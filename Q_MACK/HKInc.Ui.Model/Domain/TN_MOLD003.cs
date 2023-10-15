using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MOLD003T")]
    public class TN_MOLD003 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD003() { }

        [Key,Column("MOLD_MCODE",Order =0)] public string MoldMcode { get; set; }
        [Column("MOLD_CODE")] public string MoldCode { get; set; }
        [Key,Column("SEQ",Order =1)] public Nullable<int> Seq { get; set; }
        [Column("MOLD_NAME")] public string MoldName { get; set; }
        [Column("INOUT_DT")] public Nullable<DateTime> InoutDt { get; set; }
        [Column("ST_OUTPOSITION1")] public string StOutposition1 { get; set; }
        [Column("ST_OUTPOSITION2")] public string StOutposition2 { get; set; }
        [Column("ST_OUTPOSITION3")] public string StOutposition3 { get; set; }
        [Column("ST_INPOSITION1")] public string StInposition1 { get; set; }
        [Column("ST_INPOSITION2")] public string StInposition2 { get; set; }
        [Column("ST_INPOSITION3")] public string StInposition3 { get; set; }
        [Column("INOUT_ID")] public string InoutId { get; set; }
      
    }
}