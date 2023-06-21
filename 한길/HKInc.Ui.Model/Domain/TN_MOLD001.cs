using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MOLD001T")]
    public class TN_MOLD001 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD001()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            _Check = "N";

            Mold002List = new List<TN_MOLD002>();
            Mold003List = new List<TN_MOLD003>();
        }
        [Key,Column("MOLD_MCODE",Order =0)] public string MoldMcode { get; set; }
        [Column("MOLD_CODE"), Required(ErrorMessage = "금형코드는 필수입니다.")] public string MoldCode { get; set; }
        [Column("MOLD_NAME"), Required(ErrorMessage = "금형명은 필수입니다.")] public string MoldName { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("MOLD_MAKE_CUST")] public string MoldMakecust { get; set; }
        [Column("INPUT_DT")] public Nullable<DateTime> InputDt { get; set; }
        [Column("MAST_MC")] public string MastMc { get; set; }
        [Column("X_CASE"), Required(ErrorMessage = "Cavity는 필수입니다.")] public Nullable<int> XCase { get; set; }
        [Column("ST_POSTION1")] public string StPostion1 { get; set; }
        [Column("ST_POSTION2")] public string StPostion2 { get; set; }
        [Column("ST_POSTION3")] public string StPostion3 { get; set; }
        [Column("STD_SHOT_CNT")] public Nullable<decimal> StdShotcnt { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("CHECK_CYCLE")] public string CheckCycle { get; set; }
        [Column("MOLD_CLASS")] public string MoldClass { get; set; }
        [Column("REAL_SHOT_CNT")] public Nullable<decimal> RealShotcnt { get; set; }
        [Column("BASE_SHOT_CNT")] public Nullable<decimal> BaseShotcnt { get; set; }
        [Column("SUM_SHOT_CNT")] public Nullable<decimal> SumShotcnt { get; set; }
        [Column("IMGURL")] public string Imgurl { get; set; }
        [Column("USE_YN")] public string UseYN { get; set; }
        [Column("NEXTCHECKDATE")] public Nullable<DateTime> NextCheckDate { get; set; }
        [Column("CHECK_POINT")] public Nullable<decimal> CheckPoint { get; set; }
        [Column("SPEC")] public string Spec { get; set; }

        [NotMapped] public string _Check { get; set; }
        public virtual ICollection<TN_MOLD002> Mold002List { get; set; }
        public virtual ICollection<TN_MOLD003> Mold003List { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        [ForeignKey("MoldMakecust")]
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}
