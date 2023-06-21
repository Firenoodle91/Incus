using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1502T")]
    public class TN_PUR1502 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1502()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column("ROW_ID", Order = 0), Required(ErrorMessage = "RowId is required")] public new decimal RowId { get; set; }
        [ForeignKey("TN_PUR1501"), Column("OUT_SEQ", Order = 2)] public int OutSeq { get; set; }
        [ForeignKey("TN_PUR1501"), Column("OUT_NO",Order = 1)] public string OutNo { get; set; }
        [Column("ITEMCODE")] public string ItemCode { get; set; }
        [Column("IN_LOT_NO")] public string InLotNo { get; set; }
        [Column("OUT_LOT_NO")] public string OutLotNo { get; set; }
        [Column("RETURN_IN_QTY")] public Nullable<int> ReturnInQty { get; set; }
        [Column("RETURN_ID")] public string ReturnId { get; set; }
        [Column("RETURN_DATE")] public DateTime? ReturnDate { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_PUR1501 TN_PUR1501 { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}