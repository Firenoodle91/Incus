using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MEA1300T")]
    public class TN_MEA1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1300() {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("INSTR_NO",Order =0)] public string InstrNo { get; set; }
        [Key,Column("INSTR_SEQ",Order =1)] public int InstrSeq { get; set; }
        [Column("CHECK_DATE"), Required(ErrorMessage = "점검일자는 필수입니다")] public Nullable<DateTime> CheckDate { get; set; }
        [Column("CHECK_GU"), Required(ErrorMessage = "점검구분은 필수입니다")] public string CheckGu { get; set; }
        [Column("CHECK_ID"), Required(ErrorMessage = "점검자는 필수입니다")] public string CheckId { get; set; }
        [Column("MEMO"), Required(ErrorMessage = "점검내용은 필수입니다")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [ForeignKey("InstrNo")]
        public virtual TN_MEA1200 TN_MEA1200 { get; set; }
    }
}
