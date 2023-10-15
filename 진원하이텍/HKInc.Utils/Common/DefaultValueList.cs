using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Common
{
    static class DefaultValueList
    {
        private static readonly float defaultFontSize = 9f;
        private static readonly Enum.DatabaseCategory defaultLoginDataBase = Enum.DatabaseCategory.DefaultApplication;
        private static readonly Enum.DatabaseCategory defaultProductionDataBase = Enum.DatabaseCategory.DefaultProduction;
     
        private static readonly string RegistryCompanyName = @"\HKIncMES";

        private static readonly Dictionary<Enum.DefaultValue, string> DefaultStringValueList = new Dictionary<Enum.DefaultValue, string>()
        {
            {Enum.DefaultValue.ApplicationName,             @"HKInc MES"},
            {Enum.DefaultValue.CultureRegistryPath,         @"Culture"},
            {Enum.DefaultValue.ServerConfigRegistryPath,    @"ServerConfig"},
            {Enum.DefaultValue.MainRegistryPath,            @"Software\HKIncution\HKIncMES" + RegistryCompanyName},
            {Enum.DefaultValue.LoginInfoRegistryPath,       @"LoginInfo"},
            {Enum.DefaultValue.SkinRegistryPath,            @"Skin"},
            {Enum.DefaultValue.InputInspectionRegistryPath, @"InputInspection"},
            {Enum.DefaultValue.ProcessInspectionRegistryPath,@"ProcessInspection"},
            {Enum.DefaultValue.ShipmentInspectionRegistryPath,@"ShipmentInspection"},
            {Enum.DefaultValue.DefaultCulture,              @"ko-KR"}, // ja-JP 참조 : https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx
            {Enum.DefaultValue.SecondCulture,               @"en-US"},
            {Enum.DefaultValue.ThirdCulture,                @"zh-CN"}, 
            {Enum.DefaultValue.DefaultCultureName,          @"Korean"},
            {Enum.DefaultValue.SecondCultureName,           @"English"},
            {Enum.DefaultValue.ThirdCultureName,            @"Chinese"},
            {Enum.DefaultValue.DefaultDateTimeFormat,       @"yyyy/MM/dd HH:mm"},
            {Enum.DefaultValue.SecondDateTimeFormat,        @"yyyy/MM/dd HH:mm"},
            {Enum.DefaultValue.ThirdDateTimeFormat,         @"yyyy/MM/dd HH:mm"},
            {Enum.DefaultValue.DefaultDateFormat,           @"yyyy/MM/dd"},
            {Enum.DefaultValue.SecondDateFormat,            @"yyyy/MM/dd"},
            {Enum.DefaultValue.ThirdDateFormat,             @"yyyy/MM/dd"},
            {Enum.DefaultValue.DefaultYearMonthFormat,      @"yyyy/MM"},
            {Enum.DefaultValue.SecondYearMonthFormat,       @"yyyy/MM"},
            {Enum.DefaultValue.ThirdYearMonthFormat,        @"yyyy/MM"},
            {Enum.DefaultValue.DefaultFontName,             @"맑은고딕"},
            {Enum.DefaultValue.DefaultSkinName,             @"Visual Studio 2013 Blue"},
            {Enum.DefaultValue.PopSkinName,                 @"Visual Studio 2013 Blue"},
            {Enum.DefaultValue.TemporaryFolder,             @"HKIncTemp"},
            {Enum.DefaultValue.FromMailAddress,             @"khk0829@naver.com"},
            {Enum.DefaultValue.FromMailAddressPassword,     @"!aa4658186"},
            {Enum.DefaultValue.ReplyToMailAddress,          @"khk0829@gmail.com"},
            {Enum.DefaultValue.EncryptionKey,               @"HKIncsMes987654321"},
            {Enum.DefaultValue.LicenseEncryptionKey,        @"HKIncutionVFRAmewoRK00-01-00-01-20-F9-2B-3A-70-85-C2-0B-3D-73"},            
            {Enum.DefaultValue.PasswordLowerCharReg,        @"[a-z]+"},
            {Enum.DefaultValue.PasswordUpperCharReg,        @"[A-Z]+"},
            {Enum.DefaultValue.PasswordMinMaxCharReg,       @".{8,15}"},
            {Enum.DefaultValue.PasswordNumberReg,           @"[0-9]+"},
            {Enum.DefaultValue.PasswordSymbolReg,           @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"},
            {Enum.DefaultValue.ServerIpAddressQuery,        @"select CONNECTIONPROPERTY('local_net_address')" }
        };

        public static string GetDefaultStringValue(Enum.DefaultValue defaultValue)
        {
            return DefaultStringValueList[defaultValue];
        }
        
        //  라이센스적용여부
        public static bool LicenseDeploy { get { return false; } }

        public static float DefaultFontSize { get { return defaultFontSize; } }
        public static float POPGridFontSize { get { return 20; } }
        public static float POPLookUpFontSize { get { return 20; } }
        public static float POPButtonFontSize { get { return 20; } }
        
        public static float ShopPOPGridFontSize { get { return 40; } }
        public static float DesignPOPGridFontSize { get { return 27; } }
        public static float DesignSmallPOPGridFontSize { get { return 20; } }
        public static float DesignPOPStockGridFontSize { get { return 27; } }
        public static Enum.DatabaseCategory DefaultLoginDataBase { get { return defaultLoginDataBase; } }
        public static Enum.DatabaseCategory DefaultProductionDataBase { get { return defaultProductionDataBase; } }
     

        public static System.Drawing.Color MandatoryFieldColor { get { return System.Drawing.Color.Red; } }
        public static System.Drawing.Color GridEditableColumnColor { get { return System.Drawing.Color.Ivory; } }
        
        public static int AsPOPTimerInterval { get { return 60; } }
        public static int ShopPOPTimerInterval { get { return 60; } }
        public static int DesignPOPTimerInterval { get { return 60; } }

        public static string DefaultPOPSkin { get { return GetDefaultStringValue(Enum.DefaultValue.PopSkinName); } }
        public static string DefaultPOPFont { get { return @"맑은고딕"; } }
       
        //public static string SMTP_SERVER { get { return "58.76.212.140"; } }
        //public static int SMTP_PORT { get { return 25; } }
    }
}
