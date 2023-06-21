using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace HKInc.Main
{
    public partial class SplashScreen : DevExpress.XtraSplashScreen.SplashScreen
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }
    }
}