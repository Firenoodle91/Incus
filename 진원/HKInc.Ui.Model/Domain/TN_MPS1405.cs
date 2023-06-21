using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1405T")]
    public class TN_MPS1405 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1405()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Column("WORK_DATE")] public DateTime WorkDate { get; set; }
        [Key,Column("WORK_NO",Order =0)] public string WorkNo { get; set; }
        [Key,Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("RESULT_DATE")] public DateTime ResultDate { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("WORKING_TIME")] public string WorkingTime { get; set; }
        [Column("TYPE_CODE")] public string TypeCode { get; set; }
        [Column("P_SEQ")] public Nullable<int> Pseq { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
    }
}