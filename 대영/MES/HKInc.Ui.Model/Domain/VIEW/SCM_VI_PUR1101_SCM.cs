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
    [Table("SCM_VI_PUR1101_SCM")]
    public class SCM_VI_PUR1101_SCM
    {
      
        [Key,Column("PO_NO",Order =0)] public string PoNo { get; set; }
        [Key,Column("PO_SEQ",Order =1)] public Nullable<int> PoSeq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        
        [Column("MEMO1")] public string Memo { get; set; }
        [Column("CUSTOMER_CONFIRM_FLAG")] public string CustomerConfirm { get; set; }
    
      
    //    public virtual TN_PUR1100 TN_PUR1100 { get; set; }
    }
}