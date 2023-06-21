using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>성적서 샘플링관리</summary>	
    [Table("TN_QCT1600T")]
    public class TN_QCT1600 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1600()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID", Order = 0), Required(ErrorMessage = "RowId is required")]
        public new decimal RowId { get; set; }
        /// <summary>AQL</summary>                  
        [Key, Column("AQL", Order = 1), Required(ErrorMessage = "InspectionReportType")] public string AQL { get; set; }
        /// <summary>거래처코드</summary>                
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }

        /// <summary>최소범위</summary>                   
        [Column("MIN_VALUE")] public decimal? MinValue { get; set; }
        /// <summary>최대범위</summary>                   
        [Column("MAX_VALUE")] public decimal? MaxValue { get; set; }
        /// <summary>검사수량</summary>                   
        [Column("CHECK_QTY")] public decimal? CheckQty { get; set; }

        /// <summary>메모</summary>                      
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>               
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>               
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}