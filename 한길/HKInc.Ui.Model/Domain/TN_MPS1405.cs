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
    /// 자재투입소요량관리
    /// </summary>
    [Table("TN_MPS1405T")]
    public class TN_MPS1405 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1405()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }

        [Key, Column("IN_LOT_NO", Order = 0)] public string InLotNo { get; set; }
        [ForeignKey("TN_LOT_MST"), Key, Column("WORK_NO", Order = 1)] public string WorkNo { get; set; }
        [ForeignKey("TN_LOT_MST"), Key, Column("LOT_NO", Order = 2)] public string LotNo { get; set; }
        [Column("IN_QTY"), Required] public decimal InQty { get; set; }

        public virtual TN_LOT_MST TN_LOT_MST { get; set; }
    }
}