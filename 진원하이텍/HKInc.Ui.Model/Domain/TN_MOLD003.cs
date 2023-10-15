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
        public TN_MOLD003()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("MOLD_MCODE",Order =0)] public string MoldMcode { get; set; }
        [Column("MOLD_CODE")] public string MoldCode { get; set; }
        [Key,Column("SEQ",Order =1)] public Nullable<int> Seq { get; set; }
        [Column("MOLD_NAME")] public string MoldName { get; set; }
        [Column("INOUT_DT")] public Nullable<DateTime> InoutDt { get; set; }
        [Column("ST_OUTPOSTION1")] public string StOutpostion1 { get; set; }
        [Column("ST_OUTPOSTION2")] public string StOutpostion2 { get; set; }
        [Column("ST_OUTPOSTION3")] public string StOutpostion3 { get; set; }
        [Column("ST_INPOSTION1")] public string StInpostion1 { get; set; }
        [Column("ST_INPOSTION2")] public string StInpostion2 { get; set; }
        [Column("ST_INPOSTION3")] public string StInpostion3 { get; set; }
        [Column("INOUT_ID")] public string InoutId { get; set; }
      
    }
}