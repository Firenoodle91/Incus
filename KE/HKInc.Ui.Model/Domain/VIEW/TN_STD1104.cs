using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>품목한도이력관리</summary>	
    [Table("TN_STD1104T")]
    public class TN_STD1104 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1104()
        {

        }        
        /// <summary>품목코드</summary>        
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>        
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>검사구분</summary>        
        [Column("CHECK_LIST")] public string CheckList { get; set; }
        /// <summary>공정</summary>        
        [Column("CHECK_DIVISION")] public string CheckDivision { get; set; }
        /// <summary>검사항목</summary>        
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>적용일</summary>        
        [Column("APPLY_DATE")] public DateTime? ApplyDate { get; set; }
        /// <summary>적용자</summary>        
        [Column("APPLY_ID")] public string ApplyId { get; set; }
        /// <summary>파일명</summary>        
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>        
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>메모</summary>        
        [Column("MEMO")] public string Memo { get; set; }

      
        [NotMapped] public object localImage { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }

    }
}

