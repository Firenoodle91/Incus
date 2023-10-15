using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Helper
{
    public partial class FactoryLabel : DevExpress.XtraEditors.XtraUserControl
    {
        public FactoryLabel(string text)
        {
            InitializeComponent();

            labelControl.Text = text;
        }
    }
}
