using HKInc.Service.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKInc.Service.Forms
{
    public partial class FMessagebox : Form
    {
        public FMessagebox()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                FMCSETTING fm = (FMCSETTING)Owner;
                fm.lsCount = "A";
                this.Close();
            }
            catch
            {
                FMCSETTING_ENG fm = (FMCSETTING_ENG)Owner;
                fm.lsCount = "A";
                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                FMCSETTING fm = (FMCSETTING)Owner;
                fm.lsCount = "B";
                this.Close();
            }
            catch
            {
                FMCSETTING_ENG fm = (FMCSETTING_ENG)Owner;
                fm.lsCount = "B";
                this.Close();
            }
         
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
