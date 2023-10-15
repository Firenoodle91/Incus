using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>제품재고관리 디테일 (LOT NO별)</summary>	
    public class TEMP_PROD_STOCK_DETAIL_LOT_NO
    {
        public TEMP_PROD_STOCK_DETAIL_LOT_NO()
        {

        }
        /// <summary>품목코드</summary>           
        public string ItemCode { get; set; }
        /// <summary>생산 LOT NO</summary>                 
        public string ProductLotNo { get; set; }
        /// <summary>입고량</summary>                 
        public decimal? InQty { get; set; }
        /// <summary>출고량</summary>                 
        public decimal? OutQty { get; set; }
        /// <summary>이월량</summary>                 
        public decimal? CarryOverQty { get; set; }
        /// <summary>재고량</summary>                 
        public decimal? StockQty { get; set; }
    }
}