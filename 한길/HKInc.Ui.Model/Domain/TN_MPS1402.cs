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
    /// 포장 LOT 관리 테이블
    /// </summary>
    [Table("TN_MPS1402T")]
    public class TN_MPS1402 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1402()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
            TN_MPS1700List = new List<TN_MPS1700>();
        }
        [Key, Column("PACK_LOT_NO", Order = 0)] public string PackLotNo { get; set; }
        [ForeignKey("TN_MPS1401"),Column("WORK_DATE",Order = 1)] public DateTime WorkDate { get; set; }
        [ForeignKey("TN_MPS1401"),Column("WORK_NO",Order = 2)] public string WorkNo { get; set; }
        [ForeignKey("TN_MPS1401"),Column("SEQ",Order = 3)] public int Seq { get; set; }
        [ForeignKey("TN_MPS1401"),Column("PROCESS_CODE",Order = 4)] public string ProcessCode { get; set; }
        [Column("PACK_QTY")] public int PackQty { get; set; }

        public virtual TN_MPS1401 TN_MPS1401 { get; set; }
        public virtual ICollection<TN_MPS1700> TN_MPS1700List { get; set; }
    }
}
