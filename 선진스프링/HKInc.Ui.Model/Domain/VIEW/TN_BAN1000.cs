using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>반제품입고관리 마스터</summary>	
    [Table("TN_BAN1000T")]
    public class TN_BAN1000 : BaseDomain.MES_BaseDomain
    {
        public TN_BAN1000()
        {
            TN_BAN1001List = new List<TN_BAN1001>();
        }
        /// <summary>입고번호</summary>           
        [Key, Column("IN_NO"), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고일</summary>             
        [Column("IN_DATE"), Required(ErrorMessage = "InDate")] public DateTime InDate { get; set; }
        /// <summary>입고자</summary>             
        [Column("IN_ID")] public string InId { get; set; }
        /// <summary>메모</summary>               
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>               
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>              
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>              
        [Column("TEMP2")] public string Temp2 { get; set; }
        
        public virtual ICollection<TN_BAN1001> TN_BAN1001List { get; set; }
    }
}