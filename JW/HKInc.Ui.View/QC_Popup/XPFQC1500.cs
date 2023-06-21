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

namespace HKInc.Ui.View.QC_Popup
{
    public partial class XPFQC1500 : DevExpress.XtraEditors.XtraForm
    {
        public XPFQC1500()
        {
            InitializeComponent();
        }
        public XPFQC1500(object obj)
        {
            InitializeComponent();
            pictureEdit1.EditValue = obj;
        }
    }
}