using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>통신장비 실적</summary>          2022-08-24 김진우
    [Table("TN_MEA1700T")]
    public class TN_MEA1700 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1700() { }

        [Key, Column("IOT_NAME")] public string IotName { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("CONNECTION")] public string Connection { get; set; }
        [Column("COUNT")] public int Count { get; set; }
        [Column("RESET")] public string Reset { get; set; }
        [Column("COUNT_TIME")] public DateTime? CountTime { get; set; }
        [Column("PREV_COUNT_TIME")] public DateTime? PrevCountTime { get; set; }
        [Column("RUN_STATUS")] public string RunStatus { get; set; }
        [Column("WORK_NO")] public string WorkNo { get; set; }
    }
}