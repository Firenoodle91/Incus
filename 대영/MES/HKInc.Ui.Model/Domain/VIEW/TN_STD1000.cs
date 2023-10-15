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
    /// 공통코드관리
    /// </summary>
    [Table("TN_STD1000T")]
    public class TN_STD1000 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1000()
        {            
        }

        [Key, Column("CODE_MAIN", Order = 0), Required(ErrorMessage = "CodeMain")]
        public string CodeMain { get; set; }

        [Key, Column("CODE_TOP", Order = 1), Required(ErrorMessage = "CodeTop")]
        public string CodeTop { get; set; }

        [Key, Column("CODE_MID", Order = 2), Required(ErrorMessage = "CodeMid")]
        public string CodeMid { get; set; }

        [Key, Column("CODE_BOTTOM", Order = 3), Required(ErrorMessage = "CodeBottom")]
        public string CodeBottom { get; set; }

        [Column("CODE_VAL")]
        public string CodeVal { get; set; }

        [Column("CODE_NAME")]
        public string CodeName { get; set; }

        [Column("CODE_NAME_ENG")]
        public string CodeNameENG { get; set; }

        [Column("CODE_NAME_CHN")]
        public string CodeNameCHN { get; set; } 
        
        [Column("USE_YN")]
        public string UseYN { get; set; }

        [Column("DISPLAYORDER")]
        public decimal? DisplayOrder { get; set; }

        [Column("MEMO")]
        public string Memo { get; set; }

        /// <summary>
        /// 공정_자주검사여부
        /// </summary>
        [Column("TEMP")]
        public string Temp { get; set; }

        /// <summary>
        /// 공정_초중종검사여부
        /// </summary>
        [Column("TEMP1")]
        public string Temp1 { get; set; }

        /// <summary>
        /// 공정_공정검사여부
        /// </summary>
        [Column("TEMP2")]
        public string Temp2 { get; set; }
    }
}