using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사규격관리</summary>	
    [Table("TN_QCT1001T")]
    public class TN_QCT1001 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1001()
        {
        }
        /// <summary>리비전번호</summary>            
        [ForeignKey("TN_QCT1000"), Key, Column("REV_NO", Order = 0), Required(ErrorMessage = "RevNo")] public string RevNo { get; set; }
        /// <summary>품번(도번)</summary>            
        [ForeignKey("TN_QCT1000"), Key, Column("ITEM_CODE", Order = 1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 2), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>검사순서</summary>              
        [Column("DISPLAY_ORDER")] public int? DisplayOrder { get; set; }
        /// <summary>공정코드</summary>              
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>성적서사용여부</summary>        
        [Column("INSPECTION_REPORT_FLAG"), Required(ErrorMessage = "InspectionReportFlag")] public string InspectionReportFlag { get; set; }
        /// <summary>성적서표기</summary>            
        [Column("INSPECTION_REPORT_MEMO")] public string InspectionReportMemo { get; set; }
        /// <summary>SPC사용여부</summary>           
        [Column("SPC_FLAG"), Required(ErrorMessage = "SpcFlag")] public string SpcFlag { get; set; }
        /// <summary>SPC구분</summary>               
        [Column("SPC_DIVISION")] public string SpcDivision { get; set; }
        /// <summary>검사구분</summary>              
        [Column("CHECK_DIVISION"), Required(ErrorMessage = "InspectionDivision")] public string CheckDivision { get; set; }
        /// <summary>검사방법</summary>              
        [Column("CHECK_WAY"), Required(ErrorMessage = "InspectionWay")] public string CheckWay { get; set; }
        /// <summary>검사항목</summary>              
        [Column("CHECK_LIST"), Required(ErrorMessage = "InspectionItem")] public string CheckList { get; set; }
        /// <summary>검사위치</summary>              
        [Column("INSP_CHECK_POSITION")] public string InspCheckPosition { get; set; }
        /// <summary>공차MAX</summary>            
        [Column("CHECK_MAX")] public string CheckMax { get; set; }
        /// <summary>공차MIN</summary>            
        [Column("CHECK_MIN")] public string CheckMin { get; set; }
        /// <summary>검사규격</summary>              
        [Column("CHECK_SPEC")] public string CheckSpec { get; set; }
        /// <summary>공차상한</summary>              
        [Column("CHECK_UP_QUAD")] public string CheckUpQuad { get; set; }
        /// <summary>공차하한</summary>              
        [Column("CHECK_DOWN_QUAD")] public string CheckDownQuad { get; set; }
        /// <summary>계측기No</summary>            
        [Column("INSTRUMENT_NO")] public string InstrumentNo { get; set; }
        /// <summary>측정기코드</summary>            
        [Column("INSTRUMENT_CODE")] public string InstrumentCode { get; set; }
        /// <summary>데이터타입</summary>            
        [Column("CHECK_DATA_TYPE"), Required(ErrorMessage = "CheckDataType")] public string CheckDataType { get; set; }
        /// <summary>최대시료수</summary>            
        [Column("MAX_READING")] public string MaxReading { get; set; }
        /// <summary>사용여부</summary>              
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>검사관리항목</summary>                  
        [Column("INSTR_MEMO")] public string InstrMemo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_QCT1000 TN_QCT1000 { get; set; }


        #region POP 품질등록에서 사용

        /// <summary>측정값1</summary>             
        [NotMapped] public string Reading1 { get; set; }
        /// <summary>측정값2</summary>             
        [NotMapped] public string Reading2 { get; set; }
        /// <summary>측정값3</summary>             
        [NotMapped] public string Reading3 { get; set; }
        /// <summary>측정값4</summary>             
        [NotMapped] public string Reading4 { get; set; }
        /// <summary>측정값5</summary>             
        [NotMapped] public string Reading5 { get; set; }
        /// <summary>측정값6</summary>             
        [NotMapped] public string Reading6 { get; set; }
        /// <summary>측정값7</summary>             
        [NotMapped] public string Reading7 { get; set; }
        /// <summary>측정값8</summary>             
        [NotMapped] public string Reading8 { get; set; }
        /// <summary>측정값9</summary>             
        [NotMapped] public string Reading9 { get; set; }
        /// <summary>판정</summary>                
        [NotMapped] public string Judge { get; set; }
        #endregion
    }
}