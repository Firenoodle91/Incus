using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_WHPOSITON_QTY_D1")]
    public class VI_WHPOSITON_QTY_D1
    {
        [Key,Column("wh_code",Order =0)] public string whcode { get; set; }
        [Key,Column("Item_code",Order =1)] public string Itemcode { get; set; }
        [Column("qty")] public Nullable<int> qty { get; set; }
        [Key,Column("WH_POSITION",Order =2)] public string WhPosition { get; set; }

        [ForeignKey("Itemcode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

    }
}