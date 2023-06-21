using System;
using System.Deployment.Application;

using HKInc.Utils.Common;
using HKInc.Service.Handler;
using System.Linq;
using HKInc.Utils.Class;

namespace HKInc.Main
{
    public static class DefaultSetting
    {
        public static void SetDefaultValue()
        {
            //DefaultServer
            GlobalVariable.DatabaseIP = HKInc.Utils.Class.DatabaseIpFunction.DefaultServerName();

            // 룩앤필 스킨명            
            string skinName = RegistryHandler.GetValue(GlobalVariable.SkinPath, "Skin");
            if (String.IsNullOrEmpty(skinName))
                skinName = GlobalVariable.DefaultSkinName;

            GlobalVariable.SkinName = skinName;

            // Font name
            string fontName = RegistryHandler.GetValue(GlobalVariable.CulturePath, "FontName");
            if (String.IsNullOrEmpty(fontName))
                fontName = GlobalVariable.DefaultFontName;

            GlobalVariable.FontName = fontName;

            // Font size
            string fontSize = RegistryHandler.GetValue(GlobalVariable.CulturePath, "FontSize");
            if (String.IsNullOrEmpty(fontSize))
                fontSize = Convert.ToString(GlobalVariable.DefaultFontSize);

            GlobalVariable.FontSize = Convert.ToSingle(fontSize);
            
            //------------------------------------------------------------------
            // Application Version 
            //------------------------------------------------------------------            
            if (ApplicationDeployment.IsNetworkDeployed)            
                GlobalVariable.ApplicationVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();

            //POP Font Name
            HKInc.Utils.Interface.Helper.IMasterCode masterCode = HKInc.Service.Factory.HelperFactory.GetMasterCode();
            masterCode.Reset();
            
            System.Collections.Generic.List<Ui.Model.Domain.CodeMaster> FontList = masterCode.GetMasterCode((int)Service.Factory.MasterCodeEnum.Font).ToList();
            if (FontList.Count > 0)
            {
                string POPFontName = GlobalVariable.DefaultPOPFontName;
                float FontSize_POP_LookUp = GlobalVariable.DefaultPOPLookUpFontSize;
                float FontSize_POP_Grid = GlobalVariable.DefaultPOPGridFontSize;
                float FontSize_POP_Button = GlobalVariable.DefaultPOPButtonFontSize;

                var FontNameObj = FontList.Where(p => p.Property2 == "FontName_POP").FirstOrDefault();
                if (FontNameObj != null && !string.IsNullOrEmpty(FontNameObj.Property1.GetNullToEmpty()))
                    POPFontName = FontNameObj.Property1;

                var FontSizeObj1 = FontList.Where(p => p.Property2 == "FontSize_POP_LookUp").FirstOrDefault();
                var FontSizeObj2 = FontList.Where(p => p.Property2 == "FontSize_POP_Grid").FirstOrDefault();
                var FontSizeObj3 = FontList.Where(p => p.Property2 == "FontSize_POP_Button").FirstOrDefault();
                if (FontSizeObj1 != null && !string.IsNullOrEmpty(FontSizeObj1.Property1.GetNullToEmpty()))
                    FontSize_POP_LookUp = (float)FontSizeObj1.Property1.GetDoubleNullToZero();
                if (FontSizeObj2 != null && !string.IsNullOrEmpty(FontSizeObj2.Property1.GetNullToEmpty()))
                    FontSize_POP_Grid = (float)FontSizeObj2.Property1.GetDoubleNullToZero();
                if (FontSizeObj3 != null && !string.IsNullOrEmpty(FontSizeObj3.Property1.GetNullToEmpty()))
                    FontSize_POP_Button = (float)FontSizeObj3.Property1.GetDoubleNullToZero();

                GlobalVariable.POPFontName = POPFontName;
                GlobalVariable.POPLookUpFontSize = FontSize_POP_LookUp;
                GlobalVariable.POPGridFontSize = FontSize_POP_Grid;
                GlobalVariable.POPButtonFontSize = FontSize_POP_Button;
            }

            var SMTP_SERVER = masterCode.GetMasterCodeFindCodeName("SMTP_SERVER").FirstOrDefault();
            var SMTP_PORT = masterCode.GetMasterCodeFindCodeName("SMTP_PORT").FirstOrDefault();
            if (SMTP_SERVER != null)
                GlobalVariable.SMTP_SERVER = SMTP_SERVER.Property1.GetNullToEmpty();
            if (SMTP_PORT != null)
                GlobalVariable.SMTP_PORT = SMTP_PORT.Property1.GetIntNullToZero();
            try
            {
                string sql = "SELECT       [CODE_NAME]      ,[CODE_NAME1]  FROM  [TN_STD1000T]   where CODE_MAIN='F001' and mcode='IP'";
                GlobalVariable.FTP_SERVER = HKInc.Service.Service.DbRequestHandler.GetCellValue(sql, 0);
                //GlobalVariable.HTTP_SERVER = HKInc.Service.Service.DbRequestHandler.GetCellValue(sql, 1);
                // 20220318 오세완 차장 공통코드관리에서 수정할 수 있게 추가 처리 
                string sSql_Web = "SELECT       [CODE_NAME]      FROM  [TN_STD1000T]   where CODE_MAIN='F001' and mcode='Web'";
                GlobalVariable.HTTP_SERVER = HKInc.Service.Service.DbRequestHandler.GetCellValue(sSql_Web, 0);
            }
            catch { }
            try
            {
                string sql = " SELECT[ST1]      ,[SM1]      ,[ST2]      ,[SM2] FROM[TN_ALAM001T]";
                GlobalVariable.ALamST1 = HKInc.Service.Service.DbRequestHandler.GetCellValue(sql, 0);
                GlobalVariable.ALamSM1= HKInc.Service.Service.DbRequestHandler.GetCellValue(sql, 1);
                GlobalVariable.ALamST2 = HKInc.Service.Service.DbRequestHandler.GetCellValue(sql, 2);
                GlobalVariable.ALamSM2 = HKInc.Service.Service.DbRequestHandler.GetCellValue(sql, 3);
            }
            catch { }
            
    }
    }
}
