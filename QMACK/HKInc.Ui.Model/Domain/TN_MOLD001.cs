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
            Mold002List = new List<TN_MOLD002>();
        }

        [Key,Column("MOLD_MCODE",Order =0)] public string MoldMcode { get; set; }
        [Key,Column("MOLD_CODE",Order =1)] public string MoldCode { get; set; }
        [Column("MOLD_NAME")] public string MoldName { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("MOLD_MAKE_CUST")] public string MoldMakecust { get; set; }
        [Column("INPUT_DT")] public Nullable<DateTime> InputDt { get; set; }
        [Column("MAST_MC")] public string MastMc { get; set; }
        [Column("X_CASE")] public Nullable<int> XCase { get; set; }
        [Column("ST_POSITION1")] public string StPosition1 { get; set; }
        [Column("ST_POSITION2")] public string StPosition2 { get; set; }
        [Column("ST_POSITION3")] public string StPosition3 { get; set; }
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
        public virtual ICollection<TN_MOLD002> Mold002List { get; set; }

    }
}
