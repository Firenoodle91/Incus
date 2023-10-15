using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;
using System.Drawing;


namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_MOLD1100T")]
    public class TN_MOLD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1100()
        {
            TN_MOLD1101List = new List<TN_MOLD1101>();
            TN_MOLD1300List = new List<TN_MOLD1300>();
            TN_MOLD1400List = new List<TN_MOLD1400>();
            _Check = "N";
        }
        /// <summary>
        ///  관리번호
        /// </summary>
        [Key, Column("MOLD_MCODE", Order = 0), Required(ErrorMessage = "MoldMCode")]
        public string MoldMCode { get; set; }

        /// <summary>
        ///  금형코드        
        /// </summary>       
        [Column("MOLD_CODE"), Required(ErrorMessage = "MoldCode")]
        public string MoldCode { get; set; }

        /// <summary>
        ///  금형명
        /// </summary>
        [Column("MOLD_NAME"), Required(ErrorMessage = "MOLD_NAME")]
        public string MoldName { get; set; }

        /// <summary>
        ///  품목코드
        /// </summary>
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        /// <summary>
        ///  제작처
        /// </summary>
        [Column("MOLD_MAKER_CUST")]
        public string MoldMakerCust { get; set; }

        /// <summary>
        ///  이관일
        /// </summary>
        [Column("TRANSFER_DATE")]
        public Nullable<DateTime> TransferDate { get; set; }

        /// <summary>
        ///  메인설비
        /// </summary>
        [Column("MAIN_MACHINE_CODE")]
        public string MainMachineCode { get; set; }

        /// <summary>
        ///  캐비티
        /// 프레스가 한번 찍을때 생산되는 제품의 숫자
        /// </summary>
        [Column("CAVITY")]
        public Nullable<int> Cavity { get; set; }
        /// <summary>
        ///  창고코드
        /// </summary>
        [Column("MOLD_WH_CODE")]
        public string MoldWhCode { get; set; }
        /// <summary>
        ///  창고위치
        /// </summary>
        [Column("MOLD_WH_POSITION")]
        public string MoldWhPosition { get; set; }

        /// <summary>
        ///  위치1
        /// </summary>
        [Column("ST_POSTION1")]
        public string StPostion1 { get; set; }

        /// <summary>
        ///  위치2
        /// </summary>
        [Column("ST_POSTION2")]
        public string StPostion2 { get; set; }

        /// <summary>
        ///  위치3
        /// </summary>
        [Column("ST_POSTION3")]
        public string StPostion3 { get; set; }

        [Column("MEMO")] public string Memo { get; set; }

        /// <summary>
        ///  점검주기
        /// </summary>
        [Column("CHECK_CYCLE")] public string CheckCycle { get; set; }

        /// <summary>
        ///  등급
        /// </summary>
        [Column("MOLD_CLASS")] public string MoldClass { get; set; }

        /// <summary>
        ///  기준샷수
        /// 금형이 상품을 찍어낼 수 있는 수명도
        /// </summary>
        [Column("STD_SHOT_CNT")]
        public Nullable<decimal> StdShotcnt { get; set; }

        /// <summary>
        ///  샷수
        /// 실제로 프레스를 하여 제품을 찍어낸 숫자
        /// </summary>
        [Column("REAL_SHOT_CNT")]
        public Nullable<decimal> RealShotcnt { get; set; }

        /// <summary>
        ///  기본샷수
        /// 금형이 최초로 생성된 제품이 아닌 경우를 등록하였을 때 기존에 생산하였던 제품의 숫자를 기록
        /// </summary>
        [Column("BASE_SHOT_CNT")]
        public Nullable<decimal> BaseShotcnt { get; set; }

        /// <summary>
        ///  누적샷수
        /// TN_MPS1041T 테이블에 있는 트리거가 해당 값을 갱신한다. 
        /// </summary>
        [Column("SUM_SHOT_CNT")]
        public Nullable<decimal> SumShotcnt { get; set; }
        /// <summary>
        ///  점검기준타발수        
        /// </summary>
        [Column("CHECK_SHOT_CNT")]
        public Nullable<decimal> CheckShotcnt { get; set; }
        /// <summary>
        ///  사용여부
        /// </summary>
        [Column("USE_YN")] public string UseYN { get; set; }
        /// <summary>
        ///  금형일상점검유뮤
        /// </summary>        
        [Column("MOLD_DAY_INSP_FLAG")] public string MoldDayInspFlag { get; set; }
        /// <summary>
        ///  다음점검일
        /// </summary>
        [Column("NEXTCHECKDATE")]
        public Nullable<DateTime> NextCheckDate { get; set; }
        /// <summary>
        ///  테이블 명세에는 점검기준으로 칭하고 XPFMEA1400.cs에서는 점검기준타발로 지정한다. 
        /// 즉 해당 금형이 프레스로 n회로 제품을 생산하였을 때 점검해야 한다는 기준 숫자를 의미하는 듯 하다. 
        /// </summary>
        [Column("CHECK_POINT")]
        public Nullable<decimal> CheckPoint { get; set; }
        /// 금형사진명
        /// </summary>
        [Column("MOLD_FILE_NAME")] public string MoldFileName { get; set; }
        /// <summary>
        /// 금형사진URL
        /// </summary>
        [Column("MOLD_FILE_URL")] public string MoldFileUrl { get; set; }
        /// 금형일상점검사진명
        /// </summary>
        [Column("MOLD_CHECK_FILE_NAME")] public string MoldCheckFileName { get; set; }
        /// <summary>
        /// 금형일상점검사진URL
        /// </summary>
        [Column("MOLD_CHECK_FILE_URL")] public string MoldCheckFileUrl { get; set; }
        /// <summary>
        /// 폐기일자
        /// </summary>
        [Column("DISUSE_DATE")] public Nullable<DateTime> DisuseDate { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_MOLD1101> TN_MOLD1101List { get; set; }
        public virtual ICollection<TN_MOLD1300> TN_MOLD1300List { get; set; }
        public virtual ICollection<TN_MOLD1400> TN_MOLD1400List { get; set; }
    }
}
