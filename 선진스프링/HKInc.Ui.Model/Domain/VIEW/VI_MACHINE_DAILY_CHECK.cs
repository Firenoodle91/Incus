using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비관리</summary>	
    [Table("VI_MACHINE_DAILY_CHECK")]
    public class VI_MACHINE_DAILY_CHECK
    {
        public VI_MACHINE_DAILY_CHECK()
        {
        }
        /// <summary>설비고유코드</summary>        
        [Key, Column("MACHINE_MCODE"), Required(ErrorMessage = "MachineCode")] public string MachineMCode { get; set; }
        /// <summary>설비코드</summary>        
        [Column("MACHINE_CODE"), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        /// <summary>설비그룹코드</summary>          
        [Column("MACHINE_GROUP_CODE")] public string MachineGroupCode { get; set; }
        /// <summary>설비명</summary>          
        [Column("MACHINE_NAME"), Required(ErrorMessage = "MachineName")] public string MachineName { get; set; }
        /// <summary>설비명(영문)</summary>    
        [Column("MACHINE_NAME_ENG")] public string MachineNameENG { get; set; }
        /// <summary>설비명(중문)</summary>    
        [Column("MACHINE_NAME_CHN")] public string MachineNameCHN { get; set; }

    }
}