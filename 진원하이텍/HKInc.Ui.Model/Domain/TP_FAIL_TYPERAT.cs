using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
   public  class TP_FAIL_TYPERAT
    {
        public TP_FAIL_TYPERAT()
        { }

    
        public string ItemCode { get; set; }
        
        public string ItemNm { get; set; }
        public string ItemNm1 { get; set; }
        public string Process { get; set; }
        public string ProcessNm { get; set; }
        public string Ftype { get; set; }
        public string Fname { get; set; }

        public Nullable<int> FailQty { get; set; }       
        
        public Nullable<int> ProdQty { get; set; }

        public string Rat1 { get; set; }
        public Nullable<decimal> Rat { get; set; }


    }
}

