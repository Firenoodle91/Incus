using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summaryd> 설비별 공구 장착 리스트</summaryd></summary>	
    [Table("TN_TOOL1101T")]
    public class TN_TOOL1101 : BaseDomain.MES_BaseDomain2
    {
        public TN_TOOL1101()
        {
        }

        /// <summary>지시번호</summary>        
        [Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }

        /// <summary>설비코드</summary>        
        [Column("MACHINE_CODE", Order = 1), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }

        /// <summary> 품목코드 </summary>
        [Column("ITEM_CODE", Order = 2), Key, Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>
        /// 공정코드
        /// </summary>
        [Column("PROCESS_CODE", Order = 3), Key, Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }

        /// <summary>
        /// 공구코드
        /// </summary>
        [Column("TOOL_CODE", Order = 4), Key, Required(ErrorMessage = "ToolCode")] public string ToolCode { get; set; }

        /// <summary>설비고유코드</summary>        
        //[Column("MACHINE_MCODE"), Required(ErrorMessage = "MachineCode")] public string MachineMCode { get; set; }

        /// <summary>
        /// 공구 기본 수명
        /// </summary>
        [Column("BASE_CNT")] public Int32? BaseCNT { get; set; }

        /// <summary>
        /// 공구 잔여 수명
        /// </summary>
        [Column("LIFE_CNT")] public Int32? LifeCNT { get; set; }

        /// <summary>
        /// 교체 횟수
        /// </summary>
        [Column("CHANGE_CNT")] public Int32? ChangeCNT { get; set; }

        //[NotMapped] public string Check { get; set; }


    }
}