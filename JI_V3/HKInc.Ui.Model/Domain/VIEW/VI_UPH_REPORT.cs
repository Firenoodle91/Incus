using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비현황</summary>	
    [Table("VI_UPH_REPORT")]
    public class VI_UPH_REPORT
    {
        [Key, Column("WorkDt", Order = 0)] public DateTime WorkDt { get; set; }
        [Key, Column("MachineCode", Order = 1)] public string MachineCode { get; set; }
        [Key, Column("ItemCode", Order = 2)] public string ItemCode { get; set; }
        [Key, Column("ProcessCode", Order = 3)] public string ProcessCode { get; set; }
        [Column("StdUPH")] public Nullable<int> StdUPH { get; set; }
        [Column("RealUPH")] public Nullable<decimal> RealUPH { get; set; }

        [Column("Rat")] public Nullable<decimal> Rat { get; set; }
        [Column("WorkQty")] public Nullable<decimal> WorkQty { get; set; }
        [Column("OkQty")] public Nullable<decimal> OkQty { get; set; }
        [Column("FailQty")] public Nullable<decimal> FailQty { get; set; }
        [Column("WorkTime")] public Nullable<decimal> WorkTime { get; set; }
        [Key, Column("Worker", Order = 4)] public string Worker { get; set; }
    }
}