using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HKInc.Ui.Model.Domain
{
    [Table("MenuUserList")]
    public class MenuUserList
    {
        [Key, Column(Order = 0)]
        public decimal UserId { get; set; }

        [Key, Column(Order = 1)]
        public decimal MenuId { get; set; }

        public Nullable<decimal> ScreenId { get; set; }
        public Nullable<decimal> UpperMenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuName2 { get; set; }
        public string MenuName3 { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string MenuPath { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public Nullable<int> IconIndex { get; set; }
        public DateTime? UpdateTime { get; set; }

        [ForeignKey("ScreenId")]
        public virtual Screen Screen { get; set; }

        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}
