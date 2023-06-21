﻿using System;
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
            { ProductionPopupView.PFSTD1200, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1200(param, callback)},
            { ProductionPopupView.PFSTD1400, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1400(param, callback)},
            { ProductionPopupView.PFSTD1500, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD1500(param, callback)},
            { ProductionPopupView.PFStd4M001, (param, callback) => new HKInc.Ui.View.STD_Popup.XPFSTD4M001(param, callback)},
            #endregion
            #region ORD
             { ProductionPopupView.PFORD1000, (param, callback) => new HKInc.Ui.View.ORD_Popup.XPFORD1000(param, callback)},


            #endregion
              { ProductionPopupView.PFQCT1700, (param, callback) => new HKInc.Ui.View.QC_Popup.XPFQCT1700(param, callback)},
            #region SELECT
             { ProductionPopupView.SELECTSTD1100, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFSTD1100(param, callback)},
             { ProductionPopupView.SELECTPUR1100, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSPUR1100(param, callback)},
             { ProductionPopupView.SELECTPUR1200, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSPUR1200(param, callback)},
             { ProductionPopupView.SELECTPUR1700, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFPUR1700(param, callback)},
             { ProductionPopupView.SELECTPUR1800M, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFPUR1800M(param, callback)},
             { ProductionPopupView.SELECTPUR1800D, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFPUR1800D(param, callback)},
             { ProductionPopupView.SELECTPUR2100, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSPUR2100(param, callback)},
             { ProductionPopupView.SELECTPUR2101, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSPUR2101(param, callback)},
             { ProductionPopupView.SELECTMPS1401LIST, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFQCT(param, callback)},
             { ProductionPopupView.SELECTORD1200, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSORD1100(param, callback)},
             { ProductionPopupView.SELECTORD1201, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSORD1201(param, callback)},
             { ProductionPopupView.SELECTINSPLIST, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFQCINSP(param, callback)},
             { ProductionPopupView.SELECTQCCOPY, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFQCSTDCOPY(param, callback)},
             { ProductionPopupView.SELECTOUTLIST, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XFSORD2001(param, callback)},
              { ProductionPopupView.SELECT_MACHINE_CHECK_COPY, (param, callback) => new HKInc.Ui.View.SELECT_Popup.XSFMACHINE_CHECK_COPY(param, callback)},
            #endregion
              { ProductionPopupView.PFMEA1000, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1000(param, callback)},
              { ProductionPopupView.PFMEA1200, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1200(param, callback)},
              { ProductionPopupView.PFMEA1400, (param, callback) => new HKInc.Ui.View.MEA_Popup.XPFMEA1400(param, callback)},
              { ProductionPopupView.PFQCT2200, (param, callback) => new HKInc.Ui.View.QC_Popup.XPFQCT2200(param, callback)},
            { ProductionPopupView.PFORD1200, (param, callback) => new HKInc.Ui.View.ORD_Popup.XPFORD1200(param, callback)},
            };

        public static IPopupForm GetPopupForm(ProductionPopupView screenName, PopupDataParam param, PopupCallback callback)
        {
            return EditFormList[screenName](param, callback);
        }
    }
}
