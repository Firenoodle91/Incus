using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 월별매출관리 TEMP
    /// </summary>
    public class TEMP_XRREP5001
    {

        public string OutDate { get; set; }

        /// <summary>거래처</summary>
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }


        /// <summary>품목코드</summary>
        public string ItemCode { get; set; }

        /// <summary>품명</summary>
        public string ItemName { get; set; }


        /// <summary>단가</summary>
        public decimal Cost { get; set; }

        /// <summary>출고수량</summary>
        public decimal OutQty { get; set; }

        public decimal Sales //매출액
        {
            get
            {
                var cost = Cost;
                var outqty = OutQty;

                return cost * outqty;
            }
        }

    }
}
