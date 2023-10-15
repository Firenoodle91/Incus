using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
    {
        [Table("VI_PO_LIST_PRT")]
        public class VI_POLISTPRT 
        {
          
            [Key,Column("PoNo",Order =0)] public string Pono { get; set; }
            [Key,Column("Seq",Order =1)] public int Seq { get; set; }
            [Column("ItemName")] public string Itemname { get; set; }
            [Column("Spec")] public string Spec { get; set; }
            [Column("Unit")] public string Unit { get; set; }
            [Column("Cost")] public int Cost { get; set; }
            [Column("Qty")] public int Qty { get; set; }
            [Column("Price")] public Nullable<int> Price { get; set; }
            [Key,Column("Memo",Order =2)] public string Memo { get; set; }
            [Column("WORKNO")] public string Workno { get; set; }
        }
    }  