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
    /// <summary>
    /// 품목기준정보관리
    /// </summary>
    [Table("TN_STD1100T")]
    public class TN_STD1100 : BaseDomain.MES_BaseDomain
    {
        public TN_STD1100()
        {
            TN_STD1101List = new List<TN_STD1101>();
            TN_STD1102List = new List<TN_STD1102>();
            TN_STD1103List = new List<TN_STD1103>();
            TN_STD1104List = new List<TN_STD1104>();
            TN_QCT1000List = new List<TN_QCT1000>();
            TN_QCT1400List = new List<TN_QCT1400>();
            TN_MPS1000List = new List<TN_MPS1000>();
            TN_TRY1000List = new List<TN_TRY1000>();
            TN_MEA1201List = new List<TN_MEA1201>();
            TN_TOOL1001List = new List<TN_TOOL1001>();
        }

        /// <summary>
        /// 품번
        /// </summary>
        [Key, Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>
        /// 품명
        /// </summary>
        [Column("ITEM_NAME"), Required(ErrorMessage = "ItemName")] public string ItemName { get; set; } 
        /// <summary>
        /// 품명(영문)
        /// </summary>
        [Column("ITEM_NAME_ENG")] public string ItemNameENG { get; set; }
        /// <summary>
        /// 품명(중문)
        /// </summary>
        [Column("ITEM_NAME_CHN")] public string ItemNameCHN { get; set; }  
        /// <summary>
        /// 대분류
        /// </summary>
        [Column("TOP_CATEGORY"), Required(ErrorMessage = "TopCategory")] public string TopCategory { get; set; }  
        /// <summary>
        /// 중분류
        /// </summary>
        [Column("MIDDLE_CATEGORY")] public string MiddleCategory { get; set; }  
        /// <summary>
        /// 소분류
        /// </summary>
        [Column("BOTTOM_CATEGORY")] public string BottomCategory { get; set; }  
        /// <summary>
        /// 차종
        /// </summary>
        [Column("CAR_TYPE")] public string CarType { get; set; } 
        /// <summary>
        /// 주거래처
        /// </summary>
        [Column("MAIN_CUSTOMER_CODE")] public string MainCustomerCode { get; set; } 
        /// <summary>
        /// 거래처품번
        /// </summary>
        [Column("CUSTOMER_ITEM_CODE")] public string CustomerItemCode { get; set; } 
        /// <summary>
        /// 거래처품명
        /// </summary>
        [Column("CUSTOMER_ITEM_NAME")] public string CustomerItemName { get; set; } 
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
        /// 외주단위
        /// </summary>
        [Column("OUT_UNIT")] public string OutUnit { get; set; }
        /// <summary>
        /// 단위중량
        /// </summary>
        [Column("WEIGHT")] public decimal? Weight { get; set; }  
        /// <summary>
        /// 안전재고량
        /// </summary>
        [Column("SAFE_QTY")] public decimal? SafeQty { get; set; }  
        /// <summary>
        /// 적정재고량
        /// </summary>
        [Column("PROD_QTY")] public decimal? ProdQty { get; set; }  
        /// <summary>
        /// 단가
        /// </summary>
        [Column("COST")] public decimal? Cost { get; set; }  
        /// <summary>
        /// 제조팀코드
        /// </summary>
        [Column("PROC_TEAM_CODE")] public string ProcTeamCode { get; set; } 
        /// <summary>
        /// 기본보관위치
        /// </summary>
        [Column("STOCK_POSITION")] public string StockPosition { get; set; }  
        /// <summary>
        /// 원자재코드
        /// </summary>
        [Column("SRC_CODE")] public string SrcCode { get; set; }  
        /// <summary>
        /// 소재사용량
        /// </summary>
        [Column("SRC_WEIGHT")] public decimal? SrcWeight { get; set; }
        /// <summary>
        /// 설비그룹코드
        /// </summary>
        [Column("MAIN_MACHINE_CODE")] public string MainMachineCode { get; set; }  
        /// <summary>
        /// 표면처리항목
        /// </summary>
        [Column("SURFACE_LIST")] public string SurfaceList { get; set; }  
        /// <summary>
        /// 연마여부
        /// </summary>
        [Column("GRINDING_FLAG")] public string GrindingFlag { get; set; }  
        /// <summary>
        /// 자주검사여부
        /// </summary>
        [Column("SELF_INSP_FLAG")] public string SelfInspFlag { get; set; }  
        /// <summary>
        /// 수입검사여부
        /// </summary>
        [Column("STOCK_INSP_FLAG")] public string StockInspFlag { get; set; }  
        /// <summary>
        /// 공정검사여부
        /// </summary>
        [Column("PROC_INSP_FLAG")] public string ProcInspFlag { get; set; }  
        /// <summary>
        /// 출하검사여부
        /// </summary>
        [Column("SHIPMENT_INSP_FLAG")] public string ShipmentInspFlag { get; set; }  
        /// <summary>
        /// 제품사진명
        /// </summary>
        [Column("PROD_FILE_NAME")] public string ProdFileName { get; set; } 
        /// <summary>
        /// 제품사진URL
        /// </summary>
        [Column("PROD_FILE_URL")] public string ProdFileUrl { get; set; } 
        /// <summary>
        /// 포장단위수량
        /// </summary>
        [Column("PACK_QTY")] public decimal? PackQty { get; set; } 
        /// <summary>
        /// 포장비닐코드
        /// </summary>
        [Column("PACK_CODE")] public string PackCode { get; set; }  
        /// <summary>
        /// 출하박스코드
        /// </summary>
        [Column("OUT_BOX")] public string OutBox { get; set; }  
        /// <summary>
        /// 셋팅시간(분)
        /// </summary>
        [Column("SET_TIME")] public decimal? SetTime { get; set; } 
        /// <summary>
        /// 생성시간(초)
        /// </summary>
        [Column("PROC_TIME")] public decimal? ProcTime { get; set; }  
        /// <summary>
        /// 열처리온도
        /// </summary>
        [Column("HEAT")] public string Heat { get; set; } 
        /// <summary>
        /// 열처리속도
        /// </summary>
        [Column("RPM")] public string Rpm { get; set; }
        /// <summary>
        /// 툴코드
        /// </summary>
        [Column("TOOL_CODE")] public string ToolCode { get; set; }
        /// <summary>
        /// 툴수명수량
        /// </summary>
        [Column("TOOL_LIFE_QTY")] public decimal? ToolLifeQty { get; set; }
        /// <summary>
        /// 툴코드2
        /// </summary>
        [Column("TOOL_CODE2")] public string ToolCode2 { get; set; }
        /// <summary>
        /// 툴수명수량2
        /// </summary>
        [Column("TOOL_LIFE_QTY2")] public decimal? ToolLifeQty2 { get; set; }
        /// <summary>
        /// 공정포장수량
        /// </summary>
        [Column("PROCESS_PACK_QTY")] public decimal? ProcessPackQty { get; set; }
        /// <summary>
        /// AQL
        /// </summary>
        [Column("AQL")] public string AQL { get; set; }
        /// <summary>
        /// 사용여부
        /// </summary>
        [Column("USE_FLAG"), Required(ErrorMessage = "UseFlag")] public string UseFlag { get; set; }  
        /// <summary>
        /// 메모
        /// </summary>
        [Column("MEMO")] public string Memo { get; set; }  //메모
        /// <summary>
        /// 임시
        /// </summary>
        [Column("TEMP")] public string Temp { get; set; }  //임시
        /// <summary>
        /// 임시1
        /// </summary>
        [Column("TEMP1")] public string Temp1 { get; set; }  //임시1
        /// <summary>
        /// 파일폴더KEY명
        /// </summary>
        [Column("TEMP2")] public string FileFolderKeyName { get; set; }  //파일폴더KEY명

        [NotMapped] public object localImage { get; set; }

        [NotMapped]
        public string CombineSpec
        {
            get
            {
                var spec1 = Spec1 == null ? "" : Spec1;
                var spec2 = Spec2 == null ? "" : Spec2;
                var spec3 = Spec3 == null ? "" : Spec3;
                var spec4 = Spec4 == null ? "" : Spec4;
                return spec1 + spec2 + spec3 + spec4;
            }
        }

        /// <summary>
        /// 스페어파트 재고수량
        /// </summary>
        [NotMapped]
        public decimal SpareStockQty
        {
            get
            {
                if (TN_MEA1201List.Count == 0)
                    return 0;
                else
                {
                    var inQty = TN_MEA1201List.Where(p => p.Division == "01").Sum(c => c.Qty); //입고수량
                    var outQty = TN_MEA1201List.Where(p => p.Division == "02").Sum(c => c.Qty); //출고수량
                    var adjustQty = TN_MEA1201List.Where(p => p.Division == "03").Sum(c => c.Qty); //조정수량
                    return inQty - outQty + adjustQty;
                }
            }
        }

        /// <summary>
        /// 툴 재고수량
        /// </summary>
        [NotMapped]
        public decimal ToolStockQty
        {
            get
            {
                if (TN_TOOL1001List.Count == 0)
                    return 0;
                else
                {
                    var inQty = TN_TOOL1001List.Where(p => p.Division == "01").Sum(c => c.Qty); //입고수량
                    var outQty = TN_TOOL1001List.Where(p => p.Division == "02").Sum(c => c.Qty); //출고수량
                    var adjustQty = TN_TOOL1001List.Where(p => p.Division == "03").Sum(c => c.Qty); //조정수량
                    return inQty - outQty + adjustQty;
                }
            }
        }
        
        [ForeignKey("SrcCode")]
        public virtual TN_STD1100 TN_STD1100_SRC { get; set; }

        [ForeignKey("PackCode")]
        public virtual TN_STD1100 TN_STD1100_PACK_PLASTIC { get; set; }

        [ForeignKey("OutBox")]
        public virtual TN_STD1100 TN_STD1100_OUT_BOX { get; set; }

        [ForeignKey("ToolCode")]
        public virtual TN_STD1100 TN_STD1100_TOOL { get; set; }

        [ForeignKey("ToolCode2")]
        public virtual TN_STD1100 TN_STD1100_TOOL2 { get; set; }

        [ForeignKey("MainCustomerCode")]
        public virtual TN_STD1400 TN_STD1400 { get; set; }

        public virtual ICollection<TN_STD1101> TN_STD1101List { get; set; }
        public virtual ICollection<TN_STD1102> TN_STD1102List { get; set; }
        public virtual ICollection<TN_STD1103> TN_STD1103List { get; set; }
        public virtual ICollection<TN_STD1104> TN_STD1104List { get; set; }
        public virtual ICollection<TN_QCT1000> TN_QCT1000List { get; set; }
        public virtual ICollection<TN_QCT1400> TN_QCT1400List { get; set; }
        public virtual ICollection<TN_MPS1000> TN_MPS1000List { get; set; }
        public virtual ICollection<TN_TRY1000> TN_TRY1000List { get; set; }
        public virtual ICollection<TN_MEA1201> TN_MEA1201List { get; set; }
        public virtual ICollection<TN_TOOL1001> TN_TOOL1001List { get; set; }
    }
}