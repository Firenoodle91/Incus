using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
  public  class TP_PUR1700LIST
    {

        public TP_PUR1700LIST()
        { }
        public Nullable<DateTime> WorkDate { get; set; }
        public string WorkNo { get; set; }
      
        public string Process { get; set; }
     
        public string MachineCode { get; set; }
       
        public string ItemCode { get; set; }
      
        public int PlanQty { get; set; }
     
        public string Memo { get; set; }
      

        public string JobStatus { get; set; }
      
      
        public int PSeq { get; set; }
        public string LotNo { get; set; }

        public int OkQty { get; set; }

    }
}
