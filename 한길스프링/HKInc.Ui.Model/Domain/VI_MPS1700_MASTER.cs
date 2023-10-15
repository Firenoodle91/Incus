using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_MPS1700_MASTER")]
    public class VI_MPS1700_MASTER
    {
        public VI_MPS1700_MASTER()
        {
            _Check = "N";
        }

        [Column("WORK_NO")] public string WorkNo { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Key, Column("PACK_LOT_NO")] public string PackLotNo { get; set; }
        [Column("RESULT_DATE")] public DateTime? ResultDate { get; set; }
        [Column("TOTAL_OK_QTY")] public int? TotalOkQty { get; set; }
        [Column("REMAIN_QTY")] public int? RemainQty { get; set; }

        public virtual ICollection<TN_MPS1700> TN_MPS1700List { get; set; }

        [NotMapped] public string _Check { get; set; }
    }
}