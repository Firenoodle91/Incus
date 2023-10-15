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
    /// 외주재고 마스터 VIEW
    /// </summary>
    [Table("VI_OUT_PROC_STOCK_MASTER")]
    public class VI_OUT_PROC_STOCK_MASTER
    {
        [Key, Column("RowIndex", Order = 0)] public Int64 RowIndex { get; set; }
        [ Column("PO_NO")] public string PoNo { get; set; }
        [Column("PO_DATE")] public Nullable<DateTime> PoDate { get; set; }
        [Column("PO_ID")] public string PoId { get; set; }
        [Column("PO_CUST_CODE")] public string PoCustCode { get; set; }
        [Column("DUE_DATE")] public Nullable<DateTime> DueDate { get; set; }
        [Column("IN_NO")] public string InNo { get; set; }
        [Column("IN_DATE")] public Nullable<DateTime> InDate { get; set; }
        [Column("IN_ID")] public string InId { get; set; }
        [Column("IN_CUST_CODE")] public string InCustCode { get; set; }
    }
}