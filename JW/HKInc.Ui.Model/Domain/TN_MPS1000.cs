using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1000T")]
    public class TN_MPS1000 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1000()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Key,Column("PROCESS_CODE",Order =1)] public string ProcessCode { get; set; }
        [Column("PROCESS_SEQ")] public Nullable<int> ProcessSeq { get; set; }
        [Column("WORK_STANTAD_NM")] public string WorkStantadnm { get; set; }
        [Column("FILE_DATA")] public byte[] FileData { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("OutProc")] public string OutProc { get; set; }
        [Column("STD")] public string STD { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}