using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{

    [Table("TN_STD1500T")]
    public class TN_STD1500 : BaseDomain.BaseDomain
    {
        public TN_STD1500()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("BOM_ID", Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal BomId { get; set; }

        [Column("PARENT_BOM_ID", Order = 1)]
        public decimal? ParentBomId { get; set; }

        [Column("ITEM_CODE", Order = 2), Required(ErrorMessage = "ItemCode is required.")]
        public string ItemCode { get; set; }
   
        [Column("LEVEL")] public Nullable<int> Level { get; set; }
        [Column("SPEC_1")] public string Spec1 { get; set; }
        [Column("SPEC_2")] public string Spec2 { get; set; }
        [Column("SPEC_3")] public string Spec3 { get; set; }
        [Column("SPEC_4")] public string Spec4 { get; set; }
        [Column("UNIT")] public string Unit { get; set; }
        [Column("USE_QTY1")] public decimal UseQty1 { get; set; }
        [Column("USE_QTY2")] public decimal UseQty2 { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
       
        [Column("MEMO")] public string Memo { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }

    }
}