using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.REPORT
{
    public partial class XRPOPIMG_PRINT : DevExpress.XtraReports.UI.XtraReport
    {
        public XRPOPIMG_PRINT()
        {
            InitializeComponent();
        }

        public XRPOPIMG_PRINT(Image img)
        {
            InitializeComponent();
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(img);
        }
    }
}
