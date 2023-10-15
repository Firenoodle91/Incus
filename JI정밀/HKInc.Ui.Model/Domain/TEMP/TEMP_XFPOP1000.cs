using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.TEMP
{
    public class TEMP_XFPOP1000
    {
        /// <summary>고유 ID</summary>
        public decimal RowId { get; set; }
        /// <summary>긴급여부</summary>
        public string EmergencyFlag { get; set; }
        /// <summary>작업상태</summary>
        public string JobStates { get; set; }
        /// <summary>작업지시번호</summary>
        public string WorkNo { get; set; }
        /// <summary>공정순번</summary>
        public int ProcessSeq { get; set; }
        /// <summary>공정코드</summary>
        public string ProcessCode { get; set; }
        /// <summary>작업지시일</summary>
        public DateTime WorkDate { get; set; }
        /// <summary>초기납기요청일</summary>
        public DateTime? StartDueDate { get; set; }        
        /// <summary>설비그룹코드</summary>
        public string MachineGroupCode { get; set; }
        /// <summary>설비코드</summary>
        public string MachineCode { get; set; }
        /// <summary>설비코드</summary>
        public string MachineCode2 { get; set; }
        /// <summary>설비필수여부</summary>
        public string MachineFlag { get; set; }
        /// <summary>품번(도번)</summary>
        public string ItemCode { get; set; }
        /// <summary>품명</summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 20210625 오세완 차장 
        /// 품목명 추가
        /// </summary>
        public string ItemName1 { get; set; }
        /// <summary>품명(영문)</summary>
        public string ItemNameENG { get; set; }
        /// <summary>품명(중문)</summary>
        public string ItemNameCHN { get; set; }
        /// <summary>표면처리항목</summary>
        public string SurfaceList { get; set; }
        /// <summary>작업지시량</summary>
        public decimal WorkQty { get; set; }

        /// <summary>지시 공정별 생산 수량 합계</summary>
        public decimal ResultSumQty { get; set; }

        /// <summary>메모</summary>
        public string Memo { get; set; }
        /// <summary>작업표준서명</summary>
        public string WorkStandardDocument { get; set; }
        public string WorkStandardDocumentUrl { get; set; }
        /// <summary>품목도면명</summary>
        public string DesignFileName { get; set; }
        public string DesignFileUrl { get; set; }
        /// <summary>거래처코드</summary>
        public string CustomerCode { get; set; }
        /// <summary>공정포장수량</summary>
        public decimal? ProcessPackQty { get; set; }
        /// <summary>제조팀코드</summary>
        public string ProcTeamCode { get; set; }
        /// <summary>
        /// 20210602 오세완 차장 
        /// 대분류명 추가
        /// </summary>
        public string TopCategoryName { get; set; }


        #region JI

        //public Nullable<DateTime> WorkDate { get; set; }
        //public string WorkNo { get; set; }

        public string Process { get; set; }

        //public string MachineCode { get; set; }

        //public string ItemCode { get; set; }

        public int PlanQty { get; set; }

        //public string Memo { get; set; }


        public string JobStatus { get; set; }

        public string WorkStantadNm { get; set; }

        public byte[] FileData { get; set; }

        public string DesignFile { get; set; }

        public byte[] DesignMap { get; set; }
        public int PSeq { get; set; }
        public string ToolUseFlag { get; set; }//공구사용여부
        #endregion
    }
}
