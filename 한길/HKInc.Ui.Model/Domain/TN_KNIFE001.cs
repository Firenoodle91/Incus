using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_KNIFE001T")]
    public class TN_KNIFE001 : BaseDomain.MES_BaseDomain
    {
        public TN_KNIFE001()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            Knife002List = new List<TN_KNIFE002>();
            Knife003List = new List<TN_KNIFE003>();

            _Check = "N";
        }
        [Key,Column("KNIFE_MCODE", Order =0)] public string KnifeMcode { get; set; }
        [Key,Column("KNIFE_CODE", Order =1), Required(ErrorMessage = "칼코드는 필수입니다")] public string KnifeCode { get; set; }
        [Column("KNIFE_NAME"), Required(ErrorMessage = "칼 명은 필수입니다")] public string KnifeName { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("MAKE_CUST")] public string MakeCust { get; set; }
        [Column("INPUT_DT")] public Nullable<DateTime> InputDt { get; set; }
        [Column("MAST_MC")] public string MastMc { get; set; }
        [Column("X_CASE"), Required(ErrorMessage = "Cavity는 필수입니다")] public Nullable<int> XCase { get; set; }
        [Column("ST_POSTION1")] public string StPostion1 { get; set; }
        [Column("ST_POSTION2")] public string StPostion2 { get; set; }
        [Column("ST_POSTION3")] public string StPostion3 { get; set; }
        [Column("STD_SHOT_CNT")] public Nullable<decimal> StdShotcnt { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("CHECK_CYCLE")] public string CheckCycle { get; set; }
        [Column("CLASS")] public string Class { get; set; }
        [Column("REAL_SHOT_CNT")] public Nullable<decimal> RealShotcnt { get; set; }
        [Column("BASE_SHOT_CNT")] public Nullable<decimal> BaseShotcnt { get; set; }
        [Column("SUM_SHOT_CNT")] public Nullable<decimal> SumShotcnt { get; set; }
        [Column("IMGURL")] public string Imgurl { get; set; }
        [Column("USE_YN")] public string UseYN { get; set; }
        [Column("NEXTCHECKDATE")] public Nullable<DateTime> NextCheckDate { get; set; }
        [Column("CHECK_POINT")] public Nullable<decimal> CheckPoint { get; set; }

        [NotMapped] public string _Check { get; set; }

        public virtual ICollection<TN_KNIFE002> Knife002List { get; set; }
        public virtual ICollection<TN_KNIFE003> Knife003List { get; set; }


        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

    }
}
