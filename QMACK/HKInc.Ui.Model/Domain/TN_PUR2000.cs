using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>자재재고조정</summary>	
    [Table("TN_PUR2000T")]
    public class TN_PUR2000 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2000()
        {
        }
     
        /// <summary>재고조정타입</summary>     
        [Key, Column("STOCK_TYPE", Order = 0), Required(ErrorMessage = "StockType is required")] public string StockType { get; set; }
        /// <summary>재고조정일</summary>     
        [Key, Column("INOUT_DATE", Order = 1), Required(ErrorMessage = "InoutDate is required")] public DateTime InoutDate { get; set; }
        /// <summary>LOTNO</summary>     
        [Key,Column("LOTNO", Order = 2), Required(ErrorMessage = "LotNo is required")] public string LotNo { get; set; }
        /// <summary>품목코드</summary>     
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode is required")] public string ItemCode { get; set; }
        /// <summary>재고조정수량</summary>     
        [Column("QTY")] public decimal Qty { get; set; }
        /// <summary>조정자</summary>     
        [Column("STOCK_USER")] public string StockUser { get; set; }       
        /// <summary>메모</summary>     
        [Column("MEMO")] public string Memo { get; set; }        
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}