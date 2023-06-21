using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("MenuLog")]
    public class MenuLog : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal MenuLogId { get; set; }

        public decimal LoginLogId { get; set; }
        public decimal MenuId { get; set; }
        public Nullable<System.DateTime> OpenTime { get; set; }
        public Nullable<System.DateTime> CloseTime { get; set; }
        
        [ForeignKey("LoginLogId")]
        public virtual LoginLog LoginLog { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}
