using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>창고 위치별 상세 재고</summary>	
    [Table("VI_WH_POSITON_QTY")]
    public class VI_WH_POSITON_QTY
    {
        public VI_WH_POSITON_QTY()
        {

        }

        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>품목코드</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>LOT NO</summary>                 
        [Column("LOT_NO")] public string LotNo { get; set; }
        /// <summary>창고코드</summary>                 
        [Column("WH_CODE")] public string WhCode { get; set; }
        /// <summary>위치코드</summary>                 
        [Column("POSITION_CODE")] public string PositionCode { get; set; }
        /// <summary>재고량</summary>                 
        [Column("STOCK_QTY")] public decimal StockQty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}