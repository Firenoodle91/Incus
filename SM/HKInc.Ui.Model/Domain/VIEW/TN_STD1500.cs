using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;
using System.Drawing;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>공정별 작업지시관리</summary>	
    [Table("TN_STD1500T")]
    public class TN_STD1500 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1500()
        {

        }

        /// <summary>품번</summary>      
        [Key, Column("PROCESS_CODE", Order = 2), Required(ErrorMessage = "ProcessCode")] public string ProcessCode { get; set; }

        /// <summary>순번</summary> 
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }

        /// <summary>품목도면명</summary>      
        [Column("DESIGN_FILE_NAME"), Required(ErrorMessage ="DesignFileName")] public string DesignFileName { get; set; } 

        /// <summary>품목도면URL</summary>      
        [Column("DESIGN_FILE_URL"), Required(ErrorMessage = "DesignFileUrl")] public string DesignFileUrl { get; set; }

        [NotMapped] public object localImage { get; set; }
    }
}