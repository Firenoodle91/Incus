using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("MenuUserRight")]
    public class MenuUserRight
    {
        [Key, Column(Order = 0)]
        public decimal UserId { get; set; }

        [Key, Column(Order = 1)]
        public decimal MenuId { get; set; }

        public string Read { get; set; }
        public string Write { get; set; }
        public string Export { get; set; }
        public string Print { get; set; }
        public string Insert { get; set; }        
    }
}
