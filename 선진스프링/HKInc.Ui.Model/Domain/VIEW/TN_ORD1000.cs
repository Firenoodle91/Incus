using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
   /// <summary>수주관리 마스터</summary>	
   [Table("TN_ORD1000T")]
    public class TN_ORD1000 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1000()
        {
            TN_ORD1001List = new List<TN_ORD1001>();
        }
        /// <summary>수주번호</summary>             
        [Key, Column("ORDER_NO"), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }
        /// <summary>수주구분</summary>                 
        [Column("ORDER_TYPE")] public string OrderType { get; set; }
        /// <summary>수주일</summary>               
        [Column("ORDER_DATE"), Required(ErrorMessage = "OrderDate")] public DateTime OrderDate { get; set; }
        /// <summary>수주처</summary>               
        [ForeignKey("TN_STD1400"), Column("ORDER_CUSTOMER_CODE"), Required(ErrorMessage = "OrderCustomerCode")] public string OrderCustomerCode { get; set; }
        /// <summary>고객사담당자</summary>         
        [Column("ORDER_MANAGER_NAME")] public string OrderManagerName { get; set; }
        /// <summary>납기일</summary>               
        [Column("ORDER_DUE_DATE"), Required(ErrorMessage = "DueDate")] public DateTime OrderDueDate { get; set; }
        /// <summary>영업담당자</summary>           
        [Column("ORDER_ID")] public string OrderId { get; set; }
        /// <summary>메모</summary>                 
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                 
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                
        [Column("TEMP2")]	public string Temp2 { get; set; }

        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_ORD1001> TN_ORD1001List { get; set; }
    }
}