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
    /// 금형일상점검 디테일(이력관리 POP입력)
    /// </summary>
    [Table("TN_MOLD1500T")]
    public class TN_MOLD1500 : BaseDomain.MES_BaseDomain
    {
        public TN_MOLD1500()
        {
           
        }

        /// <summary>      
        /// 금형코드
        /// </summary>
        [ForeignKey("TN_MOLD1100"), Key, Column("MOLD_MCODE", Order = 0), Required(ErrorMessage = "MoldMCode")]
        public string MoldMCode { get; set; }        

        /// <summary>        
        /// 점검순번
        /// </summary>
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")]
        public int Seq { get; set; }

        /// <summary>        
        /// 점검일
        /// </summary>
        [Key, Column("CHECK_DATE", Order = 2), Required(ErrorMessage = "CheckDate")]
        public DateTime CheckDate { get; set; }

        /// <summary>        
        /// 점검자
        /// </summary>
        [Column("CHECK_ID")]
        public string CheckId { get; set; }

        /// <summary>        
        /// 점검값
        /// </summary>
        [Column("CHECK_VALUE")]
        public string CheckValue { get; set; }

        /// <summary>
        
        /// 메모
        /// </summary>
        [Column("MEMO")]
        public string Memo { get; set; }

        /// <summary>       
        /// 임시
        /// </summary>
        [Column("TEMP")]
        public string Temp { get; set; }

        /// <summary>        
        /// 임시1
        /// </summary>
        [Column("TEMP1")]
        public string Temp1 { get; set; }

        /// <summary>      
        /// 임시2
        /// </summary>
        [Column("TEMP2")]
        public string Temp2 { get; set; }

        public virtual TN_MOLD1100 TN_MOLD1100 { get; set; }
    }
}
