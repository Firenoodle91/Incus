using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{   
    /// <summary>제품별표준공정관리</summary>	
    [Table("TN_MPS1000T")]
    public class TN_MPS1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1000()
        {
        }
        /// <summary>품번(도번)</summary>              
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>                    
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>공정코드</summary>                
        [Column("PROCESS_CODE"), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순서</summary>                
        [Column("PROCESS_SEQ"), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>파일명</summary>                  
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                 
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>사용여부</summary>                
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        /// <summary>외주여부</summary>                
        [Column("OUT_PROC_FLAG"), Required(ErrorMessage = "OutProcFlag")] public string OutProcFlag { get; set; }
        /// <summary>설비그룹코드</summary>          
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비사용여부</summary>            
        [Column("MACHINE_FLAG"), Required(ErrorMessage = "MachineFlag")] public string MachineFlag { get; set; }
        ///// <summary>툴사용여부</summary>            
        //[Column("TOOL_USE_FLAG"), Required(ErrorMessage = "ToolUseFlag")] public string ToolUseFlag { get; set; }
        ///// <summary>
        ///// 20210520 오세완 차장
        ///// 기존에 작업설정검사인 것을 재가동 TO여부로 변경
        ///// </summary>            
        //[Column("RESTART_TO_FLAG"), Required(ErrorMessage = "RestartToFlag")]
        //public string RestartToFlag { get; set; }
        //[Column("JOB_SETTING_FLAG"), Required(ErrorMessage = "JobSettingFlag")] public string JobSettingFlag { get; set; }
        /// <summary>표준작업소요일</summary>          
        [Column("STD_WORK_DAY")] public string StdWorkDay { get; set; }
        /// <summary>금형일상점검여부</summary>          
        [Column("MOLD_DAYINSP_FLAG")] public string MoldDayInspFlag { get; set; }
        /// <summary>임시</summary>                    
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                   
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                   
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped] public object localImage { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}