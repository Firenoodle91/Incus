using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
  public  class TP_SALETODLVRAT
    {

        public TP_SALETODLVRAT()
        { }
     
        public string OrderNo { get; set; }
        public int Seq { get; set; }
        public string CustCode { get; set; }
        public string ItemCode { get; set; }
        public string CustNm { get; set; }
        public string ItemNm { get; set; }
        public string ItemNm1 { get; set; } // 2022-02-08 김진우 대리 추가
        public Nullable<decimal> OrderQty { get; set; }
        public Nullable<DateTime> PeriodDate { get; set; }
        public Nullable<DateTime> OutDate { get; set; }
        public Nullable<int> OkQty { get; set; }
        public Nullable<int> FQty { get; set; }
        public Nullable<decimal> MQty { get; set; }
        public string Rat { get; set; }
        public string TRat { get; set; }
        public Nullable<decimal> Rat1 { get; set; }


    }
}

