using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    public class Temp
    {
        public Temp()
        { }
        [Key, Column("CodeId", Order = 0)]
        public string CodeId { get; set; }

        [Column("CodeName")]
        public string CodeName { get; set; }
    }

}
