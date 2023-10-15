using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKInc.Utils.Enum
{
    public enum DefaultValue
    {
        ApplicationName,
        CultureRegistryPath,
        ServerConfigRegistryPath,
        MainRegistryPath,
        LoginInfoRegistryPath,
        SkinRegistryPath,

        DefaultCulture,
        SecondCulture,
        ThirdCulture,

        DefaultCultureName,
        SecondCultureName,
        ThirdCultureName,

        DefaultDateTimeFormat,
        SecondDateTimeFormat,
        ThirdDateTimeFormat,

        DefaultDateFormat,
        SecondDateFormat,
        ThirdDateFormat,

        DefaultYearMonthFormat,
        SecondYearMonthFormat,
        ThirdYearMonthFormat,

        DefaultFontName,        
        DefaultSkinName,
        TemporaryFolder,
        FromMailAddress,
        FromMailAddressPassword,
        ReplyToMailAddress,
        
        EncryptionKey,
        PasswordNumberReg,
        PasswordUpperCharReg,
        PasswordLowerCharReg,
        PasswordMinMaxCharReg,
        PasswordSymbolReg,
        PasswordCharReg, // 20210818 오세완 차장 대소문자 구별없이 1개만 찾아내는 조건 추가

        LicenseEncryptionKey,
        LicenseDeploy,

        ServerIpAddressQuery,
        PopSkinName,

        InputInspectionRegistryPath,
        ProcessInspectionRegistryPath,
        ShipmentInspectionRegistryPath,

        MachineRegistryPath, //레지스트리 설비코드 경로
    }
  
}
