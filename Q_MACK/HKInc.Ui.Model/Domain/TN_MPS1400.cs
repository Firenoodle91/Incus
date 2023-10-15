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
    /// 20220318 오세완 차장 작업지시마스터
    /// </summary>
    [Table("TN_MPS1400T")]
    public class TN_MPS1400 : BaseDomain.BaseDomain
    {
        public TN_MPS1400() { }
        [Key,Column("WORK_DATE",Order =0)] public Nullable<DateTime> WorkDate { get; set; }
        [Key,Column("WORK_NO",Order =1)] public string WorkNo { get; set; }
        [ForeignKey("TN_MPS1300"),Key,Column("PLAN_NO",Order =2)] public string PlanNo { get; set; }
        [Key,Column("P_SEQ",Order =3)] public int PSeq { get; set; }
        [Key,Column("PROCESS",Order =4)] public string Process { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PLAN_QTY")] public int PlanQty { get; set; }
        [Column("DELIV_DATE")] public Nullable<DateTime> DelivDate { get; set; }
        [Column("ORDER_NO")] public string OrderNo { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }

        /// <summary>
        /// 20220407 오세완 차장
        /// 임시, xfmps1201에서 작업지시일 일괄변경을 실행하면 프로시저 이름이 기록된다. 
        /// </summary>
        [Column("TEMP")] public string Temp { get; set; }
        [Column("EM_TYPE")] public string EMType { get; set; }
        [Column("OutProc")] public string OutProc { get; set; }
        [ForeignKey("TN_MPS1300"),Column("DELIV_SEQ",Order =5)] public string DelivSeq { get; set; }
        [Column("JOB_STATES")] public string JobStates { get; set; }
        public virtual TN_MPS1300 TN_MPS1300 { get; set; }
    }
}
