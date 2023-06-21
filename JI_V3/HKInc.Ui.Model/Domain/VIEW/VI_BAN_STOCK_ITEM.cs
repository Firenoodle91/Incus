using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>반제품 입고 품목 별 재고</summary>	
    [Table("VI_BAN_STOCK_ITEM")]
    public class VI_BAN_STOCK_ITEM
    {
        public VI_BAN_STOCK_ITEM()
        {

        }
        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>품목코드</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>총 입고량</summary>                 
        [Column("SUM_IN_QTY")] public decimal SumInQty { get; set; }
        /// <summary>총 출고량</summary>                 
        [Column("SUM_OUT_QTY")] public decimal SumOutQty { get; set; }
        /// <summary>총 이월재고량</summary>                 
        [Column("SUM_CARRY_OVER_QTY")] public decimal SumCarryOverQty { get; set; }
        /// <summary>총 재고량</summary>                 
        [Column("SUM_STOCK_QTY")] public decimal SumStockQty { get; set; }
        /// <summary>총 미수입검사량</summary>                 
        [Column("SUM_INSP_IN_NOT_QTY")] public decimal SumInspectionInNotQty { get; set; }
        /// <summary>총 불량수입검사량</summary>                 
        [Column("SUM_INSP_IN_BAD_QTY")] public decimal SumInspectionInBadQty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}