using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("CultureField")]
    public class CultureField : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal CultureFieldId { get; set; }

        [Required(ErrorMessage = "Entity Name is required.")]
        public string EntityName { get; set; }

        [Required(ErrorMessage = "Default Field is required.")]
        public string DefaultField { get; set; }

        [Required(ErrorMessage = "Second Field is required.")]
        public string SecondField { get; set; }

        [Required(ErrorMessage = "Third Field is required.")]
        public string ThirdField { get; set; }                
    }
}
