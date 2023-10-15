using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary> 수입/자주/공정/초중종 검사 검사항목이력 에서 사용</summary>	
    public class TEMP_XFQCT1100_SUB
    {
        /// <summary>검사일</summary>            
        public DateTime? CheckDate { get; set; }
        /// <summary>검사자</summary>            
        public string CheckId { get; set; }
        /// <summary>검사번호</summary>            
        public string InspNo { get; set; }
        /// <summary>검사순번</summary>            
        public int InspSeq { get; set; }
        /// <summary>입고 LOT NO</summary>            
        public string InLotNo { get; set; }
        /// <summary>생산 LOT NO</summary>            
        public string ProductLotNo { get; set; }
        /// <summary>검사방법</summary>            
        public string CheckWay { get; set; }
        /// <summary>검사항목</summary>            
        public string CheckList { get; set; }
        /// <summary>측정기코드</summary>            
        public string InstrumentCode { get; set; }
        /// <summary>데이터타입</summary>            
        public string CheckDataType { get; set; }
        /// <summary>규격</summary>            
        public string InspectionReportMemo { get; set; }
        /// <summary>공차MAX</summary>            
        public string CheckMax { get; set; }
        /// <summary>공차MIN</summary>            
        public string CheckMin { get; set; }
        /// <summary>검사규격</summary>            
        public string CheckSpec { get; set; }
        /// <summary>공차상한</summary>            
        public string CheckUpQuad { get; set; }
        /// <summary>공차하한</summary>            
        public string CheckDownQuad { get; set; }
        /// <summary>최대시료수</summary>             
        public string MaxReading { get; set; }
        /// <summary>측정값1</summary>             
        public string Reading1 { get; set; }
        /// <summary>측정값2</summary>             
        public string Reading2 { get; set; }
        /// <summary>측정값3</summary>             
        public string Reading3 { get; set; }
        /// <summary>측정값4</summary>             
        public string Reading4 { get; set; }
        /// <summary>측정값5</summary>             
        public string Reading5 { get; set; }
        /// <summary>측정값6</summary>             
        public string Reading6 { get; set; }
        /// <summary>측정값7</summary>             
        public string Reading7 { get; set; }
        /// <summary>측정값8</summary>             
        public string Reading8 { get; set; }
        /// <summary>측정값9</summary>             
        public string Reading9 { get; set; }
        /// <summary>측정값10</summary>             
        public string Reading10 { get; set; }
        /// <summary>측정값11</summary>             
        public string Reading11 { get; set; }
        /// <summary>측정값12</summary>             
        public string Reading12 { get; set; }
        /// <summary>측정값13</summary>             
        public string Reading13 { get; set; }
        /// <summary>판정</summary>                
        public string Judge { get; set; }
        /// <summary>메모</summary>                
        public string Memo { get; set; }
    }
}