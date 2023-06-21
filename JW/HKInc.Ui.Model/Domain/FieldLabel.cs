using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("FieldLabel")]
    public class FieldLabel : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal FieldLabelId { get; set; }

        [Required(ErrorMessage = "Field Name is required."), StringLength(100)]
        public string FieldName { get; set; }

        [Required(ErrorMessage = "Default Label is required."), StringLength(100)]
        public string LabelText { get; set; }

        [StringLength(100)]
        public string LabelText2 { get; set; }

        [StringLength(100)]
        public string LabelText3 { get; set; }

        [StringLength(100)]
        public string Active { get; set; }
        
    }
}
