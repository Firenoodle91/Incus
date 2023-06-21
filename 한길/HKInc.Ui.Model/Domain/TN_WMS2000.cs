using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_WMS2000T")]
    public class TN_WMS2000 : BaseDomain.MES_BaseDomain
    {
        public TN_WMS2000()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
            _Check = "N";
        }
        [Key, Column("WH_CODE",Order =0)] public string WhCode { get; set; }
        [Key, Column("SEQ", Order = 1)] public Nullable<int> Seq { get; set; }
        [Column("POSION_A")] public string PosionA { get; set; }
        [Column("POSION_B")] public string PosionB { get; set; }
        [Column("POSION_C")] public string PosionC { get; set; }
        [Column("POSION_D")] public string PosionD { get; set; }
        [Column("POSION_CODE"), Required(ErrorMessage = "위치코드는 필수입니다.")] public string PosionCode { get; set; }
        [Column("POSION_NAME"), Required(ErrorMessage = "위치명은 필수입니다.")] public string PosionName { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("TEMP")] public string Temp { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }

        [ForeignKey("WhCode")]
        public virtual TN_WMS1000 TN_WMS1000 { get; set; }

        [NotMapped]
        public string _Check { get; set; }
    }
}