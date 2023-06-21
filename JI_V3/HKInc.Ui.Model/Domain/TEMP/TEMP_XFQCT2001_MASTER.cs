using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFQCT2001_MASTER
    {

        public string ItemCode { get; set; } //품목코드
        public string ProcessCode { get; set; } //공정코드
        public string CheckList { get; set; } //검사항목
        public decimal CheckMin { get; set; } //하한, LCL
        public decimal CheckMax { get; set; } //상한, UCL
        public decimal MIN { get; set; } //최소측정치
        public decimal MAX { get; set; } //최대측정치
        public decimal R { get; set; } //최소 최대 측정치 차이
        public Int32 DATA_CNT { get; set; } //측정치 수
        public decimal? SUM { get; set; } //합계
        public decimal? AVG { get; set; } //평균
        public decimal? VAR { get; set; } //붕산
        public decimal? STDEV { get; set; } //표준편차, 시그마, σ
        public decimal? Cp { get; set; } //
        public decimal? CpU { get; set; } //
        public decimal? CpL { get; set; } //
        public decimal? CpK { get; set; } //

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
