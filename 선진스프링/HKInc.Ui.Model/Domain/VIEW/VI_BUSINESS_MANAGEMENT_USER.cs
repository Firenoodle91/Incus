using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>영업담당자 룩업 뷰</summary>	
    [Table("VI_BUSINESS_MANAGEMENT_USER")]
    public class VI_BUSINESS_MANAGEMENT_USER
    {
        public VI_BUSINESS_MANAGEMENT_USER()
        {

        }
                
        [Key, Column("LoginId")] public string LoginId { get; set; }     
        [Column("UserName")] public string UserName { get; set; }
    }
}