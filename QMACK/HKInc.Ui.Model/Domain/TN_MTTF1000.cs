using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MTTF1000T")]
    public class TN_MTTF1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MTTF1000() { }

        [Key, Column("MACHINE_CODE", Order = 0)] public string MachineCode { get; set; }
        [Key, Column("YYYY", Order = 1)] public string YYYY { get; set; }
        [Key, Column("MM", Order = 2)] public string MM { get; set; }
        [Column("RUN_TIME")] public Nullable<int> RunTime { get; set; }
        [Column("STOP_TIME")] public Nullable<int> StopTime { get; set; }
        [Column("STOP_CNT")] public Nullable<int> StopCnt { get; set; }
    }
}