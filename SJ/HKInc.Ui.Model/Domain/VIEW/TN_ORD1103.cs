using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>출고증관리 마스터</summary>	
    [Table("TN_ORD1103T")]
    public class TN_ORD1103 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1103()
        {
            TN_ORD1101List = new List<TN_ORD1101>();
        }

        [Key, Column("OUT_REP_NO"), Required(ErrorMessage = "OutRepNo")] public string OutRepNo { get; set; }
        /// <summary>출고예정일</summary>                
        [Column("OUT_PLAN_DATE"), Required(ErrorMessage = "OutDatePlan")] public DateTime OutDatePlan { get; set; }
        ///// <summary>영업담당자</summary>              
        [Column("BUSINESS_MANAGEMENT_ID"), Required(ErrorMessage = "BusinessManagementId")] public string BusinessManagementId { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_ORD1101> TN_ORD1101List { get; set; }
    }
}