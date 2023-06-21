using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>계측기관리</summary>	
    [Table("TN_MEA1100T")]
    public class TN_MEA1100 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1100()
        {
            TN_MEA1101List = new List<TN_MEA1101>();
        }
        /// <summary>계측기코드</summary>          
        [Key, Column("INSTR_CODE"), Required(ErrorMessage = "InstrCode")] public string InstrCode { get; set; }
        /// <summary>계측기명</summary>            
        [Column("INSTR_NAME"), Required(ErrorMessage = "InstrName")] public string InstrName { get; set; }
        /// <summary>계측기명(영문)</summary>      
        [Column("INSTR_NAME_ENG")] public string InstrNameENG { get; set; }
        /// <summary>계측기명(중문)</summary>      
        [Column("INSTR_NAME_CHN")] public string InstrNameCHN { get; set; }
        /// <summary>계측기종류</summary>                
        [Column("INSTR_KIND")] public string InstrKind { get; set; }
        /// <summary>규격</summary>                
        [Column("SPEC")] public string Spec { get; set; }
        /// <summary>제작사</summary>              
        [Column("MAKER")] public string Maker { get; set; }
        /// <summary>도입일</summary>              
        [Column("INTRODUCTION_DATE")] public DateTime? IntroductionDate { get; set; }
        /// <summary>S/N</summary>                 
        [Column("SERIAL_NO")] public string SerialNo { get; set; }
        /// <summary>교정주기</summary>            
        [Column("COR_TURN")] public string CorTurn { get; set; }
        /// <summary>교정일</summary>              
        [Column("COR_DATE")] public DateTime? CorDate { get; set; }
        /// <summary>교정예정일</summary>          
        [Column("PREDICTION_COR_DATE")] public DateTime? PredictionCorDate { get; set; }
        /// <summary>작업자</summary>              
        [Column("WORK_ID")] public string WorkId { get; set; }
        /// <summary>파일명</summary>              
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>             
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>사용여부</summary>                
        [Column("USE_FLAG")] public string UseFlag { get; set; }
        /// <summary>메모</summary>                
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")]	public string Temp2 { get; set; }

        public virtual ICollection<TN_MEA1101> TN_MEA1101List { get; set; }
    }
}