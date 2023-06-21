using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XRREP9000
    {

        /// <summary>생산계획시작일</summary>      
        public DateTime PlanStartDate { get; set; }
        public DateTime PlanEndDate { get; set; }

        public string DelivNo { get; set; } //납품계획번호
        public string PlanNo { get; set; } //생산계획번호
        public string WorkNo { get; set; } //작업지시번호
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public decimal PlanQty { get; set; } //계획수량
        public decimal WorkQty { get; set; } //지시수량
        public decimal ResultSumQty { get; set; } //첫공정 실적수량
        public decimal ReadyQty { get; set; } //첫공정 작업대기수량

        public decimal Rate //생산계획수량 대비 생산수량
        {
            get
            {
                var planqty = PlanQty;
                var resultsumqty = ResultSumQty;

                if (planqty == 0 || resultsumqty == 0)
                    return 0;
                else
                    return resultsumqty / planqty * 100;
            }
        }
    }
}
