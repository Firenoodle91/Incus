using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MEA1000T")]
    public class TN_MEA1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1000() {
            CreateId = BaseDomain.GsValue.UserId; //       HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId; //HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            TN_MEA1100List = new List<TN_MEA1100>();
            TN_MEA1001List = new List<TN_MEA1001>();
            TN_MEA1002List = new List<TN_MEA1002>();
            TN_MEA1004List = new List<TN_MEA1004>();
            TN_UPH1000List = new List<TN_UPH1000>();
        }
        [Key,Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("MACHINE_NAME")] public string MachineName { get; set; }
        [Column("MODEL_NO")] public string ModelNo { get; set; }
        [Column("MAKER")] public string Maker { get; set; }
        [Column("INSTALL_DATE")] public Nullable<DateTime> InstallDate { get; set; }
        [Column("SERIAL_NO")] public string SerialNo { get; set; }
        [Column("CHECK_TURN")] public string CheckTurn { get; set; }
        [Column("FILE_NAME")] public string FileName { get; set; }
        [Column("FILE_DATA")] public byte[] FileData { get; set; }
        [Column("FILE_NAME2")] public string FileName2 { get; set; }
        [Column("FILE_DATA2")] public byte[] FileData2 { get; set; }
        [Column("NEXT_CHECK")] public Nullable<DateTime> NextCheck { get; set; }
        [Column("DAILY_CHECK_FLAG"), Required(ErrorMessage = "DailyCheckFlag")] public string DailyCheckFlag { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual ICollection<TN_MEA1100> TN_MEA1100List { get; set; }
        public virtual ICollection<TN_UPH1000> TN_UPH1000List { get; set; }
        public virtual ICollection<TN_MEA1001> TN_MEA1001List { get; set; }
        public virtual ICollection<TN_MEA1002> TN_MEA1002List { get; set; }
        public virtual ICollection<TN_MEA1004> TN_MEA1004List { get; set; }
    }
}
