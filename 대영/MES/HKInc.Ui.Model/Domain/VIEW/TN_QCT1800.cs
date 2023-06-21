using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>교육계획자료관리</summary>	
    [Table("TN_QCT1800T")]
    public class TN_QCT1800 : BaseDomain.MES_BaseDomain2
    {
        public TN_QCT1800()
        {
        }
        /// <summary>순번</summary>                
        [Key, Column("SEQ", Order = 0), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>교육일자</summary>                   
        [Column("EDU_DATE")] public DateTime EduDate { get; set; }
        /// <summary>교육계획서명</summary>                   
        [Column("EDU_DOC_NAME")] public string EduDocName { get; set; }
        /// <summary>파일명</summary>                    
        [Column("FILE_NAME")] public string FileName { get; set; }
        /// <summary>파일URL</summary>                   
        [Column("FILE_URL")] public string FileUrl { get; set; }
        /// <summary>메모</summary>                      
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

    }
}