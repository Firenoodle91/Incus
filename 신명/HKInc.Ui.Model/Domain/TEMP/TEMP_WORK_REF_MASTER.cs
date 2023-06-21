using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 작업지시참조 마스터
    /// </summary>
    public class TEMP_WORK_REF_MASTER
    {
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>규격</summary>
        public string Spec { get; set; }
        /// <summary>예상소요량</summary>
        public decimal PredictionQty { get; set; }
        /// <summary>재공재고량</summary>
        public decimal SrcStockQty { get; set; }
        /// <summary>재고량</summary>
        public decimal StockQty { get; set; }
        /// <summary>미입고량</summary>
        public decimal NotInQty { get; set; }
        /// <summary>3개월평균소요량</summary>
        public decimal ThirdSpendQty { get; set; }
    }
}
