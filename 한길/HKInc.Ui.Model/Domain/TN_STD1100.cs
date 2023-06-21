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
    /// 품목기준정보
    /// </summary>
    [Table("TN_STD1100T")]
    public class TN_STD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1100()
        {
            CreateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            CreateTime = DateTime.Now;
            UpdateId = BaseDomain.GsValue.UserId;//HKInc.Utils.Common.GlobalVariable.LoginId;
            UpdateTime = DateTime.Now;

            QCT1000List = new List<TN_QCT1000>();
            STD1600List = new List<TN_STD1600>();
            TN_MPS1000List = new List<TN_MPS1000>();
            TN_MPS1001List = new List<TN_MPS1001>();
            TN_STD1800List = new List<TN_STD1800>();
            TN_QCT1001List = new List<TN_QCT1001>();
        }
        [Key,Column("ITEM_CODE",Order =0), Required(ErrorMessage = "품목코드는 필수입니다.")] //일련아이템코드
        public string ItemCode { get; set; }
        [Index("UK_TN_STD1100T", IsUnique = true), Column("ITEM_NM1"), Required(ErrorMessage = "품번은 필수입니다.")] //품번
        public string ItemNm1 { get; set; }
        [Column("ITEM_NM"), Required(ErrorMessage = "품명은 필수입니다.")] //품명
        public string ItemNm { get; set; }
        [Column("ITEM_GBN")]
        public string ItemGbn { get; set; }
        [Column("TOP_CATEGORY"), Required(ErrorMessage = "대분류는 필수입니다.")]
        public string TopCategory { get; set; }
        [Column("MIDDLE_CATEGORY")]
        public string MiddleCategory { get; set; }
        [Column("BOTTOM_CATEGORY")]
        public string BottomCategory { get; set; }
        [Column("SPEC_1")]
        public string Spec1 { get; set; } //원재료 공통코드관리
        [Column("SPEC_2")]
        public string Spec2 { get; set; } //색상 공통코드관리
        [Column("SPEC_3")]
        public string Spec3 { get; set; } //재료규격
        [Column("SPEC_4")]
        public string Spec4 { get; set; } //실리콘농도
        [Column("UNIT")]
        public string Unit { get; set; }
        [Column("WEIGHT")]
        public decimal Weight { get; set; }
        [Column("SAFE_QTY")]
        public decimal SafeQty { get; set; }
        [Column("SELF_INSP_YN")]
        public string SelfInspyn { get; set; }
        [Column("STOCK_INSP_YN")]
        public string StockInspyn { get; set; }
        [Column("PROC_INSP_YN")]
        public string ProcInspyn { get; set; }
        [Column("USE_YN")]
        public string UseYn { get; set; }
        [Column("MEMO")]
        public string Memo { get; set; }
        [Column("ETCFILE")]
        public byte[] Etcfile { get; set; }
       
        [Column("TEMP2")]
        public string Temp2 { get; set; } //제품규격
        [Column("TEMP3")]
        public string Temp3 { get; set; } //SET품여부
        [Column("TEMP4")]
        public string Temp4 { get; set; }
        [Column("TEMP5")]
        public string Temp5 { get; set; }
        [Column("TEMP6")]
        public string Temp6 { get; set; }
        [Column("SRC_CODE")]
        public string SrcCode { get; set; }
        [Column("MAIN_MC")]
        public string MainMc { get; set; }
        [Column("SRC_QTY")]
        public decimal SrcQty { get; set; }
        [Column("PROC_CNT")]
        public decimal? ProcCnt { get; set; }
        [Column("MAIN_CUST")]
        public string MainCust { get; set; }
        [Column("CUST_ITEMCODE")]
        public string CustItemCode { get; set; }
        [Column("CUST_ITEMNM")]
        public string CustItemNm { get; set; }
        [Column("LCTYPE")]
        public string Lctype { get; set; }
        [Column("MOLD_CODE")]
        public string MoldCode { get; set; }
        [Column("KNIFE_CODE")]
        public string KnifeCode { get; set; }
        [Column("STD_PACK_QTY")]
        public int? StdPackQty { get; set; }

        public virtual ICollection<TN_QCT1000> QCT1000List { get; set; }
        public virtual ICollection<TN_STD1600> STD1600List { get; set; }
        public virtual ICollection<TN_MPS1000> TN_MPS1000List { get; set; }
        public virtual ICollection<TN_MPS1001> TN_MPS1001List { get; set; }
        public virtual ICollection<TN_STD1800> TN_STD1800List { get; set; }
        public virtual ICollection<TN_QCT1001> TN_QCT1001List { get; set; }
        
    }
}
