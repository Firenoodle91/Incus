using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220318 오세완 차장
    /// 표준공정타입관리 마스터
    /// </summary>
    [Table("TN_MPS1010T")]
    public class TN_MPS1010 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1010()
        {
            TN_MPS1011_List = new List<TN_MPS1011>();
        }

        /// <summary>
        /// 20220318 오세완 차장
        /// 타입코드
        /// </summary>
        [Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")]
        public string TypeCode { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// </summary>
        [Column("TYPE_NAME"), Required(ErrorMessage = "TypeName")]
        public string TypeName { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 임시
        /// </summary>
        [Column("TEMP")] public string Temp { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 임시1
        /// </summary>
        [Column("TEMP1")] public string Temp1 { get; set; }

        /// <summary>
        /// 20220318 오세완 차장
        /// 임시2
        /// </summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual List<TN_MPS1011> TN_MPS1011_List { get; set; }
    }
}
