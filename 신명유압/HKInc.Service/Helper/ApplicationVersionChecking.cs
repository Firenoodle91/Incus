using HKInc.Service.Handler;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Utils.Class;

namespace HKInc.Service.Helper
{
    public class ApplicationVersionChecking
    {
        /// <summary>
        /// System.Deployment 참조 필요
        /// </summary>

        public bool CheckFlag = false;

        public void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBoxHandler.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBoxHandler.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBoxHandler.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;
                    CheckFlag = true;

                    if (!info.IsUpdateRequired)
                    {
                        //DialogResult dr = MessageBoxHandler.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        DialogResult dr = MessageBoxHandler.Show("업데이트를 사용할 수 있습니다.지금 응용 프로그램을 업데이트 하시겠습니까 ? ", "사용 가능한 업데이트", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBoxHandler.Show("이 응용 프로그램은 현재 업데이트에서 필수 업데이트를 감지했습니다. " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". 이제 응용 프로그램이 업데이트를 설치하고 다시 시작됩니다.",
                            "사용 가능한 업데이트", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            //ad.UpdateCompleted += new System.ComponentModel.AsyncCompletedEventHandler(ad_UpdateCompleted);
                            //// Indicate progress in the application's status bar.
                            //ad.UpdateProgressChanged += new DeploymentProgressChangedEventHandler(ad_UpdateProgressChanged);

                            //WaitHandler WaitHandler = new WaitHandler();
                            //WaitHandler.ShowWait();
                            ad.Update();
                            //WaitHandler.CloseWait();
                            MessageBoxHandler.Show("응용 프로그램이 업그레이드되었으며 이제 다시 시작됩니다.");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBoxHandler.Show("최신 버전의 응용 프로그램을 설치할 수 없음. \n\n네트워크 연결을 확인하거나 나중에 다시 시도하십시오. Error: " + dde);
                            CheckFlag = false;
                            return;
                        }
                    }
                }
            }
            else
                CheckFlag = true;

            //#if !DEBUG
            //    ChangeTime();
            //#endif

        }
        //void ad_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        //{
        //    WaitHandler.ShowWait();
        //    //String progressText = String.Format("{0:D}K out of {1:D}K downloaded - {2:D}% complete", e.BytesCompleted / 1024, e.BytesTotal / 1024, e.ProgressPercentage);
        //    //downloadStatus.Text = progressText;
        //}

        //void ad_UpdateCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    if (e.Cancelled)
        //    {
        //        MessageBox.Show("The update of the application's latest version was cancelled.");
        //        return;
        //    }
        //    else if (e.Error != null)
        //    {
        //        MessageBox.Show("ERROR: Could not install the latest version of the application. Reason: \n" + e.Error.Message + "\nPlease report this error to the system administrator.");
        //        return;
        //    }

        //    DialogResult dr = MessageBox.Show("The application has been updated. Restart? (If you do not restart now, the new version will not take effect until after you quit and launch the application again.)", "Restart Application", MessageBoxButtons.OKCancel);
        //    if (DialogResult.OK == dr)
        //    {
        //        Application.Restart();
        //    }
        //}

        //서버 시간 동기화 처리
        //[DllImport("kernel32.dll")]
        //public static extern bool SetLocalTime(ref SYSTEMTIME time);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }

        [DllImport("kernel32.dll")]
        public static extern bool SetLocalTime(ref SYSTEMTIME time);

        /// <summary>시스템 날짜/시간을 설정한다.</summary>
        /// <param name="dtNew">설정한 Date/Time</param>
        /// <returns>오류가 없는 경우 true를 응답하며,
        /// 그렇지 않은 경우 false를 응답한다.</returns>
        public static bool SetSystemDateTime(DateTime dtNew)
        {
            bool bRtv = false;

            SYSTEMTIME st;
            var sql = @"SELECT GETDATE(), DATEPART(WEEKDAY, GETDATE()) - 1";
            var ds = Service.DbRequestHandler.GetDataQury(sql);
            var ServerDateTime = Convert.ToDateTime((ds.Tables[0].Rows[0][0].GetNullToEmpty()));
            var DayOfWeek = ds.Tables[0].Rows[0][1].GetIntNullToZero();
            st.Year = (ushort)ServerDateTime.Year;
            st.Month = (ushort)ServerDateTime.Month;
            st.DayOfWeek = (ushort)DayOfWeek;       // 0 : 일요일 1 : 월요일 2 : 화요일 3 : 수요일 4 : 목요일 5 : 금요일 6 : 토요일
            st.Day = (ushort)ServerDateTime.Day;
            st.Hour = (ushort)ServerDateTime.Hour;
            st.Minute = (ushort)ServerDateTime.Minute;
            st.Second = (ushort)ServerDateTime.Second;
            st.Milliseconds = (ushort)ServerDateTime.Millisecond;

            bRtv = SetLocalTime(ref st); ;    // UTC+0 시간을 설정한다.
            // bRtv = YtnWin32.SetSystemTime(ref st);  // UTC + 표준시간대(대한민궁의 경우 UTC+9)를 설정한다.

            return bRtv;
        }

        public void ChangeTime()
        {
            // 시간 변경이 가능(관리자 권한)한 지 검사한다.
            if (!SetSystemDateTime(DateTime.Now))
            {
                MessageBoxHandler.Show("시스템 시간 변경 권한이 없습니다. 관리자 권한으로 시작하십시오.");
                Application.ExitThread();
                Environment.Exit(0);
            }
        }
    }
}
