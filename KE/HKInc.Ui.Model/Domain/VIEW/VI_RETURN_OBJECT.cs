using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>재입고 가능한 목록</summary>	
    [Table("VI_RETURN_OBJECT")]
    public class VI_RETURN_OBJECT
    {
        public VI_RETURN_OBJECT()
        {
        }
        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>품목코드</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>입고번호</summary>             
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>입고순번</summary>             
        [Column("IN_SEQ")] public int InSeq { get; set; }
        /// <summary>입고 LOT NO</summary>               
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>출고 LOT NO</summary>              
        [Column("OUT_LOT_NO")] public string OutLotNo { get; set; }
        /// <summary>재입고가능량</summary>             
        [Column("RETURN_POSSIBLE_QTY")] public decimal ReturnPossibleQty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}