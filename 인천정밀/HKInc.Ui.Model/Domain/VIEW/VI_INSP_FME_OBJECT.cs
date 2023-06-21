using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>초중종검사대상</summary>	
    [Table("VI_INSP_FME_OBJECT")]
    public class VI_INSP_FME_OBJECT
    {
        public VI_INSP_FME_OBJECT()
        {
            _Check = "N";
        }
        
        /// <summary>고유번호(가상번호)</summary>           
        [Key, Column("RowIndex")] public long RowIndex { get; set; }

        /// <summary>작업지시번호</summary>             
        [Column("WORK_NO")] public string WorkNo { get; set; }
        /// <summary>공정코드</summary>             
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>공정순번</summary>             
        [Column("PROCESS_SEQ")] public int ProcessSeq { get; set; }
        /// <summary>작업지시일</summary>             
        [Column("WORK_DATE")] public DateTime WorkDate { get; set; }
        /// <summary>품목코드</summary>               
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>생산 LOT NO</summary>             
        [Column("PRODUCT_LOT_NO")] public string ProductLotNo { get; set; }
        /// <summary>설비코드</summary>             
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>거래처코드</summary>             
        [Column("CUSTOMER_CODE")] public string CustomerCode { get; set; }
        /// <summary>메모</summary>               
        [Column("MEMO")] public string Memo { get; set; }
        
        [NotMapped] public string _Check { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }
    }
}