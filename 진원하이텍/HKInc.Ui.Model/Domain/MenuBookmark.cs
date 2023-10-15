using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("MenuBookmark")]
    public class MenuBookmark : BaseDomain.BaseDomain3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal MenuBookmarkId { get; set; }

        public decimal UserId { get; set; }
        public decimal MenuId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}
