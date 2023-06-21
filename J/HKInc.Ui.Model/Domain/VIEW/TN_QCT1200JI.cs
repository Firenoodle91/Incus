using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_QCT1200T_JI")]
    public class TN_QCT1200JI : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1200JI()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            QCT1201JIList = new List<TN_QCT1201JI>();
        }
        [Key,Column("NO",Order =0)] public string No { get; set; }//검사번호
        [Column("FME_NO")] public string FmeNo { get; set; }//검사구분
        [Column("FME_DIVISION")] public string FmeDivision { get; set; }//일반 01 초 02 중 03 종 04
        [Column("WORK_DATE")] public Nullable<DateTime> WorkDate { get; set; } //작업일
        [Column("WORK_NO")] public string WorkNo { get; set; } //작업지시번호  
        [Column("ITEM_CODE")] public string ItemCode { get; set; }//품목코드
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }//공정
        [Column("CHECK_DATE")] public Nullable<DateTime> CheckDate { get; set; }//검사일
        [Column("CHECK_ID")] public string CheckId { get; set; }//검사자
        [Column("CHECK_RESULT")] public string CheckResult { get; set; }//검사결과
        [Column("FILE_NAME")] public string FileName { get; set; }
        [Column("FILE_DATA")] public byte[] FileData { get; set; }
        [Column("MEMO")] public string Memo { get; set; } //메모
       
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual ICollection<TN_QCT1201JI> QCT1201JIList { get; set; }
    }
}