using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재투입소요량관리</summary>	
    [Table("TN_SRC1000T")]
    public class TN_SRC1000 : BaseDomain.MES_BaseDomain
    {
        public TN_SRC1000()
        {
        }
        /// <summary>작업지시번호</summary>                  
        [ForeignKey("TN_LOT_DTL"), Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>생산LOT_NO</summary>                    
        [ForeignKey("TN_LOT_DTL"), Key, Column("PRODUCT_LOT_NO", Order = 1), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>부모순번</summary>                          
        [ForeignKey("TN_LOT_DTL"), Key, Column("PARENT_SEQ", Order = 2), Required(ErrorMessage = "ParentSeq")] public decimal ParentSeq { get; set; }
        /// <summary>순번</summary>                          
        [Key, Column("SEQ", Order = 3), Required(ErrorMessage = "Seq")] public decimal Seq { get; set; }
        /// <summary>투입 LOT NO</summary>                   
        [Column("SRC_IN_LOT_NO"), Required(ErrorMessage = "SrcInLotNo")] public string SrcInLotNo { get; set; }
        /// <summary>소요량</summary>                        
        [Column("SPEND_QTY"), Required(ErrorMessage = "SpendQty")] public decimal SpendQty { get; set; }
        
        public virtual TN_LOT_DTL TN_LOT_DTL { get; set; }
    }
}