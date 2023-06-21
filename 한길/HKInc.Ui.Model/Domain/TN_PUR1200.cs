using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1200T")]
    public class TN_PUR1200 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1200()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            _Check = "N";

            TN_PUR1301List = new List<TN_PUR1301>();
        }
        [ForeignKey("TN_PUR1100"),Key, Column("REQ_NO", Order = 0)] public string ReqNo { get; set; }
        [Key, Column("REQ_SEQ",Order = 1)] public int ReqSeq { get; set; }       
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "품목코드는 필수입니다.")] public string ItemCode { get; set; }
        [Column("REQ_QTY"), Required(ErrorMessage = "발주수량은 필수입니다.")] public Nullable<int> ReqQty { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped] public string _Check { get; set; }

        [NotMapped]
        public Decimal Amt
        {
            get {
                decimal amt = 0;
                decimal price = 0;
                try
                {
                    price = Convert.ToDecimal(Temp1);
                }
                catch { price = 0; }
                amt = Convert.ToDecimal(ReqQty) * price;
                return amt;
            }
        }

        public int? RemainReqQty
        {
            get
            {
                if (TN_PUR1301List.Count == 0) return ReqQty;
                else
                {
                    var InputQty = TN_PUR1301List.Sum(p => p.InputQty);
                    return ReqQty - InputQty;
                }
            }
        }

       // [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
        //[ForeignKey("ReqNo")]
        public virtual TN_PUR1100 TN_PUR1100 { get; set; }

        public virtual ICollection<TN_PUR1301> TN_PUR1301List { get; set; }
    }
}