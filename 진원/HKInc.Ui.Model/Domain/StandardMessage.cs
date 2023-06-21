using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("StandardMessage")]
    public class StandardMessage : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal MessageId { get; set; }

        [Required(ErrorMessage = "Default Messaage is required.")]
        public string Message { get; set; }

        public string Message2 { get; set; }
        public string Message3 { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }        
    }
}
