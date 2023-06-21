using System;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220222 오세완 차장
    /// USP_GET_XFPOP1000_V2_LIST, USP_GET_XFPOP1000_V2_LIST1 결과 반환 객체
    /// </summary>
    public class TP_XFPOP1000_V2_LIST
    {

        public TP_XFPOP1000_V2_LIST()
        {

        }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 작업일자
        /// </summary>
        public Nullable<DateTime> WorkDate { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 작업지시번호
        /// </summary>
        public string WorkNo { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 공정코드
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 공정코드 명
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 설비코드
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 설비코드 명
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 품목코드
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 품목
        /// </summary>
        public string ItemNm { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 품번
        /// </summary>
        public string ItemNm1 { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 작업지시수량
        /// </summary>
        public int PlanQty { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 작업상태코드
        /// </summary>
        public string JobStatus { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 작업상태코드명
        /// </summary>
        public string JobStatusName { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 공정순번
        /// </summary>
        public int PSeq { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 
        /// </summary>
        public string EMType { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 
        /// </summary>
        public int Eord { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 주거래처 코드
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 20220222 오세완 차장 
        /// 주거래처 코드명
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 20220503 오세완 차장 
        /// gridfocus때문에 추가 
        /// </summary>
        public decimal RowId { get; set; }

        /// <summary>
        /// 작업자
        /// 2022-07-14 김진우
        /// </summary>
        public string WorkId { get; set; }
    }
}
