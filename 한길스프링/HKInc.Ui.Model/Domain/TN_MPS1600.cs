using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1600T")]
    public class TN_MPS1600 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1600()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("STOP_DATE",Order =0)] public Nullable<DateTime> StopDate { get; set; }
        [Key,Column("STOP_SEQ",Order =1)] public string StopSeq { get; set; }
        [Column("STOP_START_TIME")] public Nullable<DateTime> StopStarttime { get; set; }
        [Column("STOP_END_DATE")] public Nullable<DateTime> StopEnddate { get; set; }
        [Column("STOP_CODE")] public string StopCode { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [NotMapped]
        public string DdifM { get {

                DateTime? stopday = StopEnddate == null ? DateTime.Now : StopEnddate;
                TimeSpan diff = Convert.ToDateTime(StopEnddate) - Convert.ToDateTime(StopStarttime);
                return Convert.ToString(diff.Minutes);
            } }

    }
}