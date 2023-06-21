using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    public class TP_FAIL_RAT
    {
        public TP_FAIL_RAT()
        { }


        public string ItemCode { get; set; }

        public string ItemNm { get; set; }
        public string Process { get; set; }
        public string ProcessNm { get; set; }
      

        public Nullable<int> FailQty { get; set; }

        public Nullable<int> ResultQty { get; set; }

        public Nullable<decimal> Rat { get; set; }
        public string Rat1 { get; set; }

    }
}

