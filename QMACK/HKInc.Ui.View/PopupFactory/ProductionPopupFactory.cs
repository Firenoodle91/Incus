using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Ui.View.PopupFactory
{
    class ProductionPopupFactory
    {
        private static Dictionary<ProductionPopupView, Func<PopupDataParam, PopupCallback, IPopupForm>> EditFormList = new Dictionary<ProductionPopupView, Func<PopupDataParam, PopupCallback, IPopupForm>>()
        {
            #region SYS
            { ProductionPopupView.Module, (param, callback) => new  HKInc.Ui.View.SYS.ModuleEdit(param, callback)},
            { ProductionPopupView.Screen, (param, callback) => new HKInc.Ui.View.SYS.ScreenEdit(param, callback)},
            { ProductionPopupView.User, (param, callback) => new HKInc.Ui.View.SYS.UserEdit(param, callback)},
            { ProductionPopupView.UserGroupSelectList, (param, callback) => new HKInc.Ui.View.SYS.UserGroupSelect(param, callback)},
            { ProductionPopupView.Menu, (param, callback) => new HKInc.Ui.View.SYS.MenuEdit(param, callback)},
            { ProductionPopupView.UserGroup, (param, callback) => new HKInc.Ui.View.SYS.UserGroupEdit(param, callback)},
            { ProductionPopupView.UserSelectList, (param, callback) => new HKInc.Ui.View.SYS.UserSelect(param, callback)},
         
            { ProductionPopupView.MenuOpenLog, (param, callback) => new HKInc.Ui.View.SYS.MenuOpenLog(param, callback)},
            { ProductionPopupView.UserDepartmentSelect, (param, callback) => new HKInc.Ui.View.SYS.UserDepartmentSelect(param, callback)},
              #endregion
            #region STD

       
            { ProductionPopupView.PFSTD1100, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1100(param, callback)},
            { ProductionPopupView.PFSTD1100_V2, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1100_V2(param, callback)}, // 20220426 오세완 차장 소재사용량 길이 없애고 단위로 변경한 팝업
            { ProductionPopupView.PFSTD1200, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1200(param, callback)},
            { ProductionPopupView.PFSTD1400, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1400(param, callback)},
            { ProductionPopupView.PFSTD1500, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1500(param, callback)},
            { ProductionPopupView.PFStd4M001, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD4M001(param, callback)},
            #endregion
            #region ORD
             { ProductionPopupView.PFORD1000, (param, callback) => new HKInc.Ui.View.ORD_Popup.XPFORD1000(param, callback)},


            #endregion
            #region QC
            { ProductionPopupView.PFQCT1700, (param, callback) => new HKInc.Ui.View.QC_Popup.XPFQCT1700(param, callback)},
            { ProductionPopupView.PFQCT2200, (param, callback) => new HKInc.Ui.View.QC_Popup.XPFQCT2200(param, callback)},
            #endregion
            #region SELECT
            { ProductionPopupView.SELECTSTD1100, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFSTD1100(param, callback)},
             { ProductionPopupView.SELECT_STD1400, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFSTD1400(param, callback)}, // 20220408 오세완 차장 단가이력관리 거래처목록 조회 팝업 추가 
             { ProductionPopupView.SELECTPUR1100, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSPUR1100(param, callback)},
             { ProductionPopupView.SELECTPUR1200, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSPUR1200(param, callback)},
             { ProductionPopupView.SELECTPUR1700, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFPUR1700(param, callback)},
             { ProductionPopupView.SELECTPUR1800M, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFPUR1800M(param, callback)},
             { ProductionPopupView.SELECTPUR1800D, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFPUR1800D(param, callback)},
             { ProductionPopupView.XSFQCT, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFQCT(param, callback)},                    // 2022-03-10 김진우 수정    SELECTMPS1401LIST  =>  XSFQCT
             { ProductionPopupView.SELECTORD1200, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSORD1100(param, callback)},
             { ProductionPopupView.SELECTORD1201, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSORD1201(param, callback)},
             { ProductionPopupView.SELECTINSPLIST, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFQCINSP(param, callback)},
             { ProductionPopupView.XSFQCSTDCOPY, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFQCSTDCOPY(param, callback)},        // 2022-02-23 김진우 수정    검사규격관리 복사하기
             { ProductionPopupView.SELECTOUTLIST, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSORD2001(param, callback)},
             { ProductionPopupView.SELECT_MACHINE_CHECK_COPY, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFMACHINE_CHECK_COPY(param, callback)}, // 20220313 오세완 차장 다른 설비점검기준 복사 팝업 추가 
             { ProductionPopupView.XFSMPS1000, (param, callback) => new SELECT_Popup.XFSMPS1000(param, callback)}, // 20220321 오세완 차장 표준공정관리 조회 팝업

            #endregion
            #region MEA
            { ProductionPopupView.PFMEA1000, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1000(param, callback)},
              { ProductionPopupView.PFMEA1000_V2, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1000_V2(param, callback)}, // 20220325 오세완 차장 설비등록 고도화 팝업 추가 
              { ProductionPopupView.PFMEA1200, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1200(param, callback)},
              { ProductionPopupView.PFMEA1400, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1400(param, callback)},
            #endregion
            #region POP
              { ProductionPopupView.XPFWORK_START, (param, callback)        => new POP_Popup.XPFWORKSTART(param, callback)}, // 20220427 오세완 차장 작업시작 팝업 추가 
              { ProductionPopupView.XPFRESULT_DEFAULT, (param, callback)    => new POP_Popup.XPFRESULT_DEFAULT(param, callback)}, // 20220427 오세완 차장 실적 팝업 추가 
              { ProductionPopupView.XPFWORKEND, (param, callback)           => new POP_Popup.XPFWORKEND(param, callback)}, // 20220502 오세완 차장 작업종료 팝업 추가 
              { ProductionPopupView.XPFANDON, (param, callback)             => new POP_Popup.XPFANDON(param, callback)}, // 안돈      2022-07-15 김진우
            #region 러시아
            { ProductionPopupView.XPFWORKSTART_RUS, (param, callback)       => new POP_Popup.XPFWORKSTART_RUS(param, callback)},                // 작업시작 러시아     2022-08-12 김진우 추가
            { ProductionPopupView.XPFRESULT_DEFAULT_RUS, (param, callback)  => new POP_Popup.XPFRESULT_DEFAULT_RUS(param, callback)},           // 실적등록 러시아     2022-08-12 김진우 추가
            { ProductionPopupView.XPFANDON_RUS, (param, callback)           => new POP_Popup.XPFANDON_RUS(param, callback)},                    // 안돈 러시아         2022-08-16 김진우 추가
            { ProductionPopupView.XPFWORKEND_RUS, (param, callback)         => new POP_Popup.XPFWORKEND_RUS(param, callback)},                  // 작업종료 러시아     2022-08-16 김진우 추가

            #endregion
            #endregion
        };

        public static IPopupForm GetPopupForm(ProductionPopupView screenName, PopupDataParam param, PopupCallback callback)
        {
            return EditFormList[screenName](param, callback);
        }
    }
}
