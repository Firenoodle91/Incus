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
    [Table("TN_ORD1601T")]
    public class TN_ORD1601 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1601()
        {

        }

        /// <summary>
        /// 수주번호
        /// </summary>
        [Key, Column("ORDER_NO", Order = 0), Required(ErrorMessage = "OrderNo")] public string OrderNo { get; set; }

        /// <summary>
        /// 수주순번
        /// </summary>
        [Key, Column("OUT_SEQ", Order = 1)] public int OutSeq { get; set; }

        [Key, Column("DELIV_NO", Order = 2)] public string DelivNo { get; set; }

        /// <summary>
        /// 출고일
        /// </summary>
        [Column("OUT_DATE")] public DateTime OutDate { get; set; }

        /// <summary>
        /// 납품수량
        /// </summary>               
        [Column("OUT_QTY")] public decimal OutQty { get; set; }

        /// <summary>
        /// 제품단가
        /// </summary>               
        [Column("ITEM_COST")] public decimal ItemCost { get; set; }

        public decimal DueCost
        {
            get
            {
                var outqty = OutQty;
                var itemcost = ItemCost;

                return outqty * itemcost;
            }

        }

    }
}