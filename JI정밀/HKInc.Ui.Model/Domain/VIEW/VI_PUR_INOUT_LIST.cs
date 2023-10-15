using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>JI버전 자재재고</summary>	
    [Table("VI_PUR_INOUT_LIST")]
    public class VI_PUR_INOUT_LIST
    {
        public VI_PUR_INOUT_LIST()
        {

        }

        [Key, Column("RowIndex", Order = 0)] public Int64 RowIndex { get; set; }

        [Column("DIVISION")] public string Division { get; set; }
        [Column("CREATE_TIME")] public DateTime CreateTime { get; set; }
        [Column("INOUT_DATE")] public DateTime InOutDate { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("INOUT_LOT_NO")] public string InOutLotNo { get; set; }
        [Column("IN_QTY")] public decimal InQty { get; set; }  
        [Column("OUT_QTY")] public decimal OutQty { get; set; }
        [Column("INOUT_ID")] public string InOutId { get; set; }
        [Column("IN_WH_CODE")] public string InWhCode { get; set; }
        [Column("IN_WH_POSITION")] public string InWhPosition { get; set; }
        [Column("UPDATE_TIME")] public DateTime UpdateTime { get; set; }

    }
}