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
   /// <summary>제품기타출고관리 마스터</summary>	
   [Table("TN_ORD1400T")]
    public class TN_ORD1400 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1400()
        {
            TN_ORD1401List = new List<TN_ORD1401>();
        }
        /// <summary>출고번호</summary>              
        [Key, Column("OUT_NO"), Required(ErrorMessage = "OutNo")] public string OutNo { get; set; }
        /// <summary>품번(도번)</summary>            
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>            
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>요청량</summary>                
        [Column("REQ_QTY"), Required(ErrorMessage = "ReqQty")] public decimal ReqQty { get; set; }
        /// <summary>출고일</summary>                
        [Column("OUT_DATE"), Required(ErrorMessage = "OutDate")] public DateTime OutDate { get; set; }
        /// <summary>출고자</summary>                
        [Column("OUT_ID"), Required(ErrorMessage = "OutId")] public string OutId { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                  
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                 
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                 
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_ORD1401> TN_ORD1401List { get; set; }
    }
}