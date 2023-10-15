using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재 반품관리</summary>	
    [Table("TN_PUR1303T")]
    public class TN_PUR1303 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1303()
        {
        }

        /// <summary>고유ID</summary>     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("ROW_ID", Order = 0), Required(ErrorMessage = "RowId is required")] public new decimal RowId { get; set; }
        /// <summary>출고번호</summary>     
        [ForeignKey("TN_PUR1301"), Column("OUT_NO", Order = 1)] public string OutNo { get; set; }
        /// <summary>출고순번</summary>     
        [ForeignKey("TN_PUR1301"), Column("OUT_SEQ", Order = 2)] public int OutSeq { get; set; }
        /// <summary>품번(도번)</summary>     
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>입고 LOT NO</summary>     
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>출고 LOT NO</summary>     
        [Column("OUT_LOT_NO")] public string OutLotNo { get; set; }
        /// <summary>반품량</summary>     
        [Column("DISPOSAL_QTY")] public decimal? DisposalQty { get; set; }
        /// <summary>반품자</summary>     
        [Column("DISPOSAL_ID")] public string DisposalId { get; set; }
        /// <summary>반품일</summary>     
        [Column("DISPOSAL_DATE")] public DateTime? DisposalDate { get; set; }
        /// <summary>반품창고</summary>     
        [Column("DISPOSAL_WH_CODE")] public string DisposalWhCode { get; set; }
        /// <summary>반품위치</summary>     
        [Column("DISPOSAL_WH_POSITION")] public string DisposalWhPosition { get; set; }
        /// <summary>메모</summary>     
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>     
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>     
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>     
        [Column("TEMP2")] public string Temp2 { get; set; }
               
        [NotMapped] public decimal? ReturnPossibleQty { get; set; }

        public virtual TN_PUR1301 TN_PUR1301 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}