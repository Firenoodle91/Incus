using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_RESULT_QTY")]
    public class VI_RESULT_QTY
    {
        [ForeignKey("TN_STD1100"),Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Key, Column("WORK_NO", Order = 1)] public string WorkNo { get; set; }
        [Key, Column("P_SEQ", Order = 2)] public int Pseq { get; set; }
        [Key,Column("PROCESS_CODE", Order =3)] public string ProcessCode { get; set; }
        [Key,Column("RESULT_DATE", Order =4)] public DateTime ResultDate { get; set; }
        [Key, Column("WORK_ID", Order = 5)] public string WorkId { get; set; }
        [Column("ORDER_QTY")] public Nullable<int> OrderQty { get; set; }
        [Key,Column("MACHINE_CODE", Order =6)] public string MachineCode { get; set; }
        [Column("RESULTQTY")] public Nullable<int> Resultqty { get; set; }
        [Column("OKQTY")] public Nullable<int> Okqty { get; set; }
        [Column("FAILQTY")] public Nullable<int> Failqty { get; set; }

      //  [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}