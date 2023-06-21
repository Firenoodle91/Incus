using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>반제품출고관리 디테일</summary>	
    [Table("TN_BAN1101T")]
    public class TN_BAN1101 : BaseDomain.MES_BaseDomain
    {
        public TN_BAN1101()
        {
            TN_BAN1102List = new List<TN_BAN1102>();
        }
        /// <summary>출고번호</summary>                
        [ForeignKey("TN_BAN1100"), Key, Column("OUT_NO", Order = 0), Required(ErrorMessage = "OutNo")] public string OutNo { get; set; }
        /// <summary>출고순번</summary>                
        [Key, Column("OUT_SEQ", Order = 1), Required(ErrorMessage = "OutSeq")] public int OutSeq { get; set; }
        /// <summary>입고번호</summary>                
        [ForeignKey("TN_BAN1001"), Column("IN_NO", Order = 2), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>입고순번</summary>                
        [ForeignKey("TN_BAN1001"), Column("IN_SEQ", Order = 3), Required(ErrorMessage = "InSeq")] public int InSeq { get; set; }
        /// <summary>품번(도번)</summary>              
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>출고량</summary>                  
        [Column("OUT_QTY"), Required(ErrorMessage = "OutQty")] public decimal OutQty { get; set; }
        /// <summary>출고 LOT NO</summary>             
        [Column("OUT_LOT_NO"), Required(ErrorMessage = "OutLotNo")] public string OutLotNo { get; set; }
        /// <summary>
        /// 20220207 오세완 차장
        /// 입고 LOT NO, 케이즈이노텍과 동일하게 inlotno를 그냥 null처리
        /// </summary>             
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        /// <summary>메모</summary>                    
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                    
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                   
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                   
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped] public decimal? CustomStockQty { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_BAN1100 TN_BAN1100 { get; set; }
        public virtual TN_BAN1001 TN_BAN1001 { get; set; }
        public virtual ICollection<TN_BAN1102> TN_BAN1102List { get; set; }
    }
}