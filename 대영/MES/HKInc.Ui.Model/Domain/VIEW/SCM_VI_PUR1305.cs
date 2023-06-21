using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>자재발주팝업</summary>	
    [Table("SCM_VI_PUR1305")]
    public class SCM_VI_PUR1305
    {
      
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }
  
        [Column("RETURN_NO")] public string ReturnNo { get; set; }
        [Column("RETURN_SEQ")] public int ReturnSeq { get; set; }
   
        [Column("PO_NO")] public string PoNo { get; set; }
        [Column("PO_SEQ")] public Nullable<int> PoSeq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
 


    }
}