using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210610 오세완 차장
    /// USP_GET_MPS1400_LIST 반환 객체
    /// </summary>
    public class TEMP_MPS1400_LIST
    {
        public TEMP_MPS1400_LIST()
        {

        }

        /// <summary>
        /// 20210610 오세완 차장
        /// 설비관리코드
        /// </summary>
        public string MACHINE_CODE { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동유형 코드
        /// </summary>
        public string STOP_CODE { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 시작시간
        /// </summary>
        public DateTime? STOP_START_TIME { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 종료시간
        /// </summary>
        public DateTime? STOP_END_TIME { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동시간(분)
        /// </summary>
        public int? CALCULATE_MINITE { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 품질 관리여부
        /// </summary>
        public string TEMP1 { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 고유순번
        /// </summary>
        public decimal ROW_ID { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 품질확인 일자
        /// </summary>
        public DateTime? ACT_DATE { get; set; }

        /// <summary>
        /// 20210610 오세완 차장
        /// 비가동 확인자
        /// </summary>
        public string WORK_ID { get; set; }

        /// <summary>
        /// 20210610 오세완 차장 update / insert 구분자
        /// </summary>
        public string Flag { get; set; }
    }
}
