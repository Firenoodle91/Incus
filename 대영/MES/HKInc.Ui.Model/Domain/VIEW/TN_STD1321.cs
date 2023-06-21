using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>
    /// 20211018 오세완 차장
    /// BOM type detail
    /// </summary>	
    [Table("TN_STD1321T")]
    public class TN_STD1321 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1321()
        {
            CreateId = BaseDomain.GsValue.UserId;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 20211018 오세완 차장 
        /// 타입코드
        /// </summary>
        [ForeignKey("TN_STD1320"), Key, Column("TYPE_CODE", Order = 0), Required(ErrorMessage = "TypeCode")] public string TypeCode { get; set; }

        /// <summary>
        /// 순번
        /// </summary>                    
        [Key, Column("SEQ", Order = 1), Required(ErrorMessage = "Seq")] public int Seq { get; set; }

        /// <summary>품번</summary>           
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }

        /// <summary>
        /// 20211103 오세완 차장 
        /// 부모품번
        /// </summary>
        [Column("PARENT_ITEM_CODE")]
        public string ParentItemCode { get; set; }
        
        /// <summary>BOM레벨</summary>        
        [Column("LEVEL")] public int Level { get; set; }
        
        /// <summary>소요량</summary>         
        [Column("USE_QTY"), Required(ErrorMessage = "UseQty")] public decimal UseQty { get; set; }
        
        /// <summary>표시순서</summary>       
        [Column("DISPLAY_ORDER")] public int? DisplayOrder { get; set; }
        
        /// <summary>소요량예외</summary>       
        [Column("USE_QTY_EX")] public string UseQtyEx { get; set; }
        
        /// <summary>예외품번</summary>       
        [Column("ITEM_CODE_EX")] public string ItemCodeEx { get; set; }
        
        /// <summary>사용여부</summary>       
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }
        
        /// <summary>메모</summary>           
        [Column("MEMO")] public string Memo { get; set; }
        
        /// <summary>공정코드</summary>       
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        
        /// <summary>수동관리여부</summary>       
        [Column("MG_FLAG"), Required(ErrorMessage = "MgFlag")] public string MgFlag { get; set; }

        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual TN_STD1320 TN_STD1320 { get; set; }
    }
}