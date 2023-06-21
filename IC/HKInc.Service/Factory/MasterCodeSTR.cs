using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Factory
{
    public static class MasterCodeSTR
    {
        #region A~
        /// <summary>
        /// AQL
        /// </summary>
        public static string AQL = "AQL";
        /// <summary>
        /// 승인상태
        /// </summary>
        public static string ApprovalState = "A001";
        /// <summary>
        /// 교육구분
        /// </summary>
        public static string EduFlag = "A002";
        #endregion

        #region B~
        /// <summary>
        /// 단위
        /// </summary>
        public static string Unit = "B001";
        /// <summary>
        /// 불량유형(POP)
        /// </summary>
        public static string BadType_POP = "B002";

        #endregion

        #region C~
        /// <summary>
        /// 거래처구분
        /// </summary>
        public static string CustomerType = "C002"; 
        /// <summary>
        /// 마감일
        /// </summary>
        public static string DeadLine = "C003";
        /// <summary>
        /// 설비등급
        /// </summary>
        public static string MachineClass = "C004";
        /// <summary>
        /// 2021-05-28 김진우 주임 추가
        /// 검사구종류
        /// </summary>
        public static string CheckerFixtureKind = "C006";
        #endregion

        #region D~
        /// <summary>
        /// 입출고 구분
        /// </summary>
        public static string InOutDivision = "D001";
        /// <summary>
        /// 마감여부
        /// </summary>
        public static string DeadLineDivision = "D002";
        #endregion

        #region E~
        #endregion

        #region F~
        /// <summary>
        /// FTP정보
        /// </summary>
        public static string FTP = "F001";
        #endregion

        #region G~
        /// <summary>
        /// 부적합등급
        /// </summary>
        public static string BadReportGrade = "G001";
        #endregion

        #region H~
        #endregion

        #region I~
        #endregion

        #region J~
        #endregion

        #region K~
        #endregion

        #region L~
        #endregion

        #region M~
        /// <summary>
        /// 품목구분
        /// </summary>
        public static string ItemType = "M001";
        /// <summary>
        /// 설비제조사
        /// </summary>
        public static string MachineMaker = "MC01";
        /// <summary>
        /// 설비점검주기
        /// </summary>
        public static string MachineCheckCycle = "MC02";
        /// <summary>
        /// 설비이력점검구분
        /// </summary>
        public static string MachineCheckDivision = "MC03";
        /// <summary>
        /// 설비수리내역
        /// </summary>
        public static string MachineRepairHistory = "MC04";
        /// <summary>
        /// 계측기제작사
        /// </summary>
        public static string InstrMaker = "MC05";
        /// <summary>
        /// 계측기교정주기
        /// </summary>
        public static string InstrCorTurn = "MC06";
        /// <summary>
        /// 계측기검교정구분
        /// </summary>
        public static string InstrCheckDivision = "MC07";
        /// <summary>
        /// 설비점검위치
        /// </summary>
        public static string MachineCheckPosition = "MC08";
        /// <summary>
        /// 설비점검항목
        /// </summary>
        public static string MachineCheckList = "MC09";
        /// <summary>
        /// 설비점검방법
        /// </summary>
        public static string MachineCheckWay = "MC10";
        /// <summary>
        /// 설비점검기준일
        /// </summary>
        public static string MachineCheckStandardDate = "MC11";
        /// <summary>
        /// 설비모델
        /// </summary>
        public static string MachineModel = "MC12";
        /// <summary>
        /// 설비그룹
        /// </summary>
        public static string MachineGroup = "MC13";
        /// <summary>
        /// 설비점검(O,X)
        /// </summary>
        public static string MachineCheckOX = "MC14";
        /// <summary>
        /// 설비점검구분
        /// </summary>
        public static string MachineStandardCheckDivision = "MC15";
        /// <summary>
        /// 계측기종류
        /// </summary>
        public static string InspectionType = "MC16";
        /// <summary>
        /// 2021-05-26 김진우 주임 추가
        /// 설비금형구분
        /// </summary>
        public static string MachineMoldCheck = "MC17";
        /// <summary>
        /// 20210606 오세완 차장
        /// 설비 가동 / 비가동 기준코드
        /// </summary>
        public static string Machine_RunStopCode = "MC19";
        /// <summary>        
        /// 금형입출고구분
        /// </summary>
        public static string MoldInOut = "MO00";
        /// <summary>        
        /// 금형수리방법
        /// </summary>
        public static string MoldRepiar = "MO01";
        /// <summary>        
        /// 금형위치 
        /// </summary>
        public static string MoldPosition = "MO02";
        /// <summary>        
        /// 금형등급
        /// </summary>
        public static string MoldClass = "MO03";
        /// <summary>        
        /// 금형제조사
        /// </summary>
        public static string MoldMakercust = "MO04";
        /// <summary>        
        /// 금형점검주기
        /// </summary>
        public static string MoldCheckCycle = "MO05";
        /// <summary>
        /// 금형점검구분
        /// </summary>
        public static string MoldReqType = "MO06";
        /// <summary>        
        /// 금형점검위치
        /// </summary> 
        public static string MoldCheckPosition = "MO07";
        /// <summary>        
        /// 금형점검항목
        public static string MoldCheckList = "MO08";
        /// <summary>        
        /// 금형점검방법
        /// </summary> 
        public static string MoldCheckWay = "MO09";

        /// <summary>        
        /// 20210604 오세완 차장
        /// 금형일상점검 중 육안검사 O,X 판정
        /// </summary> 
        public static string MoldCheckOX = "MO10";        
        /// <summary>        
        /// 금형이상유무
        /// </summary> 
        public static string MoldWeird = "MO11";
        /// <summary>        
        /// 금형평가항목
        /// </summary> 
        public static string MoldEvolItem = "MO12";
        /// <summary>        
        /// 금형이상유무
        /// </summary> 
        public static string MoldDataType = "MO13";
        /// <summary>
        /// 2021-05-28 김진우 주임 추가
        /// 설비등급평가항목
        /// </summary>
        public static string MachineGradeEvaluationList = "MC18";
        #endregion

        #region N~
        /// <summary>
        /// 국가
        /// </summary>
        public static string NationalCode = "N001";
        #endregion

        #region O~
        #endregion

        #region P~
        /// <summary>
        /// 공정
        /// </summary>
        public static string Process = "P001";
        /// <summary>
        /// SPC구분
        /// </summary>
        public static string SpcDivision = "P002";
        
        #endregion

        #region Q~
        /// <summary>
        /// 검사항목
        /// </summary>
        public static string InspectionItem = "Q001";
        /// <summary>
        /// 검사구분
        /// </summary>
        public static string InspectionDivision = "Q002";
        /// <summary>
        /// 검사방법
        /// </summary>
        public static string InspectionWay = "Q003";
        /// <summary>
        /// 클레임유형
        /// </summary>
        public static string ClaimType = "Q004";
        /// <summary>
        /// 측정기코드
        /// </summary>
        public static string InstrumentCode = "Q005";
        /// <summary>
        /// 검사데이터타입
        /// </summary>
        public static string InspectionDataType = "Q006";
        /// <summary>
        /// 검사시점
        /// </summary>
        public static string CheckPoint = "Q007";
        /// <summary>
        /// 검사위치
        /// </summary>
        public static string InspectionCheckPosition = "Q008";
        /// <summary>
        /// 초중종 구분
        /// </summary>
        public static string InspectionFME = "Q009";
        /// <summary>
        /// 불량품처리 -부적합관리 컬럼
        /// </summary>
        public static string BadHandling = "Q010";
        /// <summary>
        /// 대책요구(대책, 불필요) - 부적합관리 컬럼
        /// </summary>
        public static string MeasureRequest = "Q011";
        /// <summary>
        /// 비용공제(공제, 미공제) - 부적합관리 컬럼
        /// </summary>
        public static string DeductionCost = "Q012";
        /// <summary>
        /// 발생시기 -부적합관리 컬럼
        /// </summary>
        public static string OccurMoment = "Q013";
        /// <summary>
        /// 발생구분 - 부적합관리 컬럼
        /// </summary>
        public static string OccurDivision = "Q014";
        /// <summary>
        /// 재발횟수 - 부적합관리 컬럼
        /// </summary>
        public static string OccurQty = "Q015";

        /// <summary>
        /// 20210621 오세완 차장
        /// 초중종물 검사 결과 저장시 사용되는 값
        /// </summary>
        public static string InspectionFME_POP = "Q016";
        /// <summary>
        /// 검사결과(OK,NG)
        /// </summary>
        public static string Judge_OKNG = "Q100";
        /// <summary>
        /// 작업설정검사 (필수 시 사용여부 체크)
        /// </summary>
        public static string JobSettingFlag = "Q101";
        /// <summary>
        /// 시료수
        /// </summary>
        public static string InspectionReadingNumber = "Q102";

        /// <summary>
        /// 20210603 오세완 차장 재가동TO
        /// </summary>
        public static string Inspection_RestartTO = "Q06";
        #endregion

        #region R~
        /// <summary>
        /// 직급
        /// </summary>
        public static string RankCode = "R001";
        /// <summary>
        /// 자사/타사
        /// </summary>
        public static string MainYn = "R002";
        /// 거래처발주확인
        /// </summary>
        public static string CustomerConfirm = "R003";
        /// <summary>
        /// 검사성적서타입
        /// </summary>
        public static string InspectionReportType = "RP01";
        #endregion

        #region S~
        /// <summary>
        /// 표면처리항목
        /// </summary>
        public static string SurfaceList = "S001";
        /// <summary>
        /// 표준작업소요일
        /// </summary>
        public static string StdWorkDay = "S002";
        /// <summary>
        /// 비가동유형
        /// </summary>
        public static string StopType = "S003";
        /// <summary>
        /// 납기회의 상태
        /// </summary>
        public static string DelivConferenceStates = "S004";
        #endregion

        #region T~
        /// <summary>
        /// 제조팀
        /// </summary>
        public static string ProductTeamCode = "T001";
        #endregion

        #region U~
        #endregion

        #region W~
        /// <summary>
        /// 위치코드(열1)
        /// </summary>
        public static string PositionColumn1 = "W001";
        /// <summary>
        /// 위치코드(열3,4)
        /// </summary>
        public static string PositionColumn3_4 = "W002";
        /// <summary>
        /// 작업상태
        /// </summary>
        public static string JobStates = "W003";
        /// <summary>
        /// 창고구분
        /// </summary>
        public static string WhCodeDivision = "W004";
        /// <summary>
        /// 위치코드(열2)
        /// </summary>
        public static string PositionColumn2 = "W005";
        /// <summary>
        /// 2021-05-28 김진우 주임 추가
        /// 용접지그종류
        /// </summary>
        public static string WeldingJigKind = "W006";
        #endregion

        #region X~
        #endregion

        #region Y~
        #endregion

        #region Z~
        /// <summary>
        /// 자재입고상태
        /// </summary>
        public static string MaterialInConfirmFlag = "Z001";
        /// <summary>
        /// 수입검사필수여부
        /// </summary>
        public static string StockInspFlag = "Z003";
        #endregion

        #region ETC
        /// <summary>
        /// 대분류-완제품
        /// </summary>
        public static string TopCategory_WAN = "A00";
        /// <summary>
        /// 20210524 오세완 차장
        /// 대분류-반제품 => 반제품(자사)로 변경
        /// </summary>
        public static string TopCategory_BAN = "A01";
        /// <summary>
        /// 대분류-원자재
        /// </summary>
        public static string TopCategory_MAT = "A02";
        /// <summary>
        /// 대분류-소모품
        /// </summary>
        public static string TopCategory_BU = "A03";
        /// <summary>
        /// 대분류-설비스페어파트
        /// </summary>
        public static string TopCategory_SPARE = "A04";
        /// <summary>
        /// 대분류-툴
        /// </summary>
        public static string TopCategory_TOOL = "A05";
        /// <summary>
        /// 20210524 오세완 차장
        /// 반제품 작업지시 생성때문에 대분류를 반제품(외주)로 구분하게 되었다. 
        /// 외주는 작업지시를 생성하지 않는 제품
        /// </summary>
        public static string TopCategory_BAN_Outsourcing = "A06";
        /// <summary>
        /// 2021-05-26 김진우 주임 추가
        /// 금형과 설비 구분을 위해 금형 추가
        /// </summary>
        public static string TopCategory_Mold = "A07";

        /// <summary>
        /// 20220119 오세완 차장
        /// 이태식 차장이 요청하여 부자재를 추가
        /// </summary>
        public static string TopCategory_Subpart = "A09";
        /// <summary>
        /// 중분류-포장비닐
        /// </summary>
        public static string MiddleCategory_PackPlastic = "A0301";
        /// <summary>
        /// 중분류-출하박스
        /// </summary>
        public static string MiddleCategory_OutBox = "A0302";
        /// <summary>
        /// 제품 Constraint
        /// </summary>
        public static string Contraint_ItemWAN = "WAN";
        /// <summary>
        /// 반제품 Constraint
        /// </summary>
        public static string Contraint_ItemBAN = "BAN";
        /// <summary>
        /// 제품&반제품 Constraint
        /// </summary>
        public static string Contraint_ItemWAN_BAN = "WAN&BAN";
        /// <summary>
        /// 자재 Constraint
        /// </summary>
        public static string Contraint_ItemMAT = "MAT";
        /// <summary>
        /// 소모품 Constraint
        /// </summary>
        public static string Contraint_ItemBU = "BU";
        /// <summary>
        /// 자재&소모품 Constraint
        /// </summary>
        public static string Contraint_ItemMAT_BU = "MAT&BU";
        /// <summary>
        /// 자재&반제품 Constraint
        /// </summary>
        public static string Contraint_ItemMAT_BAN = "MAT&BAN";
        /// <summary>
        /// 20220208 오세완 차장
        /// 케이즈이노텍과 동일하게, 완제품,반제품,툴,스페어파트 제외한 나머지 
        /// </summary>
        public static string Contraint_ItemMULTIPLE = "MULTIPLE";
        /// <summary>
        /// 툴 Constraint
        /// </summary>
        public static string Contraint_ItemTOOL = "TOOL";
        /// <summary>
        /// 스페어파트 Constraint
        /// </summary>
        public static string Contraint_ItemSPARE = "SPARE";
        /// <summary>
        /// 공정-열처리
        /// </summary>
        public static string Process_Heat = "P98";
        /// <summary>
        /// 공정-포장
        /// </summary>
        public static string Process_Packing = "P100";
        /// <summary>
        /// 공정-리워크
        /// </summary>
        public static string Process_Rework = "P111";

        /// <summary>
        /// 20210611 오세완 차장
        /// 공정 중 프레스공정
        /// </summary>
        public static string Process_Press = "P04";
        /// <summary>
        /// 작업상태-대기
        /// </summary>
        public static string JobStates_Wait = "W01";
        /// <summary>
        /// 작업상태-진행
        /// </summary>
        public static string JobStates_Start = "W02";
        /// <summary>
        /// 작업상태-비가동
        /// </summary>
        public static string JobStates_Stop = "W03";
        /// <summary>
        /// 작업상태-완료
        /// </summary>
        public static string JobStates_End = "W04";
        /// <summary>
        /// 작업상태-외주진행
        /// </summary>
        public static string JobStates_OutStart = "W05";
        /// <summary>
        /// 작업상태-외주완료
        /// </summary>
        public static string JobStates_OutEnd = "W06";
        /// <summary>
        /// 작업상태-일시정지
        /// </summary>
        public static string JobStates_Pause = "W07";
        /// <summary>
        /// 작업상태-리워크진행
        /// </summary>
        public static string JobStates_ReworkStart = "W08";
        /// <summary>
        /// 작업상태-리워크완료
        /// </summary>
        public static string JobStates_ReworkEnd = "W09";
        /// <summary>
        /// 입출고 구분 - 입고
        /// </summary>
        public static string InOutDivision_In = "01";
        /// <summary>
        /// 입출고 구분 - 출고
        /// </summary>
        public static string InOutDivision_Out = "02";
        /// <summary>
        /// 입출고 구분 - 조정
        /// </summary>
        public static string InOutDivision_Adjust = "03";
        /// <summary>
        /// 설비점검방법-치수(검사방법구분을위함)
        /// </summary>
        public static string MachineCheckWay_CheckFlag = "03";
        /// <summary>
        /// 설비점검방법-메모(검사방법구분을위함)
        /// </summary>
        public static string MachineCheckWay_Memo = "04";
        /// <summary>
        /// 검사구분-수입검사
        /// </summary>
        public static string InspectionDivision_IN = "Q01";
        /// <summary>
        /// 검사구분-자주검사
        /// </summary>
        public static string InspectionDivision_Frequently = "Q02";
        /// <summary>
        /// 검사구분-초중종검사
        /// </summary>
        public static string InspectionDivision_FME = "Q03";
        /// <summary>
        /// 검사구분-공정검사
        /// </summary>
        public static string InspectionDivision_Process = "Q04";
        /// <summary>
        /// 검사구분-출하검사(성적서)
        /// </summary>
        public static string InspectionDivision_Shipment = "Q05";
        /// <summary>
        /// 검사구분-작업설정검사
        /// </summary>
        public static string InspectionDivision_Setting = "Q06";
        /// <summary>
        /// 검사구분-최종검사
        /// </summary>
        public static string InspectionDivision_Final = "Q07";
        /// <summary>
        /// 검사방법-육안검사
        /// </summary>
        public static string InspectionWay_Eye = "QT1";
        /// <summary>
        /// 검사방법-치수검사
        /// </summary>
        public static string InspectionWay_Input = "QT2";
        /// <summary>
        /// 검사데이터타입-C
        /// </summary>
        public static string CheckDataType_C = "C";
        /// <summary>
        /// 검사시점-일반
        /// </summary>
        public static string CheckPoint_General = "01";
        /// <summary>
        /// 검사시점-선진에서는 초중종으로 통일 초물
        /// </summary>
        public static string CheckPoint_First = "02";
        /// <summary>
        /// 검사시점-선진에서는 초중종으로 통일 중물
        /// </summary>
        public static string CheckPoint_Middle = "03";
        /// <summary>
        /// 검사시점-선진에서는 초중종으로 통일 종물
        /// </summary>
        public static string CheckPoint_End = "04";
        /// <summary>
        /// 자재입고상태 - 대기
        /// </summary>
        public static string MaterialInConfirmFlag_Wait = "01";
        /// <summary>
        /// 자재입고상태 - 진행중
        /// </summary>
        public static string MaterialInConfirmFlag_Proceeding = "02";
        /// <summary>
        /// 자재입고상태 - 완료
        /// </summary>
        public static string MaterialInConfirmFlag_End = "03";
        /// <summary>
        /// 마감여부 - 마감처리
        /// </summary>
        public static string DeadLineDivision_DeadFinish = "01";
        /// <summary>
        /// 마감여부 - 마감처리취소
        /// </summary>
        public static string DeadLineDivision_DeadFinishCancel = "02";
        /// <summary>
        /// 마감여부 - 마감확정
        /// </summary>
        public static string DeadLineDivision_DeadConfirm = "03";
        /// <summary>
        /// 마감여부 - 마감확정취소
        /// </summary>
        public static string DeadLineDivision_DeadConfirmCancel = "04";
        /// <summary>
        /// 마감여부 - 마감삭제
        /// </summary>
        public static string DeadLineDivision_DeadRemove = "05";
        /// <summary>
        /// 완제품 기본 창고 코드
        /// </summary>
        public static string WAN_WhCode_DefaultCode = "WH-00001";
        /// <summary>
        /// 완제품 기본 위치 코드
        /// </summary>
        public static string WAN_PositionCode_DefaultCode = "01-임시";
        /// <summary>
        /// 원자재 기본 창고 코드
        /// </summary>
        public static string MAT_WhCode_DefaultCode = "WH-00002";
        /// <summary>
        /// 원자재 기본 위치 코드
        /// </summary>
        public static string MAT_PositionCode_DefaultCode = "02-임시";
        /// <summary>
        /// 창고구분 - 완제품창고
        /// </summary>
        public static string WhCodeDivision_WAN = "01";
        /// <summary>
        /// 창고구분 - 반제품창고
        /// </summary>
        public static string WhCodeDivision_BAN = "02";
        /// <summary>
        /// 창고구분 - 원자재창고
        /// </summary>
        public static string WhCodeDivision_MAT = "03";
        /// <summary>
        /// 창고구분 - 소모품창고
        /// </summary>
        public static string WhCodeDivision_BU = "04";
        /// <summary>
        /// 창고구분 - 턴키창고
        /// </summary>
        public static string WhCodeDivision_TURN = "05";
        /// <summary>
        /// 창고구분 - 금형창고
        /// </summary>
        public static string WhCodeDivision_MOLD = "06";
        /// <summary>
        /// 거래처구분 - 매출처
        /// </summary>
        public static string CustType_Sales = "A00";
        /// <summary>
        /// 거래처구분 - 매입처
        /// </summary>
        public static string CustType_Purchase = "A01";
        /// <summary>
        /// 거래처구분 - 기타
        /// </summary>
        public static string CustType_Etc = "A02";
        /// <summary>
        /// 설비이력점검구분_예방보전
        /// </summary>
        public static string MachineCheckDivision_Maintenance = "05";
        /// <summary>
        /// 설비점검구분_일일점검
        /// </summary>
        public static string MachineStandardCheckDivision_Daily = "01";
        /// <summary>
        /// 설비점검구분_예방보전
        /// </summary>
        public static string MachineStandardCheckDivision_Maintenance = "02";
        /// <summary>
        /// 설비점검위치_팀장확인여부
        /// </summary>
        public static string MachineCheckPosition_ConfirmFlag = "99";
        /// <summary>
        /// 권한그룹관리_기술자
        /// </summary>
        public static decimal UserGroup_Technician = 12;
        /// <summary>
        /// 권한그룹관리_공정검사자
        /// </summary>
        public static decimal UserGroup_ProcessInspection = 13;
        /// <summary>
        /// 권한그룹관리_수입검사자
        /// </summary>
        public static decimal UserGroup_IN_Inspection = 14;
        /// <summary>
        /// 권한그룹관리_최종검사자
        /// </summary>
        public static decimal UserGroup_FinalInspection = 15;
        /// <summary>
        /// 권한그룹관리_생산1팀
        /// </summary>
        public static decimal UserGroup_Product1 = 4;
        /// <summary>
        /// 권한그룹관리_생산2팀
        /// </summary>
        public static decimal UserGroup_Product2 = 5;
        /// <summary>
        /// 부서_생산부
        /// </summary>
        public static string DeptProd = "DEPT-00003";
        /// <summary>
        /// 부서_연구소
        /// </summary>
        public static string DeptRsrch = "DEPT-00008";
        /// <summary>
        /// 부서_영업부
        /// </summary>
        public static string DeptSales = "DEPT-00009";
        /// <summary>
        /// 부서_구매부
        /// </summary>
        public static string DeptPur = "DEPT-00010";
        /// <summary>
        /// 승인상태_대기
        /// </summary>
        public static string ApprovalWait = "01";
        /// <summary>
        /// 승인상태_진행
        /// </summary>
        public static string ApprovalIng = "02";
        /// <summary>
        /// 승인상태_완료
        /// </summary>
        public static string ApprovalFinish = "03";
        /// <summary>
        /// 반품
        /// </summary>
        public static string Return = "B01";

        #endregion

        #region FTP 폴더명
        /// <summary>
        /// FTP-제품사진폴더명
        /// </summary>
        public static string FtpFolder_ProdImage = "PROD_IMAGE";
        /// <summary>
        /// FTP-제품도면폴더명
        /// </summary>
        public static string FtpFolder_DesignImage = "DESIGN_IMAGE";
        /// <summary>
        /// FTP-제품한도폴더명
        /// </summary>
        public static string FtpFolder_ItemLimitImage = "LIMIT_IMAGE";
        /// <summary>
        /// FTP-설비사진폴더명
        /// </summary>
        public static string FtpFolder_MachineImage = "MC_IMAGE";
        /// <summary>
        /// FTP-설비점검포인트폴더명
        /// </summary>
        public static string FtpFolder_MachineCheckPoint = "MC_CHECK_POINT";
        /// <summary>
        /// FTP-설비예방보전점검폴더명
        /// </summary>
        public static string FtpFolder_MachineMaintenance = "MC_MAINTENANCE";
        /// <summary>
        /// FTP-계측기사진폴더명
        /// </summary>
        public static string FtpFolder_InstrImage = "INSTR_IMAGE";
        /// <summary>
        /// FTP-계측기검교정이력폴더명
        /// </summary>
        public static string FtpFolder_InstrCheckHistory = "INSTR_CHKLIST";
        /// <summary>
        /// 2021-06-09 김진우 주임 추가
        /// FTP-용접지그
        /// </summary>
        public static string FtpFolder_WeldingJig = "WELD_JIG";
        /// <summary>
        /// /// 2021-06-09 김진우 주임 추가
        /// FTP-검사구
        /// </summary>
        public static string FtpFolder_CheckerFixture = "CHECK_FIX";
        /// <summary>
        /// FTP-클레임관리폴더명
        /// </summary>
        public static string FtpFolder_ClaimFile = "CLAIM_FILE";
        /// <summary>
        /// FTP-작업표준서폴더명
        /// </summary>
        public static string FtpFolder_WorkStandardDocumentFile = "WORK_STANDARD_DOC";
        /// <summary>
        /// FTP-검사의뢰서/성적서폴더명
        /// </summary>
        public static string FtpFolder_ReqFile = "REQ_FILE";
        /// <summary>
        /// FTP-수입검사/성적서폴더명
        /// </summary>
        public static string FtpFolder_Inspection_IN_File = "INSP_IN_FILE";
        /// <summary>
        /// FTP-부적합관리 폴더명
        /// </summary>
        public static string FtpFolder_BadReport_File = "BAD_REPORT";
        /// <summary>
        /// FTP-작업설정검사관리 폴더명
        /// </summary
        public static string FtpFolder_JobsetFile = "JOBSET";
        /// <summary>
        /// FTP-4M관리 폴더명
        /// </summary
        public static string FtpFolder_4M = "4M";
        /// <summary>
        /// FTP-최종검사파일 폴더명
        /// </summary
        public static string FtpFolder_Inspection_Final_File = "INSP_FINAL_FILE";
        /// <summary>
        /// FTP-금형이미지 
        /// </summary
        public static string FtpFolder_MoldImage = "MOLD";
        // <summary>
        /// FTP-금형점검이미지 
        /// </summary
        public static string FtpFolder_MoldCheckImage = "MOLD_CHECK";
        /// <summary>
        /// /// 2021-06-16 김진우 주임 추가
        /// 설비스페어 금형 등급 평가서 이미지
        /// </summary>
        public static string FtpFolder_MachineSpare_Mold_Doc_Image = "M_SPARE_MOLD";
        #endregion

        #region EditFormat
        public static string Numeric_N0 = "#,###,###,###,###,##0";
        public static string Numeric_N0_NotZero = "#,###,###,###,###,###";
        public static string Numeric_N1 = "#,###,###,###,###,##0.#";
        public static string Numeric_N1_NotZero = "#,###,###,###,###,###.#";
        public static string Numeric_N2 = "#,###,###,###,###,##0.##";
        public static string Numeric_N2_NotZero = "#,###,###,###,###,###.##";
        public static string Numeric_N3 = "#,###,###,###,###,##0.###";
        public static string Numeric_N3_NotZero = "#,###,###,###,###,###.###";
        public static string Numeric_N4 = "#,###,###,###,###,##0.####";
        public static string Numeric_N4_NotZero = "#,###,###,###,###,###.####";
        public static string Numeric_N5 = "#,###,###,###,###,##0.#####";
        public static string Numeric_N5_NotZero = "#,###,###,###,###,###.#####";
        #endregion
    }
}
