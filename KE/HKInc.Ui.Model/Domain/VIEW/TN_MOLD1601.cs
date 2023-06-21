using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>금형등급평가기준 </summary>	
    [Table("TN_MOLD1601T")]
    public class TN_MOLD1601 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1601()
        {
            

        }
        /// <summary> 등급관리번호 </summary>                   
        [ForeignKey("TN_MOLD1600"), Key, Column("GRADE_MANAGE_NO", Order = 0), Required(ErrorMessage = "GradeManageNo")] public string GradeManageNo { get; set; }
        /// <summary> 개정일자</summary>
        [ForeignKey("TN_MOLD1600"), Key, Column("REVISION_DATE", Order = 1), Required(ErrorMessage = "RevisionDate")] public DateTime RevisionDate { get; set; }
        /// <summary> 순번</summary>
        [Key, Column("SEQ", Order = 2), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary> 금형평가항목</summary>
        [Column("EVOL_ITEM"), Required(ErrorMessage = "EvolItem")] public string EvolItem { get; set; }
        /// <summary>평가기준</summary>
        [Column("EVOL_STANDARD"), Required(ErrorMessage = "EvolStandard")] public string EvolStandard { get; set; }
        /// <summary>데이터타입</summary>
        [Column("DATA_TYPE"), Required(ErrorMessage = "DataType")] public string DataType { get; set; }
        /// <summary>최소값</summary>
        [Column("MOLD_MIN"), Required(ErrorMessage = "MoldMin")] public decimal MoldMin { get; set; }
        /// <summary>최대값</summary>
        [Column("MOLD_MAX"), Required(ErrorMessage = "MoldMax")] public decimal MoldMax { get; set; }
        /// <summary>배점</summary>
        [Column("MOLD_POINT"), Required(ErrorMessage = "MoldPoint")] public decimal MoldPoint { get; set; }
        /// <summary>메모</summary>                    
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary> 임시</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>        
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MOLD1600 TN_MOLD1600 { get; set; }
        
    }
}