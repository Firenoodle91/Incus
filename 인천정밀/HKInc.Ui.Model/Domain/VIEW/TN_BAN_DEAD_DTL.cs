using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>반제품재고마감 디테일</summary>	
    [Table("TN_BAN_DEAD_DTL")]
    public class TN_BAN_DEAD_DTL : BaseDomain.MES_BaseDomain
    {
        public TN_BAN_DEAD_DTL()
        {
        }
        /// <summary>마감일자</summary>             
        [ForeignKey("TN_BAN_DEAD_MST"), Key, Column("DEADLINE_DATE", Order = 0), Required(ErrorMessage = "DeadLineDate")] public DateTime DeadLineDate { get; set; }
        /// <summary>생산 LOT NO</summary>              
        [Key, Column("PRODUCT_LOT_NO", Order = 1), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>품번(도번)</summary>            
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>이월재고량</summary>                   
        [Column("CARRY_OVER_QTY"), Required(ErrorMessage = "CarryOverQty")] public decimal CarryOverQty { get; set; }
        /// <summary>생산량</summary>                   
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>출고량</summary>                   
        [Column("OUT_QTY"), Required(ErrorMessage = "OutQty")] public decimal OutQty { get; set; }
        /// <summary>이월재고조정량</summary>                   
        [Column("ADJUST_QTY"), Required(ErrorMessage = "AdjustQty")] public decimal AdjustQty { get; set; }
        /// <summary>총재고량</summary>                   
        [Column("STOCK_QTY"), Required(ErrorMessage = "StockQty")] public decimal StockQty { get; set; }
        /// <summary>메모</summary>                     
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                     
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                    
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                    
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_BAN_DEAD_MST TN_BAN_DEAD_MST { get; set; }
    }
}