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
        public class VI_PO_LIST_PRT
    {
          
            [Key,Column("PoNo",Order =0)] public string Pono { get; set; }
            [Key,Column("Seq",Order =1)] public int Seq { get; set; }
            [Column("ItemName")] public string Itemname { get; set; }
            [Column("Spec")] public string Spec { get; set; }
            [Column("Unit")] public string Unit { get; set; }
            [Column("Cost")] public Nullable<Decimal> Cost { get; set; }            // 2022-06-21 김진우 int => Nullable<Decimal>로 수정
            [Column("Qty")] public Nullable<Decimal> Qty { get; set; }
            [Column("Price")] public Nullable<Decimal> Price { get; set; }
            [Key,Column("Memo",Order =2)] public string Memo { get; set; }
            [Column("WORKNO")] public string Workno { get; set; }
        }
    }  