using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>SCM자재입고 마스터</summary>	
    [Table("SCM_VI_PUR1200_SCM")]
    public class SCM_VI_PUR1200_SCM
    {


        [Key,Column("IN_NO")] public string InNo { get; set; }
        [Column("PO_NO")] public string PoNo { get; set; }
        [Column("IN_DATE")] public Nullable<DateTime> InDate { get; set; }
        [Column("IN_ID")] public string InId { get; set; }
        [Column("IN_CUSTOMER_CODE")] public string InCustomercode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("MEMO1")] public string Memo1 { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        [Column("INSP_YN")] public string InspYn { get; set; }
        [Column("ORDER_CUSTOMER_CODE")] public string OrderCustomercode { get; set; }
    

    }
}