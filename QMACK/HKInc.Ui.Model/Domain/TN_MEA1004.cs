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
    /// 20220404
    /// 설비 비가동관리, TN_MPS1600을 대체
    /// </summary>	
    [Table("TN_MEA1004T")]
    public class TN_MEA1004 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1004()
        {
        }
        /// <summary>설비코드</summary>                   
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_CODE", Order = 0), Required(ErrorMessage = "MachineCode")]
        public string MachineCode { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID",Order = 1), Required(ErrorMessage = "RowId is required")]
        public new decimal RowId { get; set; }
        
        /// <summary>비가동사유</summary>                 
        [Column("STOP_CODE"), Required(ErrorMessage = "StopCode")]
        public string StopCode { get; set; }
        
        /// <summary>비가동시작시간</summary>             
        [Column("STOP_START_TIME"), Required]
        public DateTime? StopStartTime { get; set; }
        
        /// <summary>비가동종료시간</summary>             
        [Column("STOP_END_TIME")]
        public DateTime? StopEndTime { get; set; }

        /// <summary>
        /// 20220404 오세완 차장
        /// 비가동 처리한 작업지시번호
        /// </summary>
        [Column("WORK_NO")]
        public string WorkNo { get; set; }

        /// <summary>
        /// 20220404 오세완 차장
        /// 비가동 처리한 공정코드
        /// </summary>
        [Column("PROCESS")]
        public string Process { get; set; }
        
        /// <summary>메모</summary>                       
        [Column("MEMO")]
        public string Memo { get; set; }
        
        /// <summary>
        /// 20220404 오세완 차장
        /// 임시
        /// </summary>                       
        [Column("TEMP")]
        public string Temp { get; set; }

        /// <summary>
        /// 20220404 오세완 차장
        /// 임시1
        /// </summary>                      
        [Column("TEMP1")]
        public string Temp1 { get; set; }

        /// <summary>
        /// 20220404 오세완 차장
        /// 임시2
        /// </summary>                      
        [Column("TEMP2")]
        public string Temp2 { get; set; }

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