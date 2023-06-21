using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1800T")]
    public class TN_PUR1800 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1800()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            PUR1801List = new List<TN_PUR1801>();
        }
        [Key,Column("IN_NO",Order =0)] public string InNo { get; set; }
        [Column("IN_DATE")] public Nullable<DateTime> InDate { get; set; }
        [Column("IN_ID")] public string InId { get; set; }
        [Column("CUST_CODE")] public string CustCode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("PO_NO")] public string PoNo { get; set; }
        [Column("PO_ID")] public string PoId { get; set; }
        [Column("IN_SRE")] public string InSre { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual ICollection<TN_PUR1801> PUR1801List { get; set; }
    }
}