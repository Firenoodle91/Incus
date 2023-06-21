using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using HKInc.Utils.Class;
using HKInc.Utils.Common;


namespace HKInc.Service.Forms
{
    public partial class LicenseKeyInput : DevExpress.XtraEditors.XtraForm
    {
        public string KeyInput { get { return memoKey.EditValue.GetNullToEmpty(); } }

        public LicenseKeyInput()
        {
            InitializeComponent();

            btnOk.Click += BtnOk_Click;
            btnCancel.Click += BtnGenerate_Click;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(memoKey.EditValue.GetNullToEmpty()))
                this.DialogResult = DialogResult.No;
            else
                this.DialogResult = DialogResult.OK;

            Close();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }
    }
}