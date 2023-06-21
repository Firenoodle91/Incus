using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 자재재공재고 디테일 VIEW
    /// </summary>
    [Table("SCM_VI_PUR1100_SCM")]
    public class SCM_VI_PUR1100_SCM
    {

        [Key, Column("PO_NO")] public string PoNo { get; set; }
        [Column("PO_DATE")] public Nullable<DateTime> PoDate { get; set; }
        [Column("DUE_DATE")] public Nullable<DateTime> DueDate { get; set; }

        [Column("MEMO1")] public string Memo { get; set; }
        [Column("CUSTOMER_CONFIRM")] public string CustomerConfirm { get; set; }
        [Column("CUSTOMER_CONFIRM_DATE")] public Nullable<DateTime> CustomerConfirmDate { get; set; }


    }
}