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
        public static string BusinessCode = "B002";  // 업종 안씀
        public static string BusinessType = "B006";  //업태 안씀
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

        public static string STOPTYPE = "P003"; //불량유형
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
        public static string lctype = "S001"; //기종

        public static string ITEM_MATERIAL = "M003"; //원재료코드
        public static string ITEM_COLOR = "M004"; //색상코드

        public static string NCR_ReceiptWay = "NC01"; //부적합 접수방법

        public static string ItemCode_Production = "ItemCode_Production"; //완제품 Constraint
        public static string ItemCode_NotProduction = "ItemCode_NotProduction"; //완제품 이외 Constraint
        public static string ItemCode_BAN_Production = "ItemCode_BAN_Production"; //반제품 Constraint

        public static string TopCategory_Production = "P01"; //완제품
        public static string TopCategory_Material = "P03"; //원자재


        public static string ProcessPacking = "P06"; //포장
        public static string ProcessCutToPacking = "P07"; //컷팅+포장
        public static string ProcessMakeToCutToPacking = "P08"; //성형+컷팅+포장

    }
}
