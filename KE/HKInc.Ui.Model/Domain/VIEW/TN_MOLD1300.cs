using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_MOLD1300T")]
    public class TN_MOLD1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1300()
        {
         
        }
        [ForeignKey("TN_MOLD1100"), Key, Column("MOLD_MCODE", Order = 0), Required(ErrorMessage = "MoldMCode")] public string MoldMCode { get; set; }
        [Column("MOLD_CODE"), Required(ErrorMessage = "MoldCode")] public string MoldCode { get; set; }
        [Key, Column("SEQ", Order = 1)] public Nullable<int> Seq { get; set; }
        [Column("MOLD_NAME"), Required(ErrorMessage = "MoldName")] public string MoldName { get; set; }        
        [Column("DIVISION"), Required(ErrorMessage = "Division")] public string Division { get; set; }
        [Column("INOUT_DATE"), Required(ErrorMessage = "InOutDate")] public DateTime InOutDate { get; set; }
        [Column("MOLD_IN_WH_CODE")] public string MoldInWhCode { get; set; }
        [Column("MOLD_IN_WH_POSITION")] public string MoldInWhPosition { get; set; }
        [Column("MOLD_OUT_WH_CODE")] public string MoldOutWhCode { get; set; }
        [Column("MOLD_OUT_WH_POSITION")] public string MoldOutWhPosition { get; set; }
        [Column("INOUT_ID"), Required(ErrorMessage = "Division")] public string InOutId { get; set; }
        
        
        [Column("ST_OUTPOSTION1")] public string StOutpostion1 { get; set; }
        [Column("ST_OUTPOSTION2")] public string StOutpostion2 { get; set; }
        [Column("ST_OUTPOSTION3")] public string StOutpostion3 { get; set; }
        [Column("ST_INPOSTION1")] public string StInpostion1 { get; set; }
        [Column("ST_INPOSTION2")] public string StInpostion2 { get; set; }
        [Column("ST_INPOSTION3")] public string StInpostion3 { get; set; }

        public virtual TN_MOLD1100 TN_MOLD1100 { get; set; }

    }
}