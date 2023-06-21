using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비가동정보</summary>	
    [Table("TN_MTTF1000T")]
    public class TN_MTTF1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MTTF1000()
        {
        }
        /// <summary>
        ///  설비코드
        /// </summary>
        [Key, Column("MACHINE_CODE", Order = 0)] public string MachineCode { get; set; }

        /// <summary>
        /// 연도
        /// </summary>
        [Key, Column("YYYY", Order = 1)] public string YYYY { get; set; }

        /// <summary>
        /// 월
        /// </summary>
        [Key, Column("MM", Order = 2)] public string MM { get; set; }

        /// <summary>
        /// 작업시간
        /// </summary>
        [Column("RUN_TIME")] public Nullable<int> RunTime { get; set; }

        /// <summary>
        /// 정지시간
        /// </summary>
        [Column("STOP_TIME")] public Nullable<int> StopTime { get; set; }

        /// <summary>
        /// 정지횟수
        /// </summary>
        [Column("STOP_CNT")] public Nullable<int> StopCnt { get; set; }
    }
}