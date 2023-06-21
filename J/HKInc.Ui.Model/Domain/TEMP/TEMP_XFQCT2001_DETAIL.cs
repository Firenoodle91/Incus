using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFQCT2001_DETAIL
    {

        public DateTime CheckDate { get; set; } //측정일자
        public string ItemCode { get; set; } //품목코드
        public string ProcessCode { get; set; } //공정코드
        public string CheckList { get; set; } //검사항목
        public string CheckMin { get; set; } //하한, LCL
        public decimal CheckResult { get; set; } //측정cl, READING1
        public string CheckMax { get; set; } //상한, UCL


        //public decimal SumScrap //스크랩 = (단위중량-제품중량)*납품수량
        //{
        //    get
        //    {
        //        var srcweight = SrcWeight;
        //        var weight = Weight;
        //        var outqty = OutQty;
        //
        //        return (srcweight - weight) * outqty;
        //    }
        //}
        
       
    }
}
