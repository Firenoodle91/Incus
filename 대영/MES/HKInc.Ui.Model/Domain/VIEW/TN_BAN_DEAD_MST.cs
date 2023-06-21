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
    /// <summary>반제품재고마감 마스터</summary>	
    [Table("TN_BAN_DEAD_MST")]
    public class TN_BAN_DEAD_MST : BaseDomain.MES_BaseDomain
    {
        public TN_BAN_DEAD_MST()
        {
            TN_BAN_DEAD_DTL_List = new List<TN_BAN_DEAD_DTL>();
        }

        /// <summary>마감일자</summary>             
        [Key, Column("DEADLINE_DATE"), Required(ErrorMessage = "DeadLineDate")] public DateTime DeadLineDate { get; set; }
        /// <summary>실마감일자</summary>           
        [Column("REAL_DEADLINE_DATE"), Required(ErrorMessage = "RealDeadLineDate")] public DateTime RealDeadLineDate { get; set; }
        /// <summary>구분</summary>           
        [Column("DIVISION"), Required(ErrorMessage = "Division")] public string Division { get; set; }
        /// <summary>마감자</summary>           
        [Column("DEADLINE_ID"), Required(ErrorMessage = "DeadLineId")] public string DeadLineId { get; set; }
        /// <summary>메모</summary>                 
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                 
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_BAN_DEAD_DTL> TN_BAN_DEAD_DTL_List { get; set; }
    }
}