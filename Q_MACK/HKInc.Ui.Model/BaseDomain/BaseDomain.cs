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
            var datetime = DateTime.Now;

            CreateId = GsValue.UserId;
            CreateTime = datetime;
            UpdateId = GsValue.UserId;
            UpdateTime = datetime;

            _Check = "N";
        }

        [Column("INS_ID"), Required] public string CreateId { get; set; }

        [Column("INS_DATE"), Required] public DateTime CreateTime { get; set; }

        [Column("UPD_ID")] public string UpdateId { get; set; }

        [Column("UPD_DATE")] public DateTime? UpdateTime { get; set; }

        [NotMapped] public string _Check { get; set; }
    }
}
