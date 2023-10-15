using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 작업지시참조 디테일
    /// </summary>
    public class TEMP_WORK_REF_DETAIL
    {
        /// <summary>작업지시번호 </summary>
        public string WorkNo { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>제조팀</summary>
        public string ProcTeamCode { get; set; }
        /// <summary>생산계획시작일</summary>
        public DateTime? PlanStartDate { get; set; }
        /// <summary>생산계획종료일</summary>
        public DateTime? PlanEndDate { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>작업지시수량</summary>
        public decimal WorkQty { get; set; }
        /// <summary>생산수량</summary>
        public decimal ResultQty { get; set; }
        /// <summary>필요수량</summary>
        public decimal RequireQty { get; set; }
        /// <summary>본 필요수량</summary>
        public decimal WorkRequireQty { get; set; }
        /// <summary>투입중량</summary>
        public decimal? InputWeight { get; set; }
        
    }
}
