using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>작업지시투입정보</summary>	
    [Table("TN_LOT_MST")]
    public class TN_LOT_MST : BaseDomain.MES_BaseDomain
    {
        public TN_LOT_MST()
        {
            TN_LOT_DTL_List = new List<TN_LOT_DTL>();
        }
        /// <summary>작업지시번호</summary>           
        [Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>생산LOT_NO</summary>             
        [Key, Column("PRODUCT_LOT_NO", Order = 1), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>품번(도번)</summary>             
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }

        public virtual ICollection<TN_LOT_DTL> TN_LOT_DTL_List { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}