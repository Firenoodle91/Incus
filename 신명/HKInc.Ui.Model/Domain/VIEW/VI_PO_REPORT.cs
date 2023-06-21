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
    /// 자재발주, 외주발주 리포트
    /// 2023-01-11 김진우
    /// </summary>	
    [Table("VI_PO_REPORT")]
    public class VI_PO_REPORT
    {
        public VI_PO_REPORT()
        {

        }
        
        [Key, Column("PO_NO", Order = 0)] public string PoNo { get; set; }
        [Key, Column("PO_SEQ", Order = 1)] public int PoSeq { get; set; }
        [Column("PO_DATE")] public string PoDate { get; set; }
        [Column("DUE_DATE")] public string DueDate { get; set; }
        [Column("ORDER_NO")] public string OrderNo { get; set; }
        [Column("CUSTOMER_NAME")] public string CustomerName { get; set; }
        [Column("SPEC")] public string Spec { get; set; }
        [Column("ORDER_QTY")] public Decimal? OrderQty { get; set; }
        [Column("PO_QTY")] public int? PoQty { get; set; }
        [Column("ITEM_NAME")] public string ItemName { get; set; }
        [Column("PO_COST")] public int? PoCost { get; set; }
        [Column("COST")] public int? Cost { get; set; }

    }
}