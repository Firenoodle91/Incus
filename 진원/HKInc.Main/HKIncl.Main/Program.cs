using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.IO;
using DevExpress.Utils;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

using HKInc.Ui.Model.Context;

using HKInc.Utils.Common;
using HKInc.Service;
using HKInc.Utils.Encrypt;

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
            //string aa = @"c:\" + Application.CompanyName.ToString() + "\\DB"+ Application.ProductName+ ".ini";
            //string ab = @"c:\" + Application.CompanyName.ToString();
            string aa = Application.StartupPath + "\\DB.ini";
            string ab = Application.StartupPath;
            FileInfo fi = new FileInfo(aa);
            if (fi.Exists)
            {
                iniFile ini = new iniFile(ab, aa);

                GlobalVariable.DatabaseIP = AESEncrypt256.Decrypt(ini.IniReadValue("server", "ip", aa),"hkinc");   //로그인창에 DB설정으로 들어가면 db.ini파일에서 ip정보 불러와서 화면에보여주는 부분 -  김주임추가
                GlobalVariable.DBuser = AESEncrypt256.Decrypt(ini.IniReadValue("server", "user", aa),"hkinc"); //SID부분
                GlobalVariable.DBPasswd = AESEncrypt256.Decrypt(ini.IniReadValue("server", "passwd", aa),"hkinc");  //ID부분
                GlobalVariable.DBName = AESEncrypt256.Decrypt(ini.IniReadValue("server", "db", aa),"hkinc"); //PASSWD부분
                ServerInfo.Database = GlobalVariable.DBName;
                ServerInfo.UserId = GlobalVariable.DBuser;
                ServerInfo.Password = GlobalVariable.DBPasswd;
                ServerInfo.Server = GlobalVariable.DatabaseIP;
                ServerInfo.ProductionDatabase = GlobalVariable.DBName;
                ServerInfo.ConnectStringListChange();
            }
            else
            {
                iniFile ini = new iniFile(ab, aa);
                ini.IniWriteValue("server", "ip", AESEncrypt256.Encrypt(ServerInfo.Server, "hkinc"),aa ); 
                ini.IniWriteValue("server", "user", AESEncrypt256.Encrypt(ServerInfo.UserId, "hkinc"), aa);
                ini.IniWriteValue("server", "passwd", AESEncrypt256.Encrypt(ServerInfo.Password, "hkinc"), aa);
                ini.IniWriteValue("server", "db", AESEncrypt256.Encrypt(ServerInfo.ProductionDatabase, "hkinc"), aa);

            }

            using (LoginFormHKInc f = new LoginFormHKInc())
            {
                f.Text = Application.ProductName + " Log On!!";
                if (f.ShowDialog() != DialogResult.OK)
                {
                    Application.ExitThread();
                    Environment.Exit(0);
                }
            }
            // DbContext가 DB 생성하지않게 설정 -- Code First Approch 이므로
            //System.Data.Entity.Database.SetInitializer<SystemContext>(null);
            //System.Data.Entity.Database.SetInitializer<SchedulerContext>(null);
            //System.Data.Entity.Database.SetInitializer<GanttContext>(null);

            System.Data.Entity.Database.SetInitializer<ProductionContext>(null);

            //GlobalVariable.Culture = "en-US";// GlobalVariable.DefaultCulture;
            // GlobalVariable 기본값설정
            DefaultSetting.SetDefaultValue();
          //  GlobalVariable.Culture = "en-US";

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
            {                
                if (!HKInc.Service.Handler.LicenseKeyHandler.IsLicensedMachine())
                    return;
            }


            // Login Form Open
            //using (LoginFormKor f = new LoginFormKor())
            //{
            //    f.Text = Application.ProductName + " Log On!!";
            //    if (f.ShowDialog() != DialogResult.OK)
            //    {
            //        Application.ExitThread();
            //        Environment.Exit(0);
            //    }
            //}
           

 

            //-----------------------------------------------------------------
            // Loading caching data here if need
            //-----------------------------------------------------------------

            //POP-콤보박스 별 체크로직
            #region NEW
            //if (GlobalVariable.IsPOPUser.Equals("30479"))
            //    Application.Run(new HKInc.Ui.View.POP.FPop());
            //else if (GlobalVariable.IsPOPUser.Equals("30480"))
            //    Application.Run(new HKInc.Ui.View.POP.MoldMovePop());
            //else
            //    Application.Run(new MainForm());
       //    GlobalVariable.Culture = "en-US";
            if (GlobalVariable.DefaultPOPType == "MES")
            {
                Application.Run(new MainForm());
            }
            //else if(GlobalVariable.DefaultPOPType =="POP1")//일반공정
            //{
            //    //    string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;

            //    Application.Run(new Ui.View.POP.XFPOP002());
          
            //}
            else if (GlobalVariable.DefaultPOPType == "POP1")//press 공정
            {
                //    string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;

                Application.Run(new Ui.View.POP.XFPOP001());

            }
            //else if (GlobalVariable.DefaultPOPType == "POP2")//press 공정
            //{
            //    //    string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;

            //    Application.Run(new Ui.View.POP.FPOP010());

            //}
            //else if (GlobalVariable.DefaultPOPType == "POP3")//press 공정
            //{
            //    //    string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;

            //    Application.Run(new Ui.View.POP.XFPOP003());

            //}
            #endregion

            #region ORG
            //// POP User check 확인해서 POP window open
            //if (GlobalVariable.IsPOPUser)
            //{
            //    GlobalVariable.Culture = "ko-KR";

            //    //if (GlobalVariable.LoginId == "ASPOP")
            //    //{
            //    //    UserLookAndFeel.Default.SetSkinStyle(GlobalVariable.DefaultPOPSkin);                    
            //    //    Application.Run(new HKInc.Ui.View.POP.AsPOP());
            //    //}
            //    //else if (GlobalVariable.LoginId == "SHOPPOP")
            //    //{
            //    //    UserLookAndFeel.Default.SetSkinStyle(GlobalVariable.DefaultPOPSkin);                    
            //    //    Application.Run(new HKInc.Ui.View.POP.ShopPOP());
            //    //}
            //    //else if (GlobalVariable.LoginId == "DESIGNPOP")
            //    //{
            //    //    UserLookAndFeel.Default.SetSkinStyle(GlobalVariable.DefaultPOPSkin);                    
            //    //    Application.Run(new HKInc.Ui.View.POP.DesignPOP());
            //    //}
            //    //else
            //    //Application.Run(new HKInc.Ui.View.POP.FPop());


            //}            
            //else
            //{
            //    Application.Run(new MainForm());
            //}
            #endregion
        }

        public static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }

            return false;
        }
    }
}
