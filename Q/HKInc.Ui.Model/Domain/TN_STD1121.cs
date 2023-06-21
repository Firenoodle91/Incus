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
    /// 20220407 오세완 차장
    /// 단가이력관리 단가목록
    /// </summary>
    [Table("TN_STD1121T")]
    public class TN_STD1121 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1121() { }

        /// <summary>품목코드</summary>              
        [ForeignKey("TN_STD1120"), Key, Column("ITEM_CODE", Order = 0), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>거래처코드</summary>            
        [ForeignKey("TN_STD1120"), Key, Column("CUSTOMER_CODE", Order = 1), Required(ErrorMessage = "CustomerCode")] public string CustomerCode { get; set; }
        /// <summary>순번</summary>                  
        [Key, Column("SEQ", Order = 2)] public int Seq { get; set; }
        /// <summary>단가 적용 시작 날짜</summary>                
        [Column("START_DATE")] public DateTime? StartDate { get; set; }
        /// <summary>단가 적용 종료 날짜</summary>                
        [Column("END_DATE")] public DateTime? EndDate { get; set; }
        /// <summary>컷팅비</summary>              
        [Column("CUTTING_COST")] public decimal? CuttingCost { get; set; }
        /// <summary>면취비</summary>              
        [Column("CHAMFER_COST")] public decimal? ChamferCost { get; set; }
        /// <summary>가공비</summary>              
        [Column("MANUFACTURING_COST")] public decimal? ManufacturingCost { get; set; }
        /// <summary>합계단가</summary>              
        [Column("TOTAL_COST")] public decimal? TotalCost { get; set; }
        /// <summary>메모</summary>                  
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_STD1120 TN_STD1120 { get; set; }
    }
}
