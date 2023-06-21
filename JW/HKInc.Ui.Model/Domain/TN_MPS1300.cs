using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1300T")]
    public class TN_MPS1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1300() {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
          MPS1400List = new List<TN_MPS1400>();
        }
        [Key,Column("DELIV_SEQ",Order =1)] public string DelivSeq { get; set; }
        [Key,Column("PLAN_NO",Order =0)] public string PlanNo { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("PLAN_QTY")] public int PlanQty { get; set; }
        [Column("DELIV_DATE")] public Nullable<DateTime> DelivDate { get; set; }
        [Column("PLAN_STARTDT")] public Nullable<DateTime> PlanStartdt { get; set; }
        [Column("PLAN_ENDDT")] public Nullable<DateTime> PlanEnddt { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("WORKORDER_YN")] public string WorkorderYn { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string OrderNo { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        [NotMapped]
        public string PlanYYMM {
        get {

                if (PlanStartdt == null)
                {
                    return null;
                }
                else {
                    return Convert.ToDateTime(PlanStartdt).ToString("yyyyMM");
                }
                
            }
        }
        public virtual ICollection<TN_MPS1400> MPS1400List { get; set; }
    }
}
