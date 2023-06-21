using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 202220407 오세완 차장
    /// 단가이력관리 거래처목록
    /// </summary>
    [Table("TN_STD1120T")]
    public class TN_STD1120 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1120()
        {
            STD1121List = new List<TN_STD1121>();
        }

        /// <summary>품목코드</summary>          20220407 오세완 차장
        [Key, Column("ITEM_CODE", Order = 0)] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>        20220407 오세완 차장
        [Key, Column("CUSTOMER_CODE", Order = 1)] public string CustomerCode { get; set; }
        /// <summary>사용여부</summary>          20220407 오세완 차장
        [Column("USE_YN")] public string UseYn { get; set; }
        /// <summary>메모</summary>
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1400 TN_STD1400 { get; set; }
        public virtual ICollection<TN_STD1121> STD1121List { get; set; }
    }
}
