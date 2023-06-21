using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사의뢰관리</summary>	
    [Table("TN_DEV1000T")]
    public class TN_DEV1000 : BaseDomain.MES_BaseDomain
    {
        public TN_DEV1000()
        {
        }
        /// <summary>의뢰번호</summary>            
        [Key, Column("REQ_NO"), Required(ErrorMessage = "ReqNo")] public string ReqNo { get; set; }
        /// <summary>의뢰일</summary>              
        [Column("REQ_DATE"), Required(ErrorMessage = "ReqDate")] public DateTime ReqDate { get; set; }
        /// <summary>품번(도번)</summary>          
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>          
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>의뢰자</summary>              
        [Column("REQ_ID")] public string ReqId { get; set; }
        /// <summary>의뢰수량</summary>            
        [Column("REQ_QTY")] public decimal? ReqQty { get; set; }
        /// <summary>의뢰시한</summary>            
        [Column("RETURN_DATE"), Required(ErrorMessage = "ReturnDate")] public DateTime ReturnDate { get; set; }
        /// <summary>의뢰파일명</summary>          
        [Column("REQ_FILE_NAME")] public string ReqFileName { get; set; }
        /// <summary>의뢰파일URL</summary>         
        [Column("REQ_FILE_URL")] public string ReqFileUrl { get; set; }
        /// <summary>검사일</summary>              
        [Column("CHECK_DATE")] public DateTime? CheckDate { get; set; }
        /// <summary>검사자</summary>              
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>검사파일명</summary>          
        [Column("CHECK_FILE_NAME")] public string CheckFileName { get; set; }
        /// <summary>검사파일URL</summary>         
        [Column("CHECK_FILE_URL")] public string CheckFileUrl { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}