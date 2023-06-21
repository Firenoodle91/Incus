using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("Screen")]
    public class Screen : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal ScreenId { get; set; }

        [Required(ErrorMessage = "Module")]
        public decimal ModuleId { get; set; }

        public string ScreenName { get; set; }

        public string ScreenName2 { get; set; }

        public string ScreenName3 { get; set; }

        [Required(ErrorMessage = "NameSpace")]
        public string NameSpace { get; set; }

        [Required(ErrorMessage = "ClassName")]
        public string ClassName { get; set; }

        public Nullable<int> IconIndex { get; set; }

        public Nullable<int> LargeIconIndex { get; set; }

        public string Description { get; set; }

        public string Active { get; set; }        
        
        public virtual ICollection<Menu> MenuList { get; set; }

        [ForeignKey("ModuleId")]
        public virtual Module Module { get; set; }
    }
}
