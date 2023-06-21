using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("MenuFavorite")]
    public class MenuFavorite
    {
        [Key, Column(Order = 0)]       
        public decimal UserId { get; set; }

        [Key, Column(Order = 1)]
        public decimal MenuId { get; set; }

        public int OPenCount { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}
