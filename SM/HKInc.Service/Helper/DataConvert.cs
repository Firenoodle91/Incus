using System;
using System.Collections.Generic;
using System.Linq;

using HKInc.Utils.Common;
using HKInc.Ui.Model.Domain;
using HKInc.Service.Repository;
using HKInc.Utils.Interface.Repository;

namespace HKInc.Service.Helper
{
    /// <summary>
    /// 데이터에 대한 영문 중문 검출
    /// </summary>
    public static class DataConvert
    {
        /// <summary>
        /// 현재 문화에 맞는 다국어 필드 가져오기
        /// </summary>
        /// <param name="defaultFieldName"></param>
        /// <param name="secondFieldName">null일 경우 defaultFieldName + ENG</param>
        /// <param name="thirdFieldName">null일 경우 defaultFieldName + CHA</param>
        /// <returns></returns>
        public static string GetCultureDataFieldName(string defaultFieldName, string secondFieldName = null, string thirdFieldName = null)
        {
            var secondField = secondFieldName == null ? defaultFieldName + "ENG" : secondFieldName;
            var thirdField = thirdFieldName == null ? defaultFieldName + "CHN" : thirdFieldName;

            return GlobalVariable.IsDefaultCulture ? defaultFieldName : (GlobalVariable.IsSecondCulture ? secondField : thirdField);
        }

        /// <summary>
        /// 현재 문화 순서 가져오기 1:default, 2:second, 3:third
        /// </summary>
        /// <returns>1:default, 2:second, 3:third</returns>
        public static int GetCultureIndex()
        {
            return GlobalVariable.IsDefaultCulture ? 1 : (GlobalVariable.IsSecondCulture ? 2 : 3);
        }
    }
}
