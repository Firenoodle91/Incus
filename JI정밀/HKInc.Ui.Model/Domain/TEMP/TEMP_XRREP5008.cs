using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XRREP5008
    {

        public string CustomerCode { get; set; } //거래처코드
        public string CustomerName { get; set; } //거래처명

        public string ItemCode { get; set; } //품목코드
        public string CarType { get; set; } //차종
        public string ItemName { get; set; } //품목명
        public DateTime OutDate { get; set; } //납품일
        public decimal OutQty { get; set; } //납품수량
        public decimal ItemCost { get; set; } //납품금액

        public string SrcCode { get; set; } //원자재코드
        public string SrcName { get; set; } //원자재명
        public string Texture { get; set; } //원자재 재종

        public string Spec1 { get; set; } //자재규격1
        public string Spec2 { get; set; } //자재규격2
        public string Spec3 { get; set; } //자재규격3
        public string Spec4 { get; set; } //자재규격4
        public string Unit { get; set; } //단위

        public decimal SrcWeight { get; set; } //원자재 단위중량
        //SumSrcWeight 원자재소요량 납품수량*자재 단위중량
        public decimal Weight { get; set; } //제품중량
        //SumScrap 스크랩 (단위중량-제품중량)*납품수량
        //CalSrcCost 원자재단가(계산) 원자재금액/납품수량


        public decimal SumSrcWeight //원자재소요량 =  납품수량*자재 단위중량
        {
            get
            {
                var outqty = OutQty;
                var srcweight = SrcWeight;

                return outqty * srcweight;
            }
        }
        public decimal SumWeight //완제품소요량 =  납품수량*제품중량
        {
            get
            {
                var outqty = OutQty;
                var weight = Weight;

                return outqty * weight;
            }
        }
        public decimal SumScrap //스크랩 = (단위중량-제품중량)*납품수량
        {
            get
            {
                var srcweight = SrcWeight;
                var weight = Weight;
                var outqty = OutQty;

                return (srcweight - weight) * outqty;
            }
        }
       
    }
}
