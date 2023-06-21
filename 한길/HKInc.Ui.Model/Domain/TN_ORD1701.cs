using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ORD1701T")]
    public class TN_ORD1701 : BaseDomain.MES_BaseDomain
    {
        public TN_ORD1701()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [ForeignKey("TN_ORD1700"),Key, Column("IN_NO",Order =0)] public string InNo { get; set; }
        [Key, Column("SEQ",Order =1)] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("LOT_NO"), Required(ErrorMessage = "입고 LOT NO는 필수입니다")] public string LotNo { get; set; }
        [Column("IN_QTY"), Required(ErrorMessage = "입고수량은 필수입니다")] public Nullable<int> InQty { get; set; }
        [Column("IN_DATE"), Required(ErrorMessage = "입고일은 필수입니다")] public Nullable<DateTime> InDate { get; set; }
        [Column("MEMO")] public string Memo { get; set; }

        /// <summary>
        /// 20220405 오세완 차장
        /// 바코드 출력 때문에 추가 
        /// </summary>
        [NotMapped] public string _Check { get; set; }

        public virtual TN_ORD1700 TN_ORD1700 { get; set; }
    }
}