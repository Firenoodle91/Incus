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
    /// 작업실적관리마스터
    /// </summary>
    [Table("TN_MPS1401T")]
    public class TN_MPS1401 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1401() { }


        //[Key, Column("WORK_DATE", Order = 0)] public Nullable<DateTime> WorkDate { get; set; }
        /// <summary>
        /// 20220427 오세완 차장 생각해보니 key인데 nullable이 말이 되는가? DbUpdateConcurrencyException을 회피하고자 일단 제거 
        /// </summary>
        [Key, Column("WORK_DATE", Order = 0)] public DateTime WorkDate { get; set; }
        [Key,Column("WORK_NO",Order =1)] public string WorkNo { get; set; }
        [Key,Column("SEQ",Order =2)] public int Seq { get; set; }
        [Key,Column("PROCESS_CODE",Order =3)] public string ProcessCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        /// <summary>
        /// 20220328 오세완 차장
        /// 사용하지 않음
        /// </summary>
        //[Column("WORK_SEQ")] public Nullable<int> WorkSeq { get; set; }

        /// <summary>
        /// 20220328 오세완 차장
        /// 사용하지 않음
        /// </summary>
        //[Column("TYPE_CODE")] public string TypeCode { get; set; }
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

        /// <summary>
        /// 20220328 오세완 차장 
        /// 작업시작 설비코드
        /// </summary>
        [Column("MACHINE_CODE")]
        public string MachineCode { get; set; }
    }
}
