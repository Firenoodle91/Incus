using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>판매계획관리</summary>	
    [Table("TN_ORD1600T")]
    public class TN_ORD1600 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1600()
        {

        }

        /// <summary>매출번호</summary>             
        [Key, Column("SALES_NO", Order = 0), Required(ErrorMessage = "SalesNo")] public string SalesNo { get; set; }    
        [Column("SALES_CONFIRM_FLAG")] public string SalesConfirmFlag { get; set; }
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