using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>설비점검이력관리 마스터</summary>	
    [Table("VI_MEA1003_MASTER_LIST")]
    public class VI_MEA1003_MASTER_LIST 
    {
        public VI_MEA1003_MASTER_LIST()
        {
        }
        /// <summary>설비코드</summary>        
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_CODE"), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
    }
}