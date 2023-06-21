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
            { ProductionPopupView.Module, (param, callback) => new  SYS.ModuleEdit(param, callback)},
            { ProductionPopupView.Screen, (param, callback) => new SYS.ScreenEdit(param, callback)},
            { ProductionPopupView.User, (param, callback) => new SYS.UserEdit(param, callback)},
            { ProductionPopupView.Menu, (param, callback) => new SYS.MenuEdit(param, callback)},
            { ProductionPopupView.UserGroup, (param, callback) => new SYS.UserGroupEdit(param, callback)},
            { ProductionPopupView.MenuOpenLog, (param, callback) => new SYS.MenuOpenLog(param, callback)},
            #endregion

            #region STD

            { ProductionPopupView.PFSTD1100, (param, callback) => new View.STD_POPUP.XPFSTD1100(param, callback)},
            { ProductionPopupView.PFSTD1200, (param, callback) => new View.STD_POPUP.XPFSTD1200(param, callback)},
            { ProductionPopupView.PFSTD1400, (param, callback) => new View.STD_POPUP.XPFSTD1400(param, callback)},
            { ProductionPopupView.PFStd4M001, (param, callback) => new View.STD_POPUP.XPFSTD4M001(param, callback)},

            #endregion

            #region ORD
            { ProductionPopupView.PFORD1000, (param, callback) => new View.ORD_POPUP.XPFORD1000(param, callback)},
            { ProductionPopupView.PFORD1000_DEV, (param, callback) => new View.ORD_POPUP.XPFORD1000_DEV(param, callback)},
            { ProductionPopupView.XPFORD1102, (param, callback) => new View.ORD_POPUP.XPFORD1102(param, callback)},
            
            #endregion

            #region MPS
            { ProductionPopupView.PFMPS1800, (param, callback) => new View.MPS_POPUP.XPFMPS1800(param, callback)},
            { ProductionPopupView.PFMPS1801, (param, callback) => new View.MPS_POPUP.XPFMPS1801(param, callback)},
            
            
            #endregion

            #region BAN
            { ProductionPopupView.PFBAN1000, (param, callback) => new View.BAN_POPUP.XPFBAN1000(param, callback)},
            { ProductionPopupView.PFBAN1102, (param, callback) => new View.BAN_POPUP.XPFBAN1102(param, callback)},
            
            #endregion

            #region PUR
            { ProductionPopupView.PFPUR1200, (param, callback) => new View.PUR_POPUP.XPFPUR1200(param, callback)},
            { ProductionPopupView.XPFPUR1201, (param, callback) => new View.PUR_POPUP.XPFPUR1201(param, callback)},
            { ProductionPopupView.PFPUR1302, (param, callback) => new View.PUR_POPUP.XPFPUR1302(param, callback)},
            { ProductionPopupView.PFPUR1203, (param, callback) => new View.PUR_POPUP.XPFPUR1203(param, callback)},
            
            #endregion
            
            #region MEA

            { ProductionPopupView.PFMEA1000, (param, callback) => new View.MEA_POPUP.XPFMEA1000(param, callback)},
            { ProductionPopupView.PFMEA1100, (param, callback) => new View.MEA_POPUP.XPFMEA1100(param, callback)},

            #endregion

            #region QCT
            { ProductionPopupView.PFQCT1400, (param, callback) => new View.QCT_POPUP.XPFQCT1400(param, callback)},

            #endregion

            #region DEV
            { ProductionPopupView.PFDEV1000, (param, callback) => new View.DEV_POPUP.XPFDEV1000(param, callback)},

            #endregion

            #region SELECT
            { ProductionPopupView.DepartmentSelect, (param, callback) => new SELECT_POPUP.DepartmentSelect(param, callback)},
            { ProductionPopupView.UserGroupSelectList, (param, callback) => new SELECT_POPUP.UserGroupSelect(param, callback)},
            { ProductionPopupView.UserSelectList, (param, callback) => new SELECT_POPUP.UserSelect(param, callback)},
            { ProductionPopupView.SELECT_STD1100, (param, callback) => new SELECT_POPUP.XSFSTD1100(param, callback)},
            { ProductionPopupView.SELECT_PUR1100, (param, callback) => new SELECT_POPUP.XSFPUR1100(param, callback)},
            { ProductionPopupView.SELECT_PUR1101, (param, callback) => new SELECT_POPUP.XSFPUR1101(param, callback)},
            { ProductionPopupView.SELECT_PUR1401, (param, callback) => new SELECT_POPUP.XSFPUR1401(param, callback)},
            { ProductionPopupView.SELECT_MPS1200, (param, callback) => new SELECT_POPUP.XSFMPS1200(param, callback)},
            { ProductionPopupView.SELECT_ORD1100, (param, callback) => new SELECT_POPUP.XSFORD1100(param, callback)},
            { ProductionPopupView.SELECT_PUR_STATUS, (param, callback) => new SELECT_POPUP.XSFPUR_STATUS(param, callback)},
            { ProductionPopupView.SELECT_QCT_IN, (param, callback) => new SELECT_POPUP.XSFQCT_IN(param, callback)},
            { ProductionPopupView.SELECT_QCT_FME, (param, callback) => new SELECT_POPUP.XSFQCT_FME(param, callback)},
            { ProductionPopupView.SELECT_QCT_FREQNTLY, (param, callback) => new SELECT_POPUP.XSFQCT_FREQNTLY(param, callback)},
            { ProductionPopupView.SELECT_QCT_PROCESS, (param, callback) => new SELECT_POPUP.XSFQCT_PROCESS(param, callback)},
            { ProductionPopupView.SELECT_QCT_SETTING, (param, callback) => new SELECT_POPUP.XSFQCT_SETTING(param, callback)},
            { ProductionPopupView.SELECT_QCT_SHIPMENT, (param, callback) => new SELECT_POPUP.XSFQCT_SHIPMENT(param, callback)},
            { ProductionPopupView.XSFWORK_REF, (param, callback) => new SELECT_POPUP.XSFWORK_REF(param, callback)},
            { ProductionPopupView.SELECT_ORD_STATUS, (param, callback) => new SELECT_POPUP.XSFORD_STATUS(param, callback)},
            { ProductionPopupView.SELECT_PROD_STOCK, (param, callback) => new SELECT_POPUP.XSFPROD_STOCK(param, callback)},
            { ProductionPopupView.SELECT_ORD1101, (param, callback) => new SELECT_POPUP.XSFORD1101(param, callback)},
            { ProductionPopupView.SELECT_PUR1400, (param, callback) => new SELECT_POPUP.XSFPUR1400(param, callback)},
            { ProductionPopupView.SELECT_QCT_ITEM_COPY, (param, callback) => new SELECT_POPUP.XSFQCT_ITEM_COPY(param, callback)},
            { ProductionPopupView.SELECT_QCT_CHECK_DIVISION_COPY, (param, callback) => new SELECT_POPUP.XSFQCT_CHECK_DIVISION_COPY(param, callback)},
            { ProductionPopupView.XFSMPS1000, (param, callback) => new SELECT_POPUP.XFSMPS1000(param, callback)},
            { ProductionPopupView.SELECT_MACHINE_CHECK_COPY, (param, callback) => new SELECT_POPUP.XSFMACHINE_CHECK_COPY(param, callback)},
            { ProductionPopupView.SELECT_PUR1100_COPY, (param, callback) => new SELECT_POPUP.XSFPUR1100_COPY(param, callback)},
            { ProductionPopupView.XSFORDER_REF, (param, callback) => new SELECT_POPUP.XSFORDER_REF(param, callback)},
            
            #endregion

            #region POP
            { ProductionPopupView.XPFSRCIN_START, (param, callback) => new View.POP_POPUP.XPFSRCIN_START(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_START, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_START(param, callback)},
            { ProductionPopupView.XPFRESULT_DEFAULT, (param, callback) => new View.POP_POPUP.XPFRESULT_DEFAULT(param, callback)},
            { ProductionPopupView.XPFRESULT_TOOL, (param, callback) => new View.POP_POPUP.XPFRESULT_TOOL(param, callback)},
            { ProductionPopupView.XPFRESULT_HEAT_TOOL, (param, callback) => new View.POP_POPUP.XPFRESULT_HEAT_TOOL(param, callback)},
            { ProductionPopupView.XPFRESULT_HEAT, (param, callback) => new View.POP_POPUP.XPFRESULT_HEAT(param, callback)},
            { ProductionPopupView.XPFITEMMOVEPRINT, (param, callback) => new View.POP_POPUP.XPFITEMMOVEPRINT(param, callback)},
            { ProductionPopupView.XPFSRCIN_CHANGE, (param, callback) => new View.POP_POPUP.XPFSRCIN_CHANGE(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_CHANGE, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_CHANGE(param, callback)},
            { ProductionPopupView.XPFWORKEND, (param, callback) => new View.POP_POPUP.XPFWORKEND(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_START_PACK, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_START_PACK(param, callback)},
            { ProductionPopupView.XPFRESULT_PACK, (param, callback) => new View.POP_POPUP.XPFRESULT_PACK(param, callback)},
            { ProductionPopupView.XPFPACK_BARCODE_PRINT, (param, callback) => new View.POP_POPUP.XPFPACK_BARCODE_PRINT(param, callback)},
            { ProductionPopupView.XPFPACK_BARCODE_PRINT_LABEL, (param, callback) => new View.POP_POPUP.XPFPACK_BARCODE_PRINT_LABEL(param, callback)},
            { ProductionPopupView.XPFBANIN_START, (param, callback) => new View.POP_POPUP.XPFBANIN_START(param, callback)},
            { ProductionPopupView.XPFBANIN_CHANGE, (param, callback) => new View.POP_POPUP.XPFBANIN_CHANGE(param, callback)},
            { ProductionPopupView.XPFITEMMOVEPRINT_BOX, (param, callback) => new View.POP_POPUP.XPFITEMMOVEPRINT_BOX(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_CHANGE_PACK, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_CHANGE_PACK(param, callback)},
            { ProductionPopupView.XPFPACK_END, (param, callback) => new View.POP_POPUP.XPFPACK_END(param, callback)},
            { ProductionPopupView.XPFRESULT_INSP_FINAL, (param, callback) => new View.POP_POPUP.XPFRESULT_INSP_FINAL(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_START_INSP_FINAL, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_START_INSP_FINAL(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_CHANGE_INSP_FINAL, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_CHANGE_INSP_FINAL(param, callback)},
            { ProductionPopupView.XPFRAW_MATERIAL_IN, (param, callback) => new View.POP_POPUP.XPFRAW_MATERIAL_IN(param, callback)},
            { ProductionPopupView.XPFSRCIN_CHANGE_SM, (param, callback) => new View.POP_POPUP.XPFSRCIN_CHANGE_SM(param, callback)},
            { ProductionPopupView.XPFITEMMOVESCAN_START_SM, (param, callback) => new View.POP_POPUP.XPFITEMMOVESCAN_START_SM(param, callback)},
            { ProductionPopupView.XPFREWORK_START, (param, callback) => new View.POP_POPUP.XPFREWORK_START(param, callback)},
            { ProductionPopupView.XPFWORKEND_REWORK, (param, callback) => new View.POP_POPUP.XPFWORKEND_REWORK(param, callback)},
            { ProductionPopupView.XPFRESULT_REWORK, (param, callback) => new View.POP_POPUP.XPFRESULT_REWORK(param, callback)},
            
            #endregion
        };

        public static IPopupForm GetPopupForm(ProductionPopupView screenName, PopupDataParam param, PopupCallback callback)
        {
            return EditFormList[screenName](param, callback);
        }
    }
}
