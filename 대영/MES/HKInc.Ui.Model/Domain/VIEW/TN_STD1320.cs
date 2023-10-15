using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 20211018 오세완 차장
    /// bomtype master
    /// </summary>
    [Table("TN_STD1320T")]
    public class TN_STD1320 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1320()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;

            TN_STD1321List = new List<TN_STD1321>();
        }

        /// <summary>
        /// 20211018 오세완 차장
        /// 타입코드
        /// </summary>
        [Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")] public string TypeCode { get; set; }

        /// <summary>
        /// 20211018 오세완 차장
        /// 타입명
        /// </summary>
        [Column("TYPE_NAME"), Required(ErrorMessage = "TypeName")] public string TypeName { get; set; }

        /// <summary>
        /// 20211018 오세완 차장
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }

        /// <summary>
        /// 20211018 오세완 차장
        /// 임시
        /// </summary>
        [Column("TEMP")] public string Temp { get; set; }

        /// <summary>
        /// 20211018 오세완 차장
        /// 임시1
        /// </summary>
        [Column("TEMP1")] public string Temp1 { get; set; }

        /// <summary>
        /// 20211018 오세완 차장
        /// 임시2
        /// </summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>
        /// 20211018 오세완 차장
        /// bom type detail
        /// </summary>
        public virtual List<TN_STD1321> TN_STD1321List { get; set; }

    }
}
