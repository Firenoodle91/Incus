using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 거래명세서 도메인
    /// </summary>
    public class RORD1200_DETAIL
    {
        public string Date { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public decimal Qty { get; set; }
        public decimal Cost { get; set; }
        public decimal SupplyCost { get; set; }
        public decimal TaxCost { get; set; }
    }
}
