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
    /// 턴키 입고관리
    /// </summary>
    [Table("TN_PUR2001T")]
    public class TN_PUR2001 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("IN_LOT_NO", Order = 0)] public string InLotNo { get; set; }
        [ForeignKey("TN_PUR2000"), Key, Column("PO_NO",Order = 1)] public string PoNo { get; set; }
        [Column("IN_ID")] public string InId { get; set; }
        [Column("IN_DATE")] public DateTime? InDate { get; set; }
        [Column("IN_QTY"), Required(ErrorMessage = "입고수량은 필수입니다")] public int InQty { get; set; }
        [Column("IN_COST")] public int? InCost { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("IN_SRE")] public string ShipmentYN { get; set; }
        public virtual TN_PUR2000 TN_PUR2000 { get; set; }
    }
}