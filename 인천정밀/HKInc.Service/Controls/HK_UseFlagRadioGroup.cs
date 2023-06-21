using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HKInc.Service.Controls
{
    public partial class HK_UseFlagRadioGroup : XtraUserControl
    {
        public HK_UseFlagRadioGroup()
        {
            InitializeComponent();
            SelectedValue = "Y";
        }

        public string SelectedValue
        {
            get
            {
                foreach (Control ctr in layoutControl1.Controls)
                {
                    CheckEdit edit = ctr as CheckEdit;
                    if (edit == null) continue;
                    if (edit.Checked)
                        return edit.Tag.ToString();
                }
                return String.Empty;
            }

            set
            {
                foreach (Control ctr in layoutControl1.Controls)
                {
                    CheckEdit edit = ctr as CheckEdit;
                    if (edit == null) continue;
                    if (edit.Tag.ToString() == value)
                        edit.Checked = true;
                    else
                        edit.Checked = false;
                }
            }
        }

        public void SetLabelText(string use, string notUse, string all)
        {
            rbo_Use.Text = use;
            rbo_NotUse.Text = notUse;
            rbo_All.Text = all;
        }
    }
}
