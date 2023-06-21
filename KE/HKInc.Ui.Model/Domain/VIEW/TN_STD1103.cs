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
    /// <summary>품목단가이력관리</summary>	
    [Table("TN_STD1103T")]
    public class TN_STD1103 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1103()
        {
        }
        /// <summary>품목코드</summary>              
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>거래처코드</summary>            
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>변경일</summary>                
        [Column("CHANGE_DATE")] public DateTime? ChangeDate { get; set; }
        /// <summary>변경단가</summary>              
        [Column("CHANGE_COST")] public decimal? ChangeCost { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        
        [NotMapped]
        public string CustomCustomerCode //단가이력관리 디테일에 사용
        {
            get { return CustomerCode; }
        }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}