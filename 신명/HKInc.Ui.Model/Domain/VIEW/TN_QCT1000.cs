using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>검사규격 리비전관리</summary>	
    [Table("TN_QCT1000T")]
    public class TN_QCT1000 : BaseDomain.MES_BaseDomain
    {
        public TN_QCT1000()
        {
            TN_QCT1001List = new List<TN_QCT1001>();
        }
        /// <summary>리비전번호</summary>           
        [Key, Column("REV_NO", Order = 0), Required(ErrorMessage = "RevNo")] public string RevNo { get; set; }
        /// <summary>품번(도번)</summary>           
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>리비전일자</summary>           
        [Column("REV_DATE")] public DateTime? RevDate { get; set; }
        /// <summary>사용여부</summary>             
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        /// <summary>메모</summary>                 
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                 
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual ICollection<TN_QCT1001> TN_QCT1001List { get; set; }
    }
}