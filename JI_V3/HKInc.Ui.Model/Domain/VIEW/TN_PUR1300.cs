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
    /// <summary>자재출고 마스터</summary>	
    [Table("TN_PUR1300T")]
    public class TN_PUR1300 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1300()
        {
            TN_PUR1301List = new List<TN_PUR1301>();
        }
        /// <summary>출고번호</summary>              
        [Key, Column("OUT_NO"), Required(ErrorMessage = "OutNo")] public string OutNo { get; set; }
        /// <summary>출고일</summary>                
        [Column("OUT_DATE"), Required(ErrorMessage = "OutDate")] public DateTime OutDate { get; set; }
        /// <summary>출고자</summary>                
        [Column("OUT_ID"), Required(ErrorMessage = "OutId")] public string OutId { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_PUR1301> TN_PUR1301List { get; set; }
    }
}