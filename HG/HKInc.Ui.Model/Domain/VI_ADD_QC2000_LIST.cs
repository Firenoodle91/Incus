using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_ADD_QC2000_LIST")]
    public class VI_ADD_QC2000_LIST
    {
        [Column("WORK_DATE")] public Nullable<DateTime> WorkDate { get; set; }
        [Column("WORK_NO")] public string WorkNo { get; set; }
        [Column("SEQ")] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("PROCESS_TURN")] public Nullable<int> ProcessTurn { get; set; }
        [Column("RESULT_DATE")] public Nullable<DateTime> ResultDate { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("PACK_LOT_NO")] public string PackLotNo { get; set; }
        [Column("PACK_QTY")] public Nullable<int> PackQty { get; set; }
        [Key, Column("IN_NO")] public string InNo { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}