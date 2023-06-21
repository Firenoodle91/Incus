using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    public class TEMP_XFANDON1000_MASTER
    { 
        public string DepDivision { get; set; } //부서 구분
        public string CallDate { get; set; } //호출일자
        public string WorkNo { get; set; } //작업지시번호
        public string ItemCode { get; set; } //품목코드
        public string ProcessCode { get; set; } //공정코드
        public string MachineCode { get; set; } //설비코드
        public string CreateId { get; set; } //등록자
        public int Seq { get; set; }    // SEQ
    }
}
