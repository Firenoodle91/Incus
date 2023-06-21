using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MENUTOTABLE")]
    public class MENUTOTABLE : BaseDomain.BaseDomain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal Seq { get; set; }

        [Required(ErrorMessage = "MenuId")]
        public decimal MenuId { get; set; }

        public string TableName { get; set; }
               

        public string Active { get; set; }        
        

      
    }
}
