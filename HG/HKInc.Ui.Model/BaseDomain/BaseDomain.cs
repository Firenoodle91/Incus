using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.BaseDomain
{
    public abstract class BaseDomain
    {
        public BaseDomain()
        {
            NewRowFlag = "N";
        }

        public static object GsValue { get; set; }
        [Column("UPD_ID")]
        public string UpdateId { get; set; }

        [Column("UPD_DATE")]
        public Nullable<System.DateTime> UpdateTime { get; set; }

        [Column("INS_ID"), Required]
        public string CreateId { get; set; }

        [Column("INS_DATE"), Required]
        public Nullable<System.DateTime> CreateTime { get; set; }

        //우선 NewRowFlag만 사용
        [NotMapped]
        public string NewRowFlag { get; set; }
        [NotMapped]
        public string EditRowFlag { get; set; }
        [NotMapped]
        public string DeleteRowFlag { get; set; }
    }
}
