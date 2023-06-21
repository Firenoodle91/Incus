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
    /// 설비이력관리
    /// </summary>
    [Table("TN_MEA1100T")]
    public class TN_MEA1100 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1100() { }

        /// <summary>설비코드</summary>
        [Key,Column("MACHINE_CODE",Order =0)] public string MachineCode { get; set; }
        /// <summary>설비코드 순번</summary>
        [Key,Column("MACHINE_SEQ",Order =1)] public int MachineSeq { get; set; }
        /// <summary>점검 일자</summary>
        [Column("CHECK_DATE")] public Nullable<DateTime> CheckDate { get; set; }
        /// <summary>점검내역</summary>
        [Column("CHECK_MEMO")] public string CheckMemo { get; set; }
        /// <summary>점검자</summary>
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>임시</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        [ForeignKey("MachineCode")] public virtual TN_MEA1000 TN_MEA1000 { get; set; }
    }
}
