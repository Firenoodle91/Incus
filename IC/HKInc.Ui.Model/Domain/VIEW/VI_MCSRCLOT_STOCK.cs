
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
    /// 자재재공재고 디테일 VIEW
    /// </summary>
    [Table("VI_MCSRCLOT_STOCK")]
    public class VI_MCSRCLOT_STOCK
    {
        [Key, Column("RowIndex", Order = 0)] public Int64 RowIndex { get; set; }
     
        [Column("SRC_ITEM_CODE")] public string SrcItemCode { get; set; }
        [Column("SRC_ITEM_NAME")] public string SrcItemName { get; set; }
        [Column("STOCK_QTY")] public Nullable<decimal> OutQty { get; set; }

        [Column("SRC_LOT_NO")] public string SrcLotNo { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("OUT_DATE")] public DateTime OutDate { get; set; }

        //[ForeignKey("SrcItemCode")]
        //public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}