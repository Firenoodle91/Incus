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
    /// <summary>클레임관리</summary>	
    [Table("TN_QCT1400T")]
    public class TN_QCT1400 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1400()
        {
        }
        /// <summary>클레임번호</summary>             
        [Key, Column("CLAIM_NO"), Required(ErrorMessage = "ClaimNo")] public string ClaimNo { get; set; }
        /// <summary>접수일</summary>                 
        [Column("CLAIM_DATE"), Required(ErrorMessage = "ClaimDate")] public DateTime ClaimDate { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [ForeignKey("TN_STD1400"), Column("CLAIM_CUSTOMER_CODE"), Required(ErrorMessage = "CustomerName")] public string ClaimCustomerCode { get; set; }
        /// <summary>제품 LOT NO</summary>            
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>출고 LOT NO</summary>            
        [Column("OUT_LOT_NO")] public string OutLotNo { get; set; }
        /// <summary>수(중)량</summary>               
        [Column("CLAIM_QTY")] public decimal? ClaimQty { get; set; }
        /// <summary>유형</summary>                   
        [Column("CLAIM_TYPE")] public string ClaimType { get; set; }
        /// <summary>접수자</summary>                 
        [Column("CLAIM_ID")] public string ClaimId { get; set; }
        /// <summary>파일명</summary>                 
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")]	public string Memo { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}