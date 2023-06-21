using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table("Notice")]
    public class Notice : BaseDomain.BaseDomain3
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal NoticeId { get; set; }
       
        public string Contents { get; set; }       
    }
}
