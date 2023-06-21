using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1404T")]
    public class TN_MPS1404 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1404() { }

        [Key,Column("WORK_DATE",Order =0)] public Nullable<DateTime> WorkDate { get; set; }
        [Column("RESULT_DATE")] public Nullable<DateTime> ResultDate { get; set; }
        [Key, Column("WORK_NO",Order =1)] public string WorkNo { get; set; }
        [Key,Column("SEQ",Order =2)] public int Seq { get; set; }
        [Key,Column("PROCESS_CODE",Order =3)] public string ProcessCode { get; set; }
        [Column("LOT_NO")] public string LotNo { get; set; }
        [Column("FAIL_QTY")] public Nullable<int> FailQty { get; set; }
        [Column("FALE_TYPE")] public string FaleType { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("P_NO")] public string PNo { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
     
    }
}