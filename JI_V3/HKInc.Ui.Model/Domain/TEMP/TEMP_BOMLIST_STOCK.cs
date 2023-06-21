using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>bom리스트</summary>	
    public class TEMP_BOMLIST_STOCK
    {
        public TEMP_BOMLIST_STOCK()
        {
        }

        /// <summary>품목코드</summary>           
        public string ItemCode { get; set; }
        /// <summary>입고 LOT NO</summary>             
        public string InLotNo { get; set; }
        /// <summary>입고량</summary>                 
        public decimal InQty { get; set; }
        /// <summary>출고량</summary>                 
        public decimal OutQty { get; set; }
        /// <summary>재고량</summary>                 
        public decimal StockQty { get; set; }
        /// <summary>소요량</summary>                 
        public decimal UseQty { get; set; }
        /// <summary>수입검사여부</summary>                 
        public string CheckResult { get; set; }
        /*
        /// <summary>거래처lotno</summary>             
        public string InCustomerLotNo { get; set; }
        
        /// <summary>밀시트관리번호</summary>             
        public string MillManageNo { get; set; }
        /// <summary>밀시트번호</summary>             
        public string MillSheetNo { get; set; }*/
        /// <summary>재출고여부</summary>             
        public string ReOutYn { get; set; }
        public string ProcessCode { get; set; }
    }
}
