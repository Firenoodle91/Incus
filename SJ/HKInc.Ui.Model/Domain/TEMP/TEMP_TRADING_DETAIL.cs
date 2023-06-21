using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_TRADING_DETAIL
    {
        public int No { get; set; }
        public string Date { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Spec { get; set; }
        public string Unit { get; set; }
        public decimal Qty { get; set; }
        public decimal Cost { get; set; }
        public decimal Amt { get; set; }
        public decimal SupplyCost { get; set; }
        public decimal TaxCost { get; set; }
        public string Memo { get; set; }
    }
}
