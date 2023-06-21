using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>출하검사대상</summary>	
    [Table("VI_INSP_SHIPMENT_OBJECT")]
    public class VI_INSP_SHIPMENT_OBJECT
    {
        public VI_INSP_SHIPMENT_OBJECT()
        {
            _Check = "N";
        }
        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>최종검사번호</summary>             
        [Column("REV_NO")] public string RevNo { get; set; }
        /// <summary>최종검사번호</summary>             
        [Column("FINAL_INSP_NO")] public string FinalInspNo { get; set; }
        /// <summary>품목코드</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>생산 LOT NO</summary>             
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>이동표번호</summary>             
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>작업지시번호</summary>             
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>검사일</summary>             
        [Column("CHECK_DATE")] public DateTime CheckDate { get; set; }
        /// <summary>검사자</summary>             
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>검사결과</summary>             
        [Column("CHECK_RESULT")] public string CheckResult { get; set; }
        /// <summary>거래처코드</summary>             
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>고객사LOTNO</summary>             
        [Column("CUSTOMER_LOT_NO")] public string CustomerLotNo { get; set; }
        
        [Column("STOCK_QTY")] public decimal? StockQty { get; set; }

        [NotMapped] public string _Check { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}