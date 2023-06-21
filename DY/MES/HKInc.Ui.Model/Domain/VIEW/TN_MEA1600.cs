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
    /// 20210707 오세완 차장
    /// PLC인터페이스 테이블, PLC와 인터페이스할 기초정보와 PLC에 직접 반영할 리셋신호와 생산카운트값을 db에 기록
    /// </summary>
    [Table("TN_MEA1600T")]
    public class TN_MEA1600 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1600()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 20210707 오세완 차장
        /// PLC 명
        /// </summary>
        [Key, Column("PLC_NAME", Order = 0)]
        public string PlcName { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 설비코드
        /// </summary>
        [Column("MACHINE_CODE")]
        public string MachineCode { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// IP, PLC에 설정된 값
        /// </summary>
        [Column("IP")]
        public string Ip { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 포트번호, PLC에 설정된 값
        /// </summary>
        [Column("PORT")]
        public int? Port { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 연결상태, 최초 POP가 연결 성공한 경우 값을 변경
        /// </summary>
        [Column("CONNECTION")]
        public string Connection { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 생산카운트, POP가 PLC로부터 신호를 수신하면 UPDATE처리
        /// </summary>
        [Column("COUNT")]
        public int? Count { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 생산카운트 리셋여부, 작업완료나 원소재 교체시 PLC에 직접 명령후 값을 변경할 예정
        /// </summary>
        [Column("RESET")]
        public string Reset { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 실적변경시간, 마지막 생산카운트를 변경한 시간으로 사용
        /// </summary>
        [Column("COUNT_TIME")]
        public DateTime? CountTime { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 이전 실적변경시간, PLC로부터 수신받은 생산카운트를 갱신하기 전 실적을 기록한 시간
        /// </summary>
        [Column("PREV_COUNT_TIME")]
        public DateTime? PrevCountTime { get; set; }

        /// <summary>
        /// 20210707 오세완 차장
        /// 가동 / 비가동상태, COUNT_TIME과 PREV_COUNT_TIME을 계산하여 일정 주기보다 멀면 비가동 처리할 예정
        /// </summary>
        [Column("RUN_STATUS")]
        public string RunStatus { get; set; }
    }
}
