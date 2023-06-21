using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사 마스터</summary>	
    [Table("TN_QCT1100T")]
    public class TN_QCT1100 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1100()
        {
            TN_QCT1101List = new List<TN_QCT1101>();
            TN_QCT1102List = new List<TN_QCT1102>();
        }

        /// <summary>검사번호</summary>                  
        [Key, Column("INSP_NO"), Required(ErrorMessage = "InspNo")] public string InspNo { get; set; }
        /// <summary>검사구분</summary>                  
        [Column("CHECK_DIVISION"), Required(ErrorMessage = "InspectionDivision")] public string CheckDivision { get; set; }
        /// <summary>검사시점</summary>                  
        [Column("CHECK_POINT"), Required(ErrorMessage = "CheckPoint")] public string CheckPoint { get; set; }
        /// <summary>작업지시번호/입고번호</summary>     
        [Column("WORK_NO"), Required(ErrorMessage = "WorkNo_InNo")] public string WorkNo { get; set; }
        /// <summary>작업지시순번/입고순번</summary>     
        [Column("WORK_SEQ"), Required(ErrorMessage = "WorkNo_InSeq")] public int WorkSeq { get; set; }
        /// <summary>작업지시일/입고일</summary>         
        [Column("WORK_DATE"), Required(ErrorMessage = "WorkDate_InDate")] public DateTime WorkDate { get; set; }
        /// <summary>거래처코드</summary>                
        [Column("CUSTOMER_CODE"), Required(ErrorMessage = "Customer")] public string CustomerCode { get; set; }
        /// <summary>품번(도번)</summary>                
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>공정코드</summary>                  
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>생산 LOT NO</summary>               
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>입고 LOT NO</summary>               
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>검사일</summary>                    
        [Column("CHECK_DATE"), Required(ErrorMessage = "CheckDate")] public DateTime? CheckDate { get; set; }
        /// <summary>검사자</summary>                    
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>검사일(시간)1</summary>                    
        [Column("CHECK_DATE_TIME1")] public DateTime? CheckDateTime1 { get; set; }
        /// <summary>검사일(시간)2</summary>                    
        [Column("CHECK_DATE_TIME2")] public DateTime? CheckDateTime2 { get; set; }
        /// <summary>검사일(시간)3</summary>                    
        [Column("CHECK_DATE_TIME3")] public DateTime? CheckDateTime3 { get; set; }
        /// <summary>검사결과</summary>                  
        [Column("CHECK_RESULT"), Required(ErrorMessage = "CheckResult")] public string CheckResult { get; set; }    // 초중종에서 저장시 에러발생하여 필수값 제거 2022-10-16 김진우
        /// <summary>검사결과</summary>                  
        [Column("CHECK_RESULT_CUST") ] public string CheckResultCust { get; set; }
        /// <summary>파일명</summary>                    
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_DATA")] public byte[] FileData { get; set; }
        /// <summary>메모</summary>                      
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>합격수량</summary>                      
        [Column("OK_QTY"), Required(ErrorMessage = "OkQty")] public decimal OkQty { get; set; }
        /// <summary>불량수량</summary>                      
        [Column("NG_QTY"), Required(ErrorMessage = "NgQty")] public decimal NgQty { get; set; }
        /// <summary>생산일</summary>                  // 2022-09-27 김진우 추가
        //[Column("PROD_DATE")] public DateTime? ProdDate { get; set; }
        /// <summary>생산일(시간포함)</summary>        // 2022-09-27 김진우 추가
        [Column("PROD_DATE_TIME")] public DateTime? ProdDateTime { get; set; }
        /// <summary>생산검사자</summary>              // 2022-09-27 김진우 추가
        [Column("PROD_ID")] public string ProdId { get; set; }
        /// <summary>생산 OK/NG</summary>             // 2022-09-27 김진우 추가
        [Column("PROD_OKNG")] public string ProdOKNG { get; set; }
        /// <summary>검사일</summary>                  // 2022-09-27 김진우 추가
        //[Column("QUAL_DATE")] public DateTime? QualDate { get; set; }
        /// <summary>검사일(시간포함)</summary>        // 2022-09-27 김진우 추가
        [Column("QUAL_DATE_TIME")] public DateTime? QualDateTime { get; set; }
        /// <summary>품질검사자</summary>            // 2022-09-27 김진우 추가
        [Column("QUAL_ID")] public string QualId { get; set; }
        /// <summary>품질 OK/NG</summary>             // 2022-09-27 김진우 추가
        [Column("QUAL_OKNG")] public string QualOKNG { get; set; }
        /// <summary>최종 검사자</summary>             // 2022-09-27 김진우 추가
        [Column("CONF_ID")] public string ConfId { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>이동표번호</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>납품처LOTNO</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual ICollection<TN_QCT1101> TN_QCT1101List { get; set; }
        public virtual ICollection<TN_QCT1102> TN_QCT1102List { get; set; }
    }
}