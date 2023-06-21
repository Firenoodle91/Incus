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
    /// 외주품 발주관리 디테일
    /// </summary>
    [Table("TN_PUR2101T")]
    public class TN_PUR2101 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2101()
        {
            TN_PUR2201List = new List<TN_PUR2201>();
        }
        /// <summary>
        /// 발주번호
        /// </summary>
        [Key, ForeignKey("TN_PUR2100"), Column("PO_NO", Order = 0), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>
        /// 발주순번
        /// </summary>
        [Key, Column("PO_SEQ", Order = 1), Required(ErrorMessage = "PoSeq")] public int? PoSeq { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        [ForeignKey("TN_STD1100"),  Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>
        /// 발주수량
        /// </summary>
        [Column("PO_QTY"), Required(ErrorMessage = "PoQty")] public decimal PoQty { get; set; }
        /// <summary>
        /// 비용
        /// </summary>
        [Column("PO_COST")] public decimal? PoCost { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>입고상태</summary>                  
        [NotMapped]
        public string InConfirmState //입고상태
        {
            // 메인코드 : Z001, 01 : 대기, 02 : 진행중, 03 : 완료
            get
            {
                if (TN_PUR2201List.Count == 0)
                    return "01";
                else
                {
                    if (TN_PUR2201List.Any(p => p.InConfirmFlag == "Y"))
                        return "03";
                    else if (TN_PUR2201List.Sum(p => p.InQty) >= PoQty)
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
                if (TN_PUR2201List.Count == 0) return PoQty;
                else
                {
                    if (TN_PUR2201List.Any(p => p.InConfirmFlag == "Y"))
                        return 0;
                    else
                        return PoQty - TN_PUR2201List.Sum(p => p.InQty);
                }
            }
        }

        [NotMapped]
        public Decimal Amt
        {
            get
            {
                decimal amt = 0;
                decimal price = 0;
                try
                {
                    price = Convert.ToDecimal(PoCost);
                }
                catch { price = 0; }
                amt = Convert.ToDecimal(PoQty) * price;
                return amt;
            }
        }

        public virtual TN_PUR2100 TN_PUR2100 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual ICollection<TN_PUR2201> TN_PUR2201List { get; set; }
    }
}
