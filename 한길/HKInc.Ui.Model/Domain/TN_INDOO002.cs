using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_INDOO002T")]
    public class TN_INDOO002 : BaseDomain.BaseDomain
    {
        public TN_INDOO002()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key, Column("INDOO_MNO", Order = 0)] public string IndooMno { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column("SEQ",Order =1), Required(ErrorMessage = "RowId is required")]
        public decimal Seq { get; set; }
    
        [Column("INTYPE")] public string Intype { get; set; }
        [Column("WORK_DATE")] public Nullable<DateTime> WorkDate { get; set; }
        [Column("TIPCNT")] public Nullable<decimal> Tipcnt { get; set; }
        
        [Column("TIPCHANGE")] public string Tipchange { get; set; }

        [Column("MEMO")] public string Memo { get; set; }
        [Column("INUSER")] public string InUser { get; set; }
        [ForeignKey("IndooMno")]
        public virtual TN_INDOO001 TN_INDOO001 { get; set; }
    }
}