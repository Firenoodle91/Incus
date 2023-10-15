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
    /// <summary>자재발주 마스터</summary>	
    [Table("TN_PUR1100T")]
    public class TN_PUR1100 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR1100()
        {
            TN_PUR1101List = new List<TN_PUR1101>();
           
        }
        /// <summary>발주번호</summary>          
        [Key, Column("PO_NO"), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>발주일</summary>            
        [Column("PO_DATE"), Required(ErrorMessage = "PoDate")] public DateTime PoDate { get; set; }
        /// <summary>발주자</summary>            
        [Column("PO_ID"), Required(ErrorMessage = "PoId")] public string PoId { get; set; }
        /// <summary>발주처</summary>            
        [ForeignKey("TN_STD1400"), Column("PO_CUSTOMER_CODE"), Required(ErrorMessage = "PoCustomerCode")] public string PoCustomerCode { get; set; }
        /// <summary>납기예정일</summary>        
        [Column("DUE_DATE")] public DateTime? DueDate { get; set; }
        /// <summary>생산제품</summary>      
        [Column("PROD_ITEM")] public string ProdItem { get; set; }
        /// <summary>발주확정여부</summary>      
        [Column("PO_FLAG"), Required(ErrorMessage = "PoFlag")] public string PoFlag { get; set; }
        /// <summary>메모</summary>              
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>협력사메모</summary>                
        [Column("MEMO1")] public string Memo1 { get; set; }
        /// <summary>임시</summary>              
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>             
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>             
        [Column("TEMP2")] public string Temp2 { get; set; }
        /// <summary>거래처확인여부</summary>             
        [Column("CUSTOMER_CONFIRM")] public string CustomerConfirm { get; set; }
        /// <summary>거래처확인일자</summary>             
        [Column("CUSTOMER_CONFIRM_DATE")] public DateTime? CustomerConfirmDate { get; set; }

        /// <summary>입고상태</summary>                  
        [NotMapped]
        public string InConfirmState
        {
            // 메인코드 : Z001, 01 : 대기, 02 : 진행중, 03 : 완료
            get
            {
                if (TN_PUR1101List.Count == 0)
                    return "01";
                else
                {
                    var count = TN_PUR1101List.Count;
                    if (TN_PUR1101List.Where(p => p.InConfirmState == "03").Count() == count)
                        return "03";
                    else if (TN_PUR1101List.Where(p => p.InConfirmState == "01").Count() == count)
                        return "01";
                    else
                        return "02";
                }
            }
        }

        /// <summary>입고상태_SCM</summary>            
        [NotMapped]
        public string InConfirmState_Scm
        {
            // 메인코드 : Z001, 01 : 대기, 02 : 진행중, 03 : 완료
            get
            {
                if (TN_PUR1101List.Count == 0)
                    return "01";
                else
                {
                    var count = TN_PUR1101List.Count;
                    if (TN_PUR1101List.Where(p => p.InConfirmState_Scm == "03").Count() == count)
                        return "03";
                    else if (TN_PUR1101List.Where(p => p.InConfirmState_Scm == "01").Count() == count)
                        return "01";
                    else
                        return "02";
                }
            }
        }

        public virtual TN_STD1400 TN_STD1400 { get; set; }

        public virtual ICollection<TN_PUR1101> TN_PUR1101List { get; set; }
        public virtual ICollection<TN_PUR1200> TN_PUR1200List { get; set; }
       

    }
}