using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.BaseDomain
{
    public abstract class MES_BaseDomain2
    {
        [Column("UPD_ID")]
        public string UpdateId { get; set; }

        [Column("UPD_DATE")]
        public Nullable<System.DateTime> UpdateTime { get; set; }

        [Column("INS_ID"), Required]
        public string CreateId { get; set; }

        [Column("INS_DATE"), Required]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [Column("FACT_CODE"), Required]
        public string FactCode { get; set; }
    }
}
