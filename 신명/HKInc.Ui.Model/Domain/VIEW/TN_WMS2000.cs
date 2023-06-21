using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>창고관리 디테일</summary>	
    [Table("TN_WMS2000T")]
    public class TN_WMS2000 : BaseDomain.MES_BaseDomain
    {
        public TN_WMS2000()
        {
        }
        /// <summary>창고코드</summary>  
        [ForeignKey("TN_WMS1000"), Key, Column("WH_CODE", Order = 0), Required(ErrorMessage = "WhCode")] public string WhCode { get; set; }
        /// <summary>순번</summary>  
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>위치코드</summary>  
        [Column("POSITION_CODE"), Required(ErrorMessage = "PositionCode")] public string PositionCode { get; set; }
        /// <summary>위치설명</summary>  
        [Column("POSITION_NAME")] public string PositionName { get; set; }
        /// <summary>라인</summary>      
        [Column("POSITION_A")] public string PositionA { get; set; }
        /// <summary>행</summary>        
        [Column("POSITION_B")] public string PositionB { get; set; }
        /// <summary>열</summary>        
        [Column("POSITION_C")] public string PositionC { get; set; }
        /// <summary>열1</summary>       
        [Column("POSITION_D")] public string PositionD { get; set; }
        /// <summary>사용여부</summary>  
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        /// <summary>메모</summary>      
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>      
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>     
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>     
        [Column("TEMP2")] public string Temp2 { get; set; }
        
        public virtual TN_WMS1000 TN_WMS1000 { get; set; }
    }
}