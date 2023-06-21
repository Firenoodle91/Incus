using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 20210930 오세완 차장 출하검사에서 entity를 안쓰기 위한 임시객체
    /// </summary>
    public class TEMP_XFQCT1901_SUBDETAIL
    {
        public TEMP_XFQCT1901_SUBDETAIL()
        {

        }

        /// <summary>검사번호</summary>            
        public string InspNo { get; set; }
        /// <summary>검사순번</summary>            
        public int InspSeq { get; set; }
        /// <summary>검사방법</summary>            
        public string CheckWay { get; set; }
        /// <summary>
        /// 검사방법 텍스트 변환
        /// </summary>
        public string CheckWay_Value { get; set; }
        /// <summary>검사항목</summary>            
        public string CheckList { get; set; }
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
        /// <summary>데이터타입</summary>            
        public string CheckDataType { get; set; }
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
        /// <summary>판정</summary>                
        public string Judge { get; set; }
        /// <summary>메모</summary>                
        public string Memo { get; set; }
        /// <summary>규격</summary> 
        public string InspectionReportMemo { get; set; }
        /// <summary>최대시료수 코드</summary> 
        public string MaxReading { get; set; }
        /// <summary>최대시료수 값</summary> 
        public string MaxReading_Value { get; set; }

        /// <summary>
        /// 20211001 오세완 차장 insert/update구분
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 20211005 오세완 차장 insert때 필요, 리비전 번호
        /// </summary>
        public string REV_NO { get; set; }

        /// <summary>
        /// 20211005 오세완 차장 insert때 필요, 품목코드
        /// </summary>
        public string ITEM_CODE { get; set; }
    }
}
