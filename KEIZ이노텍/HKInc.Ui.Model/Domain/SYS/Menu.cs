using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table("Menu")]
    public class Menu : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal MenuId { get; set; }
        
        public Nullable<decimal> ScreenId { get; set; }

        public Nullable<decimal> UpperMenuId { get; set; }

        [Required(ErrorMessage = "MenuName")]
        public string MenuName { get; set; }
        public string MenuName2 { get; set; }
        public string MenuName3 { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string MenuPath { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        
        public Nullable<int> IconIndex { get; set; }
        public virtual ICollection<GroupMenu> GroupMenuList { get; set; }

        //public virtual ICollection<Menu> MenuList { get; set; }

        //[ForeignKey("UpperMenuId")]
        //public virtual Menu UpperMenu { get; set; }

        [ForeignKey("ScreenId")]
        public virtual Screen Screen { get; set; }

        
    }
}
