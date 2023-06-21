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
    /// 금형투입샷수관리
    /// </summary>
    [Table("TN_MPS1407T")]
    public class TN_MPS1407 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1407()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }

        [Key, Column("MOLD_CODE", Order = 0)] public string MoldCode { get; set; }
        [ForeignKey("TN_LOT_MST"), Key, Column("WORK_NO", Order = 1)] public string WorkNo { get; set; }
        [ForeignKey("TN_LOT_MST"), Key, Column("LOT_NO", Order = 2)] public string LotNo { get; set; }
        [Column("SHOT_CNT"), Required] public decimal ShotCnt { get; set; }

        public virtual TN_LOT_MST TN_LOT_MST { get; set; }
    }
}