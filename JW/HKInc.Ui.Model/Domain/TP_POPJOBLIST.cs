using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
  public  class TP_POPJOBLIST
    {

        public TP_POPJOBLIST()
        { }
        public Nullable<DateTime> WorkDate { get; set; }
        public string WorkNo { get; set; }
      
        public string Process { get; set; }
     
        public string MachineCode { get; set; }
       
        public string ItemCode { get; set; }

        public string ItemCode1 { get; set; }

        public string Spec1 { get; set; }

        public string Spec2 { get; set; }

        public string Spec3 { get; set; }

        public string Spec4 { get; set; }

        public int PlanQty { get; set; }
     
        public string Memo { get; set; }
      
        public string JobStatus { get; set; }
      
        public string WorkStantadNm { get; set; }
      
        public byte[] FileData { get; set; }
     
        public string DesignFile { get; set; }
     
        public byte[] DesignMap { get; set; }

        public int PSeq { get; set; }

        public string EMType { get; set; }

        public int Eord { get; set; }

        public string Cust { get; set; }
    }
}
