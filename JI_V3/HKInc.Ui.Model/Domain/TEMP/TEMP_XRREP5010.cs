using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XRREP5010
    {
        /// <summary>
        /// 월별납품원가현황
        /// JI 엑셀파일 '납품202101' 매출원자재 시트 참조
        /// </summary>	

        //public decimal RowId { get; set; }
        public string OrderNo { get; set; } //수주번호
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
        public string Spec1 { get; set; } //규격1
        public string Spec2 { get; set; } //규격2
        public string Spec3 { get; set; } //규격3
        public string Spec4 { get; set; } //규격4
        public decimal SrcWeight { get; set; } //원자재 단위중량
        //SumSrcWeight 원자재소요량 납품수량*자재 단위중량
        public decimal Weight { get; set; } //제품중량
        //SumScrap 스크랩 (단위중량-제품중량)*납품수량
        //CalSrcCost 원자재단가(계산) 원자재금액/납품수량
        public decimal SrcCost { get; set; } //원자재기준금액(원가기준관리)
        //원자재금액 SumSrcCost 납품수량 * 원자재금액의기준값

        public decimal BarfeederCNCcycleTime { get; set; }
        public decimal CNC1cycleTime { get; set; }
        public decimal CNC2cycleTime { get; set; }
        public decimal CNC3cycleTime { get; set; }
        public decimal MCTcycleTime { get; set; }
        public decimal TappingcycleTime { get; set; }


        public decimal SumSrcWeight //원자재소요량 =  납품수량*자재 단위중량
        {
            get
            {
                var outqty = OutQty;
                var srcweight = SrcWeight;

                return outqty * srcweight;
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
        public decimal CalSrcCost //원자재단가(계산) =  원자재금액/납품수량
        {
            get
            {
                var outqty = OutQty;

                if (outqty == 0)
                    return 0;
                else
                    return SumSrcCost / outqty;
            }
        }
        public decimal SumSrcCost //원자재금액 = 납품수량* 원자재금액의기준값
        {
            get
            {
                var outqty = OutQty;
                var srccost = SrcCost;

                return outqty * srccost;
            }
        }
        //-----------------------------------------------------------------------------------------------------
        public decimal BfCNCManHour //공수 = 1개당소요시간*납품수량/3600
        {
            get
            {
                var cycle = BarfeederCNCcycleTime;
                var outqty = OutQty;

                return cycle * outqty / 3600;
            }
        }
        public decimal BfCNCProcessCost //공정비 = 1개당소요시간*5.5
        {
            get
            {
                var cycle = BarfeederCNCcycleTime;

                return cycle * Convert.ToDecimal(5.5);
            }
        }
        public decimal BfCNCValueAdded //부가가치 = 납품수량 * 공정비 = 납품수량 * (1개당소요시간 * 5.5)
        {
            get
            {
                var outqty = OutQty;
                var cycle = BarfeederCNCcycleTime;
                var cost = BfCNCProcessCost;

                //return outqty * cost;
                return outqty * cycle * Convert.ToDecimal(5.5);
            }
        }
        //-----------------------------------------------------------------------------------------------------
        public decimal CNCManHour //공수 = 1개당소요시간*납품수량/3600
        {
            get
            {
                var cycle = CNC1cycleTime+ CNC2cycleTime+ CNC3cycleTime;
                var outqty = OutQty;

                return cycle * outqty / 3600;
            }
        }
        public decimal CNCProcessCost //공정비 = 1개당소요시간*5.5
        {
            get
            {
                var cycle = CNC1cycleTime + CNC2cycleTime + CNC3cycleTime;

                return cycle * Convert.ToDecimal(5.5);
            }
        }
        public decimal CNCValueAdded //부가가치 = 납품수량 * 공정비 = 납품수량 * (1개당소요시간 * 5.5)
        {
            get
            {
                var outqty = OutQty;
                var cycle = CNC1cycleTime + CNC2cycleTime + CNC3cycleTime;
                var cost = CNCProcessCost;

                //return outqty * cost;
                return outqty * cycle * Convert.ToDecimal(5.5);
            }
        }
        //-----------------------------------------------------------------------------------------------------
        public decimal MCTManHour //공수 = 1개당소요시간*납품수량/3600
        {
            get
            {
                var cycle = MCTcycleTime;
                var outqty = OutQty;

                return cycle * outqty / 3600;
            }
        }
        public decimal MCTProcessCost //공정비 = 1개당소요시간*5.5
        {
            get
            {
                var cycle = MCTcycleTime;

                return cycle * Convert.ToDecimal(5.5);
            }
        }
        public decimal MCTValueAdded //부가가치 = 납품수량 * 공정비 = 납품수량 * (1개당소요시간 * 5.5)
        {
            get
            {
                var outqty = OutQty;
                var cycle = MCTcycleTime;
                var cost = MCTProcessCost;

                //return outqty * cost;
                return outqty * cycle * Convert.ToDecimal(5.5);
            }
        }
        //-----------------------------------------------------------------------------------------------------
        public decimal TappingManHour //공수 = 1개당소요시간*납품수량/3600
        {
            get
            {
                var cycle = TappingcycleTime;
                var outqty = OutQty;

                return cycle * outqty / 3600;
            }
        }
        public decimal TappingProcessCost //공정비 = 1개당소요시간*5.5
        {
            get
            {
                var cycle = TappingcycleTime;

                return cycle * Convert.ToDecimal(5.5);
            }
        }
        public decimal TappingValueAdded //부가가치 = 납품수량 * 공정비(납품수량 * (1개당소요시간 * 5.5))
        {
            get
            {
                var outqty = OutQty;
                var cycle = TappingcycleTime;
                var cost = TappingProcessCost;

                //return outqty * cost;
                return outqty * cycle * Convert.ToDecimal(5.5);
            }
        }
        //-----------------------------------------------------------------------------------------------------


    }
}
