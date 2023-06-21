using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_UPH1000T")]
    public class TN_UPH1000 : BaseDomain.MES_BaseDomain
    {
        public TN_UPH1000()
        {
            CreateId = BaseDomain.GsValue.UserId; //       HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId; //HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("MACHINE_CODE", Order = 0)] public string MachineCode { get; set; }
        [Key, Column("ITEM_CODE", Order = 1)] public string ItemCode { get; set; }
        [Key, Column("PROCESS_CODE", Order = 2)] public string ProcessCode { get; set; }
        [Column("UPH")] public int? UPH { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}
