using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사구관리</summary>	
    [Table("TN_CHF2000T")]
    public class TN_CHF2000 : BaseDomain.MES_BaseDomain
    {
        public TN_CHF2000()
        {
            TN_CHF2001List = new List<TN_CHF2001>();
            TN_CHF2002List = new List<TN_CHF2002>();
        }
        /// <summary>
        /// 검사구코드
        /// </summary>
        [Key, Column("CHFI_CODE"), Required(ErrorMessage = "CheckerFixCode")] public string CheckerFixCode { get; set; }
        /// <summary>
        /// 검사구명
        /// </summary>
        [Column("CHFI_NAME"), Required(ErrorMessage = "CheckerFixName")] public string CheckerFixName { get; set; }
        /// <summary>
        /// 검사구명(영문)
        /// </summary>
        [Column("CHFI_NAME_ENG")] public string CheckerFixNameENG { get; set; }
        /// <summary>
        /// 검사구명(중명)
        /// </summary>
        [Column("CHFI_NAME_CHN")] public string CheckerFixNameCHN { get; set; }
        /// <summary>
        /// 검사구종류
        /// </summary>
        [Column("CHFI_KIND")] public string CheckerFixKind { get; set; }
        /// <summary>
        /// 도면 CO2
        /// </summary>
        [Column("MAP_CO2")] public string MapCo2 { get; set; }
        /// <summary>
        /// 도면SPOT
        /// </summary>
        [Column("MAP_SPOT")] public string MapSpot { get; set; }
        /// <summary>
        /// 제품CO2
        /// </summary>
        [Column("PROD_CO2")] public string ProdCo2 { get; set; }
        /// <summary>
        /// 제품SPOT
        /// </summary>
        [Column("PROD_SPOT")] public string ProdSpot { get; set; }
        /// <summary>
        /// 제작업체
        /// </summary>
        [Column("MAKER")] public string Maker { get; set; }
        /// <summary>
        /// 제작일자
        /// </summary>
        [Column("MAKER_DATE")] public DateTime? MakerDate { get; set; }
        /// <summary>
        /// 파일명
        /// </summary>
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>
        /// 파일URL
        /// </summary>
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>
        /// 파일명1
        /// </summary>
        [Column("FILE_NAME1")] public string FileName1 { get; set; }
        /// <summary>
        /// 파일URL1
        /// </summary>
        [Column("FILE_URL1")] public string FileUrl1 { get; set; }
        /// <summary>
        /// 파일명2
        /// </summary>
        [Column("FILE_NAME2")] public string FileName2 { get; set; }
        /// <summary>
        /// 파일URL2
        /// </summary>
        [Column("FILE_URL2")] public string FileUrl2 { get; set; }
        /// <summary>
        /// 파일명3
        /// </summary>
        [Column("FILE_NAME3")] public string FileName3 { get; set; }
        /// <summary>
        /// 파일URL3
        /// </summary>
        [Column("FILE_URL3")] public string FileUrl3 { get; set; }
        /// <summary>
        /// 사용여부
        /// </summary>
        [Column("USE_FLAG")] public string UseFlag { get; set; }
        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }
      

        public virtual ICollection<TN_CHF2001> TN_CHF2001List { get; set; }
        public virtual ICollection<TN_CHF2002> TN_CHF2002List { get; set; }

    }
}