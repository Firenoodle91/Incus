using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_MOLD1101T")]
    public class TN_MOLD1101 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1101()
        {
            
        }
        /// <summary>금형관리번호</summary>       
        [ForeignKey("TN_MOLD1100"), Key, Column("MOLD_MCODE", Order = 0), Required(ErrorMessage = "MoldMCode")] public string MoldMCode { get; set; }
        /// <summary>순번</summary>       
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>금형코드</summary>       
        [Column("MOLD_CODE")] public string MoldCode { get; set; }
        /// <summary>구분</summary>       
        [Column("REQ_TYPE")] public string ReqType { get; set; }
        /// <summary>작업시작일</summary>       
        [Column("START_DATE")] public Nullable<DateTime> StartDate { get; set; }
        /// <summary>작업종료일</summary>       
        [Column("END_DATE")] public Nullable<DateTime> EndDate { get; set; }
        /// <summary>문제사항</summary>       
        [Column("FALE_NOTE")] public string FaleNote { get; set; }
        /// <summary>수리내역</summary>       
        [Column("COMMIT_NOTE")] public string CommitNote { get; set; }
        /// <summary>수리결과</summary>       
        [Column("REQ_NOTE")] public string ReqNote { get; set; }
        /// <summary>확인자</summary>       
        [Column("COMMIT_ID")] public string CommitId { get; set; }
        /// <summary>누적샷수</summary>       
        [Column("SUM_SHOT_CNT")] public Nullable<double> ShotCnt { get; set; }
        /// <summary>현재샷수</summary>       
        [Column("REAL_SHOT_CNT")] public Nullable<double> RealShotcnt { get; set; }
        /// <summary>
        ///  등급
        /// </summary>
        [Column("MOLD_CLASS")]  public string MoldClass { get; set; }
        /// <summary>설비코드</summary>       
        [Column("MAIN_MACHINE_CODE")] public string MainMachineCode { get; set; }
        /// <summary>작업자</summary>       
        [Column("WORK_ID")] public string WorkId { get; set; }       
        /// <summary>이상유무판정</summary>       
        [Column("STATE_YN")] public string StateYn { get; set; }
        /// <summary>판정자</summary>       
        [Column("STATE_ID")] public string StateId { get; set; }
        /// <summary>비고1</summary>       
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>비고2</summary>       
        [Column("MEMO2")] public string Memo2 { get; set; }
        /// <summary>비고3</summary>       
        [Column("MEMO3")] public string Memo3 { get; set; }
        /// <summary>비고4</summary>       
        [Column("MEMO4")] public string Memo4 { get; set; }
        /// <summary>비고5</summary>       
        [Column("MEMO5")] public string Memo5 { get; set; }
        public virtual TN_MOLD1100 TN_MOLD1100 { get; set; }
    }
}