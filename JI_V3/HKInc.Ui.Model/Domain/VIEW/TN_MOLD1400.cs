using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>금형일상점검 마스터 </summary>	
    [Table("TN_MOLD1400T")]
    public class TN_MOLD1400 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1400()
        {
            //TN_MOLD1500List = new List<TN_MOLD1500>();
            _Check = "N";
        }
        /// <summary>금형관리번호</summary>
        [ForeignKey("TN_MOLD1100"), Key, Column("MOLD_MCODE", Order = 0), Required(ErrorMessage = "MoldMCode")] public string MoldMCode { get; set; }
        /// <summary> 금형코드</summary>
        [Column("MOLD_CODE")] public string MoldCode { get; set; }
        /// <summary> 점검순번</summary>
        [Key, Column("SEQ", Order = 1)] public int Seq { get; set; }
        /// <summary>점검구분</summary>
        [Column("REQ_TYPE")] public string ReqType { get; set; }
        /// <summary>파일명</summary>                    
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary> 점검위치</summary>
        [Column("CHECK_POSITION")] public string CheckPosition { get; set; }
        /// <summary> 점검항목</summary>
        [Column("CHECK_LIST")] public string CheckList { get; set; }
        /// <summary> 점검방법</summary>
        [Column("CHECK_WAY")]  public string CheckWay { get; set; }
        /// <summary> 육안검사여부</summary>
        [Column("EYE_CHECK_FLAG")] public string EyeCheckFlag { get; set; }        
        /// <summary>점검주기</summary>
        [Column("CHECK_CYCLE")] public string CheckCycle { get; set; }
        /// <summary> 점검기준일</summary>
        [Column("CHECK_STANDARD_DATE")] public string CheckStandardDate { get; set; }
        /// <summary> 관리기준 </summary>
        [Column("MANAGEMENT_STANDARD")] public string ManagementStandard { get; set; }
        /// <summary> 표시순서 </summary>
        [Column("DISPLAY_ORDER")] public int? DisplayOrder { get; set; }
        /// <summary> 메모</summary>
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary> 임시</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>        
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }
        
        public virtual TN_MOLD1100 TN_MOLD1100 { get; set; }

        //public virtual ICollection<TN_MOLD1500> TN_MOLD1500List { get; set; }
    }
}