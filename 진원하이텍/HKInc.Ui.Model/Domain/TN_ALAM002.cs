using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_ALAM002T")]
    public class TN_ALAM002 : BaseDomain.MES_BaseDomain
    {
        public TN_ALAM002()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("NO",Order =0)] public string No { get; set; }
        [Column("HH")] public string Hh { get; set; }
        [Column("MM")] public string Mm { get; set; }
        [Column("MEMO")] public string Memo { get; set; }

    }
}