using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HKInc.Ui.Model.BaseDomain;

namespace HKInc.Ui.Model.Domain.VIEW
{
    /// <summary>공구 기준정보</summary>
    [Table("TN_TOOL1000T")]
    public class TN_TOOL1000 : BaseDomain.MES_BaseDomain
    {
        public TN_TOOL1000()
        {
        }
        /// <summary>
        /// 툴 코드
        /// </summary>        
        [Column("TOOL_CODE", Order = 0), Key, Required(ErrorMessage = "ToolCode")] public string ToolCode { get; set; }

        /// <summary>
        /// 툴 품명
        /// </summary>
        [Column("TOOL_NAME"), Required(ErrorMessage = "ToolName")] public string ToolName { get; set; }

        /// <summary>
        /// 주거래처
        /// </summary>
        [Column("MAIN_CUSTOMER_CODE")] public string MainCustomerCode { get; set; }

        /// <summary>
        /// 메인설비
        /// </summary>
        [Column("MAIN_MACHINE_CODE")] public string MainMachineCode { get; set; }

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
        /// 기본수명
        /// </summary>
        [Column("BASE_CNT")] public Int32? BaseCNT { get; set; }

        /// <summary>
        /// 안전재고
        /// </summary>
        [Column("SAFE_QTY")] public decimal? SafeQty { get; set; }

        /// <summary>
        /// 적정재고
        /// </summary>
        [Column("TOOL_QTY")] public decimal? ToolQty { get; set; }

        /// <summary>
        /// 구입단가
        /// </summary>
        [Column("COST")] public decimal? Cost { get; set; }

        /// <summary>
        /// 단위중량
        /// </summary>
        [Column("WEIGHT")] public decimal? Weight { get; set; }

        /// <summary>
        /// 기본보관위치
        /// </summary>
        [Column("STOCK_POSITION")] public string StockPosition { get; set; }

        /// <summary>
        /// 사진명
        /// </summary>
        [Column("TOOL_FILE_NAME")] public string ToolFileName { get; set; }

        /// <summary>
        /// 사진URL
        /// </summary>
        [Column("TOOL_FILE_URL")] public string ToolFileUrl { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary>
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }

        /// <summary>메모</summary>              
        [Column("MEMO")] public string Memo { get; set; }

    }

}