using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;

namespace HKInc.Service.Forms
{
    public partial class ErrorMsgBox :XtraForm
    {
        public ErrorMsgBox()
        {
            InitializeComponent();
            btnOk.Click += btnOk_Click;
            btnSendServer.Click += btnSendServer_Click;
        }

        void btnSendServer_Click(object sender, EventArgs e)
        {
            //서버전송
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Initialize()
        {
            //Font 설정
        }

        public string ErrorMessage
        {
            get { return this.memMessage.Text; }
            set { this.memMessage.Text = value; }
        }

        public string ErrorStackTrace
        {
            get { return this.memStackTrace.Text; }
            set { this.memStackTrace.Text = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            txtOccurredTime.Text = DateTime.Now.ToString("F");
            tabGroup.SelectedTabPageIndex = 0;
        }
    }
}