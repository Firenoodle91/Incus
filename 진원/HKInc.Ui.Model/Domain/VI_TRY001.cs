using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_TRY001")]
    public class VI_TRY001
    {
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Column("ITEM_NM")] public string ItemNm { get; set; }
        [Column("ITEM_NM1")] public string ItemNm1 { get; set; }
        [Column("DesignFile")] public string Designfile { get; set; }
        [Column("DesignMap")] public byte[] Designmap { get; set; }
    }
}