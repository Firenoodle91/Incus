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
    public class FieldLabel : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal FieldLabelId { get; set; }

        [Required(ErrorMessage = "FieldName")]
        public string FieldName { get; set; }

        [Required(ErrorMessage = "LabelText")]
        public string LabelText { get; set; }

        public string LabelText2 { get; set; }

        public string LabelText3 { get; set; }

        public string Active { get; set; }
        
    }
}
