using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재디테일팝업</summary>	
    [Table("VI_PUR1101_SCM")]
    public class VI_PUR1101_SCM
    {
        public VI_PUR1101_SCM()
        {
            _Check = "N";
        }
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>발주번호</summary>           
        [Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>발주순번</summary>           
        [Column("PO_SEQ")] public int PoSeq { get; set; }
        /// <summary>입고번호</summary>           
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>입고순번</summary>           
        [Column("IN_SEQ")] public int InSeq { get; set; }
        /// <summary>품목코드</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>발주수량</summary>             
        [Column("PO_QTY")] public decimal PoQty { get; set; }
        /// <summary>입고수량</summary>             
        [Column("IN_QTY")] public decimal? InQty { get; set; }
        /// <summary>미입고수량</summary>             
        [Column("PO_REMAIN_QTY")] public decimal PoRemainQty { get; set; }
        /// <summary>입고상태</summary>             
        [Column("IN_CONFIRM_STATE")] public string InConfirmState { get; set; }
        /// <summary>발주단가</summary>             
        [Column("PO_COST")] public decimal? PoCost { get; set; }
        /// <summary>입고LOTNO</summary>             
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>납품처LOTNO</summary>               
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerLotNo { get; set; }
        /// <summary>협력사메모</summary>               
        [Column("MEMO1")] public string Memo1 { get; set; }        

        [NotMapped] public string _Check { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }

    }
}