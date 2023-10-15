using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_PUR1300T")]
    public class TN_PUR1300 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1300() {

            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
           PUR1301List = new List<TN_PUR1301>();
        }
        [Key,Column("INPUT_NO",Order =0)] public string InputNo { get; set; }
        [Column("INPUT_DATE")] public DateTime InputDate { get; set; }
        [Column("INPUT_ID")] public string InputId { get; set; }
        [Column("REQ_DATE")] public Nullable<DateTime> ReqDate { get; set; }
        [Column("DUE_DATE")] public  Nullable<DateTime> DueDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [Column("REQ_NO")] public string ReqNo { get; set; }
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
       public virtual ICollection<TN_PUR1301> PUR1301List { get; set; }
    }
}