using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MEA1200T")]
    public class TN_MEA1200 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1200()
        {
            TN_MEA1300List = new List<TN_MEA1300>();
        }

        [Key,Column("INSTR_NO",Order =0)] public string InstrNo { get; set; }
        [Column("INSTR_NM")] public string InstrNm { get; set; }
        [Column("MAKER")] public string Maker { get; set; }
        [Column("PURC_DATE")] public Nullable<DateTime> PurcDate { get; set; }
        [Column("SERIAL_NO")] public string SerialNo { get; set; }
        [Column("SPEC")] public string Spec { get; set; }
        [Column("INSTR_ID")] public string InstrId { get; set; }
        [Column("COR_TURN")] public string CorTurn { get; set; }
        [Column("COR_DATE")] public Nullable<DateTime> CorDate { get; set; }
        [Column("NXCOR_DATE")] public Nullable<DateTime> NxcorDate { get; set; }
        [Column("FILE_NAME")] public string FileName { get; set; }
        [Column("FILE_DATA")] public byte[] FileData { get; set; }        
        [Column("MEMO")] public string Memo { get; set; }
        [Column("TEMP")] public string UseYn { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
        public virtual ICollection<TN_MEA1300> TN_MEA1300List { get; set; }

    }
}
