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
    /// <summary>품목도면관리</summary>	
    [Table("TN_STD1101T")]
    public class TN_STD1101 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1101()
        {

        }

        /// <summary>품번</summary>      
        [ForeignKey("TN_STD1100"), Key, Column("ITEM_CODE", Order = 2), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>순번</summary> 
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }

        /// <summary>품목도면명</summary>      
        [Column("DESIGN_FILE_NAME")] public string DesignFileName { get; set; } 

        /// <summary>품목도면URL</summary>      
        [Column("DESIGN_FILE_URL")] public string DesignFileUrl { get; set; }

        [NotMapped] public object localImage { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}