using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain.VIEW
{
    [Table("TN_SFC1000T")]
    public class TN_SFC1000 : BaseDomain.MES_BaseDomain
    {
        public TN_SFC1000()
        {
    
        }


        [Key, Column("DISPLAY_NAME", Order = 0), Required(ErrorMessage = "DisplayName")] public string DisplayName { get; set; }
        [Key, Column("SEQ",Order = 1)] public int Seq { get; set; }
        //[Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        [Column("X")] public int x { get; set; }
        [Column("Y")] public int y { get; set; }

        //[Key, Column("ITEM_CODE",Order =0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

    }
}