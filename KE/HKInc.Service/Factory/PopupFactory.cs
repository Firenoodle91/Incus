using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HKInc.Utils.Common;
using HKInc.Utils.Class;
using HKInc.Utils.Enum;
using HKInc.Utils.Interface.Popup;

namespace HKInc.Service.Factory
{
    public class PopupFactory
    {
        public static IPopupCallbackForm GetPopupCallbackForm(PopupScreen screen, PopupDataParam map, PopupCallback callback)
        {
            return new HKInc.Service.Forms.MemoPopupForm(map, callback);
        }

        /// <summary>
        /// 20210118 오세완 차장
        /// 트리구조에서 메모팝업을 출력하기 위해서 interface callback 함수 추가
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="map"></param>
        /// <param name="callback"></param>
        /// <param name="bUsetreecolumn"></param>
        /// <returns></returns>
        public static IPopupCallbackForm GetPopupCallbackForm(PopupScreen screen, PopupDataParam map, PopupCallback callback, bool bUsetreecolumn)
        {
            return new HKInc.Service.Forms.MemoPopupForm(map, callback, bUsetreecolumn);
        }
    }
}
