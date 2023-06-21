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
    [Table("TN_ORD1500T")]
    public class TN_ORD1500 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1500()
        {

        }

        /// <summary>판매계획년월</summary>             
        [Key, Column("SALE_PLAN_DATE", Order = 0), Required(ErrorMessage = "SalePlanDate")] public DateTime SalePlanDate { get; set; }
        /// <summary>거래처코드</summary>             
        [ForeignKey("TN_STD1400"), Key, Column("CUSTOMER_CODE", Order = 1), Required(ErrorMessage = "CustomerName")] public string CustomerCode { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 2), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>계획수량</summary>               
        [Column("PLAN_QTY"), Required(ErrorMessage = "PlanQty")] public decimal PlanQty { get; set; }
        /// <summary>단가</summary>               
        [Column("COST")] public decimal? Cost { get; set; }
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
    }
}