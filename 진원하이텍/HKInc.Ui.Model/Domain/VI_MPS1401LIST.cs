using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_MPS1401_LIST")]
    public class VI_MPS1401LIST
    {
        [Key,Column("WORK_DATE",Order =0)] public Nullable<DateTime> WorkDate { get; set; }
        [Key,Column("WORK_NO",Order =1)] public string WorkNo { get; set; }

        [Key,Column("SEQ",Order =2)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("PROCESS_TURN")] public Nullable<int> ProcessTurn { get; set; }
        [Column("RESULT_DATE")] public Nullable<DateTime> ResultDate { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }
    }
}