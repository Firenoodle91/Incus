using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    [Table("TN_STD1800T")]
    public class TN_STD1800 : BaseDomain.BaseDomain
    {
        public TN_STD1800()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        [Key,Column("SEQ",Order =0), Required(ErrorMessage = "RowId is required")] public int Seq { get; set; }
        [Key,Column("ITEM_CODE",Order =1)] public string ItemCode { get; set; }
        [Column("NOTE1")] public string Note { get; set; }
        [Column("START_DT")] public Nullable<DateTime> StartDt { get; set; }
        [Column("END_DT")] public Nullable<DateTime> EndDt { get; set; }
        [Column("WORKUSER")] public string Workuser { get; set; }
     

    }
}


  
     