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

namespace HKInc.Ui.View.SELECT_Popup
{
    public partial class XPF_PIC : DevExpress.XtraEditors.XtraForm
    {
        public XPF_PIC()
        {
            InitializeComponent();
        }

        public XPF_PIC(object obj) : this()
        {
            pictureEdit1.EditValue = obj;
        }
    }
}