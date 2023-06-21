using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Globalization;

using DevExpress.Utils;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

using HKInc.Ui.Model.Context;

using HKInc.Utils.Common;


namespace HKInc.Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Data.Entity.Database.SetInitializer<ProductionContext>(null);

            // GlobalVariable 기본값설정
            DefaultSetting.SetDefaultValue();

            // Set Skin
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            UserLookAndFeel.Default.SetSkinStyle(GlobalVariable.SkinName);

            // GridView Filter setting
            WindowsFormsSettings.ColumnFilterPopupMode = ColumnFilterPopupMode.Excel;

            // Font
            AppearanceObject.DefaultFont = new Font(GlobalVariable.FontName, GlobalVariable.FontSize);

            // Check License Deploy
            if(GlobalVariable.LicenseDeploy)
                if (!HKInc.Service.Handler.LicenseKeyHandler.IsLicensedMachine())
                    return;

            using (LoginFormHKInc f = new LoginFormHKInc())
            {
                f.Text = Application.ProductName + " Log On!!";
                if (f.ShowDialog() != DialogResult.OK)
                {
                    Application.ExitThread();
                    Environment.Exit(0);
                }
            }

            //-----------------------------------------------------------------
            // Loading caching data here if need
            //-----------------------------------------------------------------

            #region POP-콤보박스 별 체크로직
            if (GlobalVariable.DefaultPOPType == "MES")         // MES
                Application.Run(new MainForm());
            else if (GlobalVariable.DefaultPOPType == "POP1")   // POP
                //Application.Run(new Ui.View.POP.XFPOP001());
                Application.Run(new Ui.View.POP.XFPOP001_V2());
            else if (GlobalVariable.DefaultPOPType == "POP2")   // 현황판
                Application.Run(new Ui.View.POP.FPOP010());
            else if (GlobalVariable.DefaultPOPType == "POP3")   // Russian
                Application.Run(new Ui.View.POP.XFPOP003_V2());
            #endregion
        }
    }
}
