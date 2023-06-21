using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 공구관리 → 공구 수명이력관리(공구 기준)
    /// </summary>
    public class TEMP_XFTOOL1201_DETAIL
    {
        //public decimal RowId { get; set; }
        public string WorkNo { get; set; } //지시번호
        public DateTime? WorkDate { get; set; } //지시시작일
        public string MachineCode { get; set; } //설비코드
        public string ItemCode { get; set; } //품목코드
        public string ProcessCode { get; set; } //공정코드
        public string ToolCode { get; set; } //공구코드

        public Int32 BaseCNT { get; set; } //기초수명
        public Int32 LifeCNT { get; set; } //잔여수명
        public Int32 UseCNT { get; set; } //사용수명
        public Int32 Seq { get; set; } //교체순서 0:지시 시작 전 공구 장착

        public string UserId { get; set; } //작업자

        public DateTime ChangeDate { get; set; } //공구 교체 등록 일시


        /*
        public decimal TappingValueAdded //부가가치 = 납품수량 * 공정비 = 납품수량 * (1개당소요시간 * 5.5)
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
        */


    }
}
