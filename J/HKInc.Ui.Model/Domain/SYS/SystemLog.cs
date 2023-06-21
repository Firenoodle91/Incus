using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("SystemLog")]
    public class SystemLog : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal SystemLogId { get; set; }

        public Nullable<int> LogType { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string ClassName { get; set; }
        public Nullable<int> ErrorCode { get; set; }
        public string Message { get; set; }
        public string Message2 { get; set; }
        public string Message3 { get; set; }
        public string Message4 { get; set; }
        public string Message5 { get; set; }        
    }
}
