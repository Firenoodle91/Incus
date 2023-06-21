using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>반제품입고관리 디테일</summary>	
    [Table("TN_BAN1001T")]
    public class TN_BAN1001 : BaseDomain.MES_BaseDomain
    {
        public TN_BAN1001()
        {
            TN_BAN1101List = new List<TN_BAN1101>();
        }
        /// <summary>입고번호</summary>                  
        [ForeignKey("TN_BAN1000"), Key, Column("IN_NO", Order = 0), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고순번</summary>                  
        [Key, Column("IN_SEQ", Order = 1), Required(ErrorMessage = "InSeq")] public int InSeq { get; set; }
        /// <summary>품번(도번)</summary>                
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>입고량</summary>                    
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>입고 LOT NO</summary>               
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>반제품 생산 LOT NO</summary>        
        [Column("BAN_PRODUCT_LOT_NO"), Required(ErrorMessage = "BanProductLotNo")] public string BanProductLotNo { get; set; }
        /// <summary>입고창고</summary>                  
        [Column("IN_WH_CODE")] public string InWhCode { get; set; }
        /// <summary>입고위치</summary>                  
        [Column("IN_WH_POSITION")] public string InWhPosition { get; set; }
        /// <summary>라벨수</summary>                 
        [Column("PRINT_QTY")] public int? PrintQty { get; set; }
        /// <summary>메모</summary>                      
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                      
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                     
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                     
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_BAN1000 TN_BAN1000 { get; set; }
        public virtual ICollection<TN_BAN1101> TN_BAN1101List { get; set; }
    }
}