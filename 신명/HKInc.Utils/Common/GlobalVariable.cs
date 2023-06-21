using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HKInc.Utils.Class;

namespace HKInc.Utils.Common
{
    public class GlobalVariable
    {
        #region User Information
        private static HKInc.Ui.Model.Domain.User userInfo;
        

        public static void SetUser(HKInc.Ui.Model.Domain.User user)
        {
            userInfo = user;
        }

        // User Personnal Information
        public static HKInc.Ui.Model.Domain.User UserInfo { get { return userInfo; } }

        public static decimal UserId { get { return userInfo.UserId; } }
        public static string LoginId { get { return userInfo.LoginId; } }
        public static string UserName { get { return userInfo.UserName; } }
        public static string EmployeeNo { get { return userInfo.EmployeeNo; } }
        public static string ServerIp { get; set; }
        public static string ServerIpAddressQuery { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ServerIpAddressQuery); } }
        #endregion


        #region Application Info
        // Application Name & Version
        public static string ApplicationName { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ApplicationName); } }
        public static string ApplicationVersion { get; set; }
        #endregion


        #region Registry Path
        // Registry Path
        public static string CulturePath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.CultureRegistryPath); } }
        public static string MainPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.MainRegistryPath); } }
        public static string LoginInfoPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.LoginInfoRegistryPath); } }
        public static string SkinPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.SkinRegistryPath); } }
        public static string ServerConfigPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ServerConfigRegistryPath); } }
        #endregion


        #region Server Information
        // Server Information
        public static Enum.DatabaseCategory DefaultLogInDataBase { get { return DefaultValueList.DefaultLoginDataBase; } }
        public static Enum.DatabaseCategory LogInDataBase { get; set; }
        public static Enum.DatabaseCategory DefaultProductionDataBase { get { return DefaultValueList.DefaultProductionDataBase; } }
        public static Enum.DatabaseCategory ProductionDataBase { get { return DefaultValueList.DefaultProductionDataBase; } }
        
        public static string LoginDataBaseName { get { return ServerInfo.Database; } }
        public static string ProductionDataBaseName { get { return ServerInfo.ProductionDatabase; } }
        public static string UpdateServer { get; set; }
        public static string DatabaseIP { get; set; }
        #endregion


        #region Culture
        // Culture setting
        public static string Culture { get; set; }

        public static string DefaultCulture { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultCulture); } }
        public static string SecondCulture { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.SecondCulture); } }
        public static string ThirdCulture { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ThirdCulture); } }
        public static string DefaultCultureName { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultCultureName); } }
        public static string SecondCultureName { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.SecondCultureName); } }
        public static string ThirdCultureName { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ThirdCultureName); } }

        public static bool IsDefaultCulture { get { return Culture == DefaultCulture; } }
        public static bool IsSecondCulture { get { return Culture == SecondCulture; } }
        public static bool IsThirdCulture { get { return Culture == ThirdCulture; } }
        // DateTime Format string
        public static string DateTimeFormatString
        {
            get { return Culture == DefaultCulture ? DefaultDateTimeFormat : (Culture == SecondCulture ? SecondDateTimeFormat : ThirdDateTimeFormat); }
        }
        public static string DefaultDateTimeFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultDateTimeFormat); } }
        public static string SecondDateTimeFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.SecondDateTimeFormat); } }
        public static string ThirdDateTimeFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ThirdDateTimeFormat); } }

        // Date Format string
        public static string DateFormatString
        {
            get { return Culture == DefaultCulture ? DefaultDateFormat : (Culture == SecondCulture ? SecondDateFormat : ThirdDateFormat); }
        }
        public static string DefaultDateFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultDateFormat); } }
        public static string SecondDateFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.SecondDateFormat); } }
        public static string ThirdDateFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ThirdDateFormat); } }

        public static string YearMonthFormatString
        {
            get { return Culture == DefaultCulture ? DefaultYearMonthFormat : (Culture == SecondCulture ? SecondYearMonthFormat : ThirdYearMonthFormat); }
        }
        public static string DefaultYearMonthFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultYearMonthFormat); } }
        public static string SecondYearMonthFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.SecondYearMonthFormat); } }
        public static string ThirdYearMonthFormat { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ThirdYearMonthFormat); } }
        #endregion


        #region Font info
        //폰트
        public static string DefaultFontName { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultFontName); } }
        public static float DefaultFontSize { get { return DefaultValueList.DefaultFontSize; } }
        public static string FontName { get; set; }
        public static float FontSize { get; set; }
        public static float DefaultPOPGridFontSize { get { return DefaultValueList.POPGridFontSize; } }
        public static float DefaultPOPLookUpFontSize { get { return DefaultValueList.POPLookUpFontSize; } }
        public static float DefaultPOPButtonFontSize { get { return DefaultValueList.POPLookUpFontSize; } }
        public static float POPGridFontSize { get; set; }
        public static float POPLookUpFontSize { get; set; }
        public static float POPButtonFontSize { get; set; }
        public static float ShopPOPGridFontSize { get { return DefaultValueList.ShopPOPGridFontSize; } }
        public static float DesignPOPGridFontSize { get { return DefaultValueList.DesignPOPGridFontSize; } }
        public static float DesignSmallPOPGridFontSize { get { return DefaultValueList.DesignSmallPOPGridFontSize; } }
        public static float DesignPOPStockGridFontSize { get { return DefaultValueList.DesignPOPStockGridFontSize; } }

        #endregion


        #region Skin info
        // Skin
        public static string DefaultSkinName { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.DefaultSkinName); } }
        public static string SkinName { get; set; }
        #endregion

        #region Inspection Info
        public static string InputInspectionPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.InputInspectionRegistryPath); } }
        public static string ProcessInspectionPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ProcessInspectionRegistryPath); } }
        public static string ShipmentInspectionPath { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ShipmentInspectionRegistryPath); } }
        #endregion
        // Temporary Folder
        public static string TempFolder { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.TemporaryFolder); } }

        // Login, out Log       
        public static HKInc.Ui.Model.Domain.LoginLog LoginLogInfo { get; set; }

        // Current Instance
        public static string CurrentInstance { get; set; }

        // Mail Address for DatabaseMail
        public static string FromMailAddress { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.FromMailAddress); } }
        public static string FromMailAddressPassword { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.FromMailAddressPassword); } }

        public static string SMTP_SERVER { get; set; }
        public static int SMTP_PORT { get; set; }

        public static string ReplyMailAddress { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.ReplyToMailAddress); } }  
        
        public static string EncryptionKey { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.EncryptionKey); } }
        public static string LicenseEncryptionKey { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.LicenseEncryptionKey); } }
        public static string PasswordNumberReg { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.PasswordNumberReg); } }
        public static string PasswordUpperCharReg { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.PasswordUpperCharReg); } }
        public static string PasswordMinMaxCharReg { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.PasswordMinMaxCharReg); } }
        public static string PasswordLowerCharReg { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.PasswordLowerCharReg); } }
        public static string PasswordSymbolReg { get { return DefaultValueList.GetDefaultStringValue(Enum.DefaultValue.PasswordSymbolReg); } }

        public static bool LicenseDeploy { get { return DefaultValueList.LicenseDeploy; } }

        public static HKInc.Utils.Images.IconImageCollection IconImageCollection { get; set; }

        public static System.Drawing.Color MandatoryFieldColor { get { return DefaultValueList.MandatoryFieldColor; } }
        public static System.Drawing.Color GridEditableColumnColor { get { return DefaultValueList.GridEditableColumnColor; } }

        #region POP Timer Interval
        public static int AsPOPTimerInterval { get { return DefaultValueList.AsPOPTimerInterval * 1000; } }
        public static int ShopPOPTimerInterval { get { return DefaultValueList.ShopPOPTimerInterval * 1000; } }
        public static int DesignPOPTimerInterval { get { return DefaultValueList.DesignPOPTimerInterval * 1000; } }
        #endregion

        public static string DefaultPOPSkin { get { return DefaultValueList.DefaultPOPSkin; } }
        public static string DefaultPOPFontName { get { return DefaultValueList.DefaultPOPFont; } }
        public static string POPFontName { get; set; }
        public static string DefaultPOPType { get; set; }
        public static string DefaultPOPWorkCenter { get; set; }
        public static Boolean KeyPad { get; set; }
        public static string FTP_SERVER { get; set; }
        public static string FTP_USER_ID { get; set; }
        public static string FTP_USER_PWD { get; set; }
        public static string HTTP_SERVER { get; set; }
        public static string COMPANY_NAME { get; set; }
    }
}
