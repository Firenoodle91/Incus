using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>완제품입고관리</summary>	
    [Table("TN_MPS1300T")]
    public class TN_MPS1300 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1300()
        {
        }
        /// <summary>작업지시번호</summary>           
        [ForeignKey("TN_MPS1201"), Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>               
        [ForeignKey("TN_MPS1201"), Key, Column("PROCESS_CODE", Order = 1), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>               
        [ForeignKey("TN_MPS1201"), Key, Column("PROCESS_SEQ", Order = 2), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>생산 LOT_NO</summary>            
        [ForeignKey("TN_MPS1201"), Key, Column("PRODUCT_LOT_NO", Order = 3), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>창고코드</summary>             
        [Key, Column("WH_CODE", Order = 4), Required(ErrorMessage = "WhCode")] public string WhCode { get; set; }
        /// <summary>위치코드</summary>             
        [Key, Column("POSITION_CODE", Order = 5), Required(ErrorMessage = "PositionCode")] public string PositionCode { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>입고수량</summary>               
        [Column("IN_QTY")] public decimal InQty { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MPS1201 TN_MPS1201 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}