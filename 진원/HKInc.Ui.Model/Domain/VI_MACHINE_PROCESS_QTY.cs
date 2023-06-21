using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_MACHINE_PROCESS_QTY")]
    public class VI_MACHINE_PROCESS_QTY
    {
        [Key,Column("MACHINE_CODE",Order =0)] public string MachineCode { get; set; }
        [Key, Column("ITEM_CODE",Order =1)] public string ItemCode { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("RESULT_DATE")] public DateTime ResultDate { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
        [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}