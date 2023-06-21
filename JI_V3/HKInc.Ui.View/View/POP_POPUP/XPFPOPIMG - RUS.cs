using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HKInc.Ui.Model.Domain;
using DevExpress.XtraReports.UI;

namespace HKInc.Ui.View.View.POP_POPUP
{
    /// <summary>
    /// 작업표준서 및 도면 크게 볼 수 있는 팝업 창
    /// </summary>
    public partial class XPFPOPIMG_RUS : XtraForm
    {
        public XPFPOPIMG_RUS()
        {
            InitializeComponent();            
        }
        public XPFPOPIMG_RUS(string tname, object obj)
        {
            InitializeComponent();
            this.Text = tname;
            pictureEdit1.EditValue = obj;
            pictureEdit1.Properties.ContextButtons[0].Click += XPFPOPIMG_Click;
            pictureEdit1.Properties.ContextButtons[1].Click += XPFPOPIMG_Click1;
            pictureEdit1.Properties.ContextButtons[2].Click += XPFPOPIMG_Click2;
            pictureEdit1.Properties.ContextButtons[3].Click += XPFPOPIMG_Click3;
            pictureEdit1.Properties.ContextButtons[4].Click += XPFPOPIMG_Click4;

            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            //pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
        }


        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void XPFPOPIMG_Click(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pictureEdit1.Properties.ZoomPercent += 20;
        }

        private void XPFPOPIMG_Click1(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pictureEdit1.Properties.ZoomPercent -= 20;
        }
        private void XPFPOPIMG_Click2(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Clip;
            pictureEdit1.Properties.ZoomPercent = 100;
        }
        private void XPFPOPIMG_Click3(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            this.Close();
        }

        private void XPFPOPIMG_Click4(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            if (pictureEdit1.Image != null)
            {
                REPORT.XRPOPIMG_PRINT preview = new REPORT.XRPOPIMG_PRINT(pictureEdit1.Image);
                preview.ShowPrintMarginsWarning = false;
                preview.ShowPrintStatusDialog = false;
                preview.CreateDocument();
                preview.ShowPreviewDialog();
            }
        }
    }
}
