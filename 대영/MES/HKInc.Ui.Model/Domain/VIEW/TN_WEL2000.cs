using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>용접지그관리</summary>	
    [Table("TN_WEL2000T")]
    public class TN_WEL2000 : BaseDomain.MES_BaseDomain
    {
        public TN_WEL2000()
        {
            TN_WEL2001List= new List<TN_WEL2001>();
            TN_WEL2002List = new List<TN_WEL2002>();
        }
        /// <summary>
        /// 용접지그코드
        /// </summary>
        [Key, Column("WELD_CODE"), Required(ErrorMessage = "WeldingJigCode")] public string WeldingJigCode { get; set; }
        /// <summary>
        /// 용접지그명
        /// </summary>
        [Column("WELD_NAME"), Required(ErrorMessage = "WeldingJigName")] public string WeldingJigName { get; set; }
        /// <summary>
        /// 지그명(영명)
        /// </summary>
        [Column("WELD_NAME_ENG")] public string WeldingJigNameENG { get; set; }
        /// <summary>
        /// 지그명(중명)
        /// </summary>
        [Column("WELD_NAME_CHN")] public string WeldingJigNameCHN { get; set; }
        /// <summary>
        /// 지그종류
        /// </summary>
        [Column("WELD_KIND")] public string WeldingJigKind { get; set; }
        /// <summary>
        /// 도면 CO2
        /// </summary>
        [Column("MAP_CO2")] public string MapCo2 { get; set; }
        /// <summary>
        /// 도면 SPOT
        /// </summary>
        [Column("MAP_SPOT")] public string MapSpot { get; set; }
        /// <summary>
        /// 제품 CO2
        /// </summary>
        [Column("PROD_CO2")] public string ProdCo2 { get; set; }
        /// <summary>
        /// 제품 SPOT
        /// </summary>
        [Column("PROD_SPOT")] public string ProdSpot { get; set; }
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
      

        public virtual ICollection<TN_WEL2001> TN_WEL2001List { get; set; }
        public virtual ICollection<TN_WEL2002> TN_WEL2002List { get; set; }

    }
}