using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFPUR1500_JI_MASTER
    {
        //public decimal RowId { get; set; }

        public string ItemCode { get; set; } //품목코드
        public string ItemName { get; set; } //품목명
        public string Spec1 { get; set; } //스펙1
        public string Spec2 { get; set; } //스펙2
        public string Spec3 { get; set; } //스펙2
        public string Spec4 { get; set; } //스펙3
        public string Unit { get; set; } //단위

        public decimal SafeQty { get; set; } //안전재고
        public decimal SumInQty { get; set; } //입구량
        public decimal SumOutQty { get; set; } //출고량
        public decimal SumStockQty { get; set; } //재고량


    }
}
