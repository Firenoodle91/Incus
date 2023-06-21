using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>계측기검교정이력관리</summary>	
    [Table("TN_MEA1501T")]
    public class TN_MEA1501 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1501()
        {
        }
        /// <summary>계측기코드</summary>         
        [ForeignKey("TN_MEA1500"), Key, Column("RBID_CODE", Order = 0), Required(ErrorMessage = "RBIDCode")] public string RBIDCode { get; set; }
        /// <summary>순번</summary>               
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>점검일</summary>             
        [Column("CHECK_DATE")] public DateTime? CheckDate { get; set; }
        /// <summary>구분</summary>               
        [Column("CHECK_DIVISION")] public string CheckDivision { get; set; }
        /// <summary>점검자</summary>             
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>점검내용</summary>           
        [Column("CHECK_CONTENT")] public string CheckContent { get; set; }
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

        public virtual TN_MEA1500 TN_MEA1500 { get; set; }
    }
}