using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210606 오세완 차장
    /// USP_GET_XPFMACHINESTOP_MACHINE_STATE 결과 반환 객체
    /// </summary>
    public class TEMP_XPFMACHINESTOP_MACHINE_STATUS
    {
        public TEMP_XPFMACHINESTOP_MACHINE_STATUS()
        {

        }

        /// <summary>
        /// 20210606 오세완 차장
        /// 설비고유코드
        /// </summary>
        public string MachineMCode { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 설비코드
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 설비명 한글
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 설비명 영문
        /// </summary>
        public string MachineNameENG { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 설비명 중문
        /// </summary>
        public string MachineNameCHN { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 가동 / 비가동 상태 코드
        /// </summary>
        public string MACHINE_STATUS { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 가동 / 비가동 상태명 한글
        /// </summary>
        public string STATUS_NAME { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 가동 / 비가동 상태명 영문
        /// </summary>
        public string STATUS_NAME_ENG { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 가동 / 비가동 상태명 중문
        /// </summary>
        public string STATUS_NAME_CHN { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 재가동to여부코드
        /// </summary>
        public string RESTART_TO_FLAG { get; set; }

        /// <summary>
        /// 20210606 오세완 차장
        /// 재가동to여부코드명
        /// </summary>
        public string RESTART_TO_FLAG_NAME { get; set; }

        /// <summary>
        /// 20210608 오세완 차장
        /// 작업자가 이걸 통해서 비가동을 풀어도 되는 설비인지 확인해본다. 
        /// </summary>
        public string STOP_TO_RUN_STATUS { get; set; }
    }
}
