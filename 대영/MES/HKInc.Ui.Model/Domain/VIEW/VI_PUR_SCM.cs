using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재발주팝업</summary>	
    [Table("VI_PUR_SCM")]
    public class VI_PUR_SCM
    {
        public VI_PUR_SCM()
        {
            _Check = "N";
        }
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>발주번호</summary>           
        [Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>SCM사용여부</summary>           
        [Column("SCM_YN")] public string ScmYn { get; set; }
        /// <summary>입고번호</summary>           
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>발주자</summary>             
        [Column("PO_CUSTOMER_CODE")] public string PoCustomerCode { get; set; }
        /// <summary>발주일</summary>             
        [Column("PO_DATE")] public DateTime PoDate { get; set; }
        /// <summary>발주자</summary>             
        [Column("PO_ID")] public string PoId { get; set; }
        /// <summary>납기일</summary>             
        [Column("DUE_DATE")] public DateTime DueDate { get; set; }
        /// <summary>협력사입고일</summary>             
        [Column("IN_CUSTOMER_DATE")] public DateTime? InCustomerDate { get; set; }
        /// <summary>협력사입고자</summary>             
        [Column("IN_CUSTOMER_ID")] public string InCustomerId { get; set; }
        /// <summary>발주처메모</summary>               
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>협력사메모</summary>               
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>협력사입고접수완료여부</summary>               
        [Column("IN_CUST_CONFIRM_FLAG")] public string InCustConfirmFlag { get; set; }
        /// <summary>입고상태</summary>               // 메인코드 : Z001, 01 : 대기, 02 : 진행중, 03 : 완료
        [Column("IN_CONFIRM_STATE")] public string InConfirmState { get; set; }

        [NotMapped] public string _Check { get; set; }
        

        
    }
}