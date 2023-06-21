using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1601T")]
    public class TN_ORD1601 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1601()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [ForeignKey("TN_ORD1600"),Key, Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Key, Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("LOT_NO"), Required(ErrorMessage = "출고 LOT NO는 필수입니다")] public string LotNo { get; set; }
        [Column("OUT_QTY"), Required(ErrorMessage = "출고수량은 필수입니다")] public Nullable<int> OutQty { get; set; }
        [Column("OUT_DATE"), Required(ErrorMessage = "출고일은 필수입니다")] public Nullable<DateTime> OutDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        public virtual TN_ORD1600 TN_ORD1600 { get; set; }
    }
}