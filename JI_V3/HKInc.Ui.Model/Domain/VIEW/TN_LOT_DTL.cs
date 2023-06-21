using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>작업지시투입정보</summary>	
    [Table("TN_LOT_DTL")]
    public class TN_LOT_DTL : BaseDomain.MES_BaseDomain
    {
        public TN_LOT_DTL()
        {
            TN_SRC1000List = new List<TN_SRC1000>();
        }
        /// <summary>작업지시번호</summary>           
        [ForeignKey("TN_LOT_MST"), Key, Column("WORK_NO", Order = 0), Required(ErrorMessage = "WorkNo")] public string WorkNo { get; set; }
        /// <summary>생산LOT_NO</summary>             
        [ForeignKey("TN_LOT_MST"), Key, Column("PRODUCT_LOT_NO", Order = 1), Required(ErrorMessage = "ProductLotNo")] public string ProductLotNo { get; set; }
        /// <summary>순번</summary>             
        [Key, Column("SEQ", Order = 2), Required(ErrorMessage = "Seq")] public decimal Seq { get; set; }
        /// <summary>품번(도번)</summary>             
        [Column("ITEM_CODE")] public string ItemCode { get; set; }
        /// <summary>설비코드</summary>               
        [Column("MACHINE_CODE")] public string MachineCode { get; set; }
        /// <summary>공정</summary>               
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>투입품번(도번)</summary>             
        [Column("SRC_CODE")] public string SrcCode { get; set; }
        /// <summary>투입 LOT NO</summary>     
        [Column("SRC_IN_LOT_NO")] public string SrcInLotNo { get; set; }
        /// <summary>실적시작일</summary>             
        [Column("WORKING_DATE")] public DateTime? WorkingDate { get; set; }

        public virtual ICollection<TN_SRC1000> TN_SRC1000List { get; set; }

        public virtual TN_LOT_MST TN_LOT_MST { get; set; }
    }
}