using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary> 출고 자동 계산</summary>	
    public class TEMP_AUTO_SHIPMENT
    {
        public TEMP_AUTO_SHIPMENT()
        {
            _Check = "N";
        }
        
        /// <summary>고유번호(가상번호)</summary>           
        public long RowIndex { get; set; }
        /// <summary>품목코드</summary>           
        public string ItemCode { get; set; }
        /// <summary>생산 LOT NO</summary>             
        public string ProductLotNo { get; set; }
        /// <summary>고객 LOT NO</summary>             
        public string CustomerLotNo { get; set; }
        /// <summary>재고량</summary>                 
        public decimal StockQty { get; set; }

        [NotMapped] public string _Check { get; set; }
    }
}