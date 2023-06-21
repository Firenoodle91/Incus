using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1401T")]
    public class TN_MPS1401 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1401()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            TN_MPS1402List = new List<TN_MPS1402>();
        }
        [Key,Column("WORK_DATE",Order =0)] public Nullable<DateTime> WorkDate { get; set; }
        [Key,Column("WORK_NO",Order =1)] public string WorkNo { get; set; }
        [Key,Column("SEQ",Order =2)] public int Seq { get; set; }
        [Key,Column("PROCESS_CODE",Order =3)] public string ProcessCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("WORK_SEQ")] public Nullable<int> WorkSeq { get; set; }
        [Column("TYPE_CODE")] public string TypeCode { get; set; }
        [Column("PROCESS_TURN")] public Nullable<int> ProcessTurn { get; set; }
        [Column("ORDER_QTY")] public Nullable<int> OrderQty { get; set; }
        [Column("RESULT_DATE")] public Nullable<DateTime> ResultDate { get; set; }
        [Column("RESULT_QTY")] public Nullable<int> ResultQty { get; set; }
        [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
        [Column("OK_QTY")] public Nullable<int> OkQty { get; set; }
        [Column("CONFIRM_YN")] public string ConfirmYn { get; set; }
        [Column("WORK_ID")] public string WorkId { get; set; }
        [Column("START_DATE")] public Nullable<DateTime> StartDate { get; set; }
        [Column("END_DATE")] public Nullable<DateTime> EndDate { get; set; }
        [Column("ITEMMOVENO")] public string Itemmoveno { get; set; }

        public ICollection<TN_MPS1402> TN_MPS1402List { get; set; }
    
    }
}
