using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("UserGroup")]
    public class UserGroup : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal UserGroupId { get; set; }

        public Nullable<decimal> UpperUserGroupId { get; set; }

        [Required(ErrorMessage = "User Group Name is required.")]
        public string UserGroupName { get; set; }
        public string UserGroupName2 { get; set; }
        public string UserGroupName3 { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }        
        
        public virtual ICollection<GroupMenu> GroupMenuList { get; set; }
        
        //public virtual ICollection<UserGroup> UserGroupList { get; set; }

        //[ForeignKey("UpperUserGroupId")]
        //public virtual UserGroup UpperUserGroup { get; set; }
        
        public virtual ICollection<UserUserGroup> UserUserGroupList { get; set; }
    }
}
