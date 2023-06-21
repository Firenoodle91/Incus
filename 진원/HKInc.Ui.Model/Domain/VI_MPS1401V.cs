using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_MPS1401V")]
    public class VI_MPS1401V
    {
        [Key,Column("WORK_DATE",Order =0)] public DateTime WorkDate { get; set; }
        [Key,Column("WORK_NO",Order =1)] public string WorkNo { get; set; }
        [Column("SEQ")] public int Seq { get; set; }
        [Key,Column("PROCESS_CODE",Order =2)] public string ProcessCode { get; set; }
        [Key,Column("LOT_NO",Order =3)] public string LotNo { get; set; }
        [Column("ORDER_QTY")] public Nullable<int> OrderQty { get; set; }
        [Column("RESULT_DATE")] public DateTime ResultDate { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
        [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }
    }
}