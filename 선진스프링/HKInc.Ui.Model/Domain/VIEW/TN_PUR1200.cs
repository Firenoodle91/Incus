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
    /// <summary>자재입고 마스터</summary>	
    [Table("TN_PUR1200T")]
    public class TN_PUR1200 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1200()
        {
            TN_PUR1201List = new List<TN_PUR1201>();
        }
        /// <summary>입고번호</summary>            
        [Key, Column("IN_NO"), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>발주번호</summary>            
        [ForeignKey("TN_PUR1100"), Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>입고일</summary>              
        [Column("IN_DATE"), Required(ErrorMessage = "InDate")] public DateTime InDate { get; set; }
        /// <summary>입고자</summary>              
        [Column("IN_ID"), Required(ErrorMessage = "InId")] public string InId { get; set; }
        /// <summary>입고처</summary>              
        [ForeignKey("TN_STD1400"), Column("IN_CUSTOMER_CODE"), Required(ErrorMessage = "InCustomer")] public string InCustomerCode { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual TN_PUR1100 TN_PUR1100 { get; set; }
        public virtual ICollection<TN_PUR1201> TN_PUR1201List { get; set; }
    }
}