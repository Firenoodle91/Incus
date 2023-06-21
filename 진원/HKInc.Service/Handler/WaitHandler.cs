using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace HKInc.Service.Handler
{
    public class WaitHandler
    {
        public void ShowWait()
        {
            //Application.UseWaitCursor = true;
            //Application.DoEvents();
            //CloseWait();
            ShowWait(null);
        }

        public void ShowWait(string msg)
        {
            //CloseWait();
            if (SplashScreenManager.Default == null)
            {
                SplashScreenManager.ShowForm(typeof(Forms.WaitForm), true, false);
            }
            else
            {
                if (!SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.Default.ShowWaitForm();
            }
                SetWaitMessage(msg);            
        }

        public void SetWaitMessage(string msg)
        {
            if (msg != null && SplashScreenManager.Default != null)
                SplashScreenManager.Default.SetWaitFormDescription(msg);
            
        }
        public void CloseWait()
        {
            if (SplashScreenManager.Default != null)
                SplashScreenManager.CloseForm(false);

            //Application.UseWaitCursor = false;
            //Application.DoEvents();
        }
    }
}
