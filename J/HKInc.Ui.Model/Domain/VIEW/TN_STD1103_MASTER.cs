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
    /// <summary>품목단가이력관리 마스터</summary>	
    [Table("TN_STD1103T_MASTER")]
    public class TN_STD1103_MASTER : BaseDomain.MES_BaseDomain
    {

        public TN_STD1103_MASTER()
        {
            TN_STD1103List = new List<TN_STD1103>();
        }

        /*
        ///<summary> 단가관리번호 </summary>                   
        [Key, Column("COST_MANAGE_NO", Order = 1), Required(ErrorMessage = "CostManageNo")] public string CostManageNo { get; set; }
        */

        /// <summary>품목코드</summary>              
        [Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        
        /// <summary>거래처코드</summary>            ㅅ
        [Key, Column("CUSTOMER_CODE", Order = 1), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }

        ///// <summary>순번</summary>                  
        //[Key, Column("SEQ", Order = 2), Required(ErrorMessage = "Seq")] public int Seq { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual ICollection<TN_STD1103> TN_STD1103List { get; set; }

    }
}