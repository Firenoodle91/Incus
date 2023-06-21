using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PROD_QTY_MST_LOT")]
    public class VI_PRODQTYMSTLOT
    {
      
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Key,Column("LOT_NO",Order =1)] public string LotNo { get; set; }
        [Column("InQty")] public Nullable<int> Inqty { get; set; }
        [Column("OutQty")] public Nullable<int> Outqty { get; set; }
        [Column("StockQty")] public Nullable<int> Stockqty { get; set; }
    }
}