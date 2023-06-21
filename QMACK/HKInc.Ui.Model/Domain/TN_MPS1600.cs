using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220401 오세완 차장 비가동관리
    /// 20220404 오세완 차자 TN_MEA1004로 대체 사용하기로 하여 사용하지 않음
    /// </summary>
    [Table("TN_MPS1600T")]
    public class TN_MPS1600 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1600() { }

        /// <summary>
        /// 20220401 오세완 차장 
        /// 비가동일
        /// </summary>
        [Key,Column("STOP_DATE", Order =0)]
        public Nullable<DateTime> StopDate { get; set; }

        /// <summary>
        /// 20220401 오세완 차장 
        /// 비가동ID
        /// </summary>
        [Key,Column("STOP_SEQ",Order =1)]
        public string StopSeq { get; set; }

        /// <summary>
        /// 20220401 오세완 차장 
        /// 비가동시작일
        /// </summary>
        [Column("STOP_START_TIME")]
        public Nullable<DateTime> StopStarttime { get; set; }

        /// <summary>
        /// 20220401 오세완 차장
        /// 비가동종료일
        /// </summary>
        [Column("STOP_END_DATE")]
        public Nullable<DateTime> StopEnddate { get; set; }

        /// <summary>
        /// 20220401 오세완 차장 
        /// 비가동코드
        /// </summary>
        [Column("STOP_CODE")]
        public string StopCode { get; set; }

        /// <summary>
        /// 20220401 오세완 차장
        /// 설비코드
        /// </summary>
        [Column("MACHINE_CODE")]
        public string MachineCode { get; set; }

        /// <summary>
        /// 20220401 오세완 차장
        /// 메모
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }

        /// <summary>
        /// 20220401 오세왼 차장
        /// 임시를 작업중일때 비가동처리한 작업지시 번호 매칭으로 사용
        /// </summary>
        [Column("TEMP")]
        public string StopWorkNo { get; set; }

        /// <summary>
        /// 20220401 오세완 차장
        /// 임시1을 작업중일때 비가동처리한 공정코드 매칭으로 사용
        /// </summary>
        [Column("TEMP1")]
        public string StopProcessCode { get; set; }

        /// <summary>
        /// 20220401 오세완 차장
        /// 임시2
        /// </summary>
        [Column("TEMP2")]
        public string Temp2 { get; set; }

        [NotMapped]
        public string DdifM
        { get
            {
                DateTime? stopday = StopEnddate == null ? DateTime.Now : StopEnddate;
                TimeSpan diff = Convert.ToDateTime(StopEnddate) - Convert.ToDateTime(StopStarttime);
                return Convert.ToString(diff.Minutes);
            }
        }

    }
}