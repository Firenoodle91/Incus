﻿using System;
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
            GlobalVariable.COMPANY_NAME = "주식회사 대영정밀";

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


            //POP-콤보박스 별 체크로직
            if (GlobalVariable.DefaultPOPType == "MES")
            {
                Application.Run(new MainForm());
            }
            else if (GlobalVariable.DefaultPOPType == "POP")
            {
                //string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;
                Application.Run(new Ui.View.View.POP.XFPOP1000_V2());
            }
            else if (GlobalVariable.DefaultPOPType == "PLC") // 20210715 오세완 차장 REWORK -> PLC수정
            {
                //string DefaultPOPWorkCenter = GlobalVariable.DefaultPOPWorkCenter;
                // 20210702 오세완 차장 리워크 POP가 없어서 생략처리
                //Application.Run(new Ui.View.View.POP.XFPOP_REWORK());
                Application.Run(new Ui.View.View.POP.XFPOP_PLC_V2()); // 20210715 오세완 차장 PLC로 교체
            }
            else
                Application.Run(new MainForm());

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
