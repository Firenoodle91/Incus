using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>수입검사대상</summary>	
    [Table("VI_INSP_IN_OBJECT_SCM")]
    public class VI_INSP_IN_OBJECT_SCM
    {
        public VI_INSP_IN_OBJECT_SCM()
        {
            _Check = "N";
        }
        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }

        /// <summary>구분</summary>           
        [Column("DIVISION")] public string Division { get; set; }

        /// <summary>입고번호</summary>             
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>입고순번</summary>             
        [Column("IN_SEQ")] public int InSeq { get; set; }
        /// <summary>입고일</summary>             
        [Column("IN_DATE")] public DateTime InDate { get; set; }
        /// <summary>품목코드</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>입고량</summary>             
        [Column("IN_QTY")] public decimal InQty { get; set; }
        /// <summary>입고자</summary>             
        [Column("IN_ID")] public string InId { get; set; }
        /// <summary>메모</summary>               
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>입고 lOT NO</summary>               
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>생산 LOT NO</summary>              
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>납품처 LOT NO</summary>              
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerLotNo { get; set; }
        
        [NotMapped] public string _Check { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}