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
    /// 파일만 제거한 설비기준정보
    /// </summary>
    [Table("VI_MEA1000_NOT_FILE_LIST")]
    public class VI_MEA1000_NOT_FILE_LIST
    {
        [Key, Column("MACHINE_CODE"), Required(ErrorMessage = "설비코드는 필수입니다.")] public string MachineCode { get; set; }
        [Column("MACHINE_NAME"), Required(ErrorMessage = "설비명은 필수입니다.")] public string MachineName { get; set; }
        [Column("MODEL_NO")] public string ModelNo { get; set; }
        [Column("MAKER")] public string Maker { get; set; }
        [Column("INSTALL_DATE")] public Nullable<DateTime> InstallDate { get; set; }
        [Column("SERIAL_NO")] public string SerialNo { get; set; }
        [Column("CHECK_TURN")] public string CheckTurn { get; set; }
        [Column("NEXT_CHECK")] public Nullable<DateTime> NextCheck { get; set; }
        [Column("CONTINUE_CHECK")] public string ContinueCheck { get; set; }
        [Column("DAY_QTY")] public int? DayQty { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("UPD_ID")] public string UpdateId { get; set; }
        [Column("UPD_DATE")] public Nullable<System.DateTime> UpdateTime { get; set; }
        [Column("INS_ID"), Required] public string CreateId { get; set; }
        [Column("INS_DATE"), Required] public Nullable<System.DateTime> CreateTime { get; set; }
        [Column("ROW_ID"), Required(ErrorMessage = "RowId is required")] public decimal RowId { get; set; }

        //public virtual ICollection<TN_MEA1100> TN_MEA1100List { get; set; }
        //public virtual ICollection<TN_MTTF1000> TN_MTTF1000List { get; set; }
    }
}
