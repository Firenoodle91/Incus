using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1501T")]
    public class TN_PUR1501 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1501()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            _Check = "N";
        }
        [Key,Column("OUT_SEQ",Order =1)] public int OutSeq { get; set; }
        [ForeignKey("TN_PUR1500"),Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Column("ITEMCODE")] public string ItemCode { get; set; }
        [Column("OUT_QTY")] public Nullable<decimal> OutQty { get; set; }
        [Column("MAKE_DATE")] public Nullable<DateTime> MakeDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        public virtual TN_PUR1500 TN_PUR1500 { get; set; }
        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        [NotMapped] public string _Check { get; set; }
    }
}