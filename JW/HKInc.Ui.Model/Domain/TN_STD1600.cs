using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1600T")]
    public class TN_STD1600 : BaseDomain.BaseDomain
    {
        public TN_STD1600()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        [Key,Column("SEQ"), Required(ErrorMessage = "RowId is required")] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("DESIGN_FILE")] public string DesignFile { get; set; }
        [Column("DESIGN_MAP")] public byte[] DesignMap { get; set; }
       
    }
}


  
     