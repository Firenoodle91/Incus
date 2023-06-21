using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재반품상세내역</summary>	
    [Table("VI_PUR1305")]
    public class VI_PUR1305
    {
        public VI_PUR1305()
        {
            _Check = "N";
        }

        /// <summary></summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
        /// <summary>수입검사여부</summary>           
        [Column("STOCK_INSP_FLAG")] public string StockInspFlag { get; set; }
        /// <summary>입고확정여부</summary>           
        [Column("IN_CONFIRM_FLAG")] public string InConfirmFlag { get; set; }
        /// <summary>발주번호</summary>           
        [Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>발주순번</summary>           
        [Column("PO_SEQ")] public int PoSeq { get; set; }
        /// <summary>입고번호</summary>           
        [Column("IN_NO")] public string InNo { get; set; }
        /// <summary>입고순번</summary>           
        [Column("IN_SEQ")] public int InSeq { get; set; }
        /// <summary>품목코드</summary>           
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>품명</summary>           
        [Column("ITEM_NAME")] public string ItemName { get; set; }
        /// <summary>품목명</summary>           
        [Column("ITEM_NAME1")] public string ItemName1 { get; set; }
        /// <summary>대분류</summary>           
        [Column("TOP_CATEGORY")] public string TopCategory { get; set; }
        /// <summary>중분류</summary>           
        [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
        /// <summary>소분류</summary>           
        [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
        /// <summary>규격</summary>           
        [Column("COMBINESPEC")] public string CombineSpec { get; set; }
        /// <summary>단위</summary>           
        [Column("UNIT")] public string Unit { get; set; }
        /// <summary>입고수량</summary>           
        [Column("IN_QTY")] public decimal InQty { get; set; }
        /// <summary>입고단가</summary>           
        [Column("IN_COST")] public decimal InCost { get; set; }
        /// <summary>입고lotno</summary>           
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>거래처lotno</summary>           
        [Column("IN_CUSTOMER_LOT_NO")] public string InCustomerLotNo { get; set; }
        /// <summary>수입검사번호</summary>           
        [Column("INSP_NO")] public string InspNo { get; set; }
        /// <summary>합격수량</summary>           
        [Column("OK_QTY")] public decimal OkQty { get; set; }
        /// <summary>불량수량</summary>           
        [Column("NG_QTY")] public decimal NgQty { get; set; }
        /// <summary>수입검사상태</summary>           
        [Column("IN_INSPECTIONSTATE")] public string InInspectionState { get; set; }
        /// <summary>입고창고</summary>           
        [Column("IN_WH_CODE")] public string InWhCode { get; set; }
        /// <summary>입고위치</summary>           
        [Column("IN_WH_POSITION")] public string InWhPosition { get; set; }
        /// <summary>메모</summary>           
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>협력사메모</summary>           
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>반품가능수량</summary>           
        [Column("STOCK_QTY")] public decimal StockQty { get; set; }
        

        [NotMapped]
        public string _Check { get; set; }

    }
}