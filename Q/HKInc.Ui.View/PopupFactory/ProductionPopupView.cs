using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Ui.View.PopupFactory
{
    enum ProductionPopupView
    {

        //System
        Module,
        User,
        Screen,
        UserGroupSelectList,
        Menu,
        UserGroup,
        UserSelectList,
        CodeSql,
        LicenseKeyEdit,
        MenuOpenLog,
        UserDepartmentSelect,
        //system
        PFSTD1100,
        PFSTD1100_V2, // 20220426 오세완 차장 소재사룡량 길이 없애고 단위로 변경한 팝업
        PFSTD1200,
        PFSTD1400,//거래처추가팝업
        PFSTD1500,
        PFStd2000,//설비목록
        PFStd4M001,//4M관리대장

        PFMEA1000,
        PFMEA1000_V2, // 20220325 오세완 차장 설비등록관리 고도화 팝업 추가 
        PFMEA1200,
        PFMEA1400,
        PFQCT2200,


        PFORD1000,
        PFQCT1700,

        SELECTSTD1100,
        SELECT_STD1400, // 20220408 오세완 차장 단가이력관리에서 거래처 목록 추가 팝업 지칭
        SELECTPUR1100,
        SELECTPUR1200,
        SELECTPUR1700,
        SELECTPUR1800M,
        SELECTPUR1800D,
        XSFQCT,                 // 2022-03-10 김진우 수정            SELECTMPS1401LIST  =>  XSFQCT
        SELECTORD1200,
        SELECTORD1201,
        SELECTINSPLIST,
        XSFQCSTDCOPY,           // 2022-02-23 김진우 수정        검사규격관리 복사하기
        SELECTOUTLIST,
        XFSMPS1000,             // 20220321 오세완 차장 표존공정관리 팝업 
        /// <summary>
        /// 다른설비점검기준복사
        /// </summary>
        SELECT_MACHINE_CHECK_COPY,

        #region POP
        /// <summary>
        /// 20220310 오세완 차장
        /// 고도화때 추가된 실적등록
        /// </summary>
        XPFRESULT_DEFAULT,
        /// <summary>
        /// 20220317 오세완 차장
        /// 고도화때 추가된 작업완료 팝업
        /// </summary>
        XPFWORKEND,
        /// <summary>
        /// 20220328 오세완 차장
        /// 고도화때 추가된 작업시작 팝업
        /// </summary>
        XPFWORK_START,
        /// <summary>안돈</summary>       2022-07-15 김진우 추가
        XPFANDON,

        #region 러시아
        /// <summary>작업시작 러시아</summary>         2022-08-12 김진우 추가
        XPFWORKSTART_RUS,
        /// <summary>실적등록 러시아</summary>         2022-08-12 김진우 추가
        XPFRESULT_DEFAULT_RUS,
        /// <summary>품질등록 러시아</summary>         2022-08-16 김진우 추가
        XPFINSPECTION_RUS,
        /// <summary>안돈 러시아</summary>             2022-08-16 김진우 추가
        XPFANDON_RUS,
        /// <summary>작업완료 러시아</summary>         2022-08-16 김진우 추가
        XPFWORKEND_RUS,
        #endregion

        #endregion

    }
}
