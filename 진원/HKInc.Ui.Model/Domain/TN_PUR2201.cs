using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 외주품 입고관리 디테일
    /// </summary>
    [Table("TN_PUR2201T")]
    public class TN_PUR2201 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2201()
        {
            InInspectionState = "";
        }
        /// <summary>
        /// 입고번호
        /// </summary>
        [Key, ForeignKey("TN_PUR2200"), Column("IN_NO", Order = 0)] public string InNo { get; set; }
        /// <summary>
        /// 입고순번
        /// </summary>
        [Key, Column("IN_SEQ", Order = 1)] public int InSeq { get; set; }
        /// <summary>
        /// 발주번호
        /// </summary>
        [Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>
        /// 발주순번
        /// </summary>
        [Column("PO_SEQ")] public int? PoSeq { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>
        /// 입고량
        /// </summary>
        [Column("IN_QTY"), Required(ErrorMessage = "InQty")] public decimal InQty { get; set; }
        /// <summary>
        /// 금액
        /// </summary>
        [Column("IN_COST")] public decimal? InCost { get; set; }
        /// <summary>
        /// 입고LOT
        /// </summary>
        [Column("IN_LOT_NO"), Required(ErrorMessage = "InLotNo")] public string InLotNo { get; set; }
        /// <summary>
        /// 고객사LOT
        /// </summary>
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerLotNo { get; set; }
        /// <summary>
        /// 입고창고
        /// </summary>
        [Column("IN_WH_CODE")] public string InWhCode { get; set; }
        /// <summary>
        /// 입고위치
        /// </summary>
        [Column("IN_WH_POSITION")] public string InWhPosition { get; set; }
        /// <summary>
        /// 출력라벨 수
        /// </summary>
        [Column("PRINT_QTY")] public int? PrintQty { get; set; }
        /// <summary>
        /// 입고확정여부
        /// </summary>
        [Column("IN_CONFIRM_FLAG")] public string InConfirmFlag { get; set; }
        /// <summary>
        /// 입고확정취소여부
        /// </summary>
        [Column("NOT_IN_CONFIRM_FLAG")] public string NotInconfirmFlag { get; set; }
        /// <summary>
        /// 수입검사여부
        /// </summary>
        [NotMapped] public string InInspectionState { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        //public virtual TN_PUR2101 TN_PUR2101 { get; set; }
        public virtual TN_PUR2200 TN_PUR2200 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}
