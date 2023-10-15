using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_BAN1001T")]
    public class TN_BAN1001 : BaseDomain.MES_BaseDomain
    {
        public TN_BAN1001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
            _Check = "N";
        }
        [ForeignKey("TN_BAN1000"),Key,Column("INPUT_NO",Order =0)] public string InputNo { get; set; }
        [Key,Column("INPUT_SEQ",Order =1)] public Nullable<int> InputSeq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("INPUT_QTY")] public Nullable<int> InputQty { get; set; }
        [Column("IN_YN")] public string InYn { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        public virtual TN_BAN1000 TN_BAN1000{ get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        [NotMapped] public string _Check { get; set; }
    }
}