using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>4M관리대장</summary>	
    [Table("TN_STD4M001T")]
    public class TN_STD4M001 : BaseDomain.MES_BaseDomain
    {
        public TN_STD4M001()
        {
        }
        /// <summary>L4MNO</summary>           
        [Key, Column("L4MNO", Order = 0), Required(ErrorMessage = "L4mno")] public string L4mno { get; set; }
        /// <summary>품목코드</summary>        
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>            
        [Key, Column("SEQ", Order = 2), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>고객사</summary>          
        [Column("CUST_CODE")] public string CustCode { get; set; }
        /// <summary>차종</summary>            
        [Column("CAR_TYPE")] public string CarType { get; set; }
        /// <summary>변경일</summary>          
        [Column("CHG_DATE")] public DateTime? ChgDate { get; set; }
        /// <summary>변경고객사</summary>      
        [Column("CHG_CUST")] public string ChgCust { get; set; }
        /// <summary>변경내용</summary>        
        [Column("CHG_NOTE")] public string ChgNote { get; set; }
        /// <summary>변경원인</summary>        
        [Column("CHG_MEMO")] public string ChgMemo { get; set; }
        /// <summary>접수처</summary>          
        [Column("REQ_CUST")] public string ReqCust { get; set; }
        /// <summary>접수자</summary>          
        [Column("REQ_USER")] public string ReqUser { get; set; }
        /// <summary>1차검토처</summary>       
        [Column("CHK_CUST_1CHA")] public string ChkCust1Cha { get; set; }
        /// <summary>1차검토일</summary>         
        [Column("CHK_DATE_1CHA")] public DateTime? ChkDate1Cha { get; set; }
        /// <summary>1차품질</summary>           
        [Column("CHK_QC_1CHA")] public string ChkQc1Cha { get; set; }
        /// <summary>1차검토자</summary>         
        [Column("CHK_QC_USER_1CHA")] public string ChkQcUser1Cha { get; set; }
        /// <summary>1차파일</summary>           
        [Column("CHK_QC_FILE_1CHA")] public string ChkQcFile1Cha { get; set; }
        /// <summary>2차검토처</summary>         
        [Column("CHK_CUST_2CHA")] public string ChkCust2Cha { get; set; }
        /// <summary>2차검토일</summary>         
        [Column("CHK_DATE_2CHA")] public DateTime? ChkDate2Cha { get; set; }
        /// <summary>2차품질</summary>           
        [Column("CHK_QC_2CHA")] public string ChkQc2Cha { get; set; }
        /// <summary>2차검토자</summary>         
        [Column("CHK_QC_USER_2CHA")] public string ChkQcUser2Cha { get; set; }
        /// <summary>2차파일</summary>           
        [Column("CHK_QC_FILE_2CHA")] public string ChkQcFile2Cha { get; set; }
        /// <summary>3차검토처</summary>         
        [Column("CHK_CUST_3CHA")] public string ChkCust3Cha { get; set; }
        /// <summary>3차검토일</summary>         
        [Column("CHK_DATE_3CHA")] public DateTime? ChkDate3Cha { get; set; }
        /// <summary>3차품질</summary>           
        [Column("CHK_QC_3CHA")] public string ChkQc3Cha { get; set; }
        /// <summary>3차검토자</summary>         
        [Column("CHK_QC_USER_3CHA")] public string ChkQcUser3Cha { get; set; }
        /// <summary>3차파일</summary>           
        [Column("CHK_QC_FILE_3CHA")] public string ChkQcFile3Cha { get; set; }
        /// <summary>최종승인자</summary>        
        [Column("FINAL_USER")] public string FinalUser { get; set; }
        /// <summary>양산일</summary>            
        [Column("PROD_WORK_DATE")] public DateTime? ProdWorkDate { get; set; }
        /// <summary>요청서</summary>            
        [Column("REQ_DOC")]	public string ReqDoc { get; set; }
        /// <summary>첨부1</summary> 
        [Column("ETC_FILE1")] public string EtcFile1 { get; set; }
        /// <summary>첨부2</summary> 
        [Column("ETC_FILE2")] public string EtcFile2 { get; set; }
        /// <summary>첨부3</summary> 
        [Column("ETC_FILE3")] public string EtcFile3 { get; set; }
        /// <summary>첨부4</summary> 
        [Column("ETC_FILE4")] public string EtcFile4 { get; set; }
        /// <summary>첨부5</summary> 
        [Column("ETC_FILE5")] public string EtcFile5 { get; set; }
        /// <summary>메모1</summary> 
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>메모2</summary> 
        [Column("MEMO2")] public string Memo2 { get; set; }
        /// <summary>메모3</summary> 
        [Column("MEMO3")] public string Memo3 { get; set; }
        /// <summary>메모4</summary> 
        [Column("MEMO4")] public string Memo4 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}