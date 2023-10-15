using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_INDOO001T")]
    public class TN_INDOO001 : BaseDomain.MES_BaseDomain
    {
        public TN_INDOO001()
        {
            TN_INDOO002List = new List<TN_INDOO002>();
        }
        [Key,Column("INDOO_NO",Order =0)] public string IndooNo { get; set; }
        [Column("INDOO_MNO")] public string IndooMno { get; set; }
        [Column("INDOO_NAME")] public string IndooName { get; set; }
        
        [Column("INDOO_MODEL")] public string IndooModel { get; set; }
        [Column("MAKER")] public string Maker { get; set; }
        [Column("SUMTIP_CNT")] public decimal SumtipCnt { get; set; }
        [Column("REALTIP_CNT")] public decimal RealtipCnt { get; set; }
        [Column("MANGTIP_CNT")] public decimal MangtipCnt { get; set; }
        [Column("WR_CNT")] public decimal WrCnt { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual ICollection<TN_INDOO002> TN_INDOO002List { get; set; }

    }
}