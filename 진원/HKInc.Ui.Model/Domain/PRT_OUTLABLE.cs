using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    public class PRT_OUTLABLE
    {
        public string ItemCode { get; set; }
        public string ItemNm { get; set; }
        public string ItemNm1 { get; set; }
        public Nullable<Decimal> Qty { get; set; }
        public string PrtDate { get; set; }

        public string LotNo { get; set; }
        public string CustLotNo { get; set; }
       

    }
}
