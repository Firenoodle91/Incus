using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    /// <summary>
    /// 일일생산일보
    /// </summary>
    public class TEMP_MPS1302_LIST
    {
        /// <summary>일자 </summary>
        public DateTime Date { get; set; }
        /// <summary>설비고유코드 </summary>
        public string MachineMCode { get; set; }
        /// <summary>제조팀코드 </summary>
        public string ProcTeamCode { get; set; }
        /// <summary>설비코드 </summary>
        public string MachineCode { get; set; }
        /// <summary>설비명 </summary>
        public string MachineName { get; set; }
        /// <summary>설비그룹코드</summary>
        public string MachineGroupCode { get; set; }
        /// <summary>설비공정코드</summary>
        public string MachineProcessCode { get; set; }        
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>작업지시번호</summary>
        public string WorkNo { get; set; }
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>
        public int? ProcessSeq { get; set; }
        /// <summary>생산수량</summary>
        public decimal? OkQty { get; set; }
        /// <summary>생산금액</summary>
        public decimal? Price { get; set; }
        /// <summary>계획정지</summary>         
        public decimal? PlanStop { get; set; }
        /// <summary>품번교체</summary>         
        public decimal? ItemCodeChange { get; set; }
        /// <summary>자재품절</summary>         
        public decimal? SrcSoldOut { get; set; }
        /// <summary>재조정</summary>         
        public decimal? ReAdjust { get; set; }
        /// <summary>기계고장</summary>         
        public decimal? MachineBroken { get; set; }
        /// <summary>공수부족</summary>         
        public decimal? WorkLack { get; set; }
        /// <summary>기타</summary>         
        public decimal? Etc { get; set; }
        /// <summary>셋팅건수</summary>         
        public decimal? SettingQty { get; set; }
        /// <summary>가동율</summary>
        public decimal? ProcessRate { get; set; }
        /// <summary>조업시간(근무시간)</summary>         
        public decimal? DayWorkTimeHour { get; set; }
        
        public string EditRowFlag { get; set; }
    }
}
