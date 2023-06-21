using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
  public  class TP_WORKER
    {

        public TP_WORKER()
        { }
     
        public string Process { get; set; }
      
        public string ItemCode { get; set; }
        public string ProcessNm { get; set; }

        public string ItemNm { get; set; }
        public string ItemNm1 { get; set; }
        public string Worker { get; set; }
        public string UserName { get; set; }

        public Nullable<int> ResultQty { get; set; }
       
        public Nullable<int> FQty { get; set; }
        public Nullable<int> OKQty { get; set; }







    }
}
