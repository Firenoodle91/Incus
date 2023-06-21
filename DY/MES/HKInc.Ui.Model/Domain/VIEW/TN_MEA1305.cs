using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 2021-06-16 김진우 주임 생성
    /// 설비/금형등급평가서 관리
    /// </summary>	
    [Table("TN_MEA1305T")]
    public class TN_MEA1305 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1305()
        {
        }

        /// <summary>설비금형채번값</summary>
        [Key, Column("MANAGE_NO"), Required(ErrorMessage = "ManageNo")] public string ManageNo { get; set; }
        /// <summary>설비금형코드</summary>        
        [Column("MANAGE_CODE")] public string ManageCode { get; set; }
        /// <summary>년도</summary>        
        [Column("SAVE_YEAR"), Required(ErrorMessage = "SaveYear")] public DateTime SaveYear { get; set; }
        /// <summary>설비금형구분</summary>          
        [Column("MANAGE_TYPE")] public string ManageType { get; set; }
        /// <summary>파일명</summary>          
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>         
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>메모</summary>            
        [Column("MEMO")] public string Memo { get; set; }
        
        [NotMapped]
        public object localImage { get; set; }

    }
}