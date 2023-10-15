using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재반품내역</summary>	
    [Table("VI_PUR1304")]
    public class VI_PUR1304
    {
        public VI_PUR1304()
        {
            _Check = "N";
        }

        /// <summary>입고번호</summary>           
        [Key, Column("IN_NO")] public string InNo { get; set; }
        /// <summary>scm여부</summary>           
        [Column("SCM_YN")] public string ScmYn { get; set; }
        /// <summary>입고처</summary>           
        [Column("IN_CUSTOMER_CODE")] public string InCustomerCode { get; set; }
        /// <summary>협력사입고일</summary>           
        [Column("IN_CUSTOMER_DATE")] public DateTime? InCustomerDate { get; set; }
        /// <summary>협력사입고담당자</summary>           
        [Column("IN_CUSTOMER_ID")] public string InCustomerId { get; set; }
        /// <summary>입고자</summary>           
        [Column("IN_ID")] public string InId { get; set; }
        /// <summary>메모</summary>           
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>협력사메모</summary>           
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>입고확정여부</summary>           
        [Column("IN_CONFIRM_FLAG")] public string InConfirmFlag { get; set; }
        /// <summary>입고일</summary>           
        [Column("IN_DATE")] public DateTime InDate { get; set; }
        /// <summary>발주번호</summary>           
        [Column("PO_NO")] public string PoNo { get; set; }

        [NotMapped]public string _Check { get; set; }
            

    }
}