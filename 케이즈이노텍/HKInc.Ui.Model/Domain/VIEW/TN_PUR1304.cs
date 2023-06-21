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
    /// <summary>자재반품 마스터</summary>	
    [Table("TN_PUR1304T")]
    public class TN_PUR1304 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1304()
        {
            TN_PUR1305List = new List<TN_PUR1305>();
        }
        /// <summary>입고번호</summary>            
        [Key, Column("RETURN_NO"), Required(ErrorMessage = "ReturnNo")] public string ReturnNo { get; set; }
        /// <summary>입고번호</summary>            
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>발주번호</summary>            
        [Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>입고일자</summary>              
        [Column("IN_DATE")] public DateTime InDate { get; set; }
        /// <summary>입고담당자</summary>              
        [Column("IN_ID"), Required(ErrorMessage = "InId")] public string InId { get; set; }
        /// <summary>입고처</summary>              
        [ForeignKey("TN_STD1400"), Column("IN_CUSTOMER_CODE")] public string InCustomerCode { get; set; }
        // <summary>협력사입고일</summary>              
        [Column("IN_CUSTOMER_DATE")] public DateTime? InCustomerDate { get; set; }
        // <summary>협력사입고자</summary>              
        [Column("IN_CUSTOMER_ID")] public string InCustomerId { get; set; }
        /// <summary>반품일자</summary>              
        [Column("RETURN_DATE"), Required(ErrorMessage = "ReturnDate")] public DateTime ReturnDate { get; set; }
        /// <summary>반품담당자</summary>              
        [Column("RETURN_ID"), Required(ErrorMessage = "ReturnId")] public string ReturnId { get; set; }
        /// <summary>MES메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>협력사메모</summary>                
        [Column("MEMO1")] public string Memo1 { get; set; }        
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }
        /// <summary>입고확인여부</summary>                  
        [NotMapped] public string InConfirmState { get; set; }




        public virtual TN_STD1400 TN_STD1400 { get; set; }
        //public virtual TN_PUR1100 TN_PUR1100 { get; set; }
        public virtual ICollection<TN_PUR1305> TN_PUR1305List { get; set; }
    }
}