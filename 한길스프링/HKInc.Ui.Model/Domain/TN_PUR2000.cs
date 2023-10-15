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
    /// 턴키 발주관리
    /// </summary>
    [Table("TN_PUR2000T")]
    public class TN_PUR2000 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2000()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;

            TN_PUR2001List = new List<TN_PUR2001>();
        }
        [Key, Column("PO_NO",Order = 0)] public string PoNo { get; set; }
        [ForeignKey("TN_ORD1100"), Column("ORDER_NO", Order = 1)] public string OrderNo { get; set; }
        [ForeignKey("TN_ORD1100"), Column("SEQ",Order = 2)] public int Seq { get; set; }
        [ForeignKey("TN_ORD1100"), Column("DELIV_SEQ", Order = 3)] public string DelivSeq { get; set; }
        [Column("PO_ID")] public string PoId { get; set; }
        [Column("PO_DATE")] public DateTime? PoDate { get; set; }
        [Column("PO_QTY"), Required(ErrorMessage = "발주수량은 필수입니다")] public int PoQty { get; set; }
        [Column("PO_COST")] public int? PoCost { get; set; }
        [Column("MEMO")] public string Memo { get; set; }


        [NotMapped]
        public int PoRemainQty
        {
            get
            {
                if (TN_PUR2001List.Count == 0) return PoQty;
                else return PoQty - TN_PUR2001List.Sum(p => p.InQty);
            }
        }

        public virtual TN_ORD1100 TN_ORD1100 { get; set; }

        public virtual ICollection<TN_PUR2001> TN_PUR2001List { get; set; }
    }
}