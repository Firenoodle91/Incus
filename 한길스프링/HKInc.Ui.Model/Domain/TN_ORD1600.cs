using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1600T")]
    public class TN_ORD1600 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1600()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;// HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            ORD1601List = new List<TN_ORD1601>();
        }
        [Key,Column("OUT_NO",Order =0)] public string OutNo { get; set; }
        [Column("CUST_CODE"), Required(ErrorMessage = "거래처는 필수입니다")] public string CustCode { get; set; }
        [Column("ITEM_CODE"), Required(ErrorMessage = "품목코드는 필수입니다")] public string ItemCode { get; set; }
        [Column("ORDER_QTY"), Required(ErrorMessage = "요청수량은 필수입니다")] public decimal OrderQty { get; set; }
        [Column("OUT_QTY")] public decimal OutQty { get; set; }
        [Column("OUT_DATE")] public Nullable<DateTime> OutDate { get; set; }
        [Column("OUT_ID")] public string OutId { get; set; }     
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [NotMapped]
        public string OutState
        {
            get
            {
                string val = "출고대기";
                if (OutQty == 0) { val = "출고대기"; }
                else if (OutQty < OrderQty) { val = "출고중"; }
                else if (OutQty >= OrderQty) { val = "출고완료"; }
                return val;
            }
        }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual ICollection<TN_ORD1601> ORD1601List { get; set; }
    }
}