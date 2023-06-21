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
            ////서버 시간 동기화를 위한 구문 하지만 버전관리가 되지 않는 점이 이슈로 일단 제외.
            ////관리자 권한 상승을 위한 구문
            //#if !DEBUG
            //if (IsAdministrator() == false)
            //{
            //    try
            //    {
            //        var procInfo = new System.Diagnostics.ProcessStartInfo();
            //        procInfo.UseShellExecute = true;
            //        procInfo.FileName = Application.ExecutablePath;
            //        procInfo.WorkingDirectory = Environment.CurrentDirectory;
            //        procInfo.Verb = "runas";
            //        System.Diagnostics.Process.Start(procInfo);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString());
            //    }

            //    return;
            //}
            //#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // DbContext가 DB 생성하지않게 설정 -- Code First Approch 이므로
            System.Data.Entity.Database.SetInitializer<ProductionContext>(null);
                        
            GlobalVariable.Culture = GlobalVariable.DefaultCulture;
            // GlobalVariable 기본값설정
            DefaultSetting.SetDefaultValue();
            GlobalVariable.COMPANY_NAME = "JI정밀";

            // Set Skin
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
            UserLookAndFeel.Default.SetSkinStyle(GlobalVariable.SkinName);

            // GridView Filter setting
            WindowsFormsSettings.ColumnFilterPopupMode = ColumnFilterPopupMode.Excel;

            // Font
            AppearanceObject.DefaultFont = new Font(GlobalVariable.FontName, GlobalVariable.FontSize);

            //// Check License Deploy
            //if(GlobalVariable.LicenseDeploy)
            //{                
            //    if (!HKInc.Service.Handler.LicenseKeyHandler.IsLicensedMachine())
            //        return;
            //}
            

            using (LoginFormHKInc f = new LoginFormHKInc())
            {
                f.Text = Application.ProductName + " Login In";
                if (f.ShowDialog() != DialogResult.OK)
                {
                    Application.ExitThread();
                    Environment.Exit(0);
                }
            }

            try
            {

                //POP-콤보박스 별 체크로직
                if (GlobalVariable.DefaultPOPType == "MES")
                {
                    Application.Run(new MainForm());
                }
                else if (GlobalVariable.DefaultPOPType == "POP")
                {
                    //string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;
                    Application.Run(new Ui.View.View.POP.XFPOP1000());
                    
                }
                else if (GlobalVariable.DefaultPOPType == "IFPOP")
                {
                    //string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;
                    //Application.Run(new Ui.View.View.POP.XFPOP1000());
                    Application.Run(new Ui.View.View.POP.XFPOPIF());
                }
                else if (GlobalVariable.DefaultPOPType == "RUS_IFPOP")      // 러시아 POP 추가 2022-10-24 김진우 추가
                {
                    Application.Run(new Ui.View.View.POP.XFPOPIF_RUS());
                }
                else
                    Application.Run(new MainForm());
            }
            catch(Exception ex) { }

        }

        //public static bool IsAdministrator()
        //{
        //    var identity = System.Security.Principal.WindowsIdentity.GetCurrent();

        //    if (null != identity)
        //    {
        //        var principal = new System.Security.Principal.WindowsPrincipal(identity);
        //        return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        //    }

        //    return false;
        //}
    }
}
