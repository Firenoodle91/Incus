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
    [Table("VI_STD1100")]
    public class VI_STD1100
    {


        /// <summary>
        /// 품목코드
        /// </summary>
        [Key, Column("ITEM_CODE"), Required(ErrorMessage = "ItemCode")] public string ItemCode { get; set; }
        /// <summary>
        /// 품번
        /// </summary>
        [Column("ITEM_NAME"), Required(ErrorMessage = "ItemName")] public string ItemName { get; set; }
        /// <summary>
        /// 품목명
        /// </summary>
        [Column("ITEM_NAME1"), Required(ErrorMessage = "ItemName1")] public string ItemName1 { get; set; }
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
        /// 원자재명
        /// </summary>
        [Column("SRC_NAME")] public string SrcName { get; set; }
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
        /// 금형코드
        /// </summary>
        [Column("MOLD_CODE")] public string MoldCode { get; set; }
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
        [Column("MACHINEMOLDCHECK")] public string MachineMoldCheck { get; set; }   // 설비 금형 구분

        [NotMapped] public object localImage { get; set; }

        [NotMapped]
        public string CombineSpec
        {
            get
            {
                var spec1 = Spec1 == null ? " " : Spec1;
                var spec2 = Spec2 == null ? " " : Spec2;
                var spec3 = Spec3 == null ? " " : Spec3;
                var spec4 = Spec4 == null ? " " : Spec4;
                return spec1 + spec2 + spec3 + spec4;
            }
        }

    }
}
