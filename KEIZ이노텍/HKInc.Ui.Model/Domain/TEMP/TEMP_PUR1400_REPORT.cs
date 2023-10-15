using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>외주발주 주문서</summary>	
    public class TEMP_PUR1400_REPORT
    {
        public TEMP_PUR1400_REPORT()
        {

        }
        /// <summary>순번</summary>           
        public Int64 Seq { get; set; }
        /// <summary>품번(도번)</summary>           
        public string ItemCode { get; set; }
        /// <summary>중량</summary>                 
        public decimal PoQty { get; set; }
        /// <summary>단가</summary>                 
        public decimal PoCost { get; set; }
        /// <summary>납기일</summary>                 
        public DateTime? DueDate { get; set; }
        /// <summary>메모</summary>           
        public string Memo { get; set; }
    }
}