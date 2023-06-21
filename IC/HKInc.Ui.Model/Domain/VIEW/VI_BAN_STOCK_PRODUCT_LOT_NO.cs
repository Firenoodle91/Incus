using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>반제품 생산 LOT NO 별 재고</summary>	
    [Table("VI_BAN_STOCK_PRODUCT_LOT_NO")]
    public class VI_BAN_STOCK_PRODUCT_LOT_NO
    {
        public VI_BAN_STOCK_PRODUCT_LOT_NO()
        {
            _Check = "N";
        }
        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>품목코드</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>생산 LOT NO</summary>             
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>입고량</summary>                 
        [Column("IN_QTY")] public decimal InQty { get; set; }
        /// <summary>출고량</summary>                 
        [Column("OUT_QTY")] public decimal OutQty { get; set; }
        /// <summary>이월재고량</summary>                 
        [Column("CARRY_OVER_QTY")] public decimal CarryOverQty { get; set; }
        /// <summary>재고량</summary>                 
        [Column("STOCK_QTY")] public decimal StockQty { get; set; }

        [NotMapped] public string _Check { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}