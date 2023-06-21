using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_MPS1405V")]
    public class VI_MPS1405V
    {
        [Key,Column("WorkDate",Order =0)] public DateTime WorkDate { get; set; }
        [Key,Column("WorkNo",Order =1)] public string WorkNo { get; set; }
        [Column("Seq")] public int Seq { get; set; }
        [Column("ProcessCode")] public string ProcessCode { get; set; }
        [Column("LotNo")] public string LotNo { get; set; }
        [Column("ResultDate")] public DateTime ResultDate { get; set; }
        [Column("ResultQty")] public Nullable<int> ResultQty { get; set; }
        [Column("FailQty")] public Nullable<int> FailQty { get; set; }
        [Column("OkQty")] public Nullable<int> OkQty { get; set; }
        [Column("WorkId")] public string WorkId { get; set; }
       // [Column("TypeCode")] public string TypeCode { get; set; }
        [Column("ItemCode")] public string ItemCode { get; set; }
        [Column("ItemNm")] public string ItemNm { get; set; }
        [Column("ItemNm1")] public string ItemNm1 { get; set; }

        [Key,Column("RowId",Order =3)]        public decimal RowId { get; set; }

    }
}