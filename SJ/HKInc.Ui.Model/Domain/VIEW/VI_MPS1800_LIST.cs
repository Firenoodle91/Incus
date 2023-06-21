using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>완제품입고관리 목록</summary>	
    [Table("VI_MPS1800_LIST")]
    public class VI_MPS1800_LIST
    {
        public VI_MPS1800_LIST()
        {

        }
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>작업지시번호</summary>           
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>생산 LOT_NO</summary>            
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>고객 LOT NO</summary>               
        [Column("TEMP1")] public string CustomerLotNo { get; set; }
        /// <summary>창고코드</summary>             
        [Column("WH_CODE")] public string WhCode { get; set; }
        /// <summary>위치코드</summary>             
        [Column("POSITION_CODE")] public string PositionCode { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [ForeignKey("TN_STD1400"), Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>재고수량</summary>               
        [Column("STOCK_QTY")] public decimal StockQty { get; set; }
        /// <summary>작업지시일</summary>               
        [Column("WORK_DATE")] public DateTime WorkDate { get; set; }


        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }
    }
}