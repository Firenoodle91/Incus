using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HKInc.Ui.Model.Domain
{
    /// <summary>
    /// 20220318 오세완 차장 도면관리
    /// </summary>
    [Table("TN_STD1600T")]
    public class TN_STD1600 : BaseDomain.BaseDomain
    {
        public TN_STD1600() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column("SEQ"), Required(ErrorMessage = "RowId is required")] public int Seq { get; set; }
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        [Column("DESIGN_FILE")] public string DesignFile { get; set; }
        [Column("DESIGN_MAP")] public byte[] DesignMap { get; set; }

        /// <summary>
        /// 20220321 오세완 차장
        /// 도면파일 ftp경로명 추가 
        /// </summary>
        [Column("DESIGN_FILE_URL")] 
        public string DesignFileUrl { get; set; }
       
    }
}


  
     