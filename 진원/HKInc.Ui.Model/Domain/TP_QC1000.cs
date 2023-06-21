using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    public class TP_QC1000
    {
        public TP_QC1000()
        { }


       public string ProcessCode { get; set; }
   public string CheckName { get; set; }//검사항목
         public string ProcessGu { get; set; }//검사종류
         public string CheckProv { get; set; }//검사방법
       public string CheckStand { get; set; }//규격
        public Nullable<double> UpQuad { get; set; }//상한
         public Nullable<double> DownQuad { get; set; }//하한
        public string Temp2 { get; set; }//검사방법
        public string Temp1 { get; set; }//계측기종류
    }
}

