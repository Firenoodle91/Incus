using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_WMS1000T")]
    public class TN_WMS1000 : BaseDomain.MES_BaseDomain
    {
        public TN_WMS1000()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;
            UpdateTime = DateTime.Now;
          
        }
        [Key,Column("WH_CODE",Order =0)] public string WhCode { get; set; }
        [Column("WH_NAME")] public string WhName { get; set; }
        [Column("USE_YN")] public string UseYn { get; set; }
        [Column("POSION")] public string Posion { get; set; }
        [Column("TEMP1")] public string Temp1 { get; set; }
        [Column("TEMP2")] public string Temp2 { get; set; }
      
    }
}