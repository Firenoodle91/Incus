using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>POP 작업시작 시 BOM조회 
    /// - 원자재 및 반제품 출고LOT, 품목코드 조회
    /// </summary>	
    /// 

    [Table("VI_POP_WORK_START_BOM_CHECK")]
    public class VI_POP_WORK_START_BOM_CHECK
    {
        public VI_POP_WORK_START_BOM_CHECK()
        {

        }

        /// <summary>출고 LOT NO</summary>  
        [Key, Column("OUT_LOT_NO", Order = 0)] public string OutLotNo { get; set; }
        /// <summary>품목코드</summary> 
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}