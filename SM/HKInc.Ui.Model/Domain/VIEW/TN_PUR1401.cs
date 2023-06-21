using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>외주발주 디테일</summary>	
    [Table("TN_PUR1401T")]
    public class TN_PUR1401 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1401()
        {
            TN_PUR1501List = new List<TN_PUR1501>();
        }
        /// <summary>발주번호</summary>                 
        [ForeignKey("TN_PUR1400"), Key, Column("PO_NO", Order = 0), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>발주순번</summary>                 
        [Key, Column("PO_SEQ", Order = 1), Required(ErrorMessage = "PoSeq")] public int PoSeq { get; set; }
        /// <summary>작업지시번호</summary>             
        [ForeignKey("TN_MPS1200"), Column("WORK_NO", Order = 2), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>                 
        [ForeignKey("TN_MPS1200"), Column("PROCESS_CODE", Order = 3), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>                 
        [ForeignKey("TN_MPS1200"), Column("PROCESS_SEQ", Order = 4), Required(ErrorMessage = "ProcessSeq")] public int ProcessSeq { get; set; }
        /// <summary>생산 LOT NO</summary>              
        [Column("PRODUCT_LOT_NO"), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>이동표번호</summary>              
        [Column("ITEM_MOVE_NO"), Required(ErrorMessage = "ItemMoveNo")] public string ItemMoveNo { get; set; }
        /// <summary>품번(도번)</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>발주량</summary>                   
        [Column("PO_QTY"), Required(ErrorMessage = "PoQty")] public decimal PoQty { get; set; }
        /// <summary>발주단가</summary>                 
        [Column("PO_COST")] public decimal? PoCost { get; set; }
        /// <summary>메모</summary>                     
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                     
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                    
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                    
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped] public decimal NotInQty
        {
            get
            {
                if (TN_PUR1501List.Count == 0)
                    return PoQty;
                else
                {
                    //var badQty = TN_PUR1501List.Sum(p => p.BadQty) == null ? 0 : (decimal)TN_PUR1501List.Sum(p => p.BadQty);
                    return PoQty - TN_PUR1501List.Sum(p => p.InQty);// - badQty;
                }
            }
        }

        [NotMapped]
        public bool NotInFlag
        {
            get
            {
                if (TN_PUR1501List.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_PUR1400 TN_PUR1400 { get; set; }
        public virtual TN_MPS1200 TN_MPS1200 { get; set; }
        public virtual ICollection<TN_PUR1501> TN_PUR1501List { get; set; }
    }
}