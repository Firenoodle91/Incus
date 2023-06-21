using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("LoginLog")]
    public class LoginLog : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal LoginLogId { get; set; }

        [Required]
        public decimal UserId { get; set; }

        public Nullable<System.DateTime> LoginTime { get; set; }
        public Nullable<System.DateTime> LogoutTime { get; set; }        
        public string IpAddress { get; set; }
        public string PcName { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<MenuLog> MenuLogList { get; set; }
    }
}
