using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_MPS1001T")]
    public class TN_MPS1001 : BaseDomain.MES_BaseDomain
    {
        public TN_MPS1001()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
        }
        [Key,Column("ITEM_CODE",Order =0)] public string ItemCode { get; set; }
        [Key,Column("PROCESS_CODE",Order =1)] public string ProcessCode { get; set; }
        [Key,Column("MACHINE_CODE",Order =2)] public string MachineCode { get; set; }
        [Key,Column("REV",Order =3)] public Nullable<int> Rev { get; set; }
        [Column("FILE_NAME")] public string FileName { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
     
    }
}