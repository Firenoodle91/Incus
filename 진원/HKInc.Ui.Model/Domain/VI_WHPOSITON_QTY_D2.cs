using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_WHPOSITON_QTY_D2")]
    public class VI_WHPOSITON_QTY_D2
    {
        [Key,Column("wh_code",Order =0)] public string WhCode { get; set; }
        [Key,Column("WH_POSITION",Order =1)] public string WhPosition { get; set; }
        [Key,Column("Item_code",Order =2)] public string ItemCode { get; set; }
        [Key,Column("Lot_NO",Order =3)] public string LotNo { get; set; }
        [Column("qty")] public Nullable<decimal> Qty { get; set; }
    }
}