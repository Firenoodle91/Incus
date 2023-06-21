using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>품목이슈관리</summary>	
    [Table("TN_STD1102T")]
    public class TN_STD1102 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1102()
        {
        }
        /// <summary>품목코드</summary>                
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>                    
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>이슈사항</summary>                
        [Column("ISSUE")] public string Issue { get; set; }
        /// <summary>시작일</summary>                  
        [Column("START_DATE")] public DateTime? StartDate { get; set; }
        /// <summary>종료일</summary>                  
        [Column("END_DATE")] public DateTime? EndDate { get; set; }
        /// <summary>등록자</summary>                  
        [Column("ISSUE_ID")] public string IssueId { get; set; }
        /// <summary>메모</summary>                    
        [Column("MEMO")]	public string Memo { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}