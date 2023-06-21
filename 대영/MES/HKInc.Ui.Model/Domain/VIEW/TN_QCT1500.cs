using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>성적서검사 마스터</summary>	
    [Table("TN_QCT1500T")]
    public class TN_QCT1500 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1500()
        {
            TN_QCT1501List = new List<TN_QCT1501>();
        }
        /// <summary>검사번호</summary>                  
        [Key, Column("INSP_NO"), Required(ErrorMessage = "InspNo")] public string InspNo { get; set; }
        /// <summary>최종검사번호</summary>     
        [Column("FINAL_INSP_NO"), Required(ErrorMessage = "FinalInspNo")] public string FinalInspNo { get; set; }
        /// <summary>품번(도번)</summary>                
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>                
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "Customer")] public string CustomerCode { get; set; }
        /// <summary>출고수량</summary>               
        [Column("OUT_QTY")] public decimal? OutQty { get; set; }
        /// <summary>생산 LOT NO</summary>               
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>검사일</summary>                    
        [Column("CHECK_DATE"), Required(ErrorMessage = "CheckDate")] public DateTime? CheckDate { get; set; }
        /// <summary>검사자</summary>                    
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>검사결과</summary>                  
        [Column("CHECK_RESULT"), Required(ErrorMessage = "CheckResult")] public string CheckResult { get; set; }
        /// <summary>파일명</summary>                    
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>출력일</summary>                    
        [Column("PRINT_DATE")] public DateTime? PrintDate { get; set; }
        /// <summary>메모</summary>                      
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>이동표번호</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_QCT1501> TN_QCT1501List { get; set; }
    }
}