using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_MEA1400T")]
    public class TN_MEA1400 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1400()
        {
          
        }
        [Key,Column("MOLD_CODE", Order = 0), Required(ErrorMessage = "금형코드는 필수입니다.")] public string MoldCode { get; set; }
        [Column("MOLD_NAME"), Required(ErrorMessage = "금형명은 필수입니다.")] public string MoldName { get; set; }
        [Column("MOLD_MAKE_CUST")] public string MoldMakeCust { get; set; }
        [Column("INPUT_DT")] public Nullable<DateTime> InputDt { get; set; }
       
        [Column("X_CASE")] public Nullable<int> XCase { get; set; }
        [Column("ST_POSTION1")] public string StPostion1 { get; set; }
        [Column("ST_POSTION2")] public string StPostion2 { get; set; }
        [Column("ST_POSTION3")] public string StPostion3 { get; set; }
        [Column("STD_SHOT_CNT")] public Nullable<decimal> StdShotcnt { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("CHECK_CYCLE")] public string CheckCycle { get; set; }
        [Column("MOLD_CLASS")] public string MoldClass { get; set; }
        [Column("REAL_SHOT_CNT")] public Nullable<decimal> RealShotCnt { get; set; }
        [Column("BASE_SHOT_CNT")] public Nullable<decimal> BaseShotCnt { get; set; }
        [Column("SUM_SHOT_CNT")] public Nullable<decimal> SumShotCnt { get; set; }
        [Column("IMG_FILE")] public string ImgFile { get; set; }
        [Column("IMG_URL")] public string ImgUrl { get; set; }
        [Column("USE_YN")] public string UseYN { get; set; }
        [Column("NEXT_CHECK_DATE")] public Nullable<DateTime> NextCheckDate { get; set; }
        [Column("CHECK_POINT")] public Nullable<decimal> CheckPoint { get; set; }
        [Column("SPEC")] public string Spec { get; set; }

        [NotMapped] public object localImage { get; set; }
        [ForeignKey("MoldMakeCust")]
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}
