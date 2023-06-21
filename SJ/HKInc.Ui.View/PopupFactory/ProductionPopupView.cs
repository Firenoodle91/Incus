using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.View.PopupFactory
{
    enum ProductionPopupView
    {
        #region SYS
        Module,
        User,
        Screen,
        Menu,
        UserGroup,
        CodeSql,
        LicenseKeyEdit,
        MenuOpenLog,
        #endregion

        #region SELECT POPUP
        /// <summary>
        /// 권한그룹 SELECT 팝업
        /// </summary>
        UserGroupSelectList,
        /// <summary>
        /// 부서 SELECT 팝업
        /// </summary>
        DepartmentSelect,
        /// <summary>
        /// 사용자 SELECT 팝업
        /// </summary>
        UserSelectList,
        /// <summary>
        /// 품목 SELECT 팝업
        /// </summary>
        SELECT_STD1100,
        /// <summary>
        /// 자재발주마스터 SELECT 팝업
        /// </summary>
        SELECT_PUR1100,
        /// <summary>
        /// 자재발주디테일 SELECT 팝업
        /// </summary>
        SELECT_PUR1101,
        /// <summary>
        /// 납품계획 SELECT 팝업
        /// </summary>
        SELECT_ORD1100,
        /// <summary>
        /// 외주발주디테일 SELECT 팝업
        /// </summary>
        SELECT_PUR1401,
        /// <summary>
        /// 제품재고 SELECT 팝업
        /// </summary>
        SELECT_PROD_STOCK,
        /// <summary>
        /// 반제품재고 SELECT 팝업
        /// </summary>
        SELECT_BANPROD_STOCK,
        /// <summary>
        /// 출고증 SELECT 팝업
        /// </summary>
        SELECT_ORD1101,
        /// <summary>
        /// 외주발주 SELECT 팝업
        /// </summary>
        SELECT_PUR1400,
        /// <summary>
        /// 품질 다른품목 복사 SELECT 팝업
        /// </summary>
        SELECT_QCT_ITEM_COPY,
        /// <summary>
        /// 품질 선택 검사구분 복사 SELECT 팝업
        /// </summary>
        SELECT_QCT_CHECK_DIVISION_COPY,
        /// <summary>
        /// 공정타입추가팝업창
        /// </summary>
        XFSMPS1000,
        #endregion

        #region MES
        /// <summary>
        /// 부서관리 팝업
        /// </summary>
        PFSTD1200,
        /// <summary>
        /// 품목기준정보 팝업
        /// </summary>
        PFSTD1100,
        /// <summary>
        /// 거래처관리 팝업
        /// </summary>
        PFSTD1400,
        /// <summary>
        /// 4M관리대장 팝업
        /// </summary>
        PFStd4M001,
        /// <summary>
        /// 설비관리 팝업
        /// </summary>
        PFMEA1000,
        /// <summary>
        /// 수주마스터 팝업
        /// </summary>
        PFORD1000,
        /// <summary>
        /// 개발의뢰마스터 팝업
        /// </summary>
        PFORD1000_DEV,
        /// <summary>
        /// 계측기관리 팝업
        /// </summary>
        PFMEA1100,
        /// <summary>
        /// 클레임관리 팝업
        /// </summary>
        PFQCT1400,
        /// <summary>
        /// 검사의뢰 팝업
        /// </summary>
        PFDEV1000,
        /// <summary>
        /// 구매현황 팝업
        /// </summary>
        SELECT_PUR_STATUS,
        /// <summary>
        /// 수입검사대상 팝업
        /// </summary>
        SELECT_QCT_IN,
        /// <summary>
        /// 초중종물검사대상 팝업
        /// </summary>
        SELECT_QCT_FME,
        /// <summary>
        /// 자주검사 팝업
        /// </summary>
        SELECT_QCT_FREQNTLY,
        /// <summary>
        /// 공정검사 팝업
        /// </summary>
        SELECT_QCT_PROCESS,
        /// <summary>
        /// 설정검사 팝업
        /// </summary>
        SELECT_QCT_SETTING,
        /// <summary>
        /// 출하검사 팝업
        /// </summary>
        SELECT_QCT_SHIPMENT,
        /// <summary>
        /// 재입고 팝업
        /// </summary>
        PFPUR1200,
        /// <summary>
        /// 외주발주 팝업
        /// </summary>
        SELECT_MPS1200,
        /// <summary>
        /// 작업지시참조 팝업
        /// </summary>
        XSFWORK_REF,
        /// <summary>
        /// 수주현황 팝업
        /// </summary>
        SELECT_ORD_STATUS,
        /// <summary>
        /// 포장POP - 작업시작
        /// </summary>
        XPFITEMMOVESCAN_START_PACK,
        /// <summary>
        /// 포장POP - 실적등록
        /// </summary>
        XPFRESULT_PACK,
        /// <summary>
        /// 포장라벨출력/박스라벨출력
        /// </summary>
        XPFPACK_BARCODE_PRINT,
        /// <summary>
        /// 반제품 재입고
        /// </summary>
        PFBAN1000,
        /// <summary>
        /// 납기회의 상세보기
        /// </summary>
        XPFORD1102,
        /// <summary>
        /// 완제품입고관리 창고변경 팝업
        /// </summary>
        PFMPS1800,
        /// <summary>
        /// 완제품입고관리 현 창고재고변경 팝업
        /// </summary>
        PFMPS1801,
        /// <summary>
        /// 자재 재입고 수정 팝업
        /// </summary>
        PFPUR1302,
        /// <summary>
        /// 반제품 재입고 수정 팝업
        /// </summary>
        PFBAN1102,
        /// <summary>
        /// 포장POP - 이동표교체
        /// </summary>
        XPFITEMMOVESCAN_CHANGE_PACK,
        /// <summary>
        /// 분할처리
        /// </summary>
        XPFPUR1201,
        /// <summary>
        /// 자재 반품
        /// </summary>
        PFPUR1203,
        /// <summary>
        /// 다른설비점검기준복사
        /// </summary>
        SELECT_MACHINE_CHECK_COPY,
        XPFSTD1100_BARCODE_PRINT,
        #endregion

        #region POP
        /// <summary>
        /// 작업시작 (자재투입)
        /// </summary>
        XPFSRCIN_START,
        /// <summary>
        /// 작업시작 (이동표)
        /// </summary>
        XPFITEMMOVESCAN_START,
        /// <summary>
        /// 실적등록 (기본)
        /// </summary>
        XPFRESULT_DEFAULT,
        /// <summary>
        /// 실적등록 (기본_툴연마)
        /// </summary>
        XPFRESULT_TOOL,
        /// <summary>
        /// 실적등록 (기본_열처리)
        /// </summary>
        XPFRESULT_HEAT,
        /// <summary>
        /// 실적등록 (기본_열처리_툴연마)
        /// </summary>
        XPFRESULT_HEAT_TOOL,
        /// <summary>
        /// 이동표출력
        /// </summary>
        XPFITEMMOVEPRINT,
        /// <summary>
        /// 원자재교체
        /// </summary>
        XPFSRCIN_CHANGE,
        /// <summary>
        /// 이동표교체
        /// </summary>
        XPFITEMMOVESCAN_CHANGE,
        /// <summary>
        /// 작업종료
        /// </summary>
        XPFWORKEND,
        /// <summary>
        /// 작업시작 (반제품투입)
        /// </summary>
        XPFBANIN_START,
        /// <summary>
        /// 반제품교체
        /// </summary>
        XPFBANIN_CHANGE,
        /// <summary>
        /// 이동표 박스내수량
        /// </summary>
        XPFITEMMOVEPRINT_BOX,
        /// <summary>
        /// 포장 작업완료 시
        /// </summary>
        XPFPACK_END,
        /// <summary>
        /// 작업시작 (최종검사)
        /// </summary>
        XPFITEMMOVESCAN_START_INSP_FINAL,
        /// <summary>
        /// 이동표교체 (최종검사)
        /// </summary>
        XPFITEMMOVESCAN_CHANGE_INSP_FINAL,
        XPFFIFO
        #endregion
    }
}
