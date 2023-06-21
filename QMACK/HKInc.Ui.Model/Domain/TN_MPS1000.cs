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
    /// 20220318 오세완 차장 제품별표준공정관리
    /// </summary>
    [Table("TN_MPS1000T")]
    public class TN_MPS1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1000() { }

        /// <summary>
        /// 20220318 오세완 차장
        /// 품목코드
        /// </summary>
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 공정코드
        /// </summary>
        [Key,Column("PROCESS_CODE",Order =1)] public string ProcessCode { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 공정순번
        /// </summary>
        [Column("PROCESS_SEQ")] public Nullable<int> ProcessSeq { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 작업표준서명
        /// </summary>
        [Column("WORK_STANTAD_NM")] public string WorkStantadnm { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 사용안함, ftp방식으로 변경
        /// </summary>
        [Column("FILE_DATA")] public byte[] FileData { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 사용여부
        /// </summary>
        [Column("USE_YN")] public string UseYn { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 외주여부
        /// </summary>
        [Column("OutProc")] public string OutProc { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 표준작업소요일
        /// </summary>
        [Column("STD")] public string STD { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 임시2
        /// </summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 작업지시서 ftp 경로
        /// </summary>

        [Column("WORK_STANDARD_URL")]
        public string WorkStandardUrl { get; set; }

        /// <summary>
        /// 20220323 오세완 차장
        /// 설비그룹코드 추가 
        /// </summary>
        [Column("MACHINE_GROUP_CODE")]
        public string MachineGroupCode { get; set; }

        [NotMapped] public object localImage { get; set; }

        [ForeignKey("ItemCode")] public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}