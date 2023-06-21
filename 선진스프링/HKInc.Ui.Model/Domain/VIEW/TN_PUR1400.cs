using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>외주발주 마스터</summary>	
    [Table("TN_PUR1400T")]
    public class TN_PUR1400 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1400()
        {            
            TN_PUR1401List = new List<TN_PUR1401>();
            TN_PUR1500List = new List<TN_PUR1500>();
        }
        /// <summary>발주번호</summary>               
        [Key, Column("PO_NO"), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>발주일</summary>                 
        [Column("PO_DATE"), Required(ErrorMessage = "PoDate")] public DateTime PoDate { get; set; }
        /// <summary>발주자</summary>                 
        [Column("PO_ID"), Required(ErrorMessage = "PoId")] public string PoId { get; set; }
        /// <summary>발주처</summary>                 
        [Column("PO_CUSTOMER_CODE"), Required(ErrorMessage = "PoCustomer")] public string PoCustomerCode { get; set; }
        /// <summary>납기예정일</summary>             
        [Column("DUE_DATE"), Required(ErrorMessage = "DueDate")] public DateTime DueDate { get; set; }
        /// <summary>메모</summary>                   
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>                   
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>                  
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>                  
        [Column("TEMP2")] public string Temp2 { get; set; }
        
        [NotMapped]
        public bool NotInFlag
        {
            get
            {
                if (TN_PUR1401List.Any(p => p.NotInFlag))
                    return true;
                else
                    return false;
            }
        }

        //public virtual TN_STD1400 TN_STD1400 { get; set; }

        public virtual ICollection<TN_PUR1401> TN_PUR1401List { get; set; }
        public virtual ICollection<TN_PUR1500> TN_PUR1500List { get; set; }

    }
}