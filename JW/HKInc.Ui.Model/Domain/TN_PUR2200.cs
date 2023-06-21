using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 외주품입고관리 마스터
    /// </summary>
    [Table("TN_PUR2200T")]
    public class TN_PUR2200 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2200()
        {
            TN_PUR2201List = new List<TN_PUR2201>();
            InConfirmFlag = "";
        }
        /// <summary>
        /// 입고번호
        /// </summary>
        [Key, Column("IN_NO"), Required(ErrorMessage = "InNo")] public string InNo { get; set; }
        /// <summary>
        /// 발주번호
        /// </summary>
        [ForeignKey("TN_PUR2100"), Column("PO_NO")] public string PoNo { get; set; }
        /// <summary>
        /// 입고일
        /// </summary>
        [Column("IN_DATE"), Required(ErrorMessage = "InDate")] public DateTime InDate { get; set; }
        /// <summary>
        /// 입고자
        /// </summary>
        [Column("IN_ID"), Required(ErrorMessage = "InId")] public string InId { get; set; }
        /// <summary>
        /// 입고처
        /// </summary>
        [Column("IN_CUSTOMER_CODE"), Required(ErrorMessage = "InCustomerCode")] public string InCustomerCode { get; set; }
        /// <summary>
        /// 입고확정여부
        /// </summary>
        [Column("IN_CONFIRM_FLAG")] public string InConfirmFlag { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>입고확인여부</summary>                  
        [NotMapped] public string InConfirmState { get; set; }
        public virtual TN_PUR2100 TN_PUR2100 { get; set; }
        public virtual ICollection<TN_PUR2201> TN_PUR2201List { get; set; }
    }
}
