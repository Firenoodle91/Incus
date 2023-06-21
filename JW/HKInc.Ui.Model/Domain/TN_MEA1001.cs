using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain
{
    /// <summary>설비이력관리</summary>	
    [Table("TN_MEA1001T")]
    public class TN_MEA1001 : BaseDomain.MES_BaseDomain
    {
        public TN_MEA1001()
        {
        }
        /// <summary>설비코드</summary>         
        [ForeignKey("TN_MEA1000"), Key, Column("MACHINE_CODE", Order = 0), Required(ErrorMessage = "MachineCode")] public string MachineCode { get; set; }
        /// <summary>순번</summary>             
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }
        /// <summary>점검일</summary>           
        [Column("CHECK_DATE")] public DateTime? CheckDate { get; set; }
        /// <summary>점검구분</summary>         
        [Column("CHECK_DIVISION")] public string CheckDivision { get; set; }
        /// <summary>수리내역</summary>         
        [Column("REPAIR_CODE")] public string RepairCode { get; set; }
        /// <summary>금액</summary>             
        [Column("PRICE")] public decimal? Price { get; set; }
        /// <summary>품목코드</summary>         
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>점검자</summary>           
        [Column("CHECK_ID")] public string CheckId { get; set; }
        /// <summary>메모</summary>             
        [Column("MEMO")] public string Memo { get; set; }
        /// <summary>임시</summary>             
        [Column("TEMP")] public string Temp { get; set; }
        /// <summary>임시1</summary>            
        [Column("TEMP1")] public string Temp1 { get; set; }
        /// <summary>임시2</summary>            
        [Column("TEMP2")] public string Temp2 { get; set; }

        public virtual TN_MEA1000 TN_MEA1000 { get; set; }
        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}