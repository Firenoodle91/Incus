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

        [Required] public string CreateId { get; set; }

        [Required] public DateTime CreateTime { get; set; }

        public string UpdateId { get; set; }

        public DateTime? UpdateTime { get; set; }

        [NotMapped] public string _Check { get; set; }

        [NotMapped] public string NewRowFlag { get; set; }

        [NotMapped] public string EditRowFlag { get; set; }

        [NotMapped] public string DeleteRowFlag { get; set; }

        [NotMapped] public string UploadFilePath { get; set; }

        [NotMapped] public string DeleteFilePath { get; set; }
    }
}
