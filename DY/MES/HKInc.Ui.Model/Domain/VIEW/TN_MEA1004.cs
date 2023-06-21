using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비 비가동관리</summary>	
    [Table("TN_MEA1004T")]
    public class TN_MEA1004 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1004()
        {
        }
        /// <summary>설비코드</summary>                   
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_CODE", Order = 0), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID",Order = 1), Required(ErrorMessage = "RowId is required")]
        public new decimal RowId { get; set; }
        /// <summary>비가동사유</summary>                 
        [Column("STOP_CODE"), Required(ErrorMessage = "StopCode")] public string StopCode { get; set; }
        /// <summary>비가동시작시간</summary>             
        [Column("STOP_START_TIME"), Required] public DateTime? StopStartTime { get; set; }
        /// <summary>비가동종료시간</summary>             
        [Column("STOP_END_TIME")] public DateTime? StopEndTime { get; set; }
        /// <summary>메모</summary>                       
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 20210627 오세완 차장
        /// 비가동시 품질확인 비가동으로 설정한 경우 해당 작업지시 번호를 설정
        /// 20210628 오세완 차장 품목 및 설비 비종속적으로 변경해야 해서 임시로 변경
        /// 20220114 오세완 차장 자동으로 비가동을 해제한 경우 여기에 내용 기록용도로 변경
        /// </summary>                       
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>
        /// 20210627 오세완 차장
        /// 비가동시 품질확인 비가동으로 설정시 이걸로 해재/ 설정을 제어
        /// </summary>                      
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>
        /// 20210627 오세완 차장
        /// 비가동설정시 해당 적업지시의 전 작업상태를 저장처리
        /// 20210628 오세완 차장 품목 및 설비 비종속적으로 변경해야 해서 임시로 변경
        /// </summary>                      
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped]
        public decimal StopM
        {
            get
            {
                if (StopEndTime == null)
                    return 0;
                else
                {
                    DateTime? stopday = StopEndTime == null ? DateTime.Now : StopEndTime;
                    TimeSpan diff = Convert.ToDateTime(stopday) - Convert.ToDateTime(StopStartTime);
                    return Convert.ToDecimal(diff.TotalMinutes);
                }
            }
        }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
    }
}