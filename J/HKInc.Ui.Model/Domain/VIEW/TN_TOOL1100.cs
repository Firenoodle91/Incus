using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>툴 교체관리 디테일</summary>	
    [Table("TN_TOOL1100T")]
    public class TN_TOOL1100 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1100()
        {
        }

        /// <summary>
        /// 품목코드
        /// </summary>
        [Column("ITEM_CODE", Order = 0), Key, Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>
        /// 공정코드
        /// </summary>
        [Column("PROCESS_CODE", Order = 1), Key, Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }

        /// <summary>
        /// 공구코드
        /// </summary>
        [Column("TOOL_CODE", Order = 2), Key, Required(ErrorMessage = "ToolCode")] public string ToolCode { get; set; }

        /// <summary>설비고유코드</summary>        
        //[Column("MACHINE_MCODE"), Required(ErrorMessage = "MachineCode")] public string MachineMCode { get; set; }

        /// <summary>설비코드</summary>        
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }

        /// <summary>
        /// 공구 기본 수명
        /// </summary>
        [Column("BASE_CNT")] public Int32? BaseCNT { get; set; }

        /// <summary>
        /// 공구 잔여 수명
        /// </summary>
        [Column("LIFE_CNT")] public Int32? LifeCNT { get; set; }

        /// <summary>
        /// 공구 장착 위치
        /// </summary>
        [Column("TOOL_POSITION")] public string ToolPosition { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }


    }
}