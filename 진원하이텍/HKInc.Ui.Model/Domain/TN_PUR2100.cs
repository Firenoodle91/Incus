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
    /// 외주품 발주관리 마스터
    /// </summary>
    [Table("TN_PUR2100T")]
    public class TN_PUR2100 : BaseDomain.MES_BaseDomain
    {
        public TN_PUR2100()
        {
            TN_PUR2101List = new List<TN_PUR2101>();
        }

        /// <summary>
        /// 발주번호
        /// </summary>
        [Key, Column("PO_NO"), Required(ErrorMessage = "PoNo")] public string PoNo { get; set; }
        /// <summary>
        /// 수주번호
        /// </summary>
        [Column("ORDER_NO")] public string OrderNo { get; set; }
        /// <summary>
        /// 계획번호
        /// </summary>
        [Column("DELIV_SEQ")] public string DelivSeq { get; set; }
        /// <summary>
        /// 발주일
        /// </summary>
        [Column("PO_DATE"), Required(ErrorMessage = "PoDate")] public DateTime PoDate { get; set; }
        /// <summary>
        /// 발주자
        /// </summary>
        [Column("PO_ID"), Required(ErrorMessage = "PoId")] public string PoId { get; set; }
        /// <summary>
        /// 발주처
        /// </summary>
        [Column("PO_CUSTOMER_CODE"), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>
        /// 납기일
        /// </summary>
        [Column("DUE_DATE")] public DateTime DueDate { get; set; }
        /// <summary>
        /// 발주확정여부
        /// </summary>
        [Column("PO_FLAG"), Required(ErrorMessage = "PoFlag")] public string PoFlag { get; set; }
        /// <summary>
        /// 품목코드
        /// </summary>
        //[ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>입고상태</summary>                  
        [NotMapped]
        public string InConfirmState
        {
            // 메인코드 : Z001, 01 : 대기, 02 : 진행중, 03 : 완료
            get
            {
                if (TN_PUR2101List.Count == 0)
                    return "01";
                else
                {
                    var count = TN_PUR2101List.Count;
                    if (TN_PUR2101List.Where(p => p.InConfirmState == "03").Count() == count)
                        return "03";
                    else if (TN_PUR2101List.Where(p => p.InConfirmState == "01").Count() == count)
                        return "01";
                    else
                        return "02";
                }
            }
        }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
        public virtual ICollection<TN_PUR2101> TN_PUR2101List { get; set; }
    }
}
