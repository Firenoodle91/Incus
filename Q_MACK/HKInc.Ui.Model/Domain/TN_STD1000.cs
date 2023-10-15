using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1000T")]
    public class TN_STD1000 : BaseDomain.BaseDomain
    {
        public TN_STD1000() { }

        [Key, Column("CODE_MAIN", Order = 0)] public string Codemain { get; set; }
        [Key, Column("CODE_MID", Order = 1)] public string Codemid { get; set; }
        [Key, Column("CODE_SUB", Order = 2)] public string Codesub { get; set; }
        [Key, Column("CODE_VAL", Order = 3)] public string Codeval { get; set; }
        [Column("CODE_NAME")] public string Codename { get; set; }
        [Column("CODE_NAME1")] public string Codename1 { get; set; }
        [Column("CODE_NAME2")] public string Codename2 { get; set; }
        [Column("PROPERTY1")] public string Property1 { get; set; }
        [Column("PROPERTY2")] public string Property2 { get; set; }
        [Column("USE_YN")] public string Useyn { get; set; }
        [Column("DISPLAYORDER")] public Nullable<int> Displayorder { get; set; }
        [Column("MCODE")] public string Mcode { get; set; }
    }
}