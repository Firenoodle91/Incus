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
    /// <summary>품목/거래처단가이력관리</summary>	
    /// <summary>품목단가이력관리 마스터</summary>	
    [Table("TN_STD1105T_MASTER")]
    public class TN_STD1105_MASTER : BaseDomain.MES_BaseDomain
    {

        public TN_STD1105_MASTER()
        {
            TN_STD1105List = new List<TN_STD1105>();
        }

        /// <summary>품목코드</summary>              
        [Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>거래처코드</summary>            
        [Key, Column("CUSTOMER_CODE", Order = 1), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }

        [Column("ITEM_COST")] public decimal ItemCost{ get; set; }

        [NotMapped]
        public DateTime? StartDate { get; set; }
        //public DateTime? StartDate
        //{
        //    get
        //    {
        //        if (TN_STD1105List.Count > 0)
        //        {
        //            return TN_STD1105List.Max(m => m.StartDate);
        //        }
        //        else
        //            return null;
        //    }
        //    set { }
        //}

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual ICollection<TN_STD1105> TN_STD1105List { get; set; }

    }
}