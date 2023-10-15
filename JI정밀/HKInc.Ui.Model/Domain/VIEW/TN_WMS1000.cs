using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>창고관리 마스터</summary>	
    [Table("TN_WMS1000T")]
    public class TN_WMS1000 : BaseDomain.MES_BaseDomain
    {
        public TN_WMS1000()
        {
            TN_WMS2000List = new List<TN_WMS2000>();
        }

        /// <summary>창고코드</summary>   
        [Key, Column("WH_CODE"), Required(ErrorMessage = "WhCode")] public string WhCode { get; set; }
        /// <summary>창고명</summary>     
        [Column("WH_NAME")] public string WhName { get; set; }
        /// <summary>창고명(영문)</summary>     
        [Column("WH_NAME_ENG")] public string WhNameENG { get; set; }
        /// <summary>창고명(중문)</summary>     
        [Column("WH_NAME_CHN")] public string WhNameCHN { get; set; }
        /// <summary>창고위치</summary>   
        [Column("WH_POSITION")] public string WhPosition { get; set; }
        /// <summary>사용여부</summary>   
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        /// <summary>창고구분</summary>   
        [Column("WH_DIVISION")] public string WhDivision { get; set; }
        /// <summary>메모</summary>       
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>창고구분</summary>       
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>      
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>      
        [Column("TEMP2")]	public string Temp2 { get; set; }

        public virtual ICollection<TN_WMS2000> TN_WMS2000List { get; set; }
    }
}