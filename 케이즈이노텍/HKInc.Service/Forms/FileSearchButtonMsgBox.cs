using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;

namespace HKInc.Service.Forms
{
    /// <summary>
    /// FtpFileGridButtonEdit Search 버튼 클릭
    /// </summary>
    public partial class FileSearchButtonMsgBox : XtraForm
    {
        public FileSearchButtonMsgBox()
        {
            InitializeComponent();
            btnPreview.Click += btnPreview_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }
    }

}
