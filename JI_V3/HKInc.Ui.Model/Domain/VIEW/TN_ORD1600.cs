using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>매출현황 등록 마스터</summary>	
    [Table("TN_ORD1600T")]
    public class TN_ORD1600 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1600()
        {

        }

        /// <summary>
        /// 수주번호
        /// </summary>
        [Key, Column("ORDER_NO", Order = 0), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }

        /// <summary>
        /// 거래처코드
        /// </summary>
        [Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary>
        [Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>
        /// 납품예정수량
        /// </summary>               
        [Column("PLAN_QTY")] public int PlanQty { get; set; }

        /// <summary>
        /// 수주단가
        /// </summary>               
        [Column("ORDER_ITEM_COST")] public int OrderItemCost { get; set; }




        
        [Column("DELIV_QTY"), Required(ErrorMessage = "DelivQty")] public decimal DelivQty { get; set; }

    }
}