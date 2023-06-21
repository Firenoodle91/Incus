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
    public class GroupMenu : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal GroupMenuId { get; set; }

        [Required(ErrorMessage = "AuthGroupId")]
        public decimal UserGroupId { get; set; }

        [Required(ErrorMessage = "Menu")]
        public decimal MenuId { get; set; }

        public string Read { get; set; } //읽기권한
        public string Write { get; set; } //쓰기권한
        public string Insert { get; set; } //생성권한
        public string Export { get; set; } //내보내기권한
        public string Print { get; set; } //출력권한
        public string Reload { get; set; } //재조회여부

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

        [ForeignKey("UserGroupId")]
        public virtual UserGroup UserGroup { get; set; }
    }
}
