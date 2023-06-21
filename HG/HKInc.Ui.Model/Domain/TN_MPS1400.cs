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
    /// 작업지시관리 마스터
    /// </summary>
    [Table("TN_MPS1400T")]
    public class TN_MPS1400 : BaseDomain.BaseDomain
    {
        public TN_MPS1400()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("WORK_NO",Order =1)] public string WorkNo { get; set; }
        [ForeignKey("TN_MPS1300"),Key,Column("PLAN_NO",Order =2)] public string PlanNo { get; set; }
        [Key,Column("P_SEQ",Order =3), Required(ErrorMessage = "공정순서는 필수입니다")] public int PSeq { get; set; }
        [Key,Column("PROCESS",Order =4), Required(ErrorMessage = "공정은 필수입니다")] public string Process { get; set; }
        [Column("WORK_DATE"), Required(ErrorMessage = "작업지시일은 필수입니다")] public DateTime WorkDate { get; set; }
        [Column("MACHINE_CODE"), Required(ErrorMessage = "설비는 필수입니다")] public string MachineCode { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PLAN_QTY"), Required(ErrorMessage = "지시수량은 필수입니다")] public int PlanQty { get; set; }
        [Column("DELIV_DATE")] public Nullable<DateTime> DelivDate { get; set; }
        [Column("ORDER_NO")] public string OrderNo { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("OutProc")] public string OutProc { get; set; }
        [ForeignKey("TN_MPS1300"),Column("DELIV_SEQ",Order =5)] public string DelivSeq { get; set; }
        [Column("JOB_STATES")] public string JobStates { get; set; }
        [Column("DISPLAY_ORDER")] public int? DisplayOrder { get; set; }
        public virtual TN_MPS1300 TN_MPS1300 { get; set; }
    }
}
