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
    /// <summary>품목단가이력관리 디테일</summary>	
    [Table("TN_STD1103T")]
    public class TN_STD1103 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1103()
        {
            //TN_STD1103 = new List<TN_STD1103>();
        }

        /*
        ///<summary> 단가관리번호 </summary>                   
        [Key, Column("COST_MANAGE_NO", Order = 1), Required(ErrorMessage = "CostManageNo")] public string CostManageNo { get; set; }
        */

        /// <summary>품목코드</summary>              
        [Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        
        /// <summary>거래처코드</summary>            
        [Key, Column("CUSTOMER_CODE", Order = 1), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }

        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 2)] public int Seq { get; set; }


        ///// <summary>품목코드</summary>              
        //[Column("ITEM_CODE")] public string ItemCode { get; set; }

        ///// <summary>거래처코드</summary>            
        //[Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }

        /// <summary>단가 적용 시작 날짜</summary>                
        [Column("START_DATE")] public DateTime? StartDate { get; set; }

        /// <summary>단가 적용 종료 날짜</summary>                
        [Column("END_DATE")] public DateTime? EndDate { get; set; }

        /// <summary>단가</summary>              
        [Column("ITEM_COST")] public decimal? ItemCost { get; set; }

        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }

        /// <summary>단위</summary>                  
        [Column("UNIT")] public string Unit { get; set; }

        //[NotMapped]
        //public string CustomCustomerCode //단가이력관리 디테일에 사용
        //{
        //    get { return CustomerCode; }
        //}

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual TN_STD1103_MASTER TN_STD1103_MASTER { get; set; }
        public virtual TN_STD1400 TN_STD1400 { get; set; }


    }
}