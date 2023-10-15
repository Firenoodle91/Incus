using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("GroupMenu")]
    public class GroupMenu : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal GroupMenuId { get; set; }

        [Required(ErrorMessage = "User Group is required.")]
        public decimal UserGroupId { get; set; }

        [Required(ErrorMessage = "Menu is required.")]
        public decimal MenuId { get; set; }

        public string Read { get; set; }
        public string Write { get; set; }
        public string Export { get; set; }
        public string Print { get; set; }
        public string Insert { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
