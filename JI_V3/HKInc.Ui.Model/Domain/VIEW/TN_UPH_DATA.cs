using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_UPH_DATA")]
    public class TN_UPH_DATA : BaseDomain.MES_BaseDomain
    {
        public TN_UPH_DATA()
        {

        }

        [Key, Column("WORK_DT", Order = 0)] public DateTime WorkDt { get; set; }
        [Key, Column("SEQ", Order = 1)] public string Seq { get; set; }
        [Key, Column("MACHINE_CODE", Order = 2)] public string MachineCode { get; set; }
        [Key, Column("ITEM_CODE", Order = 3)] public string ItemCode { get; set; }
        [Key, Column("PROCESS_CODE", Order = 4)] public string ProcessCode { get; set; }
        [Column("WORK_QTY")] public Nullable<decimal> WorkQty { get; set; }
        [Column("OK_QTY")] public Nullable<decimal> OkQty { get; set; }
        [Column("FAILE_QTY")] public Nullable<decimal> FailQty { get; set; }
        [Column("WORK_TIME")] public Nullable<decimal> WorkTime { get; set; }
        [Key, Column("WORKER", Order = 5)] public string Worker { get; set; }

        [ForeignKey("MachineCode")]
        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        //public virtual ICollection<TN_MEA1000> TN_MEA1000List { get; set; }
    }
}