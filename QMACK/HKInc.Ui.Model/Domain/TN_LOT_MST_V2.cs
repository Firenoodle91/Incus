using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220425 오세완 차장
    /// 신규 로트 추적 마스터
    /// </summary>
    [Table("TN_LOT_MST_V2")]
    public class TN_LOT_MST_V2 : BaseDomain.MES_BaseDomain
    {
        public TN_LOT_MST_V2()
        {
            this.LOT_DTL_List = new List<TN_LOT_DTL>();
        }

        /// <summary>
        /// 20220425 오세완 차장
        /// 작업일
        /// </summary>
        [Key, Column("WORKING_DATE", Order = 0)]
        public DateTime WorkingDate { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 생산lotno
        /// </summary>
        [Key, Column("PRODUCT_LOT_NO", Order = 1)]
        public string ProductLotNo { get; set; }

        /// <summary>
        /// 20220425 오세완 차장
        /// 작업지시번호
        /// </summary>
        [Key, Column("WORK_NO", Order = 2)]
        public string WorkNo { get; set; }


        /// <summary>
        /// 20220425 오세완 차장
        /// 생산품 품목코드
        /// </summary>
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        public virtual ICollection<TN_LOT_DTL> LOT_DTL_List { get; set; }
    }
}
