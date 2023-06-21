using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_KNIFE002T")]
    public class TN_KNIFE002 : BaseDomain.MES_BaseDomain
    {
        public TN_KNIFE002()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [ForeignKey("TN_KNIFE001"), Key,Column("KNIFE_MCODE",Order =0)] public string KnifeMcode { get; set; }
        [ForeignKey("TN_KNIFE001"), Key,Column("KNIFE_CODE", Order =1)] public string KnifeCode { get; set; }
        [Key,Column("SEQ",Order =2)] public int Seq { get; set; }
        [Column("REQ_TYPE")] public string ReqType { get; set; }
        [Column("START_DATE")] public Nullable<DateTime> StartDate { get; set; }
        [Column("END_DATE")] public Nullable<DateTime> EndDate { get; set; }
        [Column("FALE_NOTE")] public string FaleNote { get; set; }
        [Column("COMMIT_NOTE")] public string CommitNote { get; set; }
        [Column("REQ_NOTE")] public string ReqNote { get; set; }
        [Column("COMMITUCSER")] public string Commitucser { get; set; }
        [Column("SHOT_CNT")] public Nullable<double> ShotCnt { get; set; }
        [Column("REAL_SHOT_CNT")] public Nullable<double> RealShotcnt { get; set; }
        [Column("MC_CODE")] public string McCode { get; set; }
        [Column("WORKER")] public string Worker { get; set; }
        [Column("STATE_YN")] public string StateYn { get; set; }
        [Column("STATE_USER")] public string StateUser { get; set; }
        [Column("MEMO1")] public string Memo1 { get; set; }
        [Column("MEMO2")] public string Memo2 { get; set; }
        [Column("MEMO3")] public string Memo3 { get; set; }
        [Column("MEMO4")] public string Memo4 { get; set; }
        [Column("MEMO5")] public string Memo5 { get; set; }
        public virtual TN_KNIFE001 TN_KNIFE001 { get; set; }
    }
}