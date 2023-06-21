using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("VI_ORD1200_DETAIL_ADD")]
    public class VI_ORD1200_DETAIL_ADD
    {
        public VI_ORD1200_DETAIL_ADD()
        {
            EditRowFlag = "N";
            _Check = "N";
        }
        [Key, Column("RowNum", Order = 0)] public Int64 RowNum { get; set; }
        [Key, Column("ITEM_CODE",Order = 1)] public string ItemCode { get; set; }
        [Key, Column("LOT_NO",Order = 2)] public string LotNo { get; set; }
        [Key, Column("PACK_LOT_NO", Order = 3)] public string PackLotNo { get; set; }
        [Column("InQty")] public Nullable<int> Inqty { get; set; }
        [Column("OutQty")] public Nullable<int> Outqty { get; set; }

        [NotMapped]
        public string EditRowFlag { get; set; }

        [NotMapped]
        public string _Check { get; set; }

        [ForeignKey("ItemCode")]
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}