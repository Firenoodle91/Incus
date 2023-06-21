using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_PROD_QTY_DTL")]
    public class VI_PRODQTY_DTL
    {
        [Key,Column("WORK_NO",Order =0)] public string WorkNo { get; set; }
        [Key,Column("RESULT_DATE",Order =1)] public Nullable<DateTime> ResultDate { get; set; }
        [Key,Column("ITEM_CODE",Order =2)] public string ItemCode { get; set; }
        [Key,Column("LOT_NO",Order =3)] public string LotNo { get; set; }
        [Column("InQty")] public Nullable<int> Inqty { get; set; }
        [Column("OutQty")] public Nullable<int> Outqty { get; set; }
    }
}