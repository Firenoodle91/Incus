using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("Module")]
    public class Module : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal ModuleId { get; set; }

        [Required(ErrorMessage = "Module Name is required.")]
        public string ModuleName { get; set; }

        public string ModuleName2 { get; set; }
        public string ModuleName3 { get; set; }

        [Required(ErrorMessage = "MAssembly is required.")]
        public string Assembly { get; set; }
        
        [StringLength(4000)]
        public string Description { get; set; }

        public virtual ICollection<Screen> ScreenList { get; set; }
    }
}
