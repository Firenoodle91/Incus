using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1000T")]
    public class TN_ORD1000 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1000() {
           
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            TN_ORD1002List = new List<TN_ORD1002>();
        }
        [Key,Column("ORDER_NO",Order =0)] public string OrderNo { get; set; }
        [Column("ORDER_TYPE")] public string OrderType { get; set; }
        [Column("ORDER_DATE"), Required(ErrorMessage = "수주일은 필수입니다.")] public DateTime OrderDate { get; set; }
        [Column("CUST_CODE"), Required(ErrorMessage = "거래처는 필수입니다.")] public string CustomerCode { get; set; }
        [Column("CUST_ORDER_NO")] public string CustOrderno { get; set; }
        [Column("CUST_ORDER_ID")] public string CustOrderid { get; set; }
        [Column("PERIOD_DATE"), Required(ErrorMessage = "납기일은 필수입니다.")] public Nullable<DateTime> PeriodDate { get; set; }
        [Column("ORDER_ID")] public string OrderId { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("OS_YN")] public string OsYn { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
       

        public virtual TN_STD1400 TN_STD1400 { get; set; }

        public virtual ICollection<TN_ORD1002> TN_ORD1002List { get; set; }
     
    }
}