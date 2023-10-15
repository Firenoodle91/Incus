using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 표준공정타입관리 디테일
    /// </summary>
    [Table("TN_MPS1002T")]
    public class TN_MPS1002 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1002()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [ForeignKey("TN_MPS1001"), Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")] public string TypeCode { get; set; }
        /// <summary>순번</summary>                    
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>공정코드</summary>                
        [Column("PROCESS_CODE"), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순서</summary>                
        [Column("PROCESS_SEQ"), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>외주여부</summary>                
        [Column("OUT_PROC_FLAG"), Required(ErrorMessage = "OutProcFlag")] public string OutProcFlag { get; set; }
        /// <summary>설비그룹코드</summary>          
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비사용여부</summary>            
        [Column("MACHINE_FLAG"), Required(ErrorMessage = "MachineFlag")] public string MachineFlag { get; set; }
        /// <summary>툴사용여부</summary>            
        [Column("TOOL_USE_FLAG")] public string ToolUseFlag { get; set; }
        /// <summary>표준작업소요일</summary>          
        [Column("STD_WORK_DAY")] public string StdWorkDay { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MPS1001 TN_MPS1001 { get; set; }
    }
}
