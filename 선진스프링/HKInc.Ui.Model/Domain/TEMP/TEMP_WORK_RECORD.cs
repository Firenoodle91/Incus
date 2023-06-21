using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>작업지시이력 TEMP</summary>	
    public class TEMP_WORK_RECORD
    {
        public TEMP_WORK_RECORD()
        {
        }

        /// <summary>작업지시번호</summary>           
        public string WorkNo { get; set; }
        /// <summary>발행일</summary>             
        public DateTime? WorkNoDate { get; set; }
        /// <summary>작업의뢰량</summary>                 
        public decimal? PlanWorkQty { get; set; }
        /// <summary>생산수량</summary>                 
        public decimal? OkQty { get; set; }
        /// <summary>포장수량</summary>                 
        public decimal? PackQty { get; set; }
    }
}