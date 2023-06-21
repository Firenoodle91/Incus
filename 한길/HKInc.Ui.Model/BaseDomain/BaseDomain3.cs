using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.BaseDomain
{
    public abstract class BaseDomain3
    {
        public BaseDomain3()
        {
            NewRowFlag = "N";
        }

        public string UpdateId { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdateClass { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateClass { get; set; }

        //우선 NewRowFlag만 사용
        [NotMapped]
        public string NewRowFlag { get; set; }
        [NotMapped]
        public string EditRowFlag { get; set; }
        [NotMapped]
        public string DeleteRowFlag { get; set; }
    }
}
