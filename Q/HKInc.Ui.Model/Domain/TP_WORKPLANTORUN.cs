using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
  public  class TP_WORKPLANTORUN
    {

        public TP_WORKPLANTORUN()
        { }
     
        public string Process { get; set; }
      
        public string ItemCode { get; set; }
        public string ProcessNm { get; set; }

        public string ItemNm { get; set; }

        public Nullable<int> PlanQty { get; set; }
       
        public Nullable<int> RunQty { get; set; }
      
        public string Rat { get; set; }
        public Nullable<Decimal> Rat1 { get; set; }





    }
}
