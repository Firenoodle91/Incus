using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>설비금형목록</summary>	
    /// 2021-06-14 김진우 주임 생성
    [Table("VI_MACHINE_MOLD_LIST")]
    public class VI_MACHINE_MOLD_LIST
    {
        public VI_MACHINE_MOLD_LIST()
        {
        }
        
        /// <summary>설비금형코드</summary>           
        [Key, Column("MANAGE_CODE")] public string ManageCode { get; set; }
        /// <summary>설비금형명</summary>             
        [Column("NAME")] public string Name { get; set; }
        /// <summary>설비금형구분</summary>
        [Column("MANAGE_TYPE")] public string ManageType { get; set; }

    }
}