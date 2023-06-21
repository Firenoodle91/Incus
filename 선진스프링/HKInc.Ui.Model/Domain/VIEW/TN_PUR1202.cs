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
    /// <summary>구매입고현황 입고확정관리</summary>	
    [Table("TN_PUR1202T")]
    public class TN_PUR1202 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1202()
        {
        }
        /// <summary>일자</summary>             
        [Key, Column("DATE", Order = 0), Required(ErrorMessage = "Date")] public DateTime Date { get; set; }
        /// <summary>품번(도번)</summary>             
        [Key, Column("ITEM_CODE", Order = 1), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>             
        [Key, Column("CUSTOMER_CODE", Order = 2), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>확정여부</summary>                   
        [Column("CONFIRM_FLAG")] public string ConfirmFlag { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
    }
}