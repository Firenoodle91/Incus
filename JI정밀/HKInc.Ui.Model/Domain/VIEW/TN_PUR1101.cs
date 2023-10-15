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
    /// <summary>자재발주 디테일</summary>	
    [Table("TN_PUR1101T")]
    public class TN_PUR1101 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1101()
        {
            TN_PUR1201List = new List<TN_PUR1201>();
        }
        /// <summary>발주번호</summary>               
        [ForeignKey("TN_PUR1100"), Key, Column("PO_NO", Order = 0), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>발주순번</summary>               
        [Key, Column("PO_SEQ", Order = 1), Required(ErrorMessage = "PoSeq")] public int PoSeq { get; set; }
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
        /// <summary>입고상태</summary>                  
        [NotMapped]
        public string InConfirmState //입고상태
        {
            // 메인코드 : Z001, 01 : 대기, 02 : 진행중, 03 : 완료
            get
            {
                if (TN_PUR1201List.Count == 0)
                    return "01";
                else
                {
                    if (TN_PUR1201List.Any(p => p.InConfirmFlag == "Y"))
                        return "03";
                    else if (TN_PUR1201List.Sum(p => p.InQty) >= PoQty)
                        return "03";
                    else
                        return "02";
                }
            }
        }

        /// <summary> 미입고량 </summary>
        [NotMapped]
        public decimal PoRemainQty
        {
            get
            {
                if (TN_PUR1201List.Count == 0) return PoQty;
                else
                {
                    if (TN_PUR1201List.Any(p => p.InConfirmFlag == "Y"))
                        return 0;
                    else
                        return PoQty - TN_PUR1201List.Sum(p => p.InQty);
                }
            }
        }

        public virtual TN_PUR1100 TN_PUR1100 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_PUR1201> TN_PUR1201List { get; set; }
    }
}