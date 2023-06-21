using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>작업지시관리</summary>	
    [Table("TN_MPS1200T")]
    public class TN_MPS1200 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1200()
        {
            TN_MPS1201List = new List<TN_MPS1201>();
            //TN_MPS1205List = new List<TN_MPS1205>();
        }
        /// <summary>작업지시번호</summary>           
        [Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>               
        [Key, Column("PROCESS_CODE", Order = 1), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>               
        [Key, Column("PROCESS_SEQ", Order = 2), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>생산계획번호</summary>           
        //[ForeignKey("TN_MPS1100"), Column("PLAN_NO"), Required(ErrorMessage = "PlanNo")] // 20210527 오세완 차장  수동작업지시때문에 계획번호가 없는 경우가 발생하기 때문에 생략 처리
        [Column("PLAN_NO")] 
        public string PlanNo { get; set; }
        /// <summary>품번(도번)</summary>             
        [Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>긴급여부</summary>               
        [Column("EMERGENCY_FLAG"), Required(ErrorMessage = "EmergencyFlag")] public string EmergencyFlag { get; set; }
        /// <summary>작업지시일</summary>             
        [Column("WORK_DATE"), Required(ErrorMessage = "WorkDate")] public DateTime WorkDate { get; set; }
        /// <summary>실작업예정일</summary>             
        [Column("REAL_WORK_DATE")] public DateTime? RealWorkDate { get; set; }
        /// <summary>설비그룹코드</summary>          
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비코드</summary>               
        [ForeignKey("TN_MEA1000"), Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>지시수량</summary>               
        [Column("WORK_QTY"), Required(ErrorMessage = "WorkQty")] public decimal WorkQty { get; set; }
        /// <summary>외주여부</summary>               
        [Column("OUT_PROC_FLAG"), Required(ErrorMessage = "OutProcFlag")] public string OutProcFlag { get; set; }
        /// <summary>작업자</summary>                 
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary>작업상태</summary>               
        [Column("JOB_STATES"), Required(ErrorMessage = "JobStates")] public string JobStates { get; set; }
        /// <summary>이전작업상태</summary>           
        [Column("PREVIOUS_JOB_STATES")] public string PreviousJobStates { get; set; }
        /// <summary>설비필수여부</summary>           
        [Column("MACHINE_FLAG")] public string MachineFlag { get; set; }
        ///// <summary>툴사용여부</summary>            
        //[Column("TOOL_USE_FLAG"), Required(ErrorMessage = "ToolUseFlag")] public string ToolUseFlag { get; set; }
        ///// <summary>
        ///// 20210520 오세완 차장
        ///// 기존에 작업설정검사인 것을 재가동 TO여부로 변경
        ///// </summary>            
        //[Column("RESTART_TO_FLAG"), Required(ErrorMessage = "RestartToFlag")] public string RestartToFlag { get; set; }
        //[Column("JOB_SETTING_FLAG"), Required(ErrorMessage = "JobSettingFlag")] public string JobSettingFlag { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>
        /// 20211220 오세완 차장
        /// 정렬표장여부로 변경
        /// </summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>고객 LOT NO</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }
        ///// <summary>리워크생성여부</summary>                  
        //[Column("REWORK_FLAG")] public string ReworkFlag { get; set; }
        ///// <summary>리워크생성여부</summary>                  
        //[Column("REWORK_JOB_STATES")] public string ReworkJobStates { get; set; }
        ///// <summary>리워크 작업지시일</summary>             
        //[Column("REWORK_WORK_DATE")] public DateTime? ReworkWorkDate { get; set; }

        /// <summary> 작업지시여부 </summary>
        [NotMapped]
        public string WorkOrderFlag { get { return string.Empty; } }

        [NotMapped]
        public DateTime? ChangeWorkDate { get; set; }
        
        public virtual TN_MPS1100 TN_MPS1100 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual TN_MEA1000 TN_MEA1000 { get; set; }

        public virtual ICollection<TN_MPS1201> TN_MPS1201List { get; set; }
        //public virtual ICollection<TN_MPS1205> TN_MPS1205List { get; set; }
    }
}