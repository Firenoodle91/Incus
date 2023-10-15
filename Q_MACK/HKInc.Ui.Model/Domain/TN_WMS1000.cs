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
    /// 2021-10-19 김진우 주임 수정
    /// </summary>
    [Table("TN_WMS1000T")]
    public class TN_WMS1000 : BaseDomain.MES_BaseDomain
    {
        public TN_WMS1000()
        {
            // 2021-11-04 김진우 주임 추가
            TN_WMS2000List = new List<TN_WMS2000>();
        }

        /// <summary>창고 코트</summary>        필수값 추가 및 order 제거
        [Key,Column("WH_CODE"), Required(ErrorMessage = "WhCode")] public string WhCode { get; set; }
        /// <summary>창고명</summary>
        [Column("WH_NAME")] public string WhName { get; set; }
        /// <summary>창고위치</summary>         2021-11-04 김진우 주임 추가
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        /// <summary>사용여부</summary>
        [Column("USE_YN")] public string UseYn { get; set; }
        /// <summary>임시1</summary>
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        /// <summary>2021-11-04 김진우 주임 추가</summary>
        public virtual ICollection<TN_WMS2000> TN_WMS2000List { get; set; }
    }
}