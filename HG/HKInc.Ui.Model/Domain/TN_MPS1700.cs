using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1700T")]
    public class TN_MPS1700 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1700()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("IN_NO", Order = 0)] public string InNo { get; set; }
        [ForeignKey("TN_MPS1402"), Column("PACK_LOT_NO", Order = 1)] public string PackLotNo { get; set; }
        [Column("IN_QTY")] public int? InQty { get; set; }
        [Column("IN_DATE")] public DateTime? InDate { get; set; }
        [Column("IN_ID")] public string InId { get; set; }
        [Column("WH_CODE")] public string WhCode { get; set; }
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        public virtual TN_MPS1402 TN_MPS1402 { get; set; }

        [ForeignKey("PackLotNo")]
        public virtual VI_MPS1700_MASTER VI_MPS1700_MASTER { get; set; }
    }
}
