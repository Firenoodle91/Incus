using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Service.Factory
{
    public enum MasterCodeEnum : int
    {
        UserRankCode = 2, //직급
        Maker = 3,//설비제작사
        CheckTurn =17,//설비점검주기
        PopStatus = 31, // POP 작업지시 상태

        POP_Status_Wait = 32, // 대기
        POP_Status_Start = 33, // 생산중
        POP_Status_StopWait = 34, // 일시중지
        POP_Status_Stop = 35, // 비가동
        POP_Status_End = 36, // 종료
        INPUTSTATUS=39, //입고진행상태

        Font = 70574, // Font설정
        UserGroupCode = 70582, //사용자 조 관리

        DatabaseIP = 70613, //Database IP 관리
    }

    public static class MasterCodeSTR
    {
        public static string ANDONTYPE = "A003";     //POP 안돈 구분(생산,품질)
        public static string INPUTSTATUS = "B099";      // 입고상태
        public static string BusinessType = "B002";      // 종목
        public static string BusinessCode = "B006";  //업종
        public static string CustType = "B007";  //거래처구분
        public static string NationalCode ="B003";     // 국가코드
        public static string itemtype ="M001";//품목 대중소
        public static string Unit = "B004"; //단위
        public static string YN = "B005"; //YN
        public static string Process = "P001"; //표준공정
        public static string CheckGu = "B001";//검교정 구분
        public static string STD = "P002"; //공정표준작업일
        public static string QCKIND = "Q001";//검사종류
        public static string QCTYPE = "Q002"; //검사방법
        public static string QCOKNG = "Q003"; //합부판정
        public static string QCFAIL = "Q004"; //불량유형
        public static string QCSTEP = "Q005"; //일반,초,중,종물
        public static string QCPOINT = "Q006"; //검사항목        
        public static string VCTYPE = "Q007"; //계측기종류.
        public static string XSEQ = "Q008"; //시료번호

        public static string VisualInspection = "QT1";  // 육안검사         2022-03-08 김진우 추가

        public static string STOPTYPE = "P003"; //불량유형
        public static string STOPTYPE_RUS = "P004"; //불량유형 러시아어     2022-08-16 김진우 추가
        //public static string MOLD = "Q007"; //계측기종류
        public static string MOLDPOSITION = "MO02"; //금형위치
        public static string MOLDReqType = "MO01"; //금형수리구분
        /// <summary>설비스페어파트</summary>          2021-11-17 김진우 주임 추가
        public static string MACHINESPAREPART = "M004";  
        public static string CHECKCYCLE = "MC01"; //점검주기
        public static string MCMAKER = "MC02"; //설비제조사
        public static string TOOLMAKER = "MC03"; //계측기제조사

        /// <summary>설비점검주기</summary>
        public static string MachineCheckCycle = "MC04";
        /// <summary>설비점검위치</summary>
        public static string MachineCheckPosition = "MC08";
        /// <summary>설비점검항목</summary>
        public static string MachineCheckList = "MC09";
        /// <summary>설비점검방법</summary>
        public static string MachineCheckWay = "MC10";
        /// <summary>설비점검기준일</summary>
        public static string MachineCheckStandardDate = "MC11";
        /// <summary>설비그룹</summary>
        public static string MachineGroup = "MC12";
        /// <summary>설비점검(O,X)</summary>
        public static string MachineCheckOX = "MC14";
        /// <summary>설비점검구분</summary>
        public static string MachineStandardCheckDivision = "MC15";
        /// <summary>모니터링위치, 설비모니터링이라는 레포트성 화면에 출력 위치를 결정</summary>        20220325 오세완 차장
        public static string MachineMonotoringLocation = "MC16";
        /// <summary>설비등굽</summary>                 20220325 오세완 차장
        public static string MachineClass = "MC17";

        public static string MOLDCLASS = "L001"; //금형등급
        public static string CARTYPE = "C001"; //차종
        public static string MM = "D001"; //월
        public static string WHPOSITION = "W002"; //창고위치코드
        public static string lctype = "S001"; //창고위치코드
        public static string tem = "T001"; //팀코드

        public static string AlamHH = "A001"; //알람시간
        public static string AlamMM = "A002"; //알람분

        /// <summary>입고진행상황</summary>               2022-06-15 김진우 추가
        public static string InputProgressStatus = "B099";  

        /// <summary>사용여부</summary>                 2021-11-17 김진우 주임 추가
        public static string CheckYN = "U001";
        /// <summary>입출고 구분</summary>               2021-11-17 김진우 주임 추가
        public static string INOUTDIVISION = "D002";
        /// <summary>수입검사관리</summary>                2022-04-20 김진우 추가
        public static string ReceivingInspectionManagement = "Q01";
        /// <summary>초중종검사관리</summary>              2022-03-22 김진우 추가
        public static string FMECheckManagement = "Q03";
        /// <summary>출하검사관리</summary>                2022-03-22 김진우 추가
        public static string ShipmentCheckManagement = "Q05";
        /// <summary>예방보전</summary>                     2022-04-15 김진우 추가
        public static string Machine_Preventive_Maintenance = "02";

        /// <summary>권한그룹관리_기술자</summary>
        public static decimal UserGroup_Technician = 12;
        /// <summary>설비점검위치_팀장확인여부</summary>
        public static string MachineCheckPosition_ConfirmFlag = "99";
        /// <summary>설비점검방법-메모(검사방법구분을위함)</summary>
        public static string MachineCheckWay_Memo = "04";
        /// <summary>초중종검사 구분코드</summary>          20220315 오세완 차장
        public static string QC_Process_FME = "Q03";
        /// <summary>100% 검사 구분코드</summary>          20220316 오세완 차장
        public static string QC_Process_100 = "Q06";

        #region FTP 폴더명

        /// <summary>설비스페어파트 이미지 FTP 폴더</summary>         2021-11-17 김진우 주임 추가
        public static string FtpFolder_SpareImage = "SPARE_IMAGE";

        /// <summary>
        /// FTP-설비사진폴더명
        /// </summary>
        public static string FtpFolder_MachineImage = "MC_IMAGE";
        /// <summary>
        /// FTP-설비점검포인트폴더명
        /// </summary>
        public static string FtpFolder_MachineCheckPoint = "MC_CHECK_POINT";

        /// <summary>
        /// FTP-작업표준서폴더명
        /// </summary>
        public static string FtpFolder_WorkStandardDocumentFile = "WORK_STANDARD_DOC";

        /// <summary>
        /// FTP-제품도면폴더명
        /// </summary>
        public static string FtpFolder_DesignImage = "DESIGN_IMAGE";
        
        /// <summary>
        /// FTP-설비예방보전점검폴더명
        /// </summary>
        public static string FtpFolder_MachineMaintenance = "MC_MAINTENANCE";
        #endregion

        #region 대분류코드

        /// <summary>
        /// 20220219 오세완 차장 
        /// 완제품
        /// </summary>
        public static string Topcategory_Final_Product = "P01";

        /// <summary>
        /// 20220219 오세완 차장 외주가공품
        /// </summary>
        public static string Tpocategory_Outsorcing_Product = "P02";

        /// <summary>
        /// 20220219 오세완 차장 원자재
        /// </summary>
        public static string Topcategory_Material = "P03";

        /// <summary>
        /// 20220219 오세완 차장 반제품
        /// 완제품
        /// </summary>
        public static string Topcategory_Half_Product = "P05";

        /// <summary>
        /// 20220407 오세완 차장 
        /// 스페어파트
        /// </summary>
        public static string Topcategory_SPARE = "P05";

        /// <summary>
        /// 20220407 오세완 차장
        /// 소모품
        /// </summary>
        public static string Topcategory_Consumable = "P06";

        /// <summary>
        /// 20200407 오세완 차장
        /// 부자재
        /// </summary>
        public static string Topcategory_Sub_Meterial = "P07";
        #endregion

        #region 설비 모니터링
        /// <summary>
        /// 20220420 오세완 차장
        /// 설비모니터링 공통코드 
        /// </summary>
        public static string MachineMonitoring = "R001";
        #endregion
    }
}
