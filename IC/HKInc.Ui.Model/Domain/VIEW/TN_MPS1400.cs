using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 20210610 오세완 차장
    /// 설비등록관리에서 비가동품질확인 여부를 체크한 설비나 품질확인이 걸린 비가동 설비를 풀어주는 주체가 누구인지 기록하는 테이블
    /// </summary>
    [Table("TN_MPS1400T")]
    public class TN_MPS1400 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1400()
        {

        }

        /// <summary>
        /// 20210610 오세완 차장
        /// 설비관리코드
        /// </summary>
        [Key, Column("MACHINE_CODE", Order = 0)]
        public string MachineCode { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 테이블 고유 순번
        /// </summary>
        [Key, Column("MEA1004T_ROWID", Order = 1)]
        public decimal Mea1400RowId { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 품질확인자
        /// </summary>
        [Column("WORK_ID")]
        public string WorkId { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 품질확인일자
        /// </summary>
        [Column("ACT_DATE")]
        public DateTime? ActDate { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 임시
        /// </summary>
        [Column("TEMP")]
        public string Temp { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 임시1
        /// </summary>
        [Column("TEMP1")]
        public string Temp1 { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 임시2
        /// </summary>
        [Column("TEMP2")]
        public string Temp2 { get; set; }


    }
}
