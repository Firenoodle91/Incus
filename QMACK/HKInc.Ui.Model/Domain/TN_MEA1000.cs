using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 설비기준관리
    /// </summary>
    [Table("TN_MEA1000T")]
    public class TN_MEA1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1000()
        {
            TN_MEA1001List = new List<TN_MEA1001>();    // 2022-04-19 김진우 추가
            // 20220313 오세완 차장 설비일상점검기준관리 추가 
            TN_MEA1002List = new List<TN_MEA1002>();
            TN_MEA1100List = new List<TN_MEA1100>();
            TN_UPH1000List = new List<TN_UPH1000>();
        }

        /// <summary>설비고유코드</summary>
        [Key,Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>설비코드, 설비명처럼 지칭하는 코드</summary>
        [Column("MACHINE_CODE2")] public string MachineCode2 { get; set; }
        /// <summary>설비그룹코드</summary>
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비명</summary>
        [Column("MACHINE_NAME")] public string MachineName { get; set; }
        /// <summary>모델번호</summary>
        [Column("MODEL_NO")] public string ModelNo { get; set; }
        /// <summary>제조사</summary>
        [Column("MAKER")] public string Maker { get; set; }
        /// <summary>설치일</summary>
        [Column("INSTALL_DATE")] public Nullable<DateTime> InstallDate { get; set; }
        /// <summary>일련번호</summary>
        [Column("SERIAL_NO")] public string SerialNo { get; set; }
        /// <summary>기존에는 점검주기였으나 예방보전주기로 변경</summary>
        [Column("CHECK_TURN")] public string CheckTurn { get; set; }
        /// <summary>설비사진 파일명</summary>
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>기존에 설비사진 파일 데이터로 사용하였으나 사용 안할 예정</summary>
        [Column("FILE_DATA")] public byte[] FileData { get; set; }
        /// <summary>설비파일 사진 ftp경로</summary>
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>점검포인트 파일명</summary>
        [Column("CHECK_POINT_FILE_NAME")] public string CheckPointFileName { get; set; }
        /// <summary>점검포인트 ftp경로</summary>
        [Column("CHECK_POINT_FILE_NAME_URL")] public string CheckPointFileNameUrl { get; set; }
        /// <summary>예방보전점검 파일명</summary>
        [Column("MAINTERNANCE_FILE_NAME")] public string MainternanceFileName { get; set; }
        /// <summary>예방보전점검 ftp경로</summary>
        [Column("MAINTERNANCE_FILE_NAME_URL")] public string MainternanceFileNameUrl { get; set; }
        /// <summary>점검예정일</summary>
        [Column("NEXT_CHECK")] public Nullable<DateTime> NextCheck { get; set; }
        /// <summary>일상점검유무</summary>               2021-11-17 김진우 주임 추가
        [Column("DAILY_CHECK_FLAG")] public string DailyCheckFlag { get; set; }
        /// <summary>등급</summary>
        [Column("CLASS")] public string Class { get; set; }
        /// <summary>등급평가일</summary>
        [Column("CLASS_DATE")] public Nullable<DateTime> ClassDate { get; set; }
        /// <summary>점수</summary>
        [Column("SCORE")] public Nullable<decimal> Score { get; set; }
        /// <summary>모니터링위치</summary>
        [Column("MONITOR_LOCATION")] public string MonitorLocation { get; set; }
        /// <summary>사용여부</summary>
        [Column("USE_YN")] public string UseYn { get; set; }
        /// <summary>메모</summary>
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>20220323 원래는 공정이었으나 설비 그룹처럼 사용한 것이다. 설비그룹으로 컬럼을 대체할 예정</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual ICollection<TN_MEA1100> TN_MEA1100List { get; set; }
        public virtual ICollection<TN_UPH1000> TN_UPH1000List { get; set; }

        public virtual ICollection<TN_MEA1002> TN_MEA1002List { get; set; }
        /// <summary>설비이력관리</summary> 2022-04-19 김진우 추가
        public virtual ICollection<TN_MEA1001> TN_MEA1001List { get; set; }
    }
}
