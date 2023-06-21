using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_SPC_VALUE")]
    public class VI_SPC_VALUE
    {
        [Key,Column("CheckDate",Order =0)] public string Checkdate { get; set; }
        [Column("CHECK_DATE")] public Nullable<DateTime> Check_date { get; set; }
        [Key,Column("ItemCode",Order =1)] public string Itemcode { get; set; }
        [Column("ItemNm")] public string Itemnm { get; set; }
        [Key,Column("Process",Order =2)] public string Process { get; set; }
        [Column("processNm")] public string processnm { get; set; }
        [Key,Column("FME_NO",Order =3)] public string FmeNo { get; set; }
        [Key,Column("seq",Order =4)] public int seq { get; set; }
        [Column("CheckNm")] public string Checknm { get; set; }
        [Column("Std")] public Nullable<double> Std { get; set; }
        [Column("StdDown")] public Nullable<double> Stddown { get; set; }
        [Column("StdUp")] public Nullable<double> Stdup { get; set; }
        [Column("TestVal")] public Nullable<double> Testval { get; set; }
    }
}