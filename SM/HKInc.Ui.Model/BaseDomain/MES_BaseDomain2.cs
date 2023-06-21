using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.BaseDomain
{
    /// <summary>
    /// ROW_ID 없음
    /// </summary>
    public abstract class MES_BaseDomain2
    {
        public MES_BaseDomain2()
        {
            var dateTime = DateTime.Now;

            CreateId = GsValue.UserId;
            CreateTime = dateTime;
            UpdateId = GsValue.UserId;
            UpdateTime = dateTime;

            NewRowFlag = "N";
            EditRowFlag = "N";
            DeleteRowFlag = "N";
            _Check = "N";
        }

        [Column("INS_ID"), Required] public string CreateId { get; set; }

        [Column("INS_DATE"), Required] public DateTime CreateTime { get; set; }

        [Column("UPD_ID")] public string UpdateId { get; set; }

        [Column("UPD_DATE")] public DateTime? UpdateTime { get; set; }

        [NotMapped] public string _Check { get; set; }

        [NotMapped] public string NewRowFlag { get; set; }

        [NotMapped] public string EditRowFlag { get; set; }

        [NotMapped] public string DeleteRowFlag { get; set; }

        [NotMapped] public string UploadFilePath { get; set; }

        [NotMapped] public string DeleteFilePath { get; set; }
    }
}
