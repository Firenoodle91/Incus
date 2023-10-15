using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    public class TP_XFPUR1500_LIST
    {
        public TP_XFPUR1500_LIST()
        {

        }

        public string ITEM_CODE { get; set; }

        public string ITEM_NM1 { get; set; }

        public string ITEM_NM { get; set; }

        public string TOP_CATEGORY { get; set; }

        public string TOP_CATEGORY_NAME { get; set; }

        public string MIDDLE_CATEGORY { get; set; }

        public string MIDDLE_CATEGORY_NAME { get; set; }

        public string BOTTOM_CATEGORY { get; set; }

        public string BOTTOM_CATEGORY_NAME { get; set; }

        public string UNIT { get; set; }

        public string UNIT_NAME { get; set; }

        public decimal SAFE_QTY { get; set; }

        public decimal SUM_INPUT { get; set; }

        public decimal SUM_OUTPUT { get; set; }

        public decimal TOTAL_QTY{ get; set; }

    }
}
