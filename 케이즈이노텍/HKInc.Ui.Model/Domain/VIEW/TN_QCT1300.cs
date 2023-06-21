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
    /// <summary>부적합관리</summary>	
    [Table("TN_QCT1300T")]
    public class TN_QCT1300 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1300()
        {
        }

        /// <summary>부적합번호</summary>            
        [Key, Column("P_NO"), Required(ErrorMessage = "PNo")] public string PNo { get; set; }
        /// <summary>실적/클레임구분</summary>       
        [Column("P_TYPE"), Required(ErrorMessage = "PType")] public string PType { get; set; }
        /// <summary>실적/클레임 키</summary>        
        [Column("P_KEY"), Required(ErrorMessage = "PKey")] public string PKey { get; set; }
        /// <summary>품번(도번)</summary>          
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>          
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>작업지시번호</summary>          
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>              
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>              
        [Column("PROCESS_SEQ")] public int? ProcessSeq { get; set; }
        /// <summary>생산 LOT NO</summary>           
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>불량유형</summary>              
        [Column("BAD_TYPE")] public string BadType { get; set; }
        /// <summary>검사자</summary>                
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>검사량</summary>                
        [Column("CHECK_QTY")] public decimal? CheckQty { get; set; }
        /// <summary>손실비용</summary>              
        [Column("LOSE_COST")] public decimal? LoseCost { get; set; }
        /// <summary>부적합내용</summary>            
        [Column("BAD_CONTENT")] public string BadContent { get; set; }
        /// <summary>부적합량</summary>              
        [Column("BAD_QTY")] public decimal? BadQty { get; set; }
        /// <summary>처리방법</summary>              
        [Column("SOLUTION_CONTENT")] public string SolutionContent { get; set; }
        /// <summary>발생장소</summary>              
        [Column("OCCUR_LOCATION")] public string OccurLocation { get; set; }
        /// <summary>발생현상</summary>              
        [Column("OCCUR_CONENT")] public string OccurConent { get; set; }
        /// <summary>발생횟수</summary>              
        [Column("OCCUR_QTY")] public int? OccurQty { get; set; }
        /// <summary>양품사진명</summary>            
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>양품사진URL</summary>           
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>부적합품사진명</summary>        
        [Column("FILE_NAME2")] public string FileName2 { get; set; }
        /// <summary>부적합품사진URL</summary>       
        [Column("FILE_URL2")] public string FileUrl2 { get; set; }
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