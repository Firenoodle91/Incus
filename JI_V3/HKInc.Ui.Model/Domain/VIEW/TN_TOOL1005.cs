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
    [Table("TN_TOOL1005T")]
    public class TN_TOOL1005 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1005()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        /// <summary>타입코드</summary> 
        [Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")] public string TypeCode { get; set; }

        /// <summary>공정코드</summary>                
        [Key, Column("PROCESS_CODE", Order = 1), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }

        /// <summary>공정코드</summary>                
        [Key, Column("TOOL_CODE", Order = 2), Required(ErrorMessage = "ToolCode")] public string ToolCode { get; set; }
        /// <summary>기본수명</summary>                
        [Column("BASE_CNT")] public Int32? BaseCNT { get; set; }


        public virtual TN_TOOL1004 TN_TOOL1004 { get; set; }
    }
}
