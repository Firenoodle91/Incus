using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>금형등급관리기준 </summary>	
    [Table("TN_MOLD1600T")]
    public class TN_MOLD1600 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1600()
        {
            TN_MOLD1601List = new List<TN_MOLD1601>();
            TN_MOLD1602List = new List<TN_MOLD1602>();

        }
        /// <summary> 등급관리번호 </summary>                   
        [Key, Column("GRADE_MANAGE_NO", Order = 0), Required(ErrorMessage = "GradeManageNo")] public string GradeManageNo { get; set; }
        /// <summary> 개정일자</summary>
        [Key, Column("REVISION_DATE", Order = 1), Required(ErrorMessage = "RevisionDate")] public DateTime RevisionDate { get; set; }
        /// <summary> 등록일자</summary>
        [Column("REG_DATE"), Required(ErrorMessage = "RegDate")] public DateTime RegDate { get; set; }
        /// <summary>등록자</summary>
        [Column("REG_ID"), Required(ErrorMessage = "RegId")] public string RegId { get; set; }
        /// <summary>메모</summary>                    
        [Column("MEMO")] public string Memo { get; set; }        
        /// <summary> 임시</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>        
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }
        

        public virtual ICollection<TN_MOLD1601> TN_MOLD1601List { get; set; }
        public virtual ICollection<TN_MOLD1602> TN_MOLD1602List { get; set; }

    }
}