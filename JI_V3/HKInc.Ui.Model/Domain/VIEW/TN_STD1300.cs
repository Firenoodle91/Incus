using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>BOM관리</summary>	
    [Table("TN_STD1300T")]
    public class TN_STD1300 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1300()
        {
        }
        /// <summary>BOM코드</summary>        
        [Key, Column("BOM_CODE"), Required(ErrorMessage = "BomCode")] public string BomCode { get; set; }
        /// <summary>품번</summary>
        [ForeignKey("TN_STD1100"), Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>품명</summary>
        [Column("ITEM_NAME")] public string ItemName { get; set; }
        /// <summary>상위BOM코드</summary>    
        [ForeignKey("P_TN_STD1300"), Column("PARENT_BOM_CODE")] public string ParentBomCode { get; set; }
        /// <summary>BOM레벨</summary>        
        [Column("LEVEL"), Required(ErrorMessage = "Level")] public int Level { get; set; }
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
        /// <summary>
        /// 대분류
        /// </summary>
        [Column("TOP_CATEGORY")] public string TopCategory { get; set; }
        /// <summary>
        /// 중분류
        /// </summary>
        [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }
        /// <summary>
        /// 소분류
        /// </summary>
        [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }
        ///// <summary>
        ///// 순도
        ///// </summary>
        //[Column("PURITY")] public string Purity { get; set; }
        /// <summary>
        /// 규격1
        /// </summary>
        [Column("SPEC_1")] public string Spec1 { get; set; }
        /// <summary>
        /// 규격2
        /// </summary>
        [Column("SPEC_2")] public string Spec2 { get; set; }
        /// <summary>
        /// 규격3
        /// </summary>
        [Column("SPEC_3")] public string Spec3 { get; set; }
        /// <summary>
        /// 규격4
        /// </summary>
        [Column("SPEC_4")] public string Spec4 { get; set; }
        /// <summary>
        /// 단위
        /// </summary>
        [Column("UNIT")] public string Unit { get; set; }
        /// <summary>
        /// 중량
        /// </summary>        
        [Column("WEIGHT")] public decimal? Weight { get; set; }
        /// <summary>공정코드</summary>       
        [Column("PROCESS_CODE")] public string ProcessCode { get; set; }
        /// <summary>관리여부</summary>       
        [Column("MG_FLAG")] public string MgFlag { get; set; }

        [NotMapped]
        public string CustomItemCode { get { return ItemCode; } }

        [NotMapped]
        public int RowIndex { get; set; }

        //[NotMapped]
        //public string CustomItemCombineSpec
        //{
        //    get
        //    {
        //        if (TN_STD1100 == null) return string.Empty;
        //        else
        //        {
        //            var spec1 = TN_STD1100.Spec1 == null ? " " : TN_STD1100.Spec1;
        //            var spec2 = TN_STD1100.Spec2 == null ? " " : TN_STD1100.Spec2;
        //            var spec3 = TN_STD1100.Spec3 == null ? " " : TN_STD1100.Spec3;
        //            var spec4 = TN_STD1100.Spec4 == null ? " " : TN_STD1100.Spec4;
        //            return spec1 + spec2 + spec3 + spec4;
        //        }
        //    }
        //}
        

        [NotMapped]
        public string CustomItemUnit { get { return TN_STD1100 == null ? string.Empty : TN_STD1100.Unit; } }

        [NotMapped]
        public string OutLotNo { get; set; }



        public virtual TN_STD1100 TN_STD1100 { get; set; }

        public virtual TN_STD1300 P_TN_STD1300 { get; set; }
    }
}