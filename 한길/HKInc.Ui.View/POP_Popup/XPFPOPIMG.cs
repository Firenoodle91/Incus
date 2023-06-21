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

namespace HKInc.Ui.View.POP_Popup
{
    public partial class XPFPOPIMG : XtraForm
    {
        public XPFPOPIMG()
        {
            InitializeComponent();
        }
        public XPFPOPIMG(string tname, object obj)
        {
            InitializeComponent();
            this.Text = tname;
            pictureEdit1.EditValue = obj;
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
