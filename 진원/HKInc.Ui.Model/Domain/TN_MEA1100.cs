using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MEA1100T")]
    public class TN_MEA1100 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1100()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
            
        }
        [Key,Column("MACHINE_CODE",Order =0)] public string MachineCode { get; set; }
        [Key,Column("MACHINE_SEQ",Order =1)] public int MachineSeq { get; set; }
        [Column("CHECK_DATE")] public Nullable<DateTime> CheckDate { get; set; }
        [Column("CHECK_MEMO")] public string CheckMemo { get; set; }
        [Column("CHECK_ID")] public string CheckId { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [ForeignKey("MachineCode")]
        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
    }
}
