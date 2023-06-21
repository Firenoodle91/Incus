using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("UserUserGroup")]
    public class UserUserGroup : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal UserUserGroupId { get; set; }

        [Required(ErrorMessage = "UserId")]
        public decimal UserId { get; set; }

        [Required(ErrorMessage = "AuthGroupId")]
        public decimal UserGroupId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
