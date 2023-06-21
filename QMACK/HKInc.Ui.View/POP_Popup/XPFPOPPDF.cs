using DevExpress.XtraEditors;
using System;
using System.Drawing;
using HKInc.Service.Handler;
using System.IO;

namespace HKInc.Ui.View.POP_Popup
{
    /// <summary>
    /// 도면 PDF 크게
    /// </summary>
    public partial class XPFPOPPDF : XtraForm
    {
        private MemoryStream _data;
        public XPFPOPPDF()
        {
            InitializeComponent();            
        }
        public XPFPOPPDF(MemoryStream data)
        {
            InitializeComponent();
            //spreadsheetControl1 = obj;
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;

            pdfViewer1.DoubleClick += PdfViewer1_DoubleClick;
            simpleButton1.Click += SimpleButton1_Click;
            simpleButton2.Click += SimpleButton2_Click;
            simpleButton3.Click += SimpleButton3_Click;
            btnRefresh.Click += BtnRefresh_Click;

            _data = data;
        }

        private void XPFPOPPDF_Load(object sender, EventArgs e)
        {
            WaitHandler waitHandler = new WaitHandler();
            waitHandler.ShowWait();
            pdfViewer1.NavigationPaneInitialVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
            pdfViewer1.LoadDocument(_data);
            pdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.Custom;
            waitHandler.CloseWait();

            pdfViewer1.CursorMode = DevExpress.XtraPdfViewer.PdfCursorMode.HandTool;
        }

        private void PdfViewer1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            // 20210806 오세완 차장 50이 배율이 안좋아서 25로 고정처리
            pdfViewer1.ZoomFactor += 25f;
            //pdfViewer1.ZoomFactor += 50f;
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            // 20210806 오세완 차장 위에 if안에 있는 고정값은 때에 따라서 줄어들지 않는 오류를 가지고 있고 50이 배율이 안좋아서 25로 고정처리
            //var value = pdfViewer1.ZoomFactor - 50f;
            //if (value >= 76.5653152)
            //    pdfViewer1.ZoomFactor -= 50f;

            pdfViewer1.ZoomFactor -= 25f;
        }
        private void SimpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            SizeF currentPageSize = pdfViewer1.GetPageSize(pdfViewer1.CurrentPageNumber);
            float dpi = 110f;
            float pageHeightPixel = currentPageSize.Height * dpi;
            float topBottomOffset = 40f;
            pdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.Custom;
            pdfViewer1.ZoomFactor = ((float)pdfViewer1.ClientSize.Height - topBottomOffset) / pageHeightPixel * 100f;

        }

    }
}
