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

        public static string STOPTYPE = "P003"; //비가동
        //public static string MOLD = "Q007"; //계측기종류
        public static string MOLDPOSTION = "MO02"; //금형위치
        public static string MOLDReqType = "MO01"; //금형수리구분
        public static string CHECKCYCLE = "MC01"; //점검주기
        public static string MCMAKER = "MC02"; //설비제조사
        public static string TOOLMAKER = "MC03"; //계측기제조사
        public static string MOLDCLASS = "L001"; //금형등급
        public static string CARTYPE = "C001"; //차종
        public static string MM = "D001"; //월
        public static string WHPOSITION = "W002"; //창고위치코드
        public static string lctype = "S001"; //창고위치코드
        public static string tem = "T001"; //팀코드
        public static string InspectLabelTyp = "L002"; //(유/무)검사구분

        /// <summary>
        /// 20220111 오세완 차장 입고상태 코드추가
        /// </summary>
        public static string INPUTSTATUS = "B099";

        public static string AlamHH = "A001"; //알람시간
        public static string AlamMM = "A002"; //알람분
        public static string MachineModel = "MC12"; //모델
        public static string MachineStandardCheckDivision = "MC15";//점검구분
        public static string MachineCheckPosition = "MC08";
        public static string LHRHTYP = "S001"; //기종 (LH, RH)

        //품목구분
        /// <summary>
        /// 완제품
        /// </summary>
        public static string ITEM_TYPE_WAN = "P01"; //완제품
        /// <summary>
        /// 외주가공품
        /// </summary>
        public static string ITEM_TYPE_OUT = "P02"; //외주가공품
        /// <summary>
        /// 원자재
        /// </summary>
        public static string ITEM_TYPE_WON = "P03"; //원자재
        /// <summary>
        /// 반제품
        /// </summary>
        public static string ITEM_TYPE_BAN = "P05"; //반제품


        #region 검사구분

        /// <summary>
        /// 수입검사
        /// </summary>
        public static string INSPECTION_TYP_A = "Q01";
        /// <summary>
        /// 자주검사
        /// </summary>
        public static string INSPECTION_TYP_B = "Q02";
        /// <summary>
        /// 초중종검사
        /// </summary>
        public static string INSPECTION_TYP_C = "Q03";
        /// <summary>
        /// 작업설정검사
        /// </summary>
        public static string INSPECTION_TYP_D = "Q04";
        /// <summary>
        /// 출하검사
        /// </summary>
        public static string INSPECTION_TYP_E = "Q05";
        /// <summary>
        /// 100% ??
        /// </summary>
        public static string INSPECTION_TYP_F = "Q06";
        /// <summary>
        /// 공정검사
        /// </summary>
        public static string INSPECTION_TYP_G = "Q07";
        /// <summary>
        /// 2021-01-12 김진우 대리
        /// 공정검사관리 추가
        /// </summary>
        public static string Process_Inspection = "Q07";
        #endregion



        /// <summary>
        /// 설비점검항목
        /// </summary>
        public static string MachineCheckList = "MC09";
        /// <summary>
        /// 설비점검방법
        /// </summary>
        public static string MachineCheckWay = "MC10";
        /// 설비점검(O,X)
        /// </summary>
        public static string MachineCheckOX = "MC14";
        /// 설비점검위치_팀장확인여부
        /// </summary>
        public static string MachineCheckPosition_ConfirmFlag = "99";
        /// 설비점검방법-메모(검사방법구분을위함)
        /// </summary>
        public static string MachineCheckWay_Memo = "04";
        public static string MachineCheckStandardDate = "MC11";
        public static string FtpFolder_MachineImage = "MC_IMAGE";
        public static string FtpFolder_MachineCheckPoint = "MC_CHECK_POINT";


    }
}
